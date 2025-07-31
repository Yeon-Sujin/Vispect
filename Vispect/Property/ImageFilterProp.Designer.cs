namespace Vispect
{
    partial class ImageFilterProp
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbImageFilter = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnOriginal = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.tabControlParams = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // cbImageFilter
            // 
            this.cbImageFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFilter.FormattingEnabled = true;
            this.cbImageFilter.Location = new System.Drawing.Point(36, 47);
            this.cbImageFilter.Name = "cbImageFilter";
            this.cbImageFilter.Size = new System.Drawing.Size(305, 26);
            this.cbImageFilter.TabIndex = 0;
            this.cbImageFilter.SelectionChangeCommitted += new System.EventHandler(this.cbImageFilter_SelectionChangeCommitted);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.SystemColors.Info;
            this.btnApply.Location = new System.Drawing.Point(258, 119);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(83, 57);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "적용";
            this.btnApply.UseVisualStyleBackColor = false;
            // 
            // btnOriginal
            // 
            this.btnOriginal.Location = new System.Drawing.Point(146, 119);
            this.btnOriginal.Name = "btnOriginal";
            this.btnOriginal.Size = new System.Drawing.Size(83, 57);
            this.btnOriginal.TabIndex = 17;
            this.btnOriginal.Text = "원본";
            this.btnOriginal.UseVisualStyleBackColor = true;
            // 
            // btnUndo
            // 
            this.btnUndo.BackColor = System.Drawing.SystemColors.Info;
            this.btnUndo.Location = new System.Drawing.Point(36, 119);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(83, 57);
            this.btnUndo.TabIndex = 19;
            this.btnUndo.Text = "이전";
            this.btnUndo.UseVisualStyleBackColor = false;
            // 
            // tabControlParams
            // 
            this.tabControlParams.Location = new System.Drawing.Point(36, 221);
            this.tabControlParams.Name = "tabControlParams";
            this.tabControlParams.SelectedIndex = 0;
            this.tabControlParams.Size = new System.Drawing.Size(305, 227);
            this.tabControlParams.TabIndex = 20;
            // 
            // ImageFilterProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnOriginal);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbImageFilter);
            this.Controls.Add(this.tabControlParams);
            this.Name = "ImageFilterProp";
            this.Size = new System.Drawing.Size(375, 487);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbImageFilter;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnOriginal;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.TabControl tabControlParams;
    }
}
