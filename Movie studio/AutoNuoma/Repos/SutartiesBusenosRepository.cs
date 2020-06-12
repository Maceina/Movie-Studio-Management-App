using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class SutartiesBusenosRepository
    {
        public List<SutartiesBusena> getSutartiesBusenos()
        {
            List<SutartiesBusena> busenos = new List<SutartiesBusena>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"sutarties_busenos";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                busenos.Add(new SutartiesBusena
                {
                    id = Convert.ToInt32(item["id"]),
                    name = Convert.ToString(item["name"])
                });
            }
            return busenos;
        }
    }
}