using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoNuoma.ViewModels;
using AutoNuoma.Models;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace AutoNuoma.Repos
{
    public class SutartisRepository
    {
        public List<SutartisListViewModel> getSutartys()
        {
            List<SutartisListViewModel> sutartys = new List<SutartisListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT s.nr, s.sutarties_data as data, CONCAT(d.vardas,' ', d.pavarde) as darbuotojas, CONCAT(n.vardas,' ',n.pavarde) as nuomininkas, b.name as busena 
                                FROM "+Globals.dbPrefix+@"sutartys s, "+Globals.dbPrefix+@"darbuotojai d, "+Globals.dbPrefix+@"klientai n, "+Globals.dbPrefix+@"sutarties_busenos b 
                                WHERE s.fk_darbuotojas=d.tabelio_nr and s.fk_klientas=n.asmens_kodas and s.busena=b.id
                                ORDER BY 1";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                sutartys.Add(new SutartisListViewModel
                {
                    nr = Convert.ToInt32(item["nr"]),
                    darbuotojas = Convert.ToString(item["darbuotojas"]),
                    nuomininkas = Convert.ToString(item["nuomininkas"]),
                    data = Convert.ToDateTime(item["data"]),
                    busena = Convert.ToString(item["busena"])
                });
            }
            return sutartys;
        }

        public SutartisEditViewModel getSutartis(int nr)
        {
            SutartisEditViewModel sutartis = new SutartisEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM "+Globals.dbPrefix+@"sutartys where nr="+nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                sutartis.nr = Convert.ToInt32(item["nr"]);
                sutartis.sutartiesData = Convert.ToDateTime(item["sutarties_data"]);
                sutartis.nuomosDataLaikas = Convert.ToDateTime(item["nuomos_data_laikas"]);
                sutartis.planuojamaGrDataLaikas = Convert.ToDateTime(item["planuojama_grazinimo_data_laikas"]);
                sutartis.faktineGrDataLaikas = Convert.ToDateTime(item["faktine_grazinimo_data_laikas"]);
                sutartis.pradineRida = Convert.ToInt32(item["pradine_rida"]);
                sutartis.galineRida = (item["galine_rida"] == DBNull.Value ? -1:Convert.ToInt32(item["galine_rida"]));
                sutartis.kaina = Convert.ToDecimal(item["kaina"]);
                sutartis.degaluKiekisPaimant = Convert.ToInt32(item["degalu_kiekis_paimant"]);
                sutartis.degaluKiekisGrazinant = (item["dagalu_kiekis_grazinus"] == DBNull.Value ? -1 :Convert.ToInt32(item["dagalu_kiekis_grazinus"]));
                sutartis.busena = Convert.ToInt32(item["busena"]);
                sutartis.fk_klientas = Convert.ToString(item["fk_klientas"]);
                sutartis.fk_darbuotojas = Convert.ToString(item["fk_darbuotojas"]);
                sutartis.fk_automobilis = Convert.ToInt32(item["fk_automobilis"]);
                sutartis.fk_grazinimoVieta = Convert.ToInt32(item["fk_grazinimo_vieta"]);
                sutartis.fk_paemimoVieta = Convert.ToInt32(item["fk_paemimo_vieta"]);
            }

            if (sutartis.galineRida == -1)
            {
                sutartis.galineRida = null;
            }

            if (sutartis.degaluKiekisGrazinant == -1)
            {
                sutartis.degaluKiekisGrazinant = null;
            }

            return sutartis;
        }

        public bool updateSutartis(SutartisEditViewModel sutartis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `"+Globals.dbPrefix+@"sutartys` SET
                                    `sutarties_data` = ?sutdata,
                                    `nuomos_data_laikas` = ?nuomdata,
                                    `planuojama_grazinimo_data_laikas` = ?plgrlaikas,
                                    `faktine_grazinimo_data_laikas` = ?fkgrlaikas,
                                    `pradine_rida` = ?prrida,
                                    `galine_rida` = ?glrida,
                                    `kaina` = ?kaina,
                                    `degalu_kiekis_paimant` = ?dkiekispa,
                                    `dagalu_kiekis_grazinus` = ?dkiekisgr,
                                    `busena` = ?busena,
                                    `fk_klientas` = ?klientas,
                                    `fk_darbuotojas` = ?darbuotojas,
                                    `fk_automobilis` = ?automobilis,
                                    `fk_grazinimo_vieta` = ?grvieta,
                                    `fk_paemimo_vieta` = ?pavieta
                                    WHERE nr=" + sutartis.nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?sutdata", MySqlDbType.Date).Value = sutartis.sutartiesData;
            mySqlCommand.Parameters.Add("?nuomdata", MySqlDbType.DateTime).Value = sutartis.nuomosDataLaikas.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?plgrlaikas", MySqlDbType.DateTime).Value = sutartis.planuojamaGrDataLaikas.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?fkgrlaikas", MySqlDbType.DateTime).Value = sutartis.faktineGrDataLaikas.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?prrida", MySqlDbType.Int32).Value = sutartis.pradineRida;
            mySqlCommand.Parameters.Add("?glrida", MySqlDbType.Int32).Value = sutartis.galineRida;
            mySqlCommand.Parameters.Add("?kaina", MySqlDbType.Decimal).Value = sutartis.kaina;
            mySqlCommand.Parameters.Add("?dkiekispa", MySqlDbType.Int32).Value = sutartis.degaluKiekisPaimant;
            mySqlCommand.Parameters.Add("?dkiekisgr", MySqlDbType.Int32).Value = sutartis.degaluKiekisGrazinant;
            mySqlCommand.Parameters.Add("?busena", MySqlDbType.Int32).Value = sutartis.busena;
            mySqlCommand.Parameters.Add("?darbuotojas", MySqlDbType.VarChar).Value = sutartis.fk_darbuotojas;
            mySqlCommand.Parameters.Add("?klientas", MySqlDbType.VarChar).Value = sutartis.fk_klientas;
            mySqlCommand.Parameters.Add("?automobilis", MySqlDbType.Int32).Value = sutartis.fk_automobilis;
            mySqlCommand.Parameters.Add("?grvieta", MySqlDbType.Int32).Value = sutartis.fk_grazinimoVieta;
            mySqlCommand.Parameters.Add("?pavieta", MySqlDbType.Int32).Value = sutartis.fk_paemimoVieta;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addSutartis(SutartisEditViewModel sutartis)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `"+Globals.dbPrefix+@"sutartys` (
                                    `sutarties_data`,
                                    `nuomos_data_laikas`,
                                    `planuojama_grazinimo_data_laikas`,
                                    `faktine_grazinimo_data_laikas`,
                                    `pradine_rida`,
                                    `galine_rida`,
                                    `kaina`,
                                    `degalu_kiekis_paimant`,
                                    `dagalu_kiekis_grazinus`,
                                    `busena`,
                                    `fk_klientas`,
                                    `fk_darbuotojas`,
                                    `fk_automobilis`,
                                    `fk_grazinimo_vieta`,
                                    `fk_paemimo_vieta`)
                                    VALUES(
                                     ?sutdata,
                                     ?nuomdata,
                                     ?plgrlaikas,
                                     ?fkgrlaikas,
                                     ?prrida,
                                     ?glrida,
                                     ?kaina,
                                     ?dkiekispa,
                                     ?dkiekisgr,
                                     ?busena,
                                     ?klientas,
                                     ?darbuotojas,
                                     ?automobilis,
                                     ?grvieta,
                                     ?pavieta)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?sutdata", MySqlDbType.Date).Value = sutartis.sutartiesData;
            mySqlCommand.Parameters.Add("?nuomdata", MySqlDbType.DateTime).Value = sutartis.nuomosDataLaikas.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?plgrlaikas", MySqlDbType.DateTime).Value = sutartis.planuojamaGrDataLaikas.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?fkgrlaikas", MySqlDbType.DateTime).Value = sutartis.faktineGrDataLaikas.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?prrida", MySqlDbType.Int32).Value = sutartis.pradineRida;
            mySqlCommand.Parameters.Add("?glrida", MySqlDbType.Int32).Value = sutartis.galineRida;
            mySqlCommand.Parameters.Add("?kaina", MySqlDbType.Decimal).Value = sutartis.kaina;
            mySqlCommand.Parameters.Add("?dkiekispa", MySqlDbType.Int32).Value = sutartis.degaluKiekisPaimant;
            mySqlCommand.Parameters.Add("?dkiekisgr", MySqlDbType.Int32).Value = sutartis.degaluKiekisGrazinant;
            mySqlCommand.Parameters.Add("?busena", MySqlDbType.Int32).Value = sutartis.busena;
            mySqlCommand.Parameters.Add("?darbuotojas", MySqlDbType.VarChar).Value = sutartis.fk_darbuotojas;
            mySqlCommand.Parameters.Add("?klientas", MySqlDbType.VarChar).Value = sutartis.fk_klientas;
            mySqlCommand.Parameters.Add("?automobilis", MySqlDbType.Int32).Value = sutartis.fk_automobilis;
            mySqlCommand.Parameters.Add("?grvieta", MySqlDbType.Int32).Value = sutartis.fk_grazinimoVieta;
            mySqlCommand.Parameters.Add("?pavieta", MySqlDbType.Int32).Value = sutartis.fk_paemimoVieta;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteSutartis(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM "+Globals.dbPrefix+"sutartys where nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}