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
    public class AutomobiliaiRepository
    {
        public List<AutoListViewModel> getCars()
        {
            List<AutoListViewModel> autombiliai = new List<AutoListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.id, 
                                       a.valstybinis_nr,
                                       b.name AS busena,
                                       m.pavadinimas AS modelis,
                                       mm.pavadinimas AS marke
                                       FROM "+Globals.dbPrefix+@"automobiliai a
                                        LEFT JOIN "+Globals.dbPrefix+@"auto_busenos b ON b.id = a.busena
                                        LEFT JOIN "+Globals.dbPrefix+@"modeliai m ON m.id = a.fk_modelis
                                        LEFT JOIN "+Globals.dbPrefix+@"markes mm ON mm.id = m.fk_marke;";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                autombiliai.Add(new AutoListViewModel
                {
                    id = Convert.ToInt32(item["id"]),
                    valstybinisNr = Convert.ToString(item["valstybinis_nr"]),
                    busena = Convert.ToString(item["busena"]),
                    modelis = Convert.ToString(item["modelis"]),
                    marke = Convert.ToString(item["marke"])
                });
            }


            return autombiliai;
        }

        public AutoEditViewModel getCar(int id)
        {
            AutoEditViewModel autoEditViewModel = new AutoEditViewModel();

            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.id, 
                                       a.valstybinis_nr,
                                       a.pagaminimo_data,
                                       a.rida,
                                       a.radijas,
                                       a.grotuvas,
                                       a.kondicionierius,
                                       a.vietu_skaicius,
                                       a.registravimo_data,
                                       a.verte,
                                       a.pavaru_deze,
                                       a.degalu_tipas,
                                       a.kebulas,
                                       a.bagazo_dydis,
                                       a.busena,
                                       a.fk_modelis
                                       FROM "+Globals.dbPrefix+@"automobiliai a
                                       WHERE a.id= "+id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                autoEditViewModel.id = Convert.ToInt32(item["id"]);
                autoEditViewModel.valstybinisNr = Convert.ToString(item["valstybinis_nr"]);
                autoEditViewModel.pagaminimoData = Convert.ToDateTime(item["pagaminimo_data"]);
                autoEditViewModel.rida = Convert.ToInt32(item["rida"]);
                autoEditViewModel.radijas = Convert.ToBoolean(item["radijas"]);
                autoEditViewModel.grotuvas = Convert.ToBoolean(item["grotuvas"]);
                autoEditViewModel.kondicionierius = Convert.ToBoolean(item["kondicionierius"]);
                autoEditViewModel.vietuSkaicius = Convert.ToInt32(item["vietu_skaicius"]);
                autoEditViewModel.registravimoData = Convert.ToDateTime(item["registravimo_data"]);
                autoEditViewModel.verte = Convert.ToDecimal(item["verte"]);
                autoEditViewModel.pavaruDeze = Convert.ToInt32(item["pavaru_deze"]);
                autoEditViewModel.degaluTipas = Convert.ToInt32(item["degalu_tipas"]);
                autoEditViewModel.kebulas = Convert.ToInt32(item["kebulas"]);
                autoEditViewModel.bagazoDydis = Convert.ToInt32(item["bagazo_dydis"]);
                autoEditViewModel.busena = Convert.ToInt32(item["busena"]);
                autoEditViewModel.fk_modelis = Convert.ToInt32(item["fk_modelis"]);
            }

            return autoEditViewModel;

        }

        public bool addAuto(AutoEditViewModel autoEditViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `"+Globals.dbPrefix+@"automobiliai`
                                    (
                                    `valstybinis_nr`,
                                    `pagaminimo_data`,
                                    `rida`,
                                    `radijas`,
                                    `grotuvas`,
                                    `kondicionierius`,
                                    `vietu_skaicius`,
                                    `registravimo_data`,
                                    `verte`,
                                    `pavaru_deze`,
                                    `degalu_tipas`,
                                    `kebulas`,
                                    `bagazo_dydis`,
                                    `busena`,
                                    `fk_modelis`) 
                                    VALUES (
                                    ?vlst_nr,
                                    ?pag_data,
                                    ?rida,
                                    ?radijas,
                                    ?grotuvas,
                                    ?kond,
                                    ?viet_sk,
                                    ?reg_dt,
                                    ?verte,
                                    ?pav_deze,
                                    ?dega_tip,
                                    ?kebulas,
                                    ?bagaz_tip,
                                    ?busena,
                                    ?fk_mod)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vlst_nr", MySqlDbType.VarChar).Value = autoEditViewModel.valstybinisNr;
            mySqlCommand.Parameters.Add("?pag_data", MySqlDbType.Date).Value = autoEditViewModel.pagaminimoData.ToString("yyyy-MM-dd");
            mySqlCommand.Parameters.Add("?rida", MySqlDbType.Int32).Value = autoEditViewModel.rida;
            mySqlCommand.Parameters.Add("?radijas", MySqlDbType.Int32).Value = (autoEditViewModel.radijas ? 1 : 0);
            mySqlCommand.Parameters.Add("?grotuvas", MySqlDbType.Int32).Value = (autoEditViewModel.grotuvas ? 1 : 0);
            mySqlCommand.Parameters.Add("?kond", MySqlDbType.Int32).Value = (autoEditViewModel.kondicionierius ? 1 : 0);
            mySqlCommand.Parameters.Add("?viet_sk", MySqlDbType.Int32).Value = autoEditViewModel.vietuSkaicius;
            mySqlCommand.Parameters.Add("?reg_dt", MySqlDbType.Date).Value = autoEditViewModel.registravimoData.ToString("yyyy-MM-dd");
            mySqlCommand.Parameters.Add("?verte", MySqlDbType.Decimal).Value = autoEditViewModel.verte;
            mySqlCommand.Parameters.Add("?pav_deze", MySqlDbType.Int32).Value = autoEditViewModel.pavaruDeze;
            mySqlCommand.Parameters.Add("?dega_tip", MySqlDbType.Int32).Value = autoEditViewModel.degaluTipas;
            mySqlCommand.Parameters.Add("?kebulas", MySqlDbType.Int32).Value = autoEditViewModel.kebulas;
            mySqlCommand.Parameters.Add("?bagaz_tip", MySqlDbType.Int32).Value = autoEditViewModel.bagazoDydis;
            mySqlCommand.Parameters.Add("?busena", MySqlDbType.Int32).Value = autoEditViewModel.busena;
            mySqlCommand.Parameters.Add("?fk_mod", MySqlDbType.Int32).Value = autoEditViewModel.fk_modelis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool updateAuto(AutoEditViewModel autoEditViewModel)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `"+Globals.dbPrefix+@"automobiliai` SET
                                    `valstybinis_nr` = ?vlst_nr,
                                    `pagaminimo_data` = ?pag_data,
                                    `rida` = ?rida,
                                    `radijas` = ?radijas,
                                    `grotuvas` = ?grotuvas,
                                    `kondicionierius` = ?kond,
                                    `vietu_skaicius` = ?viet_sk,
                                    `registravimo_data` = ?reg_dt,
                                    `verte` = ?verte,
                                    `pavaru_deze` = ?pav_deze,
                                    `degalu_tipas` = ?dega_tip,
                                    `kebulas` = ?kebulas,
                                    `bagazo_dydis` = ?bagaz_tip,
                                    `busena` = ?busena,
                                    `fk_modelis` = ?fk_mod
                                    WHERE id="+autoEditViewModel.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vlst_nr", MySqlDbType.VarChar).Value = autoEditViewModel.valstybinisNr;
            mySqlCommand.Parameters.Add("?pag_data", MySqlDbType.Date).Value = autoEditViewModel.pagaminimoData.ToString("yyyy-MM-dd");
            mySqlCommand.Parameters.Add("?rida", MySqlDbType.Int32).Value = autoEditViewModel.rida;
            mySqlCommand.Parameters.Add("?radijas", MySqlDbType.Int32).Value = (autoEditViewModel.radijas ? 1 : 0);
            mySqlCommand.Parameters.Add("?grotuvas", MySqlDbType.Int32).Value = (autoEditViewModel.grotuvas ? 1 : 0);
            mySqlCommand.Parameters.Add("?kond", MySqlDbType.Int32).Value = (autoEditViewModel.kondicionierius ? 1 : 0);
            mySqlCommand.Parameters.Add("?viet_sk", MySqlDbType.Int32).Value = autoEditViewModel.vietuSkaicius;
            mySqlCommand.Parameters.Add("?reg_dt", MySqlDbType.Date).Value = autoEditViewModel.registravimoData.ToString("yyyy-MM-dd");
            mySqlCommand.Parameters.Add("?verte", MySqlDbType.Decimal).Value = autoEditViewModel.verte;
            mySqlCommand.Parameters.Add("?pav_deze", MySqlDbType.Int32).Value = autoEditViewModel.pavaruDeze;
            mySqlCommand.Parameters.Add("?dega_tip", MySqlDbType.Int32).Value = autoEditViewModel.degaluTipas;
            mySqlCommand.Parameters.Add("?kebulas", MySqlDbType.Int32).Value = autoEditViewModel.kebulas;
            mySqlCommand.Parameters.Add("?bagaz_tip", MySqlDbType.Int32).Value = autoEditViewModel.bagazoDydis;
            mySqlCommand.Parameters.Add("?busena", MySqlDbType.Int32).Value = autoEditViewModel.busena;
            mySqlCommand.Parameters.Add("?fk_mod", MySqlDbType.Int32).Value = autoEditViewModel.fk_modelis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public int getAutomobilisSutarciuCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(nr) as kiekis from "+Globals.dbPrefix+"sutartys where fk_automobilis=" + id;
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

        public void deleteAutomobilis(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM "+Globals.dbPrefix+"automobiliai where id=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }

    }
}