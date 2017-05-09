using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finisar.SQLite;

namespace Wix_Studio.Card_Parser
{
    class SQLHelper
    {
        private SQLiteConnection sqlConnection;
        private SQLiteCommand sqlCommand;
        private SQLiteDataAdapter dataAdapter;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        public SQLHelper()
        {
            
        }

        private void SetConnection()
        {
            sqlConnection = new SQLiteConnection("Data Source=DemoT.db;Version=3;New=False;Compress=True;");
        }

        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sqlConnection.Open();
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = txtQuery;
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
