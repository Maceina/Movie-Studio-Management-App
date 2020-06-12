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
    public class KlientasRepository
    {
        public List<Klientas> getKlientai()
        {
            List<Klientas> klientai = new List<Klientas>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"filmas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientai.Add(new Klientas
                {
                    asmensKodas = Convert.ToString(item["filmoid"]),
                    vardas = Convert.ToString(item["fpavadinimas"]),
                    pavarde = Convert.ToString(item["fmetai"]),
                    gimimoData = Convert.ToString(item["fbiudzetas"]),
                    telefonas = Convert.ToString(item["fpajamos"]),
                    epastas = Convert.ToString(item["fbusena"])
                });
            }
            return klientai;
        }

        public bool addKlientas(Klientas klientas)
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"INSERT INTO "+Globals.dbPrefix+"filmas(filmoid,fpavadinimas,fmetai,fbiudzetas,fpajamos,fbusena)VALUES(?filmoid,?fpavadinimas,?fmetai,?fbiudzetas,?fpajamos,?fbusena);";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?filmoid", MySqlDbType.VarChar).Value = klientas.asmensKodas;
                mySqlCommand.Parameters.Add("?fpavadinimas", MySqlDbType.VarChar).Value = klientas.vardas;
                mySqlCommand.Parameters.Add("?fmetai", MySqlDbType.VarChar).Value = klientas.pavarde;
                mySqlCommand.Parameters.Add("?fbiudzetas", MySqlDbType.VarChar).Value = klientas.gimimoData;
                mySqlCommand.Parameters.Add("?fpajamos", MySqlDbType.VarChar).Value = klientas.telefonas;
                mySqlCommand.Parameters.Add("?fbusena", MySqlDbType.VarChar).Value = klientas.epastas;
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

        public bool updateKlientas(Klientas klientas)
        {

            try
            {
                string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mySqlConnection = new MySqlConnection(conn);
                string sqlquery = @"UPDATE "+Globals.dbPrefix+"filmas a SET a.fpavadinimas=?fpavadinimas, a.fmetai=?fmetai, a.fbiudzetas=?fbiudzetas, a.fpajamos=?fpajamos, a.fbusena=?fbusena WHERE a.filmoid=?filmoid";
                MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
                mySqlCommand.Parameters.Add("?filmoid", MySqlDbType.VarChar).Value = klientas.asmensKodas;
                mySqlCommand.Parameters.Add("?fpavadinimas", MySqlDbType.String).Value = klientas.vardas;
                mySqlCommand.Parameters.Add("?fmetai", MySqlDbType.VarChar).Value = klientas.pavarde;
                mySqlCommand.Parameters.Add("?fbiudzetas", MySqlDbType.VarChar).Value = klientas.gimimoData;
                mySqlCommand.Parameters.Add("?fpajamos", MySqlDbType.VarChar).Value = klientas.telefonas;
                mySqlCommand.Parameters.Add("?fbusena", MySqlDbType.VarChar).Value = klientas.epastas;
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

        public Klientas getKlientas(string asmkodas)
        {
            Klientas klientas = new Klientas();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"filmas where filmoid=?filmoid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?filmoid", MySqlDbType.VarChar).Value = asmkodas;
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                klientas.asmensKodas = Convert.ToString(item["filmoid"]);
                klientas.vardas = Convert.ToString(item["fpavadinimas"]);
                klientas.pavarde = Convert.ToString(item["fmetai"]);
                klientas.gimimoData = Convert.ToString(item["fbiudzetas"]);
                klientas.telefonas = Convert.ToString(item["fpajamos"]);
                klientas.epastas = Convert.ToString(item["fbusena"]);
            }
            return klientas;
        }

        public int getKlientasSutarciuCount(string id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(filmoid) as kiekis from "+Globals.dbPrefix+"filmas" + id;
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

        public void deleteKlientas(string id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM "+Globals.dbPrefix+"filmas where filmoid=?filmoid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?filmoid", MySqlDbType.VarChar).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}