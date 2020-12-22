namespace Questify.Builder.Plugins.MathML.Default
{
    partial class MathMlEditor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.txtbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            this.txtbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbox.Location = new System.Drawing.Point(0, 0);
            this.txtbox.Multiline = true;
            this.txtbox.Name = "txtbox";
            this.txtbox.Size = new System.Drawing.Size(150, 150);
            this.txtbox.TabIndex = 0;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtbox);
            this.Name = "MathMlEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.TextBox txtbox;
    }
}
