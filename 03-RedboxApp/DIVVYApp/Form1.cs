using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace DIVVYApp
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    // #####################################################
    //
    // Helper functions:
    //

    // returns local / remote connection string based on
    // user's selection:
    string getConnectionString()
    {
      if (optLocalConnection.Checked)
        return txtLocalConnection.Text;
      else
      {
        Debug.Assert(optRemoteConnection.Checked);
        return txtRemoveConnection.Text;
      }
    }

    private void ClearStationUI()
    {
      // clear the station UI:
      txtStationLatLong.Clear();
      txtStationLatLong.Refresh();
      txtStationCapacity.Clear();
      txtStationCapacity.Refresh();
      txtStationNumDocked.Clear();
      txtStationNumDocked.Refresh();
      lstStationBikes.Items.Clear();
      lstStationBikes.Refresh();
    }


    private void ClearCustomerUI()
    {
      // clear the customer UI:
      txtCustomerEmail.Clear();
      txtCustomerEmail.Refresh();
      txtCustomerDateJoined.Clear();
      txtCustomerDateJoined.Refresh();
      txtCustomerNumOut.Clear();
      txtCustomerNumOut.Refresh();
      lstCustomerBikes.Items.Clear();
      lstCustomerBikes.Refresh();
    }

    private int GetSelectedStationID()
    {
      Debug.Assert(lstStations.SelectedIndex >= 0);

      // get selected text:
      string txt = this.lstStations.SelectedItem.ToString();

      // grab the station id at the front:
      int pos = txt.IndexOf(':');
      int sid = Convert.ToInt32(txt.Substring(0, pos));

      return sid;
    }

    private int GetSelectedStationBikeID()
    {
      Debug.Assert(lstStationBikes.SelectedIndex >= 0);

      // get selected text:
      string txt = this.lstStationBikes.SelectedItem.ToString();

      // convert id and return:
      int bid = Convert.ToInt32(txt);

      return bid;
    }

    private int GetSelectedCustomerID()
    {
      Debug.Assert(lstCustomers.SelectedIndex >= 0);

      // get selected text:
      string txt = this.lstCustomers.SelectedItem.ToString();

      // grab the customer id at the front:
      int pos = txt.IndexOf(':');
      int cid = Convert.ToInt32(txt.Substring(0, pos));
      return cid;
    }

    private int GetSelectedCustomerBikeID()
    {
      Debug.Assert(lstCustomerBikes.SelectedIndex >= 0);

      // get selected text:
      string txt = this.lstCustomerBikes.SelectedItem.ToString();

      // convert id and return:
      int bid = Convert.ToInt32(txt);

      return bid;
    }

        // #####################################################
        //
        // UI event handlers:
        //

        //
        // Called automatically just before the window appears:
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionInfo = getConnectionString();

            //
            // Display list of stations and customers together to save trip to server:
            //
            SqlConnection db = new SqlConnection(connectionInfo);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
           

            try
               {
                string sql = string.Format(@"
SELECT StationID, CrossStreet1, CrossStreet2 
FROM Stations
ORDER BY StationID ASC;

--Union 2 sql strings together

SELECT CustomerID, LastName, FirstName 
FROM Customers
ORDER BY CustomerID ASC;
");
  
                db.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                cmd.CommandText = sql;
                adapter.Fill(ds);

                foreach (DataRow row in ds.Tables["TABLE"].Rows)  //or replace the ds.Tables["TABLE"] by ds.Tables[0];
                {
                    string msg1 = string.Format("{0}: {1} and {2}",
                      Convert.ToString(row["StationID"]),
                      Convert.ToString(row["CrossStreet1"]),
                      Convert.ToString(row["CrossStreet2"]));

                    this.lstStations.Items.Add(msg1);
                }

                foreach (DataRow row in ds.Tables["TABLE1"].Rows)  // or replace the ds.Tables["TABLES1"] by ds.Tables[1];
                {
                    string msg2 = string.Format("{0}: {1}, {2}",
                      Convert.ToString(row["CustomerID"]),
                      Convert.ToString(row["LastName"]),
                      Convert.ToString(row["FirstName"]));

                    this.lstCustomers.Items.Add(msg2);
                }
            }
          catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          finally
            {
                db.Close();
            }
        }
        /*
              //
              // Display list of customers:
              //
              sql = string.Format(@"
        SELECT CustomerID, LastName, FirstName 
        FROM Customers
        ORDER BY CustomerID ASC;
        ");

              ds.Clear();

              cmd.CommandText = sql;
              adapter.Fill(ds);

              foreach (DataRow row in ds.Tables["TABLE"].Rows)
              {
                string msg = string.Format("{0}: {1}, {2}",
                  Convert.ToString(row["CustomerID"]),
                  Convert.ToString(row["LastName"]),
                  Convert.ToString(row["FirstName"]));

                this.lstCustomers.Items.Add(msg);
              }
        */


        //
        // user has selected a station, display data:
        //

        private void lstStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstStations.SelectedIndex < 0)  // nothing selected:
                return;

            ClearStationUI();

            // set and access DB to get station data:
            string connectionInfo = getConnectionString();

            int sid = GetSelectedStationID();

            // 
            // retrieve data about this station, and the 
            // bikes at this station:
            // and display list of bikes docked at this station
            //
            SqlConnection db = new SqlConnection(connectionInfo);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
          
           

            try
            {
                string sql = string.Format(@"
SELECT *
FROM Stations
WHERE StationID = {0};

SELECT BikeID
FROM Bikes
WHERE StationID = {0}
ORDER BY BikeID ASC;
", sid);


                db.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                cmd.CommandText = sql;
                adapter.Fill(ds);

                Debug.Assert(ds.Tables["TABLE"].Rows.Count == 1);
                DataRow station = ds.Tables["TABLE"].Rows[0];

                txtStationLatLong.Text = string.Format("{0}, {1}",
                  Convert.ToDouble(station["Latitude"]),
                  Convert.ToDouble(station["Longitude"]));

                txtStationCapacity.Text = Convert.ToString(
                  station["Capacity"]);

                txtStationNumDocked.Text = Convert.ToString(
                  station["BikeCount"]);

                foreach (DataRow row in ds.Tables["TABLE1"].Rows)
                {
                    string msg = Convert.ToString(row["BikeID"]);
                    this.lstStationBikes.Items.Add(msg);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }
        //batch two sql queries into 1
/*
      //
      // Display list of bikes at this station:
      //
      sql = string.Format(@"
SELECT BikeID
FROM Bikes
WHERE StationID = {0}
ORDER BY BikeID ASC;
", sid);

      ds.Clear();

      cmd.CommandText = sql;
      adapter.Fill(ds);

      foreach (DataRow row in ds.Tables["TABLE"].Rows)
      {
        string msg = Convert.ToString(row["BikeID"]);
        this.lstStationBikes.Items.Add(msg);
      }

      db.Close();
    }
*/

    //
    // user has selected a customer, display data:
    //
    private void lstCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lstCustomers.SelectedIndex < 0)  // nothing selected:
        return;

      ClearCustomerUI();

      // set and access DB to get customer data:
      string connectionInfo = getConnectionString();

      int cid = GetSelectedCustomerID();

            // 
            // retrieve data about this customer, and the 
            // bikes checked out to this customer:
            //

            SqlConnection db = new SqlConnection(connectionInfo);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            try
            {
                string sql = string.Format(@"
SELECT *
FROM Customers
WHERE CustomerID = {0};

SELECT BikeID
FROM Bikes
WHERE CustomerID = {0}
ORDER BY BikeID ASC;
", cid);


                db.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                cmd.CommandText = sql;
                adapter.Fill(ds);

                Debug.Assert(ds.Tables["TABLE"].Rows.Count == 1);
                DataRow customer = ds.Tables["TABLE"].Rows[0];

                txtCustomerEmail.Text = Convert.ToString(
                  customer["Email"]);

                txtCustomerDateJoined.Text = Convert.ToDateTime(
                  customer["DateJoined"]).ToShortDateString();

                txtCustomerNumOut.Text = ds.Tables["TABLE1"].Rows.Count.ToString();

                foreach (DataRow row in ds.Tables["TABLE1"].Rows)
                {
                    string msg = Convert.ToString(row["BikeID"]);
                    this.lstCustomerBikes.Items.Add(msg);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        /*
              //
              // Display list of bikes at this station:
              //
              sql = string.Format(@"
        SELECT BikeID
        FROM Bikes
        WHERE CustomerID = {0}
        ORDER BY BikeID ASC;
        ", cid);

              ds.Clear();

              cmd.CommandText = sql;
              adapter.Fill(ds);

              txtCustomerNumOut.Text = ds.Tables["TABLE"].Rows.Count.ToString();

              foreach (DataRow row in ds.Tables["TABLE"].Rows)
              {
                string msg = Convert.ToString(row["BikeID"]);
                this.lstCustomerBikes.Items.Add(msg);
              }

              db.Close();
            }
        */


        //
        // Refresh the entire UI with data from DB:
        //
        private void cmdRefresh_Click(object sender, EventArgs e)
    {
      int selectedStation = lstStations.SelectedIndex;
      int selectedCustomer = lstCustomers.SelectedIndex;

      //
      // clear stations:
      //
      lstStations.Items.Clear();
      lstStations.Refresh();

      ClearStationUI();

      //
      // clear customers:
      //
      lstCustomers.Items.Clear();
      lstCustomers.Refresh();

      ClearCustomerUI();

      //
      // now let's reload:
      //
      Form1_Load(sender, e);

      if (selectedStation >= 0)  // a station is selected:
        lstStations.SelectedIndex = selectedStation;  // this triggers event to display:

      if (selectedCustomer >= 0)
        lstCustomers.SelectedIndex = selectedCustomer;
    }

        //
        // Customer bike checkout:
        //


        private void cmdBikeCheckout_Click(object sender, EventArgs e)
        {
            // is a customer selected?
            if (lstCustomers.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a customer...");
                return;
            }

            // is a bike selected from a station?
            if (lstStations.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a station...");
                return;
            }
            if (lstStationBikes.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a station bike...");
                return;
            }

            //
            // extract the customer id, station id, and bike id
            // from the selected items...
            //
            int cid = GetSelectedCustomerID();
            int sid = GetSelectedStationID();
            int bid = GetSelectedStationBikeID();

            //MessageBox.Show(cid.ToString() + ", " + sid.ToString() + ", " + bid.ToString());

            //
            // to checkout the bike, we need to:
            //
            //   1. update bike, setting station id to NULL and
            //        customer id to cid
            //   2. update station, reducing bike count
            //   3. insert history record denoting checkout
            //
            string connectionInfo = getConnectionString();

            DateTime curDateTime = DateTime.Now;

            SqlConnection db = new SqlConnection(connectionInfo);

            SqlCommand cmd = new SqlCommand();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            SqlTransaction tx;

            DataSet ds = new DataSet();
            
            try
            {

                string sql = string.Format(@"
UPDATE Bikes
SET    StationID = NULL,
       CustomerID = {0}
WHERE  BikeID = {1};
SELECT @@ROWCOUNT;

UPDATE  Stations
SET     BikeCount = BikeCount - 1
WHERE   StationID = {2};
SELECT @@ROWCOUNT;

INSERT INTO 
  History(CustomerID,BikeID,Checkout,StationIDout,Checkin,StationIDin)
  Values({0}, {1}, '{3}', {2}, NULL, NULL);
SELECT @@ROWCOUNT;
", cid, bid, sid, curDateTime.ToString());

                cmd.Connection = db;

                db.Open();

                tx = db.BeginTransaction();

                cmd.CommandText = sql;

                cmd.Transaction = tx;

                adapter.Fill(ds);

                int rowsModified1 = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
                if (rowsModified1 != 1)
                {
                    tx.Rollback();
                    throw new ApplicationException("Unable to update Bikes table?!");
                }

                int rowsModified2 = Convert.ToInt32(ds.Tables[1].Rows[0].ItemArray[0]);
                if (rowsModified2 != 1)
                {
                    tx.Rollback();
                    throw new ApplicationException("Unable to update Stations table?!");
                }

                int rowsModified3 = Convert.ToInt32(ds.Tables[2].Rows[0].ItemArray[0]);
                if (rowsModified3 != 1)
                {
                    tx.Rollback();
                    throw new ApplicationException("Unable to insert logs into History table?!");
                }
                /*
                      cmd.CommandText = sql2;

                      rowsModified = cmd.ExecuteNonQuery();
                      if (rowsModified != 1)
                        throw new ApplicationException("Unable to update Stations table?!");

                      cmd.CommandText = sql3;

                      rowsModified = cmd.ExecuteNonQuery();
                      if (rowsModified != 1)
                        throw new ApplicationException("Unable to insert into History table?!");
                */
                tx.Commit();
            }
          
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Close();
            }

            //
            // success, update GUI for station and customer:
            //
            int selectedStation = lstStations.SelectedIndex;

            ClearStationUI();
            lstStations.SelectedIndex = -1;  // re-select to update:
            lstStations.SelectedIndex = selectedStation;

            int selectedCustomer = lstCustomers.SelectedIndex;

            ClearCustomerUI();
            lstCustomers.SelectedIndex = -1;  // re-select to update:
            lstCustomers.SelectedIndex = selectedCustomer;
            }

    //
    // Customer bike checkin:
    //

    private void cmdBikeCheckin_Click(object sender, EventArgs e)
    {
      // is a customer selected?  one of customer's bikes?
      if (lstCustomers.SelectedIndex < 0)
      {
        MessageBox.Show("Please select a customer...");
        return;
      }
      if (lstCustomerBikes.SelectedIndex < 0)
      {
        MessageBox.Show("Please select a customer bike...");
        return;
      }

      // is a station selected?
      if (lstStations.SelectedIndex < 0)
      {
        MessageBox.Show("Please select a station...");
        return;
      }

      //
      // is there room at this station to dock the bike?
      //
      int capacity = Convert.ToInt32(txtStationCapacity.Text);
      int count = Convert.ToInt32(txtStationNumDocked.Text);

      if (count == capacity)
      {
        MessageBox.Show("Station is full, please select a different station...");
        return;
      }

      //
      // extract the customer id, station id, and bike id
      // from the selected items...
      //
      int cid = GetSelectedCustomerID();
      int sid = GetSelectedStationID();
      int bid = GetSelectedCustomerBikeID();

            // MessageBox.Show(cid.ToString() + ", " + sid.ToString() + ", " + bid.ToString());

            //
            // to checkin the bike, we need to:
            //
            //   1. update bike, setting station id to sid and
            //        customer id to NULL
            //   2. update station, increasing bike count
            //   3. update history record denoting checkin
            //
            string connectionInfo = getConnectionString();

            DateTime curDateTime = DateTime.Now;

            SqlConnection db = new SqlConnection(connectionInfo);

            SqlCommand cmd = new SqlCommand();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            SqlTransaction tx;

            DataSet ds = new DataSet();

            try
            {
                string sql1 = string.Format(@"
UPDATE Bikes
SET    StationID = {0},
       CustomerID = NULL
WHERE  BikeID = {1};
SELECT @@ROWCOUNT;

UPDATE  Stations
SET     BikeCount = BikeCount + 1
WHERE   StationID = {0};
SELECT @@ROWCOUNT;

DECLARE @hid AS INTEGER;

SELECT @hid = HistoryID
FROM   History
WHERE  CustomerID = {2} AND
       BikeID = {1} AND
       StationIDin IS NULL;

UPDATE History
SET CheckIn = '{3}', 
    StationIDin = {0}
WHERE HistoryID = @hid;
SELECT @@ROWCOUNT;
", sid, bid, cid, curDateTime.ToString());

                cmd.Connection = db;

                db.Open();

                tx = db.BeginTransaction();

                cmd.CommandText = sql1;

                cmd.Transaction = tx;

                adapter.Fill(ds);

                int rowsModified1 = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
                if (rowsModified1 != 1)
                {
                    tx.Rollback();
                    throw new ApplicationException("Unable to update Bikes table?!");
                }

                int rowsModified2 = Convert.ToInt32(ds.Tables[1].Rows[0].ItemArray[0]);
                if (rowsModified2 != 1)
                {
                    tx.Rollback();
                    throw new ApplicationException("Unable to update Stations table?!");
                }

                int rowsModified3 = ds.Tables[2].Rows.Count;
                if (rowsModified3 != 1)
                {
                    tx.Rollback();
                    throw new ApplicationException("Unable to select the matching bike's checkout information from History table?!");
                }
               /* int rowsModified4 = Convert.ToInt32(ds.Tables[3].Rows[0].ItemArray[0]);
                if (rowsModified4 != 1)
                {
                    tx.Rollback();
                    throw new ApplicationException("Unable to update History table?!");
                }
                */
                tx.Commit();

            }

            catch (ApplicationException ex1)
            {
                MessageBox.Show(ex1.Message);
            }
            catch (SqlException ex2)
            {
                MessageBox.Show(ex2.Message);
            }

            finally
            {
                db.Close();
            }
      

      //
      // success, update GUI for station and customer:
      //
      int selectedStation = lstStations.SelectedIndex;

      ClearStationUI();
      lstStations.SelectedIndex = -1;  // re-select to update:
      lstStations.SelectedIndex = selectedStation;

      int selectedCustomer = lstCustomers.SelectedIndex;

      ClearCustomerUI();
      lstCustomers.SelectedIndex = -1;  // re-select to update:
      lstCustomers.SelectedIndex = selectedCustomer;
    }

    //
    // User wants to see Customer history:
    //
    private void cmdCustomerHistory_Click(object sender, EventArgs e)
    {
      // is a customer selected?
      if (lstCustomers.SelectedIndex < 0)
      {
        MessageBox.Show("Please select a customer...");
        return;
      }

      //
      // extract customer id and display their history:
      //
      int cid = GetSelectedCustomerID();

      string connectionInfo = getConnectionString();

      //
      // Get customer's history and display:
      //
      string sql = string.Format(@"
SELECT *  
FROM   History
WHERE CustomerID = {0} AND
      Checkin IS NOT NULL
ORDER BY Checkin DESC;
", cid);

      SqlConnection db = new SqlConnection(connectionInfo);
      db.Open();

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;
      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet ds = new DataSet();

      cmd.CommandText = sql;
      adapter.Fill(ds);

      FormHistory frm = new FormHistory();
      frm.Text = "History for customer " + lstCustomers.SelectedItem.ToString();

      frm.lstHistory.Items.Add("BikeID\tCheckout\t\t\tStation\tCheckin\t\t\tStation");

      foreach (DataRow row in ds.Tables["TABLE"].Rows)
      {
        string msg = string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
          Convert.ToString(row["BikeID"]),
          Convert.ToDateTime(row["Checkout"]).ToString(),
          Convert.ToString(row["StationIDout"]),
          Convert.ToDateTime(row["Checkin"]).ToString(),
          Convert.ToString(row["StationIDin"]));

        frm.lstHistory.Items.Add(msg);
      }

      db.Close();

      //
      // show form modally so user must dismiss before
      // we continue:
      //
      frm.ShowDialog();
    }

    //
    // Reset database back to its original state:
    //
    private void cmdReset_Click(object sender, EventArgs e)
    {
      var choice = MessageBox.Show("Do you really want to reset the database by reloading the original data?", "DivvyApp", MessageBoxButtons.YesNo);
      if (choice == DialogResult.No)
        return;

      this.Cursor = Cursors.WaitCursor;

      //
      // first let's clear the UI:
      //
      lstStations.Items.Clear();
      lstStations.Refresh();

      ClearStationUI();

      lstCustomers.Items.Clear();
      lstCustomers.Refresh();

      ClearCustomerUI();

      //
      // now reload the data into the DB:
      //
      string connectionInfo = getConnectionString();

      LoadCSV.ResetData(connectionInfo);

      //
      // now reload into the GUI:
      //
      Form1_Load(sender, e);

      this.Cursor = Cursors.Default;
    }

  }//class
}//namespace
