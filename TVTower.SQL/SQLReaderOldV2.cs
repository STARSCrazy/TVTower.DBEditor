using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace TVTower.SQL
{
    public class SQLReaderOldV2
    {
        MySqlDataReader reader;

        public SQLReaderOldV2(MySqlDataReader reader)
        {
            this.reader = reader;
        }

        public int GetInt(string field)
        {
            return int.Parse(reader[field].ToString());
        }

        public string GetString(string field)
        {
            return reader[field].ToString();
        }

        public bool GetBool(string field)
        {
            var value = reader[field].ToString();
            if (value == "0")
                return false;
            else if (value == "1")
                return true;
            else if (value == "3") //Kompatibilität: Später weg
                return true;

            return bool.Parse(value);
        }
    }
}
