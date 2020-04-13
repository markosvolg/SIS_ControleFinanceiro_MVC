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
    public class TransacaoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a Descrição!")]
        public string Data { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int Conta_Id { get; set; }
        public int Plano_Conta_Id { get; set; }

        public string NomeConta { get; set; }

        public string DescricaoPlanoConta { get; set; }


        public string DataFinal { get; set; }


        public IHttpContextAccessor HttpContextAccessor { get; set; }


        public TransacaoModel(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
        }


        public TransacaoModel()
        {

        }


        public List<TransacaoModel> ListaTransacao()
        {

            List<TransacaoModel> lista = new List<TransacaoModel>();

            TransacaoModel item;


            string filtro = "";

            if (Data != null && DataFinal != null)
            {
                filtro += $" and t.Data >='{DateTime.Parse(Data).ToString("yyyy/MM/dd")}' and t.Data <='{DateTime.Parse(DataFinal).ToString("yyyy/MM/dd")}'";
            }

            if (Tipo != null)
            {
                if (Tipo != "A")
                {
                    filtro += $" and t.tipo ='{Tipo}'";
                }
            }

            if (Conta_Id != 0)
            {
                filtro += $" and t.Conta_Id ='{Conta_Id}'";
            }




            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");

            string sql = $"Select t.id, t.data, t.tipo, t.valor,t.descricao as Historico,c.Nome as Conta, c.saldo, p.descricao as Plano_Conta," +
                        $" a.nome as Nome_Usuario, a.id as Usuario_Id, a.email, t.conta_Id, t.Plano_Contas_Id " +
                        $" from Transacao t " +
                        $" join conta c on c.id = t.conta_id " +
                        $" join plano_contas p on p.id = t.plano_contas_id " +
                        $" join usuario a on a.id = c.usuario_id " +
                        $" where a.id = '{_usuario_id}' {filtro} order by t.data desc ";

            DAL objdal = new DAL();
            DataTable dt = objdal.retDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new TransacaoModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Data = (dt.Rows[i]["Data"].ToString());
                item.Tipo = dt.Rows[i]["TIPO"].ToString();
                item.Descricao = dt.Rows[i]["Historico"].ToString();
                item.Valor = double.Parse(dt.Rows[i]["Valor"].ToString());
                item.NomeConta = dt.Rows[i]["Conta"].ToString();
                item.DescricaoPlanoConta = dt.Rows[i]["Plano_Conta"].ToString();
                item.Conta_Id = int.Parse(dt.Rows[i]["Conta_Id"].ToString());
                item.Plano_Conta_Id = int.Parse(dt.Rows[i]["Plano_Contas_Id"].ToString());
                lista.Add(item);

            }

            return lista;

        }


        public void RegistrarTransacao()
        {


            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");

            string sql = "";

            if (Id == 0)
            {
                sql = $"INSERT INTO TRANSACAO (DATA, TIPO, DESCRICAO, VALOR, CONTA_ID, PLANO_CONTAS_ID) VALUES('{DateTime.Parse(Data).ToString("yyyy/MM/dd")}','{Tipo}','{Descricao}','{Valor}','{Conta_Id}','{Plano_Conta_Id}')";
            }
            else
            {
                sql = $"UPDATE TRANSACAO SET Data ='{DateTime.Parse(Data).ToString("yyyy/MM/dd")}', DESCRICAO = '{Descricao}', TIPO ='{Tipo}', VALOR ='{Valor}', CONTA_ID ='{Conta_Id}', PLANO_CONTAS_ID ='{Plano_Conta_Id}' WHERE ID ='{Id}'";
            }


            DAL objdal = new DAL();

            objdal.ExecutaComendoSQL(sql);



        }


        public TransacaoModel CarregaRegistro(int? id)
        {
            TransacaoModel item = new TransacaoModel();

            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");


            string sql = $"Select t.id, t.data, t.tipo, t.valor,t.descricao as Historico,c.Nome as Conta, c.saldo, p.descricao as Plano_Conta," +
                        $" a.nome as Nome_Usuario, a.id as Usuario_Id, a.email, t.conta_Id, t.Plano_Contas_Id " +
                        $" from Transacao t " +
                        $" join conta c on c.id = t.conta_id " +
                        $" join plano_contas p on p.id = t.plano_contas_id " +
                        $" join usuario a on a.id = c.usuario_id " +
                        $" where t.id = '{id}' order by t.data desc ";



            DAL objdal = new DAL();
            DataTable dt = objdal.retDataTable(sql);

            item = new TransacaoModel();
            item.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            item.Data = DateTime.Parse(dt.Rows[0]["Data"].ToString()).ToString("dd/MM/yyyy");
            item.Tipo = dt.Rows[0]["TIPO"].ToString();
            item.Descricao = dt.Rows[0]["Historico"].ToString();
            item.Valor = double.Parse(dt.Rows[0]["Valor"].ToString());
            item.NomeConta = dt.Rows[0]["Conta"].ToString();
            item.DescricaoPlanoConta = dt.Rows[0]["Plano_Conta"].ToString();
            item.Conta_Id = int.Parse(dt.Rows[0]["Conta_Id"].ToString());
            item.Plano_Conta_Id = int.Parse(dt.Rows[0]["Plano_Contas_Id"].ToString());

            return item;

        }


        public void Excluir(int id)
        {
            string _usuario_id = HttpContextAccessor.HttpContext.Session.GetString("UsuarioLogadoId");
            string sql = $"DELETE FROM TRANSACAO WHERE ID='{id}'";

            DAL objdal = new DAL();

            objdal.ExecutaComendoSQL(sql);


        }








    }


    public class Dashboard
    {

        public double Total { get; set; }
        public string PlanoConta { get; set; }


        public List<Dashboard> RetornarDespesasDashBoard()
        {


            List<Dashboard> lista = new List<Dashboard>();

            Dashboard item;

            string sql = $"select sum(t.Valor) as Total, t.Descricao from transacao t" +
                            " join plano_contas p on p.Id = t.Plano_Contas_Id " +
                            " where t.Tipo = 'D' " +
                            " group by p.Descricao ";

            DAL objdal = new DAL();
            DataTable dt = new DataTable();
            dt = objdal.retDataTable(sql);


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                item = new Dashboard();
                item.Total = double.Parse(dt.Rows[i]["Total"].ToString());
                item.PlanoConta = dt.Rows[i]["Descricao"].ToString();

                lista.Add(item);


            }

            return lista;

        }
    }
}