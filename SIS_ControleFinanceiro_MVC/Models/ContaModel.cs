using Microsoft.AspNetCore.Http;
using SIS_ControleFinanceiro_MVC.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SIS_ControleFinanceiro_MVC.Models
{

    public class ContaModel
    {



        public int Id { get; set; }
        [Required(ErrorMessage = "Digite uma Conta")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite um valor")]
        public double Saldo { get; set; }
        public int Usuario_Id { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }



        public ContaModel()
        {

        }

        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;


        }

        public List<ContaModel> ListaConta()
        {
            ContaModel item;

            List<ContaModel> lista = new List<ContaModel>();

            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");

            string sql = $"SELECT ID,NOME,SALDO,USUARIO_ID FROM CONTA WHERE USUARIO_ID = '{_usuario_id}'";
            DAL objdal = new DAL();

            DataTable dt = objdal.retDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ContaModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Nome = dt.Rows[i]["Nome"].ToString();
                item.Saldo = double.Parse(dt.Rows[i]["Saldo"].ToString());
                item.Usuario_Id = int.Parse(dt.Rows[i]["Usuario_Id"].ToString());

                lista.Add(item);

            }

            return lista;

        }


        public void AdicionarConta()
        {


            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");

            string sql = $"INSERT INTO CONTA (NOME, SALDO,USUARIO_ID) VALUES('{Nome}','{Saldo}','{_usuario_id}')";

            DAL objdal = new DAL();

            objdal.ExecutaComendoSQL(sql);



        }

        public void ExcluirConta(int id_conta)
        {
            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");
            string sql = $"DELETE FROM CONTA WHERE ID='{id_conta}'";

            DAL objdal = new DAL();

            objdal.ExecutaComendoSQL(sql);


        }

    }
}
