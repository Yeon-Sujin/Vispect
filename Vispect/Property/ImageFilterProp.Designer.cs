namespace Vispect
{
    partial class ImageFilterProp
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cbImageFilter;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnOriginal;
        private System.Windows.Forms.Button btnUndo;

        private System.Windows.Forms.Panel panelResizeOptions;
        private System.Windows.Forms.Label lblResizeRange;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown numHeight;

        private System.Windows.Forms.Panel panelBinaryOptions;
        private System.Windows.Forms.Label lblBinaryThreshold;
        private System.Windows.Forms.TrackBar trackBarThreshold;
        private System.Windows.Forms.Label lblThresholdValue;

        private System.Windows.Forms.Panel panelFlipOptions;
        private System.Windows.Forms.RadioButton radioHorizontal;
        private System.Windows.Forms.RadioButton radioVertical;
        private System.Windows.Forms.RadioButton radioBoth;

        private System.Windows.Forms.Panel panelRotateOptions;
        private System.Windows.Forms.Label lblRotateAngle;
        private System.Windows.Forms.TrackBar trackBarAngle;
        private System.Windows.Forms.Label lblAngleValue;

        private System.Windows.Forms.Panel panelCannyOptions;
        private System.Windows.Forms.Label lblCannyLower;
        private System.Windows.Forms.TrackBar trackBarCannyLower;
        private System.Windows.Forms.Label lblCannyLowerValue;
        private System.Windows.Forms.Label lblCannyUpper;
        private System.Windows.Forms.TrackBar trackBarCannyUpper;
        private System.Windows.Forms.Label lblCannyUpperValue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbImageFilter = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnOriginal = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.panelResizeOptions = new System.Windows.Forms.Panel();
            this.lblResizeRange = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.panelBinaryOptions = new System.Windows.Forms.Panel();
            this.lblBinaryThreshold = new System.Windows.Forms.Label();
            this.trackBarThreshold = new System.Windows.Forms.TrackBar();
            this.lblThresholdValue = new System.Windows.Forms.Label();
            this.panelFlipOptions = new System.Windows.Forms.Panel();
            this.radioHorizontal = new System.Windows.Forms.RadioButton();
            this.radioVertical = new System.Windows.Forms.RadioButton();
            this.radioBoth = new System.Windows.Forms.RadioButton();
            this.panelRotateOptions = new System.Windows.Forms.Panel();
            this.lblRotateAngle = new System.Windows.Forms.Label();
            this.trackBarAngle = new System.Windows.Forms.TrackBar();
            this.lblAngleValue = new System.Windows.Forms.Label();
            this.panelCannyOptions = new System.Windows.Forms.Panel();
            this.lblCannyLower = new System.Windows.Forms.Label();
            this.trackBarCannyLower = new System.Windows.Forms.TrackBar();
            this.lblCannyLowerValue = new System.Windows.Forms.Label();
            this.lblCannyUpper = new System.Windows.Forms.Label();
            this.trackBarCannyUpper = new System.Windows.Forms.TrackBar();
            this.lblCannyUpperValue = new System.Windows.Forms.Label();
            this.panelResizeOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            this.panelBinaryOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).BeginInit();
            this.panelFlipOptions.SuspendLayout();
            this.panelRotateOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAngle)).BeginInit();
            this.panelCannyOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCannyLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCannyUpper)).BeginInit();
            this.SuspendLayout();
            // 
            // cbImageFilter
            // 
            this.cbImageFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFilter.FormattingEnabled = true;
            this.cbImageFilter.Location = new System.Drawing.Point(36, 46);
            this.cbImageFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbImageFilter.Name = "cbImageFilter";
            this.cbImageFilter.Size = new System.Drawing.Size(307, 26);
            this.cbImageFilter.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.SystemColors.Info;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(259, 118);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(83, 57);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "적용";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnOriginal
            // 
            this.btnOriginal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOriginal.FlatAppearance.BorderSize = 0;
            this.btnOriginal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOriginal.Location = new System.Drawing.Point(146, 118);
            this.btnOriginal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOriginal.Name = "btnOriginal";
            this.btnOriginal.Size = new System.Drawing.Size(83, 57);
            this.btnOriginal.TabIndex = 2;
            this.btnOriginal.Text = "원본";
            this.btnOriginal.UseVisualStyleBackColor = false;
            this.btnOriginal.Click += new System.EventHandler(this.btnOriginal_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.BackColor = System.Drawing.SystemColors.Info;
            this.btnUndo.FlatAppearance.BorderSize = 0;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Location = new System.Drawing.Point(36, 118);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(83, 57);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "이전";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // panelResizeOptions
            // 
            this.panelResizeOptions.Controls.Add(this.panelCannyOptions);
            this.panelResizeOptions.Controls.Add(this.lblResizeRange);
            this.panelResizeOptions.Controls.Add(this.lblWidth);
            this.panelResizeOptions.Controls.Add(this.numWidth);
            this.panelResizeOptions.Controls.Add(this.lblHeight);
            this.panelResizeOptions.Controls.Add(this.numHeight);
            this.panelResizeOptions.Location = new System.Drawing.Point(36, 181);
            this.panelResizeOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelResizeOptions.Name = "panelResizeOptions";
            this.panelResizeOptions.Size = new System.Drawing.Size(307, 165);
            this.panelResizeOptions.TabIndex = 4;
            this.panelResizeOptions.Visible = false;
            // 
            // lblResizeRange
            // 
            this.lblResizeRange.AutoSize = true;
            this.lblResizeRange.Location = new System.Drawing.Point(7, 8);
            this.lblResizeRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResizeRange.Name = "lblResizeRange";
            this.lblResizeRange.Size = new System.Drawing.Size(122, 18);
            this.lblResizeRange.TabIndex = 0;
            this.lblResizeRange.Text = "10% ~ 2000%";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(7, 38);
            this.lblWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(57, 18);
            this.lblWidth.TabIndex = 1;
            this.lblWidth.Text = "Width:";
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(100, 34);
            this.numWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(143, 28);
            this.numWidth.TabIndex = 2;
            this.numWidth.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(7, 82);
            this.lblHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(63, 18);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "Height:";
            // 
            // numHeight
            // 
            this.numHeight.Location = new System.Drawing.Point(100, 80);
            this.numHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(143, 28);
            this.numHeight.TabIndex = 4;
            this.numHeight.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // panelBinaryOptions
            // 
            this.panelBinaryOptions.Controls.Add(this.lblBinaryThreshold);
            this.panelBinaryOptions.Controls.Add(this.trackBarThreshold);
            this.panelBinaryOptions.Controls.Add(this.lblThresholdValue);
            this.panelBinaryOptions.Location = new System.Drawing.Point(36, 180);
            this.panelBinaryOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelBinaryOptions.Name = "panelBinaryOptions";
            this.panelBinaryOptions.Size = new System.Drawing.Size(307, 120);
            this.panelBinaryOptions.TabIndex = 5;
            this.panelBinaryOptions.Visible = false;
            // 
            // lblBinaryThreshold
            // 
            this.lblBinaryThreshold.AutoSize = true;
            this.lblBinaryThreshold.Location = new System.Drawing.Point(7, 8);
            this.lblBinaryThreshold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBinaryThreshold.Name = "lblBinaryThreshold";
            this.lblBinaryThreshold.Size = new System.Drawing.Size(94, 18);
            this.lblBinaryThreshold.TabIndex = 0;
            this.lblBinaryThreshold.Text = "Threshold:";
            // 
            // trackBarThreshold
            // 
            this.trackBarThreshold.Location = new System.Drawing.Point(11, 45);
            this.trackBarThreshold.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarThreshold.Maximum = 255;
            this.trackBarThreshold.Name = "trackBarThreshold";
            this.trackBarThreshold.Size = new System.Drawing.Size(286, 69);
            this.trackBarThreshold.TabIndex = 1;
            this.trackBarThreshold.Value = 128;
            this.trackBarThreshold.Scroll += new System.EventHandler(this.trackBarThreshold_Scroll);
            // 
            // lblThresholdValue
            // 
            this.lblThresholdValue.AutoSize = true;
            this.lblThresholdValue.Location = new System.Drawing.Point(243, 8);
            this.lblThresholdValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblThresholdValue.Name = "lblThresholdValue";
            this.lblThresholdValue.Size = new System.Drawing.Size(38, 18);
            this.lblThresholdValue.TabIndex = 2;
            this.lblThresholdValue.Text = "128";
            // 
            // panelFlipOptions
            // 
            this.panelFlipOptions.Controls.Add(this.radioHorizontal);
            this.panelFlipOptions.Controls.Add(this.radioVertical);
            this.panelFlipOptions.Controls.Add(this.radioBoth);
            this.panelFlipOptions.Location = new System.Drawing.Point(36, 180);
            this.panelFlipOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelFlipOptions.Name = "panelFlipOptions";
            this.panelFlipOptions.Size = new System.Drawing.Size(307, 150);
            this.panelFlipOptions.TabIndex = 6;
            this.panelFlipOptions.Visible = false;
            // 
            // radioHorizontal
            // 
            this.radioHorizontal.AutoSize = true;
            this.radioHorizontal.Location = new System.Drawing.Point(11, 38);
            this.radioHorizontal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioHorizontal.Name = "radioHorizontal";
            this.radioHorizontal.Size = new System.Drawing.Size(69, 22);
            this.radioHorizontal.TabIndex = 0;
            this.radioHorizontal.Text = "수평";
            // 
            // radioVertical
            // 
            this.radioVertical.AutoSize = true;
            this.radioVertical.Location = new System.Drawing.Point(11, 68);
            this.radioVertical.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioVertical.Name = "radioVertical";
            this.radioVertical.Size = new System.Drawing.Size(69, 22);
            this.radioVertical.TabIndex = 1;
            this.radioVertical.Text = "수직";
            // 
            // radioBoth
            // 
            this.radioBoth.AutoSize = true;
            this.radioBoth.Location = new System.Drawing.Point(11, 98);
            this.radioBoth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioBoth.Name = "radioBoth";
            this.radioBoth.Size = new System.Drawing.Size(126, 22);
            this.radioBoth.TabIndex = 2;
            this.radioBoth.Text = "수평 + 수직";
            // 
            // panelRotateOptions
            // 
            this.panelRotateOptions.Controls.Add(this.lblRotateAngle);
            this.panelRotateOptions.Controls.Add(this.trackBarAngle);
            this.panelRotateOptions.Controls.Add(this.lblAngleValue);
            this.panelRotateOptions.Location = new System.Drawing.Point(36, 180);
            this.panelRotateOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelRotateOptions.Name = "panelRotateOptions";
            this.panelRotateOptions.Size = new System.Drawing.Size(307, 135);
            this.panelRotateOptions.TabIndex = 7;
            this.panelRotateOptions.Visible = false;
            // 
            // lblRotateAngle
            // 
            this.lblRotateAngle.AutoSize = true;
            this.lblRotateAngle.Location = new System.Drawing.Point(7, 12);
            this.lblRotateAngle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRotateAngle.Name = "lblRotateAngle";
            this.lblRotateAngle.Size = new System.Drawing.Size(59, 18);
            this.lblRotateAngle.TabIndex = 0;
            this.lblRotateAngle.Text = "Angle:";
            // 
            // trackBarAngle
            // 
            this.trackBarAngle.Location = new System.Drawing.Point(11, 45);
            this.trackBarAngle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarAngle.Maximum = 365;
            this.trackBarAngle.Minimum = 1;
            this.trackBarAngle.Name = "trackBarAngle";
            this.trackBarAngle.Size = new System.Drawing.Size(286, 69);
            this.trackBarAngle.TabIndex = 1;
            this.trackBarAngle.Value = 90;
            this.trackBarAngle.Scroll += new System.EventHandler(this.trackBarAngle_Scroll);
            // 
            // lblAngleValue
            // 
            this.lblAngleValue.AutoSize = true;
            this.lblAngleValue.Location = new System.Drawing.Point(243, 12);
            this.lblAngleValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAngleValue.Name = "lblAngleValue";
            this.lblAngleValue.Size = new System.Drawing.Size(35, 18);
            this.lblAngleValue.TabIndex = 2;
            this.lblAngleValue.Text = "90°";
            // 
            // panelCannyOptions
            // 
            this.panelCannyOptions.Controls.Add(this.lblCannyLower);
            this.panelCannyOptions.Controls.Add(this.lblCannyLowerValue);
            this.panelCannyOptions.Controls.Add(this.lblCannyUpper);
            this.panelCannyOptions.Controls.Add(this.lblCannyUpperValue);
            this.panelCannyOptions.Controls.Add(this.trackBarCannyLower);
            this.panelCannyOptions.Controls.Add(this.trackBarCannyUpper);
            this.panelCannyOptions.Location = new System.Drawing.Point(0, 8);
            this.panelCannyOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelCannyOptions.Name = "panelCannyOptions";
            this.panelCannyOptions.Size = new System.Drawing.Size(307, 237);
            this.panelCannyOptions.TabIndex = 8;
            this.panelCannyOptions.Visible = false;
            // 
            // lblCannyLower
            // 
            this.lblCannyLower.AutoSize = true;
            this.lblCannyLower.Location = new System.Drawing.Point(7, 12);
            this.lblCannyLower.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCannyLower.Name = "lblCannyLower";
            this.lblCannyLower.Size = new System.Drawing.Size(68, 18);
            this.lblCannyLower.TabIndex = 0;
            this.lblCannyLower.Text = "하한값:";
            // 
            // trackBarCannyLower
            // 
            this.trackBarCannyLower.Location = new System.Drawing.Point(11, 45);
            this.trackBarCannyLower.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarCannyLower.Maximum = 255;
            this.trackBarCannyLower.Name = "trackBarCannyLower";
            this.trackBarCannyLower.Size = new System.Drawing.Size(286, 69);
            this.trackBarCannyLower.TabIndex = 1;
            this.trackBarCannyLower.Value = 100;
            this.trackBarCannyLower.Scroll += new System.EventHandler(this.trackBarCannyLower_Scroll);
            // 
            // lblCannyLowerValue
            // 
            this.lblCannyLowerValue.AutoSize = true;
            this.lblCannyLowerValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCannyLowerValue.Location = new System.Drawing.Point(83, 12);
            this.lblCannyLowerValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCannyLowerValue.Name = "lblCannyLowerValue";
            this.lblCannyLowerValue.Size = new System.Drawing.Size(38, 18);
            this.lblCannyLowerValue.TabIndex = 2;
            this.lblCannyLowerValue.Text = "100";
            // 
            // lblCannyUpper
            // 
            this.lblCannyUpper.AutoSize = true;
            this.lblCannyUpper.Location = new System.Drawing.Point(7, 120);
            this.lblCannyUpper.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCannyUpper.Name = "lblCannyUpper";
            this.lblCannyUpper.Size = new System.Drawing.Size(68, 18);
            this.lblCannyUpper.TabIndex = 3;
            this.lblCannyUpper.Text = "상한값:";
            // 
            // trackBarCannyUpper
            // 
            this.trackBarCannyUpper.Location = new System.Drawing.Point(11, 153);
            this.trackBarCannyUpper.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBarCannyUpper.Maximum = 255;
            this.trackBarCannyUpper.Name = "trackBarCannyUpper";
            this.trackBarCannyUpper.Size = new System.Drawing.Size(286, 69);
            this.trackBarCannyUpper.TabIndex = 4;
            this.trackBarCannyUpper.Value = 200;
            this.trackBarCannyUpper.Scroll += new System.EventHandler(this.trackBarCannyUpper_Scroll);
            // 
            // lblCannyUpperValue
            // 
            this.lblCannyUpperValue.AutoSize = true;
            this.lblCannyUpperValue.Location = new System.Drawing.Point(83, 118);
            this.lblCannyUpperValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCannyUpperValue.Name = "lblCannyUpperValue";
            this.lblCannyUpperValue.Size = new System.Drawing.Size(38, 18);
            this.lblCannyUpperValue.TabIndex = 5;
            this.lblCannyUpperValue.Text = "200";
            // 
            // ImageFilterProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBinaryOptions);
            this.Controls.Add(this.panelFlipOptions);
            this.Controls.Add(this.panelRotateOptions);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnOriginal);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbImageFilter);
            this.Controls.Add(this.panelResizeOptions);
            this.Font = new System.Drawing.Font("굴림", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ImageFilterProp";
            this.Size = new System.Drawing.Size(376, 454);
            this.panelResizeOptions.ResumeLayout(false);
            this.panelResizeOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            this.panelBinaryOptions.ResumeLayout(false);
            this.panelBinaryOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).EndInit();
            this.panelFlipOptions.ResumeLayout(false);
            this.panelFlipOptions.PerformLayout();
            this.panelRotateOptions.ResumeLayout(false);
            this.panelRotateOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAngle)).EndInit();
            this.panelCannyOptions.ResumeLayout(false);
            this.panelCannyOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCannyLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCannyUpper)).EndInit();
            this.ResumeLayout(false);

        }
    }
}