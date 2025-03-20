namespace MyCapture
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_capture = new Button();
            btn_region = new Button();
            btn_GetApp = new Button();
            btn_Selected = new Button();
            lbl_Process = new Label();
            btn_SendKey = new Button();
            cmb_keys = new ComboBox();
            label1 = new Label();
            btn_PathSelect = new Button();
            lbl_path = new Label();
            txt_Lable = new TextBox();
            txt_StartNo = new TextBox();
            label2 = new Label();
            label3 = new Label();
            btn_Start = new Button();
            txt_Counts = new TextBox();
            label4 = new Label();
            btn_Cancel = new Button();
            progressBar = new ProgressBar();
            txt_delay = new TextBox();
            label5 = new Label();
            label6 = new Label();
            txt_folder = new TextBox();
            SuspendLayout();
            // 
            // btn_capture
            // 
            btn_capture.Location = new Point(136, 87);
            btn_capture.Name = "btn_capture";
            btn_capture.Size = new Size(127, 29);
            btn_capture.TabIndex = 0;
            btn_capture.Text = "Capture test";
            btn_capture.UseVisualStyleBackColor = true;
            btn_capture.Click += btn_capture_Click;
            // 
            // btn_region
            // 
            btn_region.Location = new Point(12, 87);
            btn_region.Name = "btn_region";
            btn_region.Size = new Size(118, 29);
            btn_region.TabIndex = 1;
            btn_region.Text = "Select region";
            btn_region.UseVisualStyleBackColor = true;
            btn_region.Click += btn_region_Click;
            // 
            // btn_GetApp
            // 
            btn_GetApp.Location = new Point(12, 17);
            btn_GetApp.Name = "btn_GetApp";
            btn_GetApp.Size = new Size(94, 29);
            btn_GetApp.TabIndex = 3;
            btn_GetApp.Text = "Set App.";
            btn_GetApp.UseVisualStyleBackColor = true;
            btn_GetApp.Click += btn_GetApp_Click;
            // 
            // btn_Selected
            // 
            btn_Selected.Location = new Point(595, 17);
            btn_Selected.Name = "btn_Selected";
            btn_Selected.Size = new Size(94, 29);
            btn_Selected.TabIndex = 5;
            btn_Selected.Text = "Selected";
            btn_Selected.UseVisualStyleBackColor = true;
            btn_Selected.Click += btn_Selected_Click;
            // 
            // lbl_Process
            // 
            lbl_Process.BorderStyle = BorderStyle.Fixed3D;
            lbl_Process.Location = new Point(112, 17);
            lbl_Process.Name = "lbl_Process";
            lbl_Process.Size = new Size(477, 29);
            lbl_Process.TabIndex = 6;
            // 
            // btn_SendKey
            // 
            btn_SendKey.Location = new Point(269, 53);
            btn_SendKey.Name = "btn_SendKey";
            btn_SendKey.Size = new Size(115, 29);
            btn_SendKey.TabIndex = 7;
            btn_SendKey.Text = "Send key test";
            btn_SendKey.UseVisualStyleBackColor = true;
            btn_SendKey.Click += btn_SendKey_Click;
            // 
            // cmb_keys
            // 
            cmb_keys.FormattingEnabled = true;
            cmb_keys.Items.AddRange(new object[] { "Space", "Enter", "Back Space", "Delete", "PageUp", "PageDown" });
            cmb_keys.Location = new Point(112, 53);
            cmb_keys.Name = "cmb_keys";
            cmb_keys.Size = new Size(151, 28);
            cmb_keys.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 57);
            label1.Name = "label1";
            label1.Size = new Size(91, 20);
            label1.TabIndex = 9;
            label1.Text = "Next button:";
            // 
            // btn_PathSelect
            // 
            btn_PathSelect.Location = new Point(12, 122);
            btn_PathSelect.Name = "btn_PathSelect";
            btn_PathSelect.Size = new Size(94, 29);
            btn_PathSelect.TabIndex = 10;
            btn_PathSelect.Text = "Path:";
            btn_PathSelect.UseVisualStyleBackColor = true;
            btn_PathSelect.Click += btn_PathSelect_Click;
            // 
            // lbl_path
            // 
            lbl_path.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl_path.BorderStyle = BorderStyle.Fixed3D;
            lbl_path.Location = new Point(112, 122);
            lbl_path.Name = "lbl_path";
            lbl_path.Size = new Size(405, 27);
            lbl_path.TabIndex = 11;
            // 
            // txt_Lable
            // 
            txt_Lable.Location = new Point(69, 157);
            txt_Lable.Name = "txt_Lable";
            txt_Lable.Size = new Size(125, 27);
            txt_Lable.TabIndex = 12;
            txt_Lable.Text = "image_";
            // 
            // txt_StartNo
            // 
            txt_StartNo.Location = new Point(256, 157);
            txt_StartNo.Name = "txt_StartNo";
            txt_StartNo.Size = new Size(71, 27);
            txt_StartNo.TabIndex = 13;
            txt_StartNo.Text = "0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 160);
            label2.Name = "label2";
            label2.Size = new Size(48, 20);
            label2.TabIndex = 14;
            label2.Text = "Label:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(205, 160);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.TabIndex = 15;
            label3.Text = "From:";
            // 
            // btn_Start
            // 
            btn_Start.Location = new Point(12, 190);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(160, 29);
            btn_Start.TabIndex = 16;
            btn_Start.Text = "Start Captures";
            btn_Start.UseVisualStyleBackColor = true;
            btn_Start.Click += btn_Start_Click;
            // 
            // txt_Counts
            // 
            txt_Counts.Location = new Point(408, 157);
            txt_Counts.Name = "txt_Counts";
            txt_Counts.Size = new Size(63, 27);
            txt_Counts.TabIndex = 17;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(345, 160);
            label4.Name = "label4";
            label4.Size = new Size(57, 20);
            label4.TabIndex = 18;
            label4.Text = "Counts:";
            // 
            // btn_Cancel
            // 
            btn_Cancel.Enabled = false;
            btn_Cancel.Location = new Point(595, 190);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(94, 29);
            btn_Cancel.TabIndex = 19;
            btn_Cancel.Text = "Abort";
            btn_Cancel.UseVisualStyleBackColor = true;
            btn_Cancel.Click += btn_Cancel_Click;
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Location = new Point(0, 231);
            progressBar.Margin = new Padding(0, 3, 0, 0);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(701, 25);
            progressBar.Step = 1;
            progressBar.TabIndex = 20;
            // 
            // txt_delay
            // 
            txt_delay.Location = new Point(454, 54);
            txt_delay.Name = "txt_delay";
            txt_delay.Size = new Size(63, 27);
            txt_delay.TabIndex = 21;
            txt_delay.Text = "300";
            txt_delay.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(401, 58);
            label5.Name = "label5";
            label5.Size = new Size(47, 20);
            label5.TabIndex = 22;
            label5.Text = "Delay";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(517, 58);
            label6.Name = "label6";
            label6.Size = new Size(28, 20);
            label6.TabIndex = 23;
            label6.Text = "ms";
            // 
            // txt_folder
            // 
            txt_folder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txt_folder.Location = new Point(523, 122);
            txt_folder.Name = "txt_folder";
            txt_folder.Size = new Size(166, 27);
            txt_folder.TabIndex = 24;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(701, 256);
            Controls.Add(txt_folder);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txt_delay);
            Controls.Add(progressBar);
            Controls.Add(btn_Cancel);
            Controls.Add(label4);
            Controls.Add(txt_Counts);
            Controls.Add(btn_Start);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txt_StartNo);
            Controls.Add(txt_Lable);
            Controls.Add(lbl_path);
            Controls.Add(btn_PathSelect);
            Controls.Add(label1);
            Controls.Add(cmb_keys);
            Controls.Add(btn_SendKey);
            Controls.Add(lbl_Process);
            Controls.Add(btn_Selected);
            Controls.Add(btn_GetApp);
            Controls.Add(btn_region);
            Controls.Add(btn_capture);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Main";
            Text = "My image capture";
            FormClosing += Main_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_capture;
        private Button btn_region;
        private Button btn_GetApp;
        private ComboBox cmb_Apps;
        private Button btn_Selected;
        private Label lbl_Process;
        private Button btn_SendKey;
        private ComboBox cmb_keys;
        private Label label1;
        private Button btn_PathSelect;
        private Label lbl_path;
        private TextBox txt_Lable;
        private TextBox txt_StartNo;
        private Label label2;
        private Label label3;
        private Button btn_Start;
        private TextBox txt_Counts;
        private Label label4;
        private Button btn_Cancel;
        private ProgressBar progressBar;
        private TextBox txt_delay;
        private Label label5;
        private Label label6;
        private TextBox txt_folder;
    }
}
