namespace SingleCopyAppTest
{
    partial class MainForm
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
            this.btnOpenAnotherForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenAnotherForm
            // 
            this.btnOpenAnotherForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenAnotherForm.Location = new System.Drawing.Point(302, 191);
            this.btnOpenAnotherForm.Name = "btnOpenAnotherForm";
            this.btnOpenAnotherForm.Size = new System.Drawing.Size(199, 66);
            this.btnOpenAnotherForm.TabIndex = 0;
            this.btnOpenAnotherForm.Text = "Open another form";
            this.btnOpenAnotherForm.UseVisualStyleBackColor = true;
            this.btnOpenAnotherForm.Click += new System.EventHandler(this.btnOpenAnotherForm_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenAnotherForm);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenAnotherForm;
    }
}

