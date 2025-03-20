using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static MyCapture.NativeMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyCapture
{
    public partial class Main : Form
    {

        private Rectangle CaptureRegion;
        private Process ActiveProcess;
        private int NextKey;
        private string Path;
        private string Label;
        private int Counts;
        private int Start;
        private int Delay;

        ImageCodecInfo jpgEncoder;
        EncoderParameters encoderParams;

        private BackgroundWorker backgroundWorker;

        public Main()
        {
            InitializeComponent();
            cmb_keys.SelectedIndex = 0;

            jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            // EncoderParametersを作成
            encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            encoderParams.Dispose();
        }

        private static Bitmap CaptureShot_All()
        {
            int left = SystemInformation.VirtualScreen.Left;
            int top = SystemInformation.VirtualScreen.Top;
            int width = SystemInformation.VirtualScreen.Width;
            int hight = SystemInformation.VirtualScreen.Height;

            Rectangle rect = new Rectangle(left, top, width, hight);

            return CaptureScreen(rect);
        }

        public static Bitmap CaptureScreen(Rectangle screenRect)
        {

            Bitmap bmp = new Bitmap(screenRect.Size.Width, screenRect.Size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(screenRect.Location, new Point(0, 0), bmp.Size);
            }
            return bmp;

        }

        private static Bitmap CaptureActiveWindow()
        {
            //アクティブなウィンドウのデバイスコンテキストを取得
            IntPtr hWnd = NativeMethods.GetForegroundWindow();
            IntPtr winDC = NativeMethods.GetWindowDC(hWnd);
            //ウィンドウの大きさを取得
            NativeMethods.RECT winRect = new NativeMethods.RECT();
            NativeMethods.DwmGetWindowAttribute(
                hWnd,
                NativeMethods.DWMWA_EXTENDED_FRAME_BOUNDS,
                out var bounds,
                Marshal.SizeOf(typeof(NativeMethods.RECT)));
            NativeMethods.GetWindowRect(hWnd, ref winRect);
            //Bitmapの作成
            var offsetX = bounds.left - winRect.left;
            var offsetY = bounds.top - winRect.top;
            Bitmap bmp = new Bitmap(bounds.right - bounds.left, bounds.bottom - bounds.top);

            //Graphicsの作成
            using (var g = Graphics.FromImage(bmp))
            {
                //Graphicsのデバイスコンテキストを取得
                IntPtr hDC = g.GetHdc();
                //Bitmapに画像をコピーする
                Console.WriteLine(winRect);
                NativeMethods.BitBlt(hDC, 0, 0, bmp.Width, bmp.Height, winDC, offsetX, offsetY, NativeMethods.SRCCOPY);
                //解放
                g.ReleaseHdc(hDC);
            }
            NativeMethods.ReleaseDC(hWnd, winDC);

            return bmp;
        }

        #region 領域の設定
        private List<SelectRect> RegionSelectionList = new List<SelectRect>();
        private void btn_region_Click(object sender, EventArgs e)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (var screen in screens)
            {
                SelectRect dlg = new SelectRect(screen);
                dlg.FormClosed += RegionSelection_DialogClosed;
                dlg.Show(this);
                this.RegionSelectionList.Add(dlg);
            }
        }

        private void RegionSelection_DialogClosed(object sender, FormClosedEventArgs e)
        {
            SelectRect dlg = (SelectRect)sender;
            if (dlg.DialogResult != DialogResult.OK)
            {
                return;
            }

            CaptureRegion = dlg.ScreenRegion;　//選択された領域をRectangleで受け取れる

            foreach (var d in this.RegionSelectionList)
            {
                if (d.IsDisposed == false && d.Disposing == false)
                {
                    d.Dispose();
                }
            }
            this.RegionSelectionList.Clear();

        }
        #endregion

        #region Select Process
        private IntPtr hookID = IntPtr.Zero;
        private LowLevelMouseProc mouseProc;
        private void btn_GetApp_Click(object sender, EventArgs e)
        {
            mouseProc = HookCallback;
            hookID = SetHook(mouseProc);
            SetGUISelectingMode();
        }

        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return NativeMethods.SetWindowsHookEx(NativeMethods.WH_MOUSE_LL, proc, NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)NativeMethods.WM_LBUTTONDOWN)
            {
                NativeMethods.MSLLHOOKSTRUCT hookStruct = Marshal.PtrToStructure<NativeMethods.MSLLHOOKSTRUCT>(lParam);
                IntPtr hWnd = NativeMethods.WindowFromPoint(hookStruct.pt);
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint processId);

                Process curProcess = Process.GetCurrentProcess();
                if (processId != curProcess.Id)
                {
                    ActiveProcess = Process.GetProcessById((int)processId);
                    lbl_Process.Text = $"Process: {ActiveProcess.ProcessName}";

                    //MessageBox.Show($"Clicked Process ID: {processId}", "Process ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return NativeMethods.CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        private void btn_Selected_Click(object sender, EventArgs e)
        {
            NativeMethods.UnhookWindowsHookEx(hookID);
            if (ActiveProcess.MainWindowHandle == IntPtr.Zero)
            {
                Process[] procs = Process.GetProcessesByName(ActiveProcess.ProcessName);
                foreach (Process proc in procs)
                {
                    if (proc.MainWindowHandle == ActiveProcess.Handle)
                    {
                        ActiveProcess = proc;
                    }
                }
            }
            lbl_Process.Text = $"Process: {ActiveProcess.ProcessName}";

            GoDefault();
        }

        private void SetGUISelectingMode()
        {
            btn_Cancel.Enabled = false;
            btn_GetApp.Enabled = false;
            btn_PathSelect.Enabled = false;
            btn_capture.Enabled = false;
            btn_region.Enabled = false;
            btn_SendKey.Enabled = false;
            btn_Start.Enabled = false;
            txt_Counts.Enabled = false;
            txt_Lable.Enabled = false;
            txt_StartNo.Enabled = false;
            txt_delay.Enabled = false;
        }
        private void GoDefault()
        {
            btn_Cancel.Enabled = true;
            btn_GetApp.Enabled = true;
            btn_PathSelect.Enabled = true;
            btn_capture.Enabled = true;
            btn_region.Enabled = true;
            btn_SendKey.Enabled = true;
            btn_Start.Enabled = true;
            txt_Counts.Enabled = true;
            txt_Lable.Enabled = true;
            txt_StartNo.Enabled = true;
            txt_delay.Enabled = true;
        }
        #endregion

        private void btn_capture_Click(object sender, EventArgs e)
        {
            if (CaptureRegion == null) return;
            Bitmap bmp = CaptureScreen(CaptureRegion);
            bmp.Save("image.jpg", jpgEncoder, encoderParams);
            bmp.Dispose();
        }

        private void btn_SendKey_Click(object sender, EventArgs e)
        {
            Microsoft.VisualBasic.Interaction.AppActivate(ActiveProcess.Id);

            int key;
            switch (cmb_keys.Text)
            {
                case "Space":
                    key = NativeMethods.KEY_SPACE;
                    break;
                case "Enter":
                    key = NativeMethods.KEY_ENTER;
                    break;
                case "BackSpace":
                    key = NativeMethods.KEY_BACKSPACE;
                    break;
                case "Delete":
                    key = NativeMethods.KEY_DEL;
                    break;
                case "PageUp":
                    key = NativeMethods.KEY_PGUP;
                    break;
                case "PageDown":
                    key = NativeMethods.KEY_PGDN;
                    break;
                default:
                    key = NativeMethods.KEY_ENTER;
                    break;

            }

            SendKey(key);

        }

        private void btn_PathSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog() != DialogResult.OK) return;

            string path = fbd.SelectedPath;

            string folder = path.Substring(path.LastIndexOf("\\")+1, path.Length- (path.LastIndexOf("\\")+1));

            lbl_path.Text = path.Substring(0, path.LastIndexOf("\\")+1);
            txt_folder.Text = folder;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Label = txt_Lable.Text;
            try
            {
                Start = int.Parse(txt_StartNo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong type.");
                return;
            }
            try
            {
                Counts = int.Parse(txt_Counts.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong type.");
                return;
            }
            try
            {
                Delay = int.Parse(txt_delay.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong type.");
                return;
            }

            switch (cmb_keys.Text)
            {
                case "Space":
                    NextKey = NativeMethods.KEY_SPACE;
                    break;
                case "Enter":
                    NextKey = NativeMethods.KEY_ENTER;
                    break;
                case "BackSpace":
                    NextKey = NativeMethods.KEY_BACKSPACE;
                    break;
                case "Delete":
                    NextKey = NativeMethods.KEY_DEL;
                    break;
                case "PageUp":
                    NextKey = NativeMethods.KEY_PGUP;
                    break;
                case "PageDown":
                    NextKey = NativeMethods.KEY_PGDN;
                    break;
                default:
                    NextKey = NativeMethods.KEY_ENTER;
                    break;

            }

            Path = System.IO.Path.Combine(lbl_path.Text, txt_folder.Text);
            if(!System.IO.Path.Exists(Path))
            {
                var info = System.IO.Directory.CreateDirectory(Path);
            }

            backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            if (!backgroundWorker.IsBusy)
            {
                progressBar.Value = 0;
                btn_Cancel.Enabled = true;
                btn_GetApp.Enabled = false;
                btn_PathSelect.Enabled = false;
                btn_capture.Enabled = false;
                btn_region.Enabled = false;
                btn_SendKey.Enabled = false;
                btn_Selected.Enabled = false;
                btn_Start.Enabled = false;
                txt_Counts.Enabled = false;
                txt_Lable.Enabled = false;
                txt_StartNo.Enabled = false;
                txt_delay.Enabled = false;
                backgroundWorker.RunWorkerAsync();
            }

            //ExecuteCaptures();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }

        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Microsoft.VisualBasic.Interaction.AppActivate(ActiveProcess.Id);

            for (int i = 0; i < Counts; i++)
            {
                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                string AbsPath;
                AbsPath = System.IO.Path.Combine(Path, Label + (i + Start).ToString("0000") + ".jpg");
                Bitmap bmp = CaptureScreen(CaptureRegion);
                bmp.Save(AbsPath, jpgEncoder, encoderParams);
                bmp.Dispose();

                SendKey(NextKey);

                Thread.Sleep(Delay);

                backgroundWorker.ReportProgress((int)(i*100/(double)Counts));
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            progressBar.Value = 0;

            this.Activate();

            if (e.Cancelled)
            {
                MessageBox.Show("Abort the process", "Abort", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Complete the process", "Complete", MessageBoxButtons.OK);
            }

            btn_Cancel.Enabled = false;
            btn_GetApp.Enabled = true;
            btn_PathSelect.Enabled = true;
            btn_capture.Enabled = true;
            btn_region.Enabled = true;
            btn_SendKey.Enabled = true;
            btn_Selected.Enabled = true;
            btn_Start.Enabled = true;
            txt_Counts.Enabled = true;
            txt_Lable.Enabled = true;
            txt_StartNo.Enabled = true;
            txt_delay.Enabled = true;
            
        }

        private void SendKey(int key)
        {
            if (ActiveProcess.Handle != IntPtr.Zero)
                NativeMethods.PostMessage(ActiveProcess.MainWindowHandle, 0x0100, key, 0);
        }

        private void ExecuteCaptures()
        {
            Microsoft.VisualBasic.Interaction.AppActivate(ActiveProcess.Id);

            for (int i = 0; i < Counts; i++)
            {
                string totalPath;
                totalPath = System.IO.Path.Combine(Path, Label + (i + Start).ToString("0000") + ".jpg");
                Bitmap bmp = CaptureScreen(CaptureRegion);
                bmp.Save(totalPath, jpgEncoder, encoderParams);
                bmp.Dispose();


                SendKey(NextKey);

                Thread.Sleep(300);
            }

        }

        static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }


    }
}
