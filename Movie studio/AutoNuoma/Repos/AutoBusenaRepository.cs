using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class AutoBusenaRepository
    {
        public List<AutoBusena> getBusenos()
        {
            List<AutoBusena> busenos = new List<AutoBusena>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.id, a.name FROM "+Globals.dbPrefix+"auto_busenos a";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                busenos.Add(new AutoBusena
                {
                    id = Convert.ToInt32(item["id"]),
                    pavadinimas = Convert.ToString(item["name"])
                });
            }

            return busenos;
        }
    }
}