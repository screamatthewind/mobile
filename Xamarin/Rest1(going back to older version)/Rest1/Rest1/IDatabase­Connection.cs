using System;
using System.Collections.Generic;
using System.Text;

namespace Rest1
{
    public interface IDatabaseConnection
    {
      SQLite.SQLiteConnection DbConnection();
    }
}
