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
    public class UzsakytaPaslaugaRepository
    {
        public List<UzsakytaPaslauga> getUzsakytosPaslaugos(int sutartis)
        {
            List<UzsakytaPaslauga> paslaugos = new List<UzsakytaPaslauga>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "select * from "+Globals.dbPrefix+"uzsakytos_paslaugos WHERE fk_sutartis="+sutartis;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                paslaugos.Add(new UzsakytaPaslauga
                {
                    fk_sutartis = Convert.ToInt32(item["fk_sutartis"]),
                    fk_kainaGaliojaNuo = Convert.ToDateTime(item["fk_kaina_galioja_nuo"]),
                    fk_paslauga = Convert.ToInt32(item["fk_paslauga"]),
                    kiekis = Convert.ToInt32(item["kiekis"]),
                    kaina = Convert.ToDecimal(item["kaina"])
                });
            }

            return paslaugos;
        }

        public bool deleteUzsakytasPaslaugas(int sutartis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM a USING "+Globals.dbPrefix+@"uzsakytos_paslaugos as a
                                where a.fk_sutartis=?fkid";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?fkid", MySqlDbType.Int32).Value = sutartis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public bool insertUzsakytaPaslauga(UzsakytaPaslauga uzsakytaPaslauga)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO "+Globals.dbPrefix+@"uzsakytos_paslaugos(
                                        fk_sutartis,
                                        fk_kaina_galioja_nuo,
                                        fk_paslauga,
                                        kiekis,
                                        kaina)
                                        VALUES(
                                        ?fk_sutartis,
                                        ?galioja_nuo,
                                        ?fk_paslauga,
                                        ?kiekis,
                                        ?kaina
                                        )";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?fk_sutartis", MySqlDbType.Int32).Value = uzsakytaPaslauga.fk_sutartis;
            mySqlCommand.Parameters.Add("?galioja_nuo", MySqlDbType.Date).Value = uzsakytaPaslauga.fk_kainaGaliojaNuo.ToString("yyyy-MM-dd");
            mySqlCommand.Parameters.Add("?fk_paslauga", MySqlDbType.Int32).Value = uzsakytaPaslauga.fk_paslauga;
            mySqlCommand.Parameters.Add("?kaina", MySqlDbType.Decimal).Value = uzsakytaPaslauga.kaina;
            mySqlCommand.Parameters.Add("?kiekis", MySqlDbType.Int32).Value = uzsakytaPaslauga.kiekis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }
    }
}