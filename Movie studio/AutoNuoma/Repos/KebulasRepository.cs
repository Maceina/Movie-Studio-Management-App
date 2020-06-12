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
    public class KebulasRepository
    {
        public List<KebuloTipas> getKebuloTipai()
        {
            List<KebuloTipas> kebuloTipai = new List<KebuloTipas>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.id, a.name FROM "+Globals.dbPrefix+"kebulu_tipai a";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                kebuloTipai.Add(new KebuloTipas
                {
                    id = Convert.ToInt32(item["id"]),
                    pavadinimas = Convert.ToString(item["name"])
                });
            }

            return kebuloTipai;
        }
    }
}