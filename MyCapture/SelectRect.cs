using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCapture
{
    public partial class SelectRect: Form
    {
        private Point startPos;
        private Point endPos;
        private bool isSelecting;
        private int reservePaint = 0;

        public SelectRect(Screen screen)
        {
            InitializeComponent();

            this.Screen = screen;
            this.FormBorderStyle = FormBorderStyle.None;
            //this.BackColor = Color.FromArgb(128, Color.Black);
            this.BackColor = Color.LightGray;
            this.Opacity = 0.5;
            this.StartPosition = FormStartPosition.Manual;
            this.Bounds = this.Screen.Bounds;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.MouseDown += OverlayForm_MouseDown;
            this.MouseMove += OverlayForm_MouseMove;
            this.MouseUp += OverlayForm_MouseUp;
            this.Paint += OverlayForm_Paint;
        }

        public Screen Screen { get; set; }
        public Rectangle SelectedRegion { get; set; }
        public Rectangle ScreenRegion { get { return this.RectangleToScreen(this.SelectedRegion); } }

        private void DialogRegionSelection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void OverlayForm_MouseDown(object sender, MouseEventArgs e)
        {
            isSelecting = true;
            this.startPos = e.Location;
            SelectedRegion = new Rectangle(e.Location, new Size());
        }

        private void OverlayForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)
            {
                this.endPos = e.Location;

                //selectedRegion = new Rectangle(selectedRegion.Location, new Size(e.X - selectedRegion.Left, e.Y - selectedRegion.Top));

                int x1 = this.startPos.X;
                int x2 = this.endPos.X;
                int y1 = this.startPos.Y;
                int y2 = this.endPos.Y;
                SelectedRegion = Rectangle.FromLTRB(Math.Min(x1, x2), Math.Min(y1, y2), Math.Max(x1, x2), Math.Max(y1, y2));

                this.reservePaint++;
            }
        }

        private void OverlayForm_MouseUp(object sender, MouseEventArgs e)
        {
            isSelecting = false;
            // ユーザーが領域選択を完了した場合の処理をここに追加します
            // selectedRegion 変数に選択された領域の情報が格納されています
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OverlayForm_Paint(object sender, PaintEventArgs e)
        {
            if (isSelecting)
            {
                //using (var brush = new SolidBrush(Color.FromArgb(128, Color.Blue)))
                //{
                //    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                //}
                using (var pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, SelectedRegion);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.reservePaint > 0)
            {
                this.reservePaint = 0;
                this.Invalidate();
            }
        }
    }
}
