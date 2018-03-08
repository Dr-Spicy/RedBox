
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIVVYApp
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());
    }
  }
}
