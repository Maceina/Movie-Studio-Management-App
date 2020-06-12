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
    public class DarbuotojasRepository
    {
        public List<Darbuotojas> getDarbuotojai()
        {
            List<Darbuotojas> darbuotojai = new List<Darbuotojas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"darbuotojai";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                darbuotojai.Add(new Darbuotojas
                {
                    tabelis = Convert.ToString(item["tabelio_nr"]),
                    vardas = Convert.ToString(item["vardas"]),
                    pavarde = Convert.ToString(item["pavarde"])
                });
            }

            return darbuotojai;
        }

        public Darbuotojas getDarbuotojas(string tabnr)
        {
            Darbuotojas darbuotojas = new Darbuotojas();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"darbuotojai where tabelio_nr=?tab";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?tab", MySqlDbType.VarChar).Value = tabnr;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                darbuotojas.tabelis = Convert.ToString(item["tabelio_nr"]);
                darbuotojas.vardas = Convert.ToString(item["vardas"]);
                darbuotojas.pavarde = Convert.ToString(item["pavarde"]);
            }

            return darbuotojas;
        }

        public bool updateDarbuotojas(Darbuotojas darbuotojas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE "+Globals.dbPrefix+"darbuotojai a SET a.vardas=?vardas, a.pavarde=?pavarde WHERE a.tabelio_nr=?tab";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = darbuotojas.vardas;
                mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = darbuotojas.pavarde;
                mySqlCommand.Parameters.Add("?tab", MySqlDbType.VarChar).Value = darbuotojas.tabelis;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool addDarbuotojas(Darbuotojas darbuotojas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO "+Globals.dbPrefix+"darbuotojai(tabelio_nr,vardas,pavarde)VALUES(?tabelio_nr,?vardas,?pavarde);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?vardas", MySqlDbType.VarChar).Value = darbuotojas.vardas;
                mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.VarChar).Value = darbuotojas.pavarde;
                mySqlCommand.Parameters.Add("?tabelio_nr", MySqlDbType.VarChar).Value = darbuotojas.tabelis;
                mySqlConnection.Open();
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int getDarbuotojasSutarciuCount(string id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(nr) as kiekis from "+Globals.dbPrefix+"sutartys where fk_darbuotojas= '" + id+"'";
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

        public void deleteDarbuotojas(string id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM "+Globals.dbPrefix+"darbuotojai where tabelio_nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}