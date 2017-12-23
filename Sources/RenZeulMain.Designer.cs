namespace renzeulware
{
    partial class RenZeulMain
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
            this.VisualProgress = new System.Windows.Forms.ProgressBar();
            this.Progress = new System.Windows.Forms.Label();
            this.Level = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // VisualProgress
            // 
            this.VisualProgress.Location = new System.Drawing.Point(12, 12);
            this.VisualProgress.Maximum = 10000000;
            this.VisualProgress.Name = "VisualProgress";
            this.VisualProgress.Size = new System.Drawing.Size(420, 23);
            this.VisualProgress.TabIndex = 0;
            // 
            // Progress
            // 
            this.Progress.BackColor = System.Drawing.Color.Transparent;
            this.Progress.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Progress.ForeColor = System.Drawing.Color.White;
            this.Progress.Location = new System.Drawing.Point(254, 38);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(178, 23);
            this.Progress.TabIndex = 1;
            this.Progress.Text = "? / 100000000";
            this.Progress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Level
            // 
            this.Level.BackColor = System.Drawing.Color.Transparent;
            this.Level.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Level.ForeColor = System.Drawing.Color.Red;
            this.Level.Location = new System.Drawing.Point(9, 395);
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(178, 23);
            this.Level.TabIndex = 2;
            this.Level.Text = "련선 켜주세려 @~@";
            this.Level.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RenZeulMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::renzeulware.Properties.Resources.th12;
            this.ClientSize = new System.Drawing.Size(444, 421);
            this.Controls.Add(this.Level);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.VisualProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenZeulMain";
            this.Text = "즐거운 련선 도우미";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar VisualProgress;
        private System.Windows.Forms.Label Progress;
        private System.Windows.Forms.Label Level;
    }
}

