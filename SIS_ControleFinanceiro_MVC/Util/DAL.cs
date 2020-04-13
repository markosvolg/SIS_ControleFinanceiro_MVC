using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SIS_ControleFinanceiro_MVC.Util
{
    public class DAL
    {



        private static string server = "localhost";
        private static string database = "db_financeiro";
        private static string user = "root";
        private static string password = "ma@123456";
        private static string connectionString = $"Server={server};Database={database};Uid={user};Pwd={password}";

        private MySqlConnection connection; //Variavel de Conexao 


        //Construtor que já preenche a variavel com a connectionstring
        public DAL()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }


        //Executa Selects
        public DataTable retDataTable(string sql)
        {

            DataTable dataTable = new DataTable();//Instancia o DataTable
            MySqlCommand command = new MySqlCommand(sql, connection);//Receber o comando SQL e abre conexao 
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command); //Adapta os dados do banco para C#

            dataAdapter.Fill(dataTable); //Preenche o DataTable


            return dataTable; //Retorna os dados
        }


        //Executa Insert, Update, Delete
        public void ExecutaComendoSQL(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }




    }
}
