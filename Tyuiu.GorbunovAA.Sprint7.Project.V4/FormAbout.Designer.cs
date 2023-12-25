
namespace Tyuiu.GorbunovAA.Sprint7.Project.V4
{
    partial class FormAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.labelAbout_GAA = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelAbout_GAA
            // 
            this.labelAbout_GAA.AutoSize = true;
            this.labelAbout_GAA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAbout_GAA.Location = new System.Drawing.Point(12, 9);
            this.labelAbout_GAA.Name = "labelAbout_GAA";
            this.labelAbout_GAA.Size = new System.Drawing.Size(616, 105);
            this.labelAbout_GAA.TabIndex = 0;
            this.labelAbout_GAA.Text = resources.GetString("labelAbout_GAA.Text");
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 132);
            this.Controls.Add(this.labelAbout_GAA);
            this.Name = "FormAbout";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "О программе";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAbout_GAA;
    }
}