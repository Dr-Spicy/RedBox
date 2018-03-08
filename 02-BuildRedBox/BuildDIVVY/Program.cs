//
// Console app to build a RedBox database
//
// Xiaohan Liu
// Xiaotong He
// U. of Illinois, Chicago
// CS480, Summer 2016
// Final Project
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BuildFinalProject_RedBox
{
  class Program
  {

    /// <summary>
    /// Makes a copy of an existing Microsoft SQL Server database file 
    /// and log file.  Throws an exception if an error occurs, otherwise
    /// returns normally upon successful copying.  Assumes files are in
    /// sub-folder bin\Debug or bin\Release --- i.e. same folder as .exe.
    /// </summary>
    /// <param name="from">base file name to copy from</param>
    /// <param name="to">base file name to copy to</param>
    static void CopyEmptyFile(string from, string to)
    {
      string from_file, to_file;

      //
      // copy .mdf:
      //
      from_file = from + ".mdf";
      to_file = to + ".mdf";

      if (System.IO.File.Exists(to_file))
      {
        System.IO.File.Delete(to_file);
      }

      System.IO.File.Copy(from_file, to_file);

      // 
      // now copy .ldf:
      //
      from_file = from + "_log.ldf";
      to_file = to + "_log.ldf";

      if (System.IO.File.Exists(to_file))
      {
        System.IO.File.Delete(to_file);
      }

      System.IO.File.Copy(from_file, to_file);
    }


    /// <summary>
    /// Executes the given SQL string, which should be an "action" such as 
    /// create table, drop table, insert, update, or delete.  Returns 
    /// normally if successful, throws an exception if not.
    /// </summary>
    /// <param name="sql">query to execute</param>
    static void ExecuteActionQuery(SqlConnection db, string sql)
    {
      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;
      cmd.CommandText = sql;

      cmd.ExecuteNonQuery();
    }

    static void Main(string[] args)
    {
      Console.WriteLine();
      Console.WriteLine("** Build RedBox Database Console App **");
      Console.WriteLine();

      string connectionInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\RedBox.mdf;Integrated Security=True;");
      var db = new SqlConnection(connectionInfo);

      string sql;

            try
            {
                //
                // 1. Make a copy of empty MDF file to get us started:
                //
                Console.WriteLine("Copying empty database file...");

                CopyEmptyFile("__EmptyDB", "RedBox");

                //
                // 2. Now open DB:
                //
                Console.Write("Opening database connection: ");

                db.Open();

                Console.WriteLine(db.State);

                //
                // 3. Create tables:
                //

                Console.WriteLine("Creating tables...");

                Console.WriteLine("  Access...");

                sql = string.Format(@"
CREATE TABLE Access (
   AID           INT IDENTITY(1,1) PRIMARY KEY,
   AccessType    NVARCHAR(64) NOT NULL UNIQUE
);
");

                ExecuteActionQuery(db, sql);

                Console.WriteLine("  Customers...");

                sql = string.Format(@"
CREATE TABLE Customers (
   CID          INT IDENTITY(1,1) PRIMARY KEY,
   FirstName    NVARCHAR(128) NOT NULL,         
   LastName     NVARCHAR(128) NOT NULL,
   Age          INT NOT NULL,
   JoinDate     DATE NOT NULL,
   Email        NVARCHAR(128) NOT NULL UNIQUE,
   AID          INT NOT NULL FOREIGN KEY REFERENCES Access(AID),
   Password         NVARCHAR(128) NOT NULL            
);

CREATE INDEX Customers_FIRSTNAME ON Customers(FirstName);

CREATE INDEX Customers_LASTNAME ON Customers(LastName);

CREATE INDEX Customers_Email ON Customers(Email);

CREATE INDEX Customers_AID ON Customers(AID);

");//应当限制pswd

                ExecuteActionQuery(db, sql);

                Console.WriteLine("  Stations...");

                sql = string.Format(@"
CREATE TABLE Stations (
   SID           INT IDENTITY(1,1) PRIMARY KEY,
   Capacity      INT NOT NULL CHECK (Capacity >= 0),
   DiscCount     INT DEFAULT 0 NOT NULL CHECK (DiscCount >= 0),
   Location      NVARCHAR(128) NOT NULL UNIQUE,
   CrossStreet1  NVARCHAR(64) NOT NULL,         
   CrossStreet2  NVARCHAR(64) NOT NULL,
   CONSTRAINT    DiscCount_Check CHECK (DiscCount <= Capacity)
);

CREATE INDEX Stations_Location 
ON Stations(Location);

");

                ExecuteActionQuery(db, sql);

                Console.WriteLine("  Qualities...");

                sql = string.Format(@"
CREATE TABLE Qualities (
   QID             INT IDENTITY(1,1) PRIMARY KEY,
   QualityType     NVARCHAR(64) NOT NULL UNIQUE
);

CREATE INDEX Qualities_QualityType 
ON Qualities(QualityType);
");

                ExecuteActionQuery(db, sql);

                Console.WriteLine("  Categories...");

                sql = string.Format(@"
CREATE TABLE Categories (
   CateID        INT IDENTITY(1,1) PRIMARY KEY,
   Category      NVARCHAR(64) NOT NULL UNIQUE
);

CREATE INDEX Categories_Category 
ON Categories(Category);
");

                ExecuteActionQuery(db, sql);

                Console.WriteLine("   Genres...");

                sql = string.Format(@"
CREATE TABLE Genres (
    GID     INT IDENTITY(1,1) PRIMARY KEY,
    Genre   NVARCHAR(64) NOT NULL UNIQUE
);

CREATE INDEX Genres_Genre 
ON Genres(Genre);
");

                ExecuteActionQuery(db, sql);

                Console.WriteLine("   Movies...");

                sql = string.Format(@"
CREATE TABLE Movies (
    MID         INT IDENTITY(1,1) PRIMARY KEY,
    Title       NVARCHAR(128) NOT NULL UNIQUE,
    Duration    INT NOT NULL,  --by minutes
    PubTime     DATE NOT NULL, 
    ImdbRating  FLOAT NOT NULL CHECK((0 < ImdbRating) AND (ImdbRating < 10)),        
    CateID      INT NOT NULL FOREIGN KEY REFERENCES Categories(CateID)
);

CREATE INDEX Movies_Title 
ON Movies(Title);

CREATE INDEX Movies_PubTime
ON Movies(PubTime);

CREATE INDEX Movies_ImdbRating
ON Movies(ImdbRating);

CREATE INDEX Movies_CateID
ON Movies(CateID);
 
");

                ExecuteActionQuery(db, sql);

                Console.WriteLine("   MovieGenre...");

                sql = string.Format(@"
CREATE TABLE MovieGenre (
    MID         INT NOT NULL FOREIGN KEY REFERENCES Movies(MID) ON DELETE CASCADE,
    GID         INT NOT NULL FOREIGN KEY REFERENCES Genres(GID) ON DELETE CASCADE
);

CREATE INDEX MovieGenre_MID 
ON MovieGenre(MID);

CREATE INDEX MovieGenre_GID 
ON MovieGenre(GID); 
");

                ExecuteActionQuery(db, sql);

                Console.WriteLine("  Discs...");

                sql = string.Format(@"
CREATE TABLE Discs (
   DID            INT IDENTITY(1,1) PRIMARY KEY,
   ServiceDate    DATE NOT NULL,
   MID            INT NOT NULL FOREIGN KEY REFERENCES Movies(MID),
   QID            INT NOT NULL FOREIGN KEY REFERENCES Qualities(QID),
   SID            INT DEFAULT NULL FOREIGN KEY REFERENCES Stations(SID),
   CID            INT DEFAULT NULL FOREIGN KEY REFERENCES Customers(CID),
   CONSTRAINT     DiscWhere_Check CHECK ((SID IS NULL AND CID IS NULL) OR
                                        (SID IS NOT NULL AND CID IS NULL) OR
                                        (SID is NULL AND CID IS NOT NULL))
);

CREATE INDEX Discs_MID
ON Discs(MID);

CREATE INDEX Discs_QID
ON Discs(QID);

CREATE INDEX Discs_SID
ON Discs(SID);

CREATE INDEX Discs_CID
ON Discs(CID);
");

                ExecuteActionQuery(db, sql);

                Console.WriteLine("  OutHistory...");

                sql = string.Format(@"
CREATE TABLE OutHistory(
  OHID          INT IDENTITY(2, 2) PRIMARY KEY,  --All Outhistory log ID are even numbers
  CID           INT NOT NULL FOREIGN KEY REFERENCES Customers(CID),
  DID           INT NOT NULL FOREIGN KEY REFERENCES Discs(DID),
  Checkout      DATETIME NOT NULL,
  SIDout        INT NOT NULL FOREIGN KEY REFERENCES Stations(SID)
);

CREATE INDEX OutHistory_CID
ON OutHistory(CID);

CREATE INDEX OutHistory_DID
ON OutHistory(DID);

CREATE INDEX OutHistory_Checkout
ON OutHistory(Checkout);

CREATE INDEX OutHistory_SIDout
ON OutHistory(SIDout);
");

                ExecuteActionQuery(db, sql);

                Console.WriteLine("  InHistory...");

                sql = string.Format(@"
CREATE TABLE InHistory(
  IHID          INT IDENTITY(1,2) PRIMARY KEY,   --All Inhistory log ID are odd numbers
  CID           INT NOT NULL FOREIGN KEY REFERENCES Customers(CID),
  DID           INT NOT NULL FOREIGN KEY REFERENCES Discs(DID),
  Checkin       DATETIME NOT NULL,
  SIDin         INT NOT NULL FOREIGN KEY REFERENCES Stations(SID)
);

CREATE INDEX InHistory_CID
ON InHistory(CID);

CREATE INDEX InHistory_DID
ON InHistory(DID);

CREATE INDEX InHistory_Checkin
ON InHistory(Checkin);

CREATE INDEX InHistory_SIDin
ON InHistory(SIDin);
");

                ExecuteActionQuery(db, sql);

                //
                // 4. Insert data:
                //


                Console.WriteLine("Inserting data...");

                Console.WriteLine("  Access...");
                string filename = "Access.csv";

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

                        int aid = Convert.ToInt32(values[0]);
                        string AccessType = values[1];

                        sql = string.Format(@"
INSERT INTO 
  Access(AccessType)
  Values('{0}');
",
            AccessType);

                        ExecuteActionQuery(db, sql);
                    }//while
                }//using

                Console.WriteLine("  Customers...");
                filename = "Customers.csv";

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

                        int CID = Convert.ToInt32(values[0]);
                        string FirstName = values[1];
                        string LastName = values[2];
                        int Age = Convert.ToInt32(values[3]);
                        DateTime JoinDate = Convert.ToDateTime(values[4]);
                        string Email = values[5];
                        int AID = Convert.ToInt32(values[6]);
                        string Password = values[7];


                        sql = string.Format(@"
INSERT INTO 
  Customers(FirstName, LastName, Age, JoinDate, Email, AID, Password)
  Values('{0}', '{1}', {2}, '{3}', '{4}', {5}, '{6}');
",
            FirstName, LastName, Age, JoinDate.ToShortDateString(), Email, AID, Password);

                        ExecuteActionQuery(db, sql);
                    }//while
                }//using


                Console.WriteLine("  Stations...");
                filename = "Stations.csv";

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

                        int sid = Convert.ToInt32(values[0]);
                        int capacity = Convert.ToInt32(values[1]);
                        string location = values[2];
                        string street1 = values[3];
                        string street2 = values[4];

                        sql = string.Format(@"
Insert Into
  Stations(Capacity, Location, CrossStreet1,CrossStreet2)
  Values({0},'{1}','{2}','{3}');
",
            capacity, location, street1, street2);

                        ExecuteActionQuery(db, sql);
                    }//while
                }//using


                Console.WriteLine("  Qualities...");
                filename = "Qualities.csv";

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

                        int qid = Convert.ToInt32(values[0]);
                        string qualityType = values[1];

                        sql = string.Format(@"
INSERT INTO 
  Qualities(QualityType)
  Values('{0}');
",
            qualityType);

                        ExecuteActionQuery(db, sql);
                    }//while
                }//using

                Console.WriteLine("  Categories...");
                filename = "Categories.csv";

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

                        int cateid = Convert.ToInt32(values[0]);
                        string categoryType = values[1];

                        sql = string.Format(@"
INSERT INTO 
  Categories(Category)
  Values('{0}');
",
            categoryType);

                        ExecuteActionQuery(db, sql);
                    }//while
                }//using

                Console.WriteLine("  Genres...");
                filename = "Genres.csv";

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

                        int gid = Convert.ToInt32(values[0]);
                        string genreType = values[1];

                        sql = string.Format(@"
INSERT INTO 
  Genres(Genre)
  Values('{0}');
",
            genreType);

                        ExecuteActionQuery(db, sql);
                    }//while
                }//using

                Console.WriteLine("  Movies...");
                filename = "Movies.csv";

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

                        int mid = Convert.ToInt32(values[0]);
                        string title = values[1];
                        int duration = Convert.ToInt32(values[2]);
                        DateTime pubTime = Convert.ToDateTime(values[3]);
                        double imdbRating = Convert.ToDouble(values[4]);
                        int cateid = Convert.ToInt32(values[5]);

                        sql = string.Format(@"
INSERT INTO 
  Movies(Title, Duration, PubTime, ImdbRating, CateID)
  Values('{0}', {1}, '{2}', {3}, {4});
",
title, duration, pubTime.ToShortDateString(), imdbRating, cateid);

                        ExecuteActionQuery(db, sql);
                    }//while
                }//using

                Console.WriteLine("  MovieGenre...");
                filename = "MovieGenre.csv";

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

                        int Mid = Convert.ToInt32(values[0]);
                        int Gid = Convert.ToInt32(values[1]);

                        sql = string.Format(@"
INSERT INTO 
  MovieGenre(MID, GID)
  Values({0}, {1});
",
            Mid, Gid);

                        ExecuteActionQuery(db, sql);
                    }//while
                }//using



                Console.WriteLine("  Discs...");
                filename = "Discs.csv";

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

                        int Did = Convert.ToInt32(values[0]);
                        DateTime ServicesDate = Convert.ToDateTime(values[1]);
                        int Mid = Convert.ToInt32(values[2]);
                        int Qid = Convert.ToInt32(values[3]);

                        sql = string.Format(@"
Insert Into
  Discs(ServiceDate, MID, QID)
  Values('{0}',{1},{2});
",
ServicesDate.ToShortDateString(), Mid, Qid);

                        ExecuteActionQuery(db, sql);
                    }//while
                }//using

 Console.WriteLine("  Initial Deployment...");
        filename = "InitialDeployment.csv";

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

            int dID = Convert.ToInt32(values[0]);
            int sID = Convert.ToInt32(values[1]);
            int cID = Convert.ToInt32(values[2]);
            DateTime deployedTime = Convert.ToDateTime(values[3]);

            string dateAndTime = string.Format("{0} {1}",
              deployedTime.ToShortDateString(),
              deployedTime.ToShortTimeString());

            sql = string.Format(@"
INSERT INTO 
  InHistory(CID, DID, Checkin, SIDin)
  Values({0}, {1}, '{2}', {3});
",
cID, dID, dateAndTime, sID);

            ExecuteActionQuery(db, sql);

            //
            // As we deploy discs, we have to update the DiscCount
            // for that station:
            // We also have to update the Discs table to denote where the disc is located:
            //

            sql = string.Format(@"
UPDATE  Stations
  SET   DiscCount = DiscCount + 1
  WHERE SID = {0};
UPDATE Discs
  Set   SID = {0}
  WHERE DID = {1};

",
sID, dID);

            ExecuteActionQuery(db, sql);
          }//while
        }//using





                ///////////////////////////////////////////////////////////////////////////////////

                //
                // Done
                //
     }//try
            catch (NullReferenceException ex1)
            {
                Console.WriteLine("**Null Reference Exception: '{0}'", ex1.Message);
            }
            catch (IndexOutOfRangeException ex2)
            {
                Console.WriteLine("**Index Out Of Range Exception: '{0}'", ex2.Message);
            }
            catch (SqlException ex3)
            {
                Console.WriteLine("**SQL Exception: '{0}'", ex3.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("**Exception: '{0}'", ex.Message);
            }
            finally
            {
                Console.Write("Closing database connection: ");

                db.Close();

                Console.WriteLine(db.State);
            }

      Console.WriteLine();
      Console.WriteLine("** Done **");
      Console.WriteLine();
    }//Main

  }//class
}//namespace
