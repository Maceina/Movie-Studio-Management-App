using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.ViewModels;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class Modeliu1Repository
    {
        public List<Modelis1ViewModel> getModeliai()
        {
            List<Modelis1ViewModel> modelisViewModels = new List<Modelis1ViewModel>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.dsutartiesid, m.dshonoraras, mm.kinostudijosid AS kinostudija1 
                                FROM "+Globals.dbPrefix+@"darbosutartis m
                                LEFT JOIN "+Globals.dbPrefix+@"kinostudija mm ON mm.kinostudijosid=m.fk_KINOSTUDIJAkinostudijosid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                modelisViewModels.Add(new Modelis1ViewModel
                {
                    id = Convert.ToInt32(item["dsutartiesid"]),
                    pavadinimas = Convert.ToString(item["dshonoraras"]),
                    marke = Convert.ToString(item["kinostudija1"])
                });
            }

            return modelisViewModels;
        }

        public Modelis1EditViewModel getModelis(int id)
        {
            Modelis1EditViewModel modelis = new Modelis1EditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.* 
                                FROM "+Globals.dbPrefix+@"darbosutartis m WHERE m.dsutartiesid=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                modelis.id = Convert.ToInt32(item["dsutartiesid"]);
                modelis.pavadinimas = Convert.ToString(item["dshonoraras"]);
                modelis.fk_marke = Convert.ToInt32(item["fk_KINOSTUDIJAkinostudijosid"]);
            }

            return modelis;
        }

        public bool updateModelis(Modelis1EditViewModel modelis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE "+Globals.dbPrefix+"darbosutartis a SET a.dshonoraras=?dshonoraras, a.fk_KINOSTUDIJAkinostudijosid=?kinostudija WHERE a.dsutartiesid=?dsutartiesid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?dsutartiesid", MySqlDbType.VarChar).Value = modelis.id;
            mySqlCommand.Parameters.Add("?dshonoraras", MySqlDbType.VarChar).Value = modelis.pavadinimas;
            mySqlCommand.Parameters.Add("?kinostudija", MySqlDbType.VarChar).Value = modelis.fk_marke;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addModelis(Modelis1EditViewModel modelis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO "+Globals.dbPrefix+"darbosutartis(dsutartiesid,dshonoraras,fk_KINOSTUDIJAkinostudijosid)VALUES(?dsutartiesid,?dshonoraras,?kinostudija)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?dshonoraras", MySqlDbType.VarChar).Value = modelis.pavadinimas;
            mySqlCommand.Parameters.Add("?dsutartiesid", MySqlDbType.VarChar).Value = modelis.id;
            mySqlCommand.Parameters.Add("?kinostudija", MySqlDbType.VarChar).Value = modelis.fk_marke;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public int getModelisCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(id) as kiekis from "+Globals.dbPrefix+"automobiliai where fk_modelis=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                naudota = Convert.ToInt32(item["kiekis"] == DBNull.Value ? 0 : item["kiekis"]);
            }
            return naudota;
        }

        public void deleteModelis(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM "+Globals.dbPrefix+"darbosutartis where dsutartiesid=?dsutartiesid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?dsutartiesid", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }


    }
}