Steps to getting DIVVY app up and running:

1. startup Visual Studio, view Server Explorer, add a connection to the "__EmptyDB" in 01-Empty folder, and click OK --- if asked to update the database, do so.  Delete the connection and exit visual studio.

2. Drill down into 01-Empty folder, select the 2 database files, and copy (Ctrl-C).

3. Drill down into 02-BuildDIVVY folder, then BuildDIVVY, and then down into bin\Debug.  Paste the 2 database files (Ctrl-V).

4. Open The 02-BuildDIVVY folder, and double-click on the Solution (.sln) file to open BuildDIVVY in Visual Studio.  Run (Ctrl+F5).  When it's done, exit Visual Studio.

5. Drill down into 02-BuildDIVVY folder, and down into bin\Debug.  Select the 2 DIVVY database files, and copy (Ctrl+C).

6. Drill down into 03-DIVVYApp folder, then DIVVYApp, and then down into bin\Debug.  Paste the 2 database files (Ctrl+V).

7. Drill down into 03-DIVVYApp folder, and double-click on the Solution (.sln) file to open DIVVYApp in Visual Studio.  Run (F5) and all should be well.

######################
