using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using AutoNuoma.Models;
using MySql.Data.MySqlClient;
using AutoNuoma.ViewModels;

namespace AutoNuoma.Repos
{
    public class PaslaugosKainaRepository
    {
        public List<PaslaugosKaina> getPaslaugosKainos(int id)
        {
            List<PaslaugosKaina> paslaugosKainos = new List<PaslaugosKaina>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * from "+Globals.dbPrefix+"paslaugu_kainos WHERE fk_paslauga="+id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                paslaugosKainos.Add(new PaslaugosKaina
                {
                    fk_paslauga = Convert.ToInt32(item["fk_paslauga"]),
                    galiojaNuo = Convert.ToDateTime(item["galioja_nuo"]),
                    galiojaIki = (item["galioja_iki"] == null) ? Convert.ToDateTime(item["galioja_iki"]): default(DateTime),
                    kaina = Convert.ToDecimal(item["kaina"])

                });
            }

            return paslaugosKainos;
        }

        public List<PaslaugosKainaViewModel> getPaslaugosKainos2(int id)
        {
            List<PaslaugosKainaViewModel> paslaugosKainos = new List<PaslaugosKainaViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT psl_k.fk_paslauga, psl_k.galioja_nuo, psl_k.galioja_iki, psl_k.kaina, count(psl_uzs.fk_sutartis) as kiekis FROM `"+Globals.dbPrefix+@"paslaugu_kainos` as psl_k 
	                                    left join `"+Globals.dbPrefix+@"uzsakytos_paslaugos` psl_uzs on psl_uzs.fk_paslauga=psl_k.fk_paslauga and psl_uzs.fk_kaina_galioja_nuo=psl_k.galioja_nuo
                                        where psl_k.fk_paslauga="+id+" group by psl_k.fk_paslauga, psl_k.galioja_nuo, psl_k.galioja_iki, psl_k.kaina order by count(psl_uzs.fk_sutartis) desc";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                paslaugosKainos.Add(new PaslaugosKainaViewModel
                {
                    fk_paslauga = Convert.ToInt32(item["fk_paslauga"]),
                    galiojaNuo = Convert.ToDateTime(item["galioja_nuo"]),
                    galiojaIki = (item["galioja_iki"] == null) ? Convert.ToDateTime(item["galioja_iki"]) : default(DateTime),
                    kaina = Convert.ToDecimal(item["kaina"]),
                    kiekis = Convert.ToInt32(item["kiekis"])

                });
            }

            return paslaugosKainos;
        }

        public bool deletePaslaugosKainos(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM a USING "+Globals.dbPrefix+@"paslaugu_kainos as a
                                where a.fk_paslauga=?fkid 
                                    and not exists (select 1 from "+Globals.dbPrefix+@"uzsakytos_paslaugos psl where psl.fk_paslauga=a.fk_paslauga and psl.fk_kaina_galioja_nuo=a.galioja_nuo)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?fkid", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            
            return true;
        }

        public bool insertPaslaugosKaina(PaslaugosKainaViewModel paslaugosKainaViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO "+Globals.dbPrefix+@"paslaugu_kainos(
                                        fk_paslauga,
                                        galioja_nuo,
                                        galioja_iki,
                                        kaina)VALUES(
                                        ?fk_paslauga,
                                        ?galioja_nuo,
                                        ?galioja_iki,
                                        ?kaina
                                        )";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?fk_paslauga", MySqlDbType.Int32).Value = paslaugosKainaViewModel.fk_paslauga;
            mySqlCommand.Parameters.Add("?galioja_nuo", MySqlDbType.Date).Value = paslaugosKainaViewModel.galiojaNuo;
            mySqlCommand.Parameters.Add("?galioja_iki", MySqlDbType.Date).Value = null;
            mySqlCommand.Parameters.Add("?kaina", MySqlDbType.Decimal).Value = paslaugosKainaViewModel.kaina;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            return true;
        }

        public decimal getPaslaugosKaina(int id, DateTime galiojaNuo)
        {
            decimal kaina=0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.kaina
                                       FROM "+Globals.dbPrefix+@"paslaugu_kainos a
                                       WHERE a.fk_paslauga=?id and a.galioja_nuo=?galioja";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlCommand.Parameters.Add("?galioja", MySqlDbType.Date).Value = galiojaNuo.ToString("yyyy-MM-dd");
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                kaina = Convert.ToDecimal(item["kaina"]);
            }


            return kaina;
        }
    }
}