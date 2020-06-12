using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using System.Configuration;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AutoNuoma.Repos
{
    public class PaslaugaRepository
    {
        public List<Paslauga> getPaslaugos()
        {
            List<Paslauga> paslaugos = new List<Paslauga>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * from "+Globals.dbPrefix+"paslaugos";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                paslaugos.Add(new Paslauga
                {
                    id = Convert.ToInt32(item["id"]),
                    pavadinimas = Convert.ToString(item["pavadinimas"]),
                    aprasymas = Convert.ToString(item["aprasymas"])
                });
            }

            return paslaugos;
        }

        public Paslauga getPaslauga(int id)
        {
            Paslauga paslauga = new Paslauga();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.id,
                                       a.pavadinimas,
                                       a.aprasymas
                                       FROM "+Globals.dbPrefix+@"paslaugos a
                                       WHERE a.id= " + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                paslauga.id = Convert.ToInt32(item["id"]);
                paslauga.pavadinimas = Convert.ToString(item["pavadinimas"]);
                paslauga.aprasymas = Convert.ToString(item["aprasymas"]);
            }

            return paslauga;
        }

        public bool updatePaslauga(Paslauga paslauga)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE "+Globals.dbPrefix+"paslaugos a SET a.pavadinimas=?pavadinimas, a.aprasymas=?aprasymas WHERE a.id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.VarChar).Value = paslauga.id;
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = paslauga.pavadinimas;
            mySqlCommand.Parameters.Add("?aprasymas", MySqlDbType.VarChar).Value = paslauga.aprasymas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public int insertPaslauga(Paslauga paslauga)
        {
            int insertedId = -1;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO "+Globals.dbPrefix+"paslaugos(pavadinimas,aprasymas)VALUES(?pavadinimas,?aprasymas);";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.VarChar).Value = paslauga.pavadinimas;
            mySqlCommand.Parameters.Add("?aprasymas", MySqlDbType.VarChar).Value = paslauga.aprasymas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            insertedId = Convert.ToInt32(mySqlCommand.LastInsertedId);
            return insertedId;
        }

        public void deletePaslauga(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM "+Globals.dbPrefix+"paslaugos where id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}