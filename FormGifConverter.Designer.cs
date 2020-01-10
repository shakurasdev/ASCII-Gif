namespace GifConversionApp
{
    partial class FormGifConverter
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownFont = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownDivisor = new System.Windows.Forms.NumericUpDown();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.cbLowerCase = new System.Windows.Forms.CheckBox();
            this.cbUpperCase = new System.Windows.Forms.CheckBox();
            this.cbNumbers = new System.Windows.Forms.CheckBox();
            this.cbSpecialChars1 = new System.Windows.Forms.CheckBox();
            this.cbSpecialChars2 = new System.Windows.Forms.CheckBox();
            this.cbSpecialChars3 = new System.Windows.Forms.CheckBox();
            this.cbSpecialChars4 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDivisor)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(372, 65);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Font size";
            // 
            // numericUpDownFont
            // 
            this.numericUpDownFont.Location = new System.Drawing.Point(75, 116);
            this.numericUpDownFont.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownFont.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownFont.Name = "numericUpDownFont";
            this.numericUpDownFont.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownFont.TabIndex = 8;
            this.numericUpDownFont.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Granularity";
            // 
            // numericUpDownDivisor
            // 
            this.numericUpDownDivisor.Location = new System.Drawing.Point(75, 90);
            this.numericUpDownDivisor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDivisor.Name = "numericUpDownDivisor";
            this.numericUpDownDivisor.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownDivisor.TabIndex = 6;
            this.numericUpDownDivisor.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 161);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(372, 46);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 10;
            // 
            // cbLowerCase
            // 
            this.cbLowerCase.AutoSize = true;
            this.cbLowerCase.Location = new System.Drawing.Point(152, 107);
            this.cbLowerCase.Name = "cbLowerCase";
            this.cbLowerCase.Size = new System.Drawing.Size(46, 17);
            this.cbLowerCase.TabIndex = 11;
            this.cbLowerCase.Text = "a - z";
            this.cbLowerCase.UseVisualStyleBackColor = true;
            // 
            // cbUpperCase
            // 
            this.cbUpperCase.AutoSize = true;
            this.cbUpperCase.Location = new System.Drawing.Point(152, 130);
            this.cbUpperCase.Name = "cbUpperCase";
            this.cbUpperCase.Size = new System.Drawing.Size(49, 17);
            this.cbUpperCase.TabIndex = 12;
            this.cbUpperCase.Text = "A - Z";
            this.cbUpperCase.UseVisualStyleBackColor = true;
            // 
            // cbNumbers
            // 
            this.cbNumbers.AutoSize = true;
            this.cbNumbers.Location = new System.Drawing.Point(152, 84);
            this.cbNumbers.Name = "cbNumbers";
            this.cbNumbers.Size = new System.Drawing.Size(47, 17);
            this.cbNumbers.TabIndex = 13;
            this.cbNumbers.Text = "0 - 9";
            this.cbNumbers.UseVisualStyleBackColor = true;
            // 
            // cbSpecialChars1
            // 
            this.cbSpecialChars1.AutoSize = true;
            this.cbSpecialChars1.Location = new System.Drawing.Point(205, 84);
            this.cbSpecialChars1.Name = "cbSpecialChars1";
            this.cbSpecialChars1.Size = new System.Drawing.Size(128, 17);
            this.cbSpecialChars1.TabIndex = 14;
            this.cbSpecialChars1.Text = "[SPACE]!\"#$%&\'()*+,-./";
            this.cbSpecialChars1.UseVisualStyleBackColor = true;
            // 
            // cbSpecialChars2
            // 
            this.cbSpecialChars2.AutoSize = true;
            this.cbSpecialChars2.Location = new System.Drawing.Point(204, 107);
            this.cbSpecialChars2.Name = "cbSpecialChars2";
            this.cbSpecialChars2.Size = new System.Drawing.Size(67, 17);
            this.cbSpecialChars2.TabIndex = 15;
            this.cbSpecialChars2.Text = ":;<=>?@";
            this.cbSpecialChars2.UseVisualStyleBackColor = true;
            // 
            // cbSpecialChars3
            // 
            this.cbSpecialChars3.AutoSize = true;
            this.cbSpecialChars3.Location = new System.Drawing.Point(204, 130);
            this.cbSpecialChars3.Name = "cbSpecialChars3";
            this.cbSpecialChars3.Size = new System.Drawing.Size(66, 17);
            this.cbSpecialChars3.TabIndex = 16;
            this.cbSpecialChars3.Text = "[\\]^_{|}~";
            this.cbSpecialChars3.UseVisualStyleBackColor = true;
            // 
            // cbSpecialChars4
            // 
            this.cbSpecialChars4.AutoSize = true;
            this.cbSpecialChars4.Location = new System.Drawing.Point(276, 130);
            this.cbSpecialChars4.Name = "cbSpecialChars4";
            this.cbSpecialChars4.Size = new System.Drawing.Size(89, 17);
            this.cbSpecialChars4.TabIndex = 17;
            this.cbSpecialChars4.Text = "char 128-254";
            this.cbSpecialChars4.UseVisualStyleBackColor = true;
            // 
            // FormGifConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 219);
            this.Controls.Add(this.cbSpecialChars4);
            this.Controls.Add(this.cbSpecialChars3);
            this.Controls.Add(this.cbSpecialChars2);
            this.Controls.Add(this.cbSpecialChars1);
            this.Controls.Add(this.cbNumbers);
            this.Controls.Add(this.cbUpperCase);
            this.Controls.Add(this.cbLowerCase);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownFont);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownDivisor);
            this.Controls.Add(this.button1);
            this.Name = "FormGifConverter";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDivisor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownDivisor;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox cbLowerCase;
        private System.Windows.Forms.CheckBox cbUpperCase;
        private System.Windows.Forms.CheckBox cbNumbers;
        private System.Windows.Forms.CheckBox cbSpecialChars1;
        private System.Windows.Forms.CheckBox cbSpecialChars2;
        private System.Windows.Forms.CheckBox cbSpecialChars3;
        private System.Windows.Forms.CheckBox cbSpecialChars4;
    }
}