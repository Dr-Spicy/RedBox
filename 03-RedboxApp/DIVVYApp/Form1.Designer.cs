namespace DIVVYApp
{
  partial class Form1
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.txtRemoveConnection = new System.Windows.Forms.TextBox();
      this.txtLocalConnection = new System.Windows.Forms.TextBox();
      this.optRemoteConnection = new System.Windows.Forms.RadioButton();
      this.optLocalConnection = new System.Windows.Forms.RadioButton();
      this.lstStations = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lstCustomers = new System.Windows.Forms.ListBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtStationLatLong = new System.Windows.Forms.TextBox();
      this.txtStationCapacity = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtStationNumDocked = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.lstStationBikes = new System.Windows.Forms.ListBox();
      this.txtCustomerEmail = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.txtCustomerDateJoined = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.lstCustomerBikes = new System.Windows.Forms.ListBox();
      this.label9 = new System.Windows.Forms.Label();
      this.txtCustomerNumOut = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.cmdRefresh = new System.Windows.Forms.Button();
      this.cmdBikeCheckout = new System.Windows.Forms.Button();
      this.cmdBikeCheckin = new System.Windows.Forms.Button();
      this.cmdCustomerHistory = new System.Windows.Forms.Button();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.cmdReset = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.Aquamarine;
      this.panel1.Controls.Add(this.txtRemoveConnection);
      this.panel1.Controls.Add(this.txtLocalConnection);
      this.panel1.Controls.Add(this.optRemoteConnection);
      this.panel1.Controls.Add(this.optLocalConnection);
      this.panel1.Location = new System.Drawing.Point(22, 555);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(912, 80);
      this.panel1.TabIndex = 0;
      // 
      // txtRemoveConnection
      // 
      this.txtRemoveConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtRemoveConnection.Location = new System.Drawing.Point(123, 47);
      this.txtRemoveConnection.Name = "txtRemoveConnection";
      this.txtRemoveConnection.Size = new System.Drawing.Size(767, 22);
      this.txtRemoveConnection.TabIndex = 3;
      // 
      // txtLocalConnection
      // 
      this.txtLocalConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtLocalConnection.Location = new System.Drawing.Point(123, 12);
      this.txtLocalConnection.Name = "txtLocalConnection";
      this.txtLocalConnection.Size = new System.Drawing.Size(767, 22);
      this.txtLocalConnection.TabIndex = 2;
      this.txtLocalConnection.Text = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\DIVVY.mdf;Int" +
    "egrated Security=True;";
      // 
      // optRemoteConnection
      // 
      this.optRemoteConnection.AutoSize = true;
      this.optRemoteConnection.Location = new System.Drawing.Point(23, 45);
      this.optRemoteConnection.Name = "optRemoteConnection";
      this.optRemoteConnection.Size = new System.Drawing.Size(84, 24);
      this.optRemoteConnection.TabIndex = 1;
      this.optRemoteConnection.Text = "Remote";
      this.optRemoteConnection.UseVisualStyleBackColor = true;
      // 
      // optLocalConnection
      // 
      this.optLocalConnection.AutoSize = true;
      this.optLocalConnection.Checked = true;
      this.optLocalConnection.Location = new System.Drawing.Point(23, 10);
      this.optLocalConnection.Name = "optLocalConnection";
      this.optLocalConnection.Size = new System.Drawing.Size(65, 24);
      this.optLocalConnection.TabIndex = 0;
      this.optLocalConnection.TabStop = true;
      this.optLocalConnection.Text = "Local";
      this.optLocalConnection.UseVisualStyleBackColor = true;
      // 
      // lstStations
      // 
      this.lstStations.FormattingEnabled = true;
      this.lstStations.ItemHeight = 20;
      this.lstStations.Location = new System.Drawing.Point(22, 51);
      this.lstStations.Name = "lstStations";
      this.lstStations.Size = new System.Drawing.Size(332, 264);
      this.lstStations.TabIndex = 1;
      this.lstStations.SelectedIndexChanged += new System.EventHandler(this.lstStations_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(140, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(98, 25);
      this.label1.TabIndex = 2;
      this.label1.Text = "Stations";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(533, 17);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(124, 25);
      this.label2.TabIndex = 4;
      this.label2.Text = "Customers";
      // 
      // lstCustomers
      // 
      this.lstCustomers.FormattingEnabled = true;
      this.lstCustomers.ItemHeight = 20;
      this.lstCustomers.Location = new System.Drawing.Point(427, 51);
      this.lstCustomers.Name = "lstCustomers";
      this.lstCustomers.Size = new System.Drawing.Size(332, 264);
      this.lstCustomers.TabIndex = 3;
      this.lstCustomers.SelectedIndexChanged += new System.EventHandler(this.lstCustomers_SelectedIndexChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(29, 334);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(64, 20);
      this.label3.TabIndex = 5;
      this.label3.Text = "lat/long:";
      // 
      // txtStationLatLong
      // 
      this.txtStationLatLong.Location = new System.Drawing.Point(104, 331);
      this.txtStationLatLong.Name = "txtStationLatLong";
      this.txtStationLatLong.ReadOnly = true;
      this.txtStationLatLong.Size = new System.Drawing.Size(250, 26);
      this.txtStationLatLong.TabIndex = 6;
      // 
      // txtStationCapacity
      // 
      this.txtStationCapacity.Location = new System.Drawing.Point(104, 365);
      this.txtStationCapacity.Name = "txtStationCapacity";
      this.txtStationCapacity.ReadOnly = true;
      this.txtStationCapacity.Size = new System.Drawing.Size(120, 26);
      this.txtStationCapacity.TabIndex = 8;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(29, 368);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(71, 20);
      this.label4.TabIndex = 7;
      this.label4.Text = "capacity:";
      // 
      // txtStationNumDocked
      // 
      this.txtStationNumDocked.Location = new System.Drawing.Point(104, 400);
      this.txtStationNumDocked.Name = "txtStationNumDocked";
      this.txtStationNumDocked.ReadOnly = true;
      this.txtStationNumDocked.Size = new System.Drawing.Size(120, 26);
      this.txtStationNumDocked.TabIndex = 10;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(29, 403);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(65, 20);
      this.label5.TabIndex = 9;
      this.label5.Text = "docked:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(29, 436);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(50, 20);
      this.label6.TabIndex = 11;
      this.label6.Text = "bikes:";
      // 
      // lstStationBikes
      // 
      this.lstStationBikes.FormattingEnabled = true;
      this.lstStationBikes.ItemHeight = 20;
      this.lstStationBikes.Location = new System.Drawing.Point(104, 436);
      this.lstStationBikes.Name = "lstStationBikes";
      this.lstStationBikes.Size = new System.Drawing.Size(120, 104);
      this.lstStationBikes.TabIndex = 12;
      // 
      // txtCustomerEmail
      // 
      this.txtCustomerEmail.Location = new System.Drawing.Point(509, 331);
      this.txtCustomerEmail.Name = "txtCustomerEmail";
      this.txtCustomerEmail.ReadOnly = true;
      this.txtCustomerEmail.Size = new System.Drawing.Size(250, 26);
      this.txtCustomerEmail.TabIndex = 14;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(434, 334);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(50, 20);
      this.label7.TabIndex = 13;
      this.label7.Text = "email:";
      // 
      // txtCustomerDateJoined
      // 
      this.txtCustomerDateJoined.Location = new System.Drawing.Point(509, 365);
      this.txtCustomerDateJoined.Name = "txtCustomerDateJoined";
      this.txtCustomerDateJoined.ReadOnly = true;
      this.txtCustomerDateJoined.Size = new System.Drawing.Size(120, 26);
      this.txtCustomerDateJoined.TabIndex = 16;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(434, 368);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(55, 20);
      this.label8.TabIndex = 15;
      this.label8.Text = "joined:";
      // 
      // lstCustomerBikes
      // 
      this.lstCustomerBikes.FormattingEnabled = true;
      this.lstCustomerBikes.ItemHeight = 20;
      this.lstCustomerBikes.Location = new System.Drawing.Point(509, 436);
      this.lstCustomerBikes.Name = "lstCustomerBikes";
      this.lstCustomerBikes.Size = new System.Drawing.Size(120, 104);
      this.lstCustomerBikes.TabIndex = 18;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(434, 436);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(50, 20);
      this.label9.TabIndex = 17;
      this.label9.Text = "bikes:";
      // 
      // txtCustomerNumOut
      // 
      this.txtCustomerNumOut.Location = new System.Drawing.Point(509, 400);
      this.txtCustomerNumOut.Name = "txtCustomerNumOut";
      this.txtCustomerNumOut.ReadOnly = true;
      this.txtCustomerNumOut.Size = new System.Drawing.Size(120, 26);
      this.txtCustomerNumOut.TabIndex = 20;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(434, 403);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(71, 20);
      this.label10.TabIndex = 19;
      this.label10.Text = "num out:";
      // 
      // cmdRefresh
      // 
      this.cmdRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdRefresh.Location = new System.Drawing.Point(810, 215);
      this.cmdRefresh.Name = "cmdRefresh";
      this.cmdRefresh.Size = new System.Drawing.Size(121, 35);
      this.cmdRefresh.TabIndex = 21;
      this.cmdRefresh.Text = "Refresh";
      this.cmdRefresh.UseVisualStyleBackColor = true;
      this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
      // 
      // cmdBikeCheckout
      // 
      this.cmdBikeCheckout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdBikeCheckout.Location = new System.Drawing.Point(810, 112);
      this.cmdBikeCheckout.Name = "cmdBikeCheckout";
      this.cmdBikeCheckout.Size = new System.Drawing.Size(121, 35);
      this.cmdBikeCheckout.TabIndex = 22;
      this.cmdBikeCheckout.Text = "Checkout...";
      this.cmdBikeCheckout.UseVisualStyleBackColor = true;
      this.cmdBikeCheckout.Click += new System.EventHandler(this.cmdBikeCheckout_Click);
      // 
      // cmdBikeCheckin
      // 
      this.cmdBikeCheckin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdBikeCheckin.Location = new System.Drawing.Point(810, 153);
      this.cmdBikeCheckin.Name = "cmdBikeCheckin";
      this.cmdBikeCheckin.Size = new System.Drawing.Size(121, 35);
      this.cmdBikeCheckin.TabIndex = 23;
      this.cmdBikeCheckin.Text = "Checkin...";
      this.cmdBikeCheckin.UseVisualStyleBackColor = true;
      this.cmdBikeCheckin.Click += new System.EventHandler(this.cmdBikeCheckin_Click);
      // 
      // cmdCustomerHistory
      // 
      this.cmdCustomerHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdCustomerHistory.Location = new System.Drawing.Point(810, 51);
      this.cmdCustomerHistory.Name = "cmdCustomerHistory";
      this.cmdCustomerHistory.Size = new System.Drawing.Size(121, 35);
      this.cmdCustomerHistory.TabIndex = 24;
      this.cmdCustomerHistory.Text = "History";
      this.cmdCustomerHistory.UseVisualStyleBackColor = true;
      this.cmdCustomerHistory.Click += new System.EventHandler(this.cmdCustomerHistory_Click);
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::DIVVYApp.Properties.Resources.divvy;
      this.pictureBox1.Location = new System.Drawing.Point(700, 391);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(231, 132);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 25;
      this.pictureBox1.TabStop = false;
      // 
      // cmdReset
      // 
      this.cmdReset.BackColor = System.Drawing.Color.Red;
      this.cmdReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdReset.ForeColor = System.Drawing.Color.White;
      this.cmdReset.Location = new System.Drawing.Point(810, 280);
      this.cmdReset.Name = "cmdReset";
      this.cmdReset.Size = new System.Drawing.Size(121, 35);
      this.cmdReset.TabIndex = 26;
      this.cmdReset.Text = "Reset";
      this.cmdReset.UseVisualStyleBackColor = false;
      this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Yellow;
      this.ClientSize = new System.Drawing.Size(962, 647);
      this.Controls.Add(this.cmdReset);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.cmdCustomerHistory);
      this.Controls.Add(this.cmdBikeCheckin);
      this.Controls.Add(this.cmdBikeCheckout);
      this.Controls.Add(this.cmdRefresh);
      this.Controls.Add(this.txtCustomerNumOut);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.lstCustomerBikes);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.txtCustomerDateJoined);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.txtCustomerEmail);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.lstStationBikes);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.txtStationNumDocked);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtStationCapacity);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtStationLatLong);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lstCustomers);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lstStations);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "Form1";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "DIVVY App (Prof Joe Hummel, CS 480, Summer 2016)";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox txtRemoveConnection;
    private System.Windows.Forms.TextBox txtLocalConnection;
    private System.Windows.Forms.RadioButton optRemoteConnection;
    private System.Windows.Forms.RadioButton optLocalConnection;
    private System.Windows.Forms.ListBox lstStations;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListBox lstCustomers;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtStationLatLong;
    private System.Windows.Forms.TextBox txtStationCapacity;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtStationNumDocked;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ListBox lstStationBikes;
    private System.Windows.Forms.TextBox txtCustomerEmail;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtCustomerDateJoined;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ListBox lstCustomerBikes;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtCustomerNumOut;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Button cmdRefresh;
    private System.Windows.Forms.Button cmdBikeCheckout;
    private System.Windows.Forms.Button cmdBikeCheckin;
    private System.Windows.Forms.Button cmdCustomerHistory;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button cmdReset;
  }
}

