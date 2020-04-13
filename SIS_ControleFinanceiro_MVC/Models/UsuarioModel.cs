using SIS_ControleFinanceiro_MVC.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SIS_ControleFinanceiro_MVC.Models
{
    public class UsuarioModel
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Informe o seu Nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o seu Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe sua Senha!")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe a Data de Nascimento")]
        public string Data_Nascimento { get; set; }



        public bool ValidarLogin()
        {
            string sql = $"select id, nome,data_nascimento from usuario where email='{Email}' and senha='{Senha}'";
            DAL objdal = new DAL();
            DataTable dt = objdal.retDataTable(sql);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    Id = int.Parse(dt.Rows[0]["Id"].ToString());
                    Nome = dt.Rows[0]["Nome"].ToString();
                    Data_Nascimento = dt.Rows[0]["data_nascimento"].ToString();

                    return true;
                }
            }


            return false;
        }


        public void RegistrarUsuario()
        {
            string _dataNascimento = DateTime.Parse(Data_Nascimento).ToString("yyyy/MM/dd");

            string sql = $"INSERT INTO Usuario (Nome,Email,Senha,Data_Nascimento) Values('{Nome}','{Email}','{Senha}','{_dataNascimento}')";
            DAL objdal = new DAL();

            objdal.ExecutaComendoSQL(sql);



        }


    }
}
