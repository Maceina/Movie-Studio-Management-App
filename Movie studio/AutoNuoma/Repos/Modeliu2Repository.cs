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
    public class Modeliu2Repository
    {
        public List<Modelis2ViewModel> getModeliai()
        {
            List<Modelis2ViewModel> modelisViewModels = new List<Modelis2ViewModel>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.asutartiesid, m.ashonoraras, mm.kinostudijosid AS kinostudija1 
                                FROM "+Globals.dbPrefix+@"aktorystessutartis m
                                LEFT JOIN "+Globals.dbPrefix+@"kinostudija mm ON mm.kinostudijosid=m.fk_KINOSTUDIJAkinostudijosid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                modelisViewModels.Add(new Modelis2ViewModel
                {
                    id = Convert.ToInt32(item["asutartiesid"]),
                    pavadinimas = Convert.ToString(item["ashonoraras"]),
                    marke = Convert.ToString(item["kinostudija1"])
                });
            }

            return modelisViewModels;
        }

        public Modelis2EditViewModel getModelis(int id)
        {
            Modelis2EditViewModel modelis = new Modelis2EditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.* 
                                FROM "+Globals.dbPrefix+@"aktorystessutartis m WHERE m.asutartiesid=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                modelis.id = Convert.ToInt32(item["asutartiesid"]);
                modelis.pavadinimas = Convert.ToString(item["ashonoraras"]);
                modelis.fk_marke = Convert.ToInt32(item["fk_KINOSTUDIJAkinostudijosid"]);
            }

            return modelis;
        }

        public bool updateModelis(Modelis2EditViewModel modelis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE "+Globals.dbPrefix+"aktorystessutartis a SET a.ashonoraras=?ashonoraras, a.fk_KINOSTUDIJAkinostudijosid=?kinostudija WHERE a.asutartiesid=?asutartiesid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?asutartiesid", MySqlDbType.VarChar).Value = modelis.id;
            mySqlCommand.Parameters.Add("?ashonoraras", MySqlDbType.VarChar).Value = modelis.pavadinimas;
            mySqlCommand.Parameters.Add("?kinostudija", MySqlDbType.VarChar).Value = modelis.fk_marke;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addModelis(Modelis2EditViewModel modelis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO "+Globals.dbPrefix+"aktorystessutartis(asutartiesid,ashonoraras,fk_KINOSTUDIJAkinostudijosid)VALUES(?asutartiesid,?ashonoraras,?kinostudija)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?ashonoraras", MySqlDbType.VarChar).Value = modelis.pavadinimas;
            mySqlCommand.Parameters.Add("?asutartiesid", MySqlDbType.VarChar).Value = modelis.id;
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
            string sqlquery = @"DELETE FROM "+Globals.dbPrefix+"aktorystessutartis where asutartiesid=?asutartiesid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?asutartiesid", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }


    }
}