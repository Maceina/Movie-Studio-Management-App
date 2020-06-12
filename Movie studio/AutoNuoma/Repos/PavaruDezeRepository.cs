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
    public class PavaruDezeRepository
    {
        public List<PavaruDeze> getPavaruDezes()
        {
            List<PavaruDeze> pavaruDezes = new List<PavaruDeze>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.id, a.name FROM "+Globals.dbPrefix+"pavaru_dezes a";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                pavaruDezes.Add(new PavaruDeze
                {
                    id = Convert.ToInt32(item["id"]),
                    pavadinimas = Convert.ToString(item["name"])
                });
            }

            return pavaruDezes;
        }
    }
}