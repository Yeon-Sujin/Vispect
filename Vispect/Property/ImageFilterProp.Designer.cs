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

            // Resize
            this.panelResizeOptions = new System.Windows.Forms.Panel();
            this.lblResizeRange = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();

            // Binary
            this.panelBinaryOptions = new System.Windows.Forms.Panel();
            this.lblBinaryThreshold = new System.Windows.Forms.Label();
            this.trackBarThreshold = new System.Windows.Forms.TrackBar();
            this.lblThresholdValue = new System.Windows.Forms.Label();

            // Flip
            this.panelFlipOptions = new System.Windows.Forms.Panel();
            this.radioHorizontal = new System.Windows.Forms.RadioButton();
            this.radioVertical = new System.Windows.Forms.RadioButton();
            this.radioBoth = new System.Windows.Forms.RadioButton();

            // Rotate
            this.panelRotateOptions = new System.Windows.Forms.Panel();
            this.lblRotateAngle = new System.Windows.Forms.Label();
            this.trackBarAngle = new System.Windows.Forms.TrackBar();
            this.lblAngleValue = new System.Windows.Forms.Label();

            // Canny
            this.panelCannyOptions = new System.Windows.Forms.Panel();
            this.lblCannyLower = new System.Windows.Forms.Label();
            this.trackBarCannyLower = new System.Windows.Forms.TrackBar();
            this.lblCannyLowerValue = new System.Windows.Forms.Label();
            this.lblCannyUpper = new System.Windows.Forms.Label();
            this.trackBarCannyUpper = new System.Windows.Forms.TrackBar();
            this.lblCannyUpperValue = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCannyLower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCannyUpper)).BeginInit();

            this.SuspendLayout();

            // 전체 Font를 굴림으로
            this.Font = new System.Drawing.Font("굴림", 9F);

            // cbImageFilter
            this.cbImageFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFilter.FormattingEnabled = true;
            this.cbImageFilter.Location = new System.Drawing.Point(25, 31);
            this.cbImageFilter.Name = "cbImageFilter";
            this.cbImageFilter.Size = new System.Drawing.Size(215, 20);
            this.cbImageFilter.TabIndex = 0;

            // btnApply
            this.btnApply.BackColor = System.Drawing.SystemColors.Info;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(181, 79);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(58, 38);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "적용";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            // btnOriginal
            this.btnOriginal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOriginal.FlatAppearance.BorderSize = 0;
            this.btnOriginal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOriginal.Location = new System.Drawing.Point(102, 79);
            this.btnOriginal.Name = "btnOriginal";
            this.btnOriginal.Size = new System.Drawing.Size(58, 38);
            this.btnOriginal.TabIndex = 2;
            this.btnOriginal.Text = "원본";
            this.btnOriginal.UseVisualStyleBackColor = false;
            this.btnOriginal.Click += new System.EventHandler(this.btnOriginal_Click);

            // btnUndo
            this.btnUndo.BackColor = System.Drawing.SystemColors.Info;
            this.btnUndo.FlatAppearance.BorderSize = 0;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Location = new System.Drawing.Point(25, 79);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(58, 38);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "이전";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);

            // panelResizeOptions
            this.panelResizeOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelResizeOptions.Location = new System.Drawing.Point(25, 120);
            this.panelResizeOptions.Name = "panelResizeOptions";
            this.panelResizeOptions.Size = new System.Drawing.Size(215, 110);
            this.panelResizeOptions.TabIndex = 4;
            this.panelResizeOptions.Visible = false;

            this.lblResizeRange.AutoSize = true;
            this.lblResizeRange.Location = new System.Drawing.Point(5, 5);
            this.lblResizeRange.Name = "lblResizeRange";
            this.lblResizeRange.Size = new System.Drawing.Size(100, 12);
            this.lblResizeRange.Text = "10% ~ 2000%";

            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(5, 25);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(44, 12);
            this.lblWidth.Text = "Width:";

            this.numWidth.Location = new System.Drawing.Point(70, 23);
            this.numWidth.Minimum = 10;
            this.numWidth.Maximum = 2000;
            this.numWidth.Value = 300;
            this.numWidth.Size = new System.Drawing.Size(100, 21);

            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(5, 55);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(47, 12);
            this.lblHeight.Text = "Height:";

            this.numHeight.Location = new System.Drawing.Point(70, 53);
            this.numHeight.Minimum = 10;
            this.numHeight.Maximum = 2000;
            this.numHeight.Value = 300;
            this.numHeight.Size = new System.Drawing.Size(100, 21);

            this.panelResizeOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblResizeRange, this.lblWidth, this.numWidth, this.lblHeight, this.numHeight });

            // panelBinaryOptions
            this.panelBinaryOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelBinaryOptions.Location = new System.Drawing.Point(25, 120);
            this.panelBinaryOptions.Name = "panelBinaryOptions";
            this.panelBinaryOptions.Size = new System.Drawing.Size(215, 80);
            this.panelBinaryOptions.TabIndex = 5;
            this.panelBinaryOptions.Visible = false;

            this.lblBinaryThreshold.AutoSize = true;
            this.lblBinaryThreshold.Location = new System.Drawing.Point(5, 5);
            this.lblBinaryThreshold.Name = "lblBinaryThreshold";
            this.lblBinaryThreshold.Size = new System.Drawing.Size(71, 12);
            this.lblBinaryThreshold.Text = "Threshold:";

            this.trackBarThreshold.Location = new System.Drawing.Point(8, 30);
            this.trackBarThreshold.Maximum = 255;
            this.trackBarThreshold.Minimum = 0;
            this.trackBarThreshold.TickFrequency = 1;
            this.trackBarThreshold.Size = new System.Drawing.Size(200, 45);
            this.trackBarThreshold.Value = 128;
            this.trackBarThreshold.Scroll += new System.EventHandler(this.trackBarThreshold_Scroll);

            this.lblThresholdValue.AutoSize = true;
            this.lblThresholdValue.Location = new System.Drawing.Point(170, 5);
            this.lblThresholdValue.Name = "lblThresholdValue";
            this.lblThresholdValue.Size = new System.Drawing.Size(35, 12);
            this.lblThresholdValue.Text = "128";

            this.panelBinaryOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBinaryThreshold, this.trackBarThreshold, this.lblThresholdValue });

            // panelFlipOptions
            this.panelFlipOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelFlipOptions.Location = new System.Drawing.Point(25, 120);
            this.panelFlipOptions.Name = "panelFlipOptions";
            this.panelFlipOptions.Size = new System.Drawing.Size(215, 100);
            this.panelFlipOptions.TabIndex = 6;
            this.panelFlipOptions.Visible = false;

            this.radioHorizontal.AutoSize = true;
            this.radioHorizontal.Location = new System.Drawing.Point(8, 25);
            this.radioHorizontal.Name = "radioHorizontal";
            this.radioHorizontal.Size = new System.Drawing.Size(59, 16);
            this.radioHorizontal.Text = "수평";
            this.radioVertical.AutoSize = true;
            this.radioVertical.Location = new System.Drawing.Point(8, 45);
            this.radioVertical.Name = "radioVertical";
            this.radioVertical.Size = new System.Drawing.Size(47, 16);
            this.radioVertical.Text = "수직";
            this.radioBoth.AutoSize = true;
            this.radioBoth.Location = new System.Drawing.Point(8, 65);
            this.radioBoth.Name = "radioBoth";
            this.radioBoth.Size = new System.Drawing.Size(85, 16);
            this.radioBoth.Text = "수평 + 수직";

            this.panelFlipOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.radioHorizontal, this.radioVertical, this.radioBoth });

            // panelRotateOptions
            this.panelRotateOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelRotateOptions.Location = new System.Drawing.Point(25, 120);
            this.panelRotateOptions.Name = "panelRotateOptions";
            this.panelRotateOptions.Size = new System.Drawing.Size(215, 90);
            this.panelRotateOptions.TabIndex = 7;
            this.panelRotateOptions.Visible = false;

            this.lblRotateAngle.AutoSize = true;
            this.lblRotateAngle.Location = new System.Drawing.Point(5, 8);
            this.lblRotateAngle.Name = "lblRotateAngle";
            this.lblRotateAngle.Size = new System.Drawing.Size(41, 12);
            this.lblRotateAngle.Text = "Angle:";

            this.trackBarAngle.Location = new System.Drawing.Point(8, 30);
            this.trackBarAngle.Maximum = 365;
            this.trackBarAngle.Minimum = 1;
            this.trackBarAngle.TickFrequency = 1;
            this.trackBarAngle.Size = new System.Drawing.Size(200, 45);
            this.trackBarAngle.Value = 90;
            this.trackBarAngle.Scroll += new System.EventHandler(this.trackBarAngle_Scroll);

            this.lblAngleValue.AutoSize = true;
            this.lblAngleValue.Location = new System.Drawing.Point(170, 8);
            this.lblAngleValue.Name = "lblAngleValue";
            this.lblAngleValue.Size = new System.Drawing.Size(35, 12);
            this.lblAngleValue.Text = "90°";

            this.panelRotateOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblRotateAngle, this.trackBarAngle, this.lblAngleValue });

            // panelCannyOptions
            this.panelCannyOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelCannyOptions.Location = new System.Drawing.Point(25, 120);
            this.panelCannyOptions.Name = "panelCannyOptions";
            this.panelCannyOptions.Size = new System.Drawing.Size(215, 140);
            this.panelCannyOptions.TabIndex = 8;
            this.panelCannyOptions.Visible = false;

            this.lblCannyLower.AutoSize = true;
            this.lblCannyLower.Location = new System.Drawing.Point(5, 8);
            this.lblCannyLower.Name = "lblCannyLower";
            this.lblCannyLower.Size = new System.Drawing.Size(50, 12);
            this.lblCannyLower.Text = "하한값:";

            this.trackBarCannyLower.Location = new System.Drawing.Point(8, 30);
            this.trackBarCannyLower.Maximum = 255;
            this.trackBarCannyLower.Minimum = 0;
            this.trackBarCannyLower.TickFrequency = 1;
            this.trackBarCannyLower.Size = new System.Drawing.Size(200, 45);
            this.trackBarCannyLower.Value = 100;
            this.trackBarCannyLower.Scroll += new System.EventHandler(this.trackBarCannyLower_Scroll);

            this.lblCannyLowerValue.AutoSize = true;
            this.lblCannyLowerValue.Location = new System.Drawing.Point(170, 30);
            this.lblCannyLowerValue.Name = "lblCannyLowerValue";
            this.lblCannyLowerValue.Size = new System.Drawing.Size(35, 12);
            this.lblCannyLowerValue.Text = "100";

            this.lblCannyUpper.AutoSize = true;
            this.lblCannyUpper.Location = new System.Drawing.Point(5, 80);
            this.lblCannyUpper.Name = "lblCannyUpper";
            this.lblCannyUpper.Size = new System.Drawing.Size(50, 12);
            this.lblCannyUpper.Text = "상한값:";

            this.trackBarCannyUpper.Location = new System.Drawing.Point(8, 102);
            this.trackBarCannyUpper.Maximum = 255;
            this.trackBarCannyUpper.Minimum = 0;
            this.trackBarCannyUpper.TickFrequency = 1;
            this.trackBarCannyUpper.Size = new System.Drawing.Size(200, 45);
            this.trackBarCannyUpper.Value = 200;
            this.trackBarCannyUpper.Scroll += new System.EventHandler(this.trackBarCannyUpper_Scroll);

            this.lblCannyUpperValue.AutoSize = true;
            this.lblCannyUpperValue.Location = new System.Drawing.Point(170, 102);
            this.lblCannyUpperValue.Name = "lblCannyUpperValue";
            this.lblCannyUpperValue.Size = new System.Drawing.Size(35, 12);
            this.lblCannyUpperValue.Text = "200";

            this.panelCannyOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCannyLower, this.trackBarCannyLower, this.lblCannyLowerValue,
                this.lblCannyUpper, this.trackBarCannyUpper, this.lblCannyUpperValue });

            // container
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelResizeOptions);
            this.Controls.Add(this.panelBinaryOptions);
            this.Controls.Add(this.panelFlipOptions);
            this.Controls.Add(this.panelRotateOptions);
            this.Controls.Add(this.panelCannyOptions);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnOriginal);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbImageFilter);
            this.Name = "ImageFilterProp";
            this.Size = new System.Drawing.Size(262, 480);

            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCannyLower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCannyUpper)).EndInit();
            this.ResumeLayout(false);
        }
    }
}