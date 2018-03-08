namespace DIVVYApp
{
  partial class FormHistory
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
      this.lstHistory = new System.Windows.Forms.ListBox();
      this.SuspendLayout();
      // 
      // lstHistory
      // 
      this.lstHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lstHistory.FormattingEnabled = true;
      this.lstHistory.ItemHeight = 20;
      this.lstHistory.Location = new System.Drawing.Point(31, 27);
      this.lstHistory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.lstHistory.Name = "lstHistory";
      this.lstHistory.Size = new System.Drawing.Size(767, 404);
      this.lstHistory.TabIndex = 0;
      // 
      // FormHistory
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Teal;
      this.ClientSize = new System.Drawing.Size(835, 491);
      this.Controls.Add(this.lstHistory);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "FormHistory";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "FormHistory";
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.ListBox lstHistory;
  }
}