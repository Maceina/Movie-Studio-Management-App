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
    public class Marke1Repository
    {
        public List<Marke> getMarkes()
        {
            List<Marke> markes = new List<Marke>();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.vaidmenstipoid, m.vtpavadinimas
                                FROM " + Globals.dbPrefix + "vaidmenstipas m";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                markes.Add(new Marke
                {
                    id = Convert.ToInt32(item["vaidmenstipoid"]),
                    pavadinimas = Convert.ToString(item["vtpavadinimas"]),
                });
            }

            return markes;
        }

        public Marke getMarke(int id)
        {
            Marke marke = new Marke();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT m.vaidmenstipoid, m.vtpavadinimas 
                                FROM " + Globals.dbPrefix + "vaidmenstipas m WHERE m.vaidmenstipoid=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {

                marke.id = Convert.ToInt32(item["vaidmenstipoid"]);
                marke.pavadinimas = Convert.ToString(item["vtpavadinimas"]);
            }

            return marke;
        }

        public bool updateMarke(Marke marke)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE " + Globals.dbPrefix + "vaidmenstipas a SET a.vtpavadinimas=?vtpavadinimas WHERE a.vaidmenstipoid=?vaidmenstipoid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vaidmenstipoid", MySqlDbType.VarChar).Value = marke.id;
            mySqlCommand.Parameters.Add("?vtpavadinimas", MySqlDbType.VarChar).Value = marke.pavadinimas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool addMarke(Marke marke)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO " + Globals.dbPrefix + "vaidmenstipas(vtpavadinimas,vaidmenstipoid)VALUES(?vtpavadinimas,?vaidmenstipoid,?pav,?pavid)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vtpavadinimas", MySqlDbType.VarChar).Value = marke.pavadinimas;
            mySqlCommand.Parameters.Add("?vaidmenstipoid", MySqlDbType.VarChar).Value = marke.id;
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.VarChar).Value = marke.pavadinimas;
            mySqlCommand.Parameters.Add("?pavid", MySqlDbType.VarChar).Value = marke.id;
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public int getMarkeCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(vaidmenstipoid) as kiekis from " + Globals.dbPrefix + "vaidmenstipas";
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

        public void deleteMarke(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM " + Globals.dbPrefix + "vaidmenstipas where vaidmenstipoid=?vaidmenstipoid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vaidmenstipoid", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

    }
}