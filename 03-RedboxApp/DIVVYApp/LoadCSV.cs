
//// Multi-user GUI app to simulate DIVVY bike operations.  Uses exception handling,
// transactions, and batch SQL to be more efficient and tolerant of multi-user,
// real-world usage.
//
// Xiaohan Liu
// U. of Illinois, Chicago
// CS480, Summer 2016
// HW #5
//

using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace DIVVYApp
{
  class LoadCSV
  {
    /// <summary>
    /// Executes the given SQL string, which should be an "action" such as 
    /// create table, drop table, insert, update, or delete.  Returns 
    /// normally if successful, throws an exception if not.
    /// </summary>
    /// <param name="sql">query to execute</param>
    private static void ExecuteActionQuery(SqlConnection db, string sql, bool checkResult = true)
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;
      cmd.CommandText = sql;

      int rowsModified = cmd.ExecuteNonQuery();

      if (checkResult && rowsModified != 1)
      {
        string msg = string.Format("ExecuteActionQuery failed on '{0}'...",
          sql);
        throw new ApplicationException(msg);
      }
    }

    public static void ResetData(string connectionInfo)
    {
      var db = new SqlConnection(connectionInfo);
      db.Open();

      //
      // First, delete all data from the existing tables:
      //
      string sql = "DELETE FROM History;DELETE FROM Bikes;DELETE FROM Customers;DELETE FROM Stations;DBCC CHECKIDENT ('Customers', RESEED, 2000);DBCC CHECKIDENT ('Stations', RESEED, 1000);DBCC CHECKIDENT ('Bikes', RESEED, 0);DBCC CHECKIDENT ('History', RESEED, 0);";
      ExecuteActionQuery(db, sql, false);
      //###############################
      //We should batch all these sql commands to save round-trips to server 
      //###########################
/*
      sql = "DELETE FROM Bikes;";
      ExecuteActionQuery(db, sql, false);

      sql = "DELETE FROM Customers;";
      ExecuteActionQuery(db, sql, false);

      sql = "DELETE FROM Stations;";
      ExecuteActionQuery(db, sql, false);

      sql = "DBCC CHECKIDENT ('Customers', RESEED, 2000);";
      ExecuteActionQuery(db, sql, false);

      sql = "DBCC CHECKIDENT ('Stations', RESEED, 1000);";
      ExecuteActionQuery(db, sql, false);

      sql = "DBCC CHECKIDENT ('Bikes', RESEED, 0);";
      ExecuteActionQuery(db, sql, false);

      sql = "DBCC CHECKIDENT ('History', RESEED, 0);";
      ExecuteActionQuery(db, sql, false);
      */
      //
      // Now reload all the data from the original 
      // .CSV files:
      //
      try
      {
        string filename = System.IO.Path.Combine("csv", "Customers.csv");

        using (var file = new System.IO.StreamReader(filename))
        {
          bool firstline = true;

          while (!file.EndOfStream)
          {
            string line = file.ReadLine();

            if (firstline)  // skip first line (header row):
            {
              firstline = false;
              continue;
            }

            string[] values = line.Split(',');

            int id = Convert.ToInt32(values[0]);
            string firstName = values[1];
            string lastName = values[2];
            string email = values[3];
            DateTime dateJoined = Convert.ToDateTime(values[4]);

            sql = string.Format(@"
INSERT INTO 
  Customers(FirstName, LastName, Email, DateJoined)
  Values('{0}', '{1}', '{2}', '{3}');
",
firstName, lastName, email, dateJoined.ToShortDateString());

            ExecuteActionQuery(db, sql);
          }//while
        }//using

        filename = System.IO.Path.Combine("csv", "Stations.csv");

        using (var file = new System.IO.StreamReader(filename))
        {
          bool firstline = true;

          while (!file.EndOfStream)
          {
            string line = file.ReadLine();

            if (firstline)  // skip first line (header row):
            {
              firstline = false;
              continue;
            }

            string[] values = line.Split(',');

            int stationid = Convert.ToInt32(values[0]);
            int capacity = Convert.ToInt32(values[1]);
            string street1 = values[2];
            string street2 = values[3];
            double latitude = Convert.ToDouble(values[4]);
            double longitude = Convert.ToDouble(values[5]);

            sql = string.Format(@"
Insert Into
  Stations(Capacity,CrossStreet1,CrossStreet2,Latitude,Longitude)
  Values({0},'{1}','{2}',{3},{4});
",
capacity, street1, street2, latitude, longitude);

            ExecuteActionQuery(db, sql);
          }//while
        }//using

        filename = System.IO.Path.Combine("csv", "Bikes.csv");

        using (var file = new System.IO.StreamReader(filename))
        {
          bool firstline = true;

          while (!file.EndOfStream)
          {
            string line = file.ReadLine();

            if (firstline)  // skip first line (header row):
            {
              firstline = false;
              continue;
            }

            string[] values = line.Split(',');

            int id = Convert.ToInt32(values[0]);
            int modelNum = Convert.ToInt32(values[1]);
            DateTime dateInService = Convert.ToDateTime(values[2]);

            sql = string.Format(@"
INSERT INTO 
  Bikes(ModelNumber, DateInService)
  Values({0}, '{1}');
",
modelNum, dateInService);

            ExecuteActionQuery(db, sql);
          }//while
        }//using

        filename = System.IO.Path.Combine("csv", "InitialDeployment.csv");

        using (var file = new System.IO.StreamReader(filename))
        {
          bool firstline = true;

          while (!file.EndOfStream)
          {
            string line = file.ReadLine();

            if (firstline)  // skip first line (header row):
            {
              firstline = false;
              continue;
            }

            string[] values = line.Split(',');

            int bikeID = Convert.ToInt32(values[0]);
            int stationID = Convert.ToInt32(values[1]);
            int customerID = Convert.ToInt32(values[2]);
            DateTime checkin = Convert.ToDateTime(values[3]);

            string dateAndTime = string.Format("{0} {1}",
              checkin.ToShortDateString(),
              checkin.ToShortTimeString());

            sql = string.Format(@"
INSERT INTO 
  History(CustomerID,BikeID,Checkout,StationIDout,Checkin,StationIDin)
  Values({0}, {1}, '{2}', {3}, '{4}', {5});
",
customerID, bikeID, dateAndTime, stationID, dateAndTime, stationID);

            //
            // As we deploy bikes, we have to update the BikeCount
            // for that station:
            //
            sql = string.Format(@"
UPDATE  Stations
  SET   BikeCount = BikeCount + 1
  WHERE StationID = {0};
",
stationID);

            ExecuteActionQuery(db, sql);

            //
            // we also have to update the Bikes table to denote 
            // where bike is docked:
            //
            sql = string.Format(@"
UPDATE  Bikes
  SET   StationID = {0}
  WHERE BikeID = {1};
",
stationID, bikeID);

            ExecuteActionQuery(db, sql);
          }//while
        }//using

        filename = System.IO.Path.Combine("csv", "History.csv");

        using (var file = new System.IO.StreamReader(filename))
        {
          bool firstline = true;

          while (!file.EndOfStream)
          {
            string line = file.ReadLine();

            if (firstline)  // skip first line (header row):
            {
              firstline = false;
              continue;
            }

            string[] values = line.Split(',');

            int bikeID = Convert.ToInt32(values[0]);
            int customerID = Convert.ToInt32(values[1]);
            int stationIDout = Convert.ToInt32(values[2]);
            string checkout = values[3];  // in date/time format
            int stationIDin = Convert.ToInt32(values[4]);
            string checkin = values[5];  // in date/time format

            sql = string.Format(@"
INSERT INTO 
  History(CustomerID,BikeID,Checkout,StationIDout,Checkin,StationIDin)
  Values({0}, {1}, '{2}', {3}, '{4}', {5});
",
customerID, bikeID, checkout, stationIDout, checkin, stationIDin);

            ExecuteActionQuery(db, sql);

            //
            // As we deploy bikes, we have to update the BikeCount
            // for the 2 stations involved:
            //
            sql = string.Format(@"
UPDATE  Stations
  SET   BikeCount = BikeCount - 1
  WHERE StationID = {0};
",
stationIDout);

            ExecuteActionQuery(db, sql);

            sql = string.Format(@"
UPDATE  Stations
  SET   BikeCount = BikeCount + 1
  WHERE StationID = {0};
",
stationIDin);

            ExecuteActionQuery(db, sql);

            // 
            // we have to likewise update Bikes table as to 
            // where bike is:
            //
            sql = string.Format(@"
UPDATE  Bikes
  SET   StationID = {0}
  WHERE BikeID = {1};
",
stationIDin, bikeID);

            ExecuteActionQuery(db, sql);
          }//while
        }//using

        //
        // Done
        //
      }
      catch (Exception ex)
      {
        string msg = string.Format("Error in LoadCSV.ResetData: '{0}'", ex.Message);

        MessageBox.Show(msg);
      }
      finally
      {
        db.Close();
      }
    }

  }//class
}//namespace
