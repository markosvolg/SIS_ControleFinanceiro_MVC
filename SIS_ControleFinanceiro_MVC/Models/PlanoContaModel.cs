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
    public class PlanoContaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a Descrição!")]
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int Usuriao_Id { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }


        public PlanoContaModel(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
        }


        public PlanoContaModel()
        {

        }

        public string IdUsuarioLogado()
        {

            return HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");
        }


        public List<PlanoContaModel> ListaPlanoConta()
        {

            List<PlanoContaModel> lista = new List<PlanoContaModel>();

            PlanoContaModel item;


            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");
            string sql = $"SELECT ID, DESCRICAO, TIPO, USUARIO_ID  FROM PLANO_CONTAS WHERE USUARIO_ID ='{IdUsuarioLogado()}'";

            DAL objdal = new DAL();
            DataTable dt = objdal.retDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new PlanoContaModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Descricao = dt.Rows[i]["DESCRICAO"].ToString();
                item.Tipo = dt.Rows[i]["TIPO"].ToString();
                item.Usuriao_Id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());
                lista.Add(item);

            }

            return lista;

        }


        public void AdicionarConta()
        {


            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");

            string sql = "";

            if (Id ==0)
            {
                 sql = $"INSERT INTO PLANO_CONTAS (DESCRICAO, TIPO,USUARIO_ID) VALUES('{Descricao}','{Tipo}','{IdUsuarioLogado()}')";
            }
            else
            {
                 sql = $"UPDATE PLANO_CONTAS SET DESCRICAO = '{Descricao}', TIPO ='{Tipo}' WHERE ID ='{Id}'";
            }


            DAL objdal = new DAL();

            objdal.ExecutaComendoSQL(sql);



        }

        public void ExcluirConta(int id_conta)
        {
            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");
            string sql = $"DELETE FROM PLANO_CONTAS WHERE ID='{id_conta}'";

            DAL objdal = new DAL();

            objdal.ExecutaComendoSQL(sql);


        }


        public PlanoContaModel CarregaRegistro(int? id)
        {
            PlanoContaModel item = new PlanoContaModel();

            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");
            string sql = $"SELECT ID, DESCRICAO, TIPO, USUARIO_ID  FROM PLANO_CONTAS WHERE USUARIO_ID ='{IdUsuarioLogado()}' AND ID = '{id}'";
            DAL objdal = new DAL();
            DataTable dt = objdal.retDataTable(sql);

            item.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            item.Descricao = dt.Rows[0]["Descricao"].ToString();
            item.Tipo = dt.Rows[0]["Tipo"].ToString();
            item.Usuriao_Id = int.Parse(dt.Rows[0]["Usuario_Id"].ToString());

            return item;

        }

    }
}
