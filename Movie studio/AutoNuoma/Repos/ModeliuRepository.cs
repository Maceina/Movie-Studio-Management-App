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
    public class ModeliuRepository
    {
        public List<ModelisViewModel> getModeliai()
        {
            List<ModelisViewModel> modelisViewModels = new List<ModelisViewModel>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.irangosid, m.ipavadinimas, mm.kinostudijosid AS kinostudija1 
                                FROM "+Globals.dbPrefix+@"iranga m
                                LEFT JOIN "+Globals.dbPrefix+@"kinostudija mm ON mm.kinostudijosid=m.fk_KINOSTUDIJAkinostudijosid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                modelisViewModels.Add(new ModelisViewModel
                {
                    id = Convert.ToInt32(item["irangosid"]),
                    pavadinimas = Convert.ToString(item["ipavadinimas"]),
                    marke = Convert.ToString(item["kinostudija1"])
                });
            }

            return modelisViewModels;
        }

        public ModelisEditViewModel getModelis(int id)
        {
            ModelisEditViewModel modelis = new ModelisEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.* 
                                FROM "+Globals.dbPrefix+@"iranga m WHERE m.irangosid=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                modelis.id = Convert.ToInt32(item["irangosid"]);
                modelis.pavadinimas = Convert.ToString(item["ipavadinimas"]);
                modelis.fk_marke = Convert.ToInt32(item["fk_KINOSTUDIJAkinostudijosid"]);
            }

            return modelis;
        }

        public bool updateModelis(ModelisEditViewModel modelis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE "+Globals.dbPrefix+"iranga a SET a.ipavadinimas=?ipavadinimas, a.fk_KINOSTUDIJAkinostudijosid=?kinostudija WHERE a.irangosid=?irangosid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?irangosid", MySqlDbType.VarChar).Value = modelis.id;
            mySqlCommand.Parameters.Add("?ipavadinimas", MySqlDbType.VarChar).Value = modelis.pavadinimas;
            mySqlCommand.Parameters.Add("?kinostudija", MySqlDbType.VarChar).Value = modelis.fk_marke;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addModelis(ModelisEditViewModel modelis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO "+Globals.dbPrefix+"iranga(irangosid,ipavadinimas,fk_KINOSTUDIJAkinostudijosid)VALUES(?irangosid,?ipavadinimas,?kinostudija)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?ipavadinimas", MySqlDbType.VarChar).Value = modelis.pavadinimas;
            mySqlCommand.Parameters.Add("?irangosid", MySqlDbType.VarChar).Value = modelis.id;
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
            string sqlquery = @"DELETE FROM "+Globals.dbPrefix+"iranga where irangosid=?irangosid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?irangosid", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }


    }
}