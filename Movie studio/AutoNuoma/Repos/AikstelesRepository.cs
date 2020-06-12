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
    public class AikstelesRepository
    {
        public List<Aikstele> getAiksteles()
        {
            List<Aikstele> aiksteles = new List<Aikstele>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"aiksteles";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                aiksteles.Add(new Aikstele
                {
                    id = Convert.ToInt32(item["id"]),
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    adresas = Convert.ToString(item["adresas"]),
                    fk_miestas = Convert.ToInt32(item["fk_miestas"])
                });
            }
            return aiksteles;
        }

        public List<Aikstele> getMiestoAiksteles(int miestas)
        {
            List<Aikstele> aiksteles = new List<Aikstele>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"aiksteles where fk_miestas="+miestas;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                aiksteles.Add(new Aikstele
                {
                    id = Convert.ToInt32(item["id"]),
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    adresas = Convert.ToString(item["adresas"]),
                    fk_miestas = Convert.ToInt32(item["fk_miestas"])
                });
            }
            return aiksteles;
        }
    }
}