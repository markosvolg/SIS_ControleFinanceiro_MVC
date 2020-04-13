using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIS_ControleFinanceiro_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIS_ControleFinanceiro_MVC.Controllers
{
    public class TransacaoController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;



        public TransacaoController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {

            TransacaoModel objmodal = new TransacaoModel(HttpContextAccessor);

            ViewBag.ListaPlanoConta = objmodal.ListaTransacao();


            return View();
        }


        [HttpGet]
        public IActionResult Registrar(int? id)
        {

            if (id != null)
            {
                TransacaoModel objmodal = new TransacaoModel(HttpContextAccessor);
                ViewBag.Registro = objmodal.CarregaRegistro(id);
            }

            ViewBag.ListaContas = new ContaModel(HttpContextAccessor).ListaConta();

            ViewBag.ListaPlanoConta = new PlanoContaModel(HttpContextAccessor).ListaPlanoConta();

            return View();
        }


        [HttpPost]
        public IActionResult Registrar(TransacaoModel transacao)
        {

            if (ModelState.IsValid)
            {
                transacao.HttpContextAccessor = HttpContextAccessor;
                transacao.RegistrarTransacao();
                return RedirectToAction("Index");
            }



            return View();
        }

        [HttpGet]
        public IActionResult ExcluirTransacao(int id)
        {
            TransacaoModel objmodal = new TransacaoModel(HttpContextAccessor);
            ViewBag.Registro = objmodal.CarregaRegistro(id);

            return View();
        }


        [HttpGet]
        public IActionResult Excluir(int id)
        {
            TransacaoModel objmodal = new TransacaoModel(HttpContextAccessor);

            objmodal.Excluir(id);

            return RedirectToAction("Index");

        }



        [HttpGet]
        [HttpPost]
        public IActionResult Extrato(TransacaoModel formulario)
        {
            formulario.HttpContextAccessor = HttpContextAccessor;
            ViewBag.ListaTransacao = formulario.ListaTransacao();
            ViewBag.ListaContas = new ContaModel(HttpContextAccessor).ListaConta();

            return View();
        }


        public IActionResult DashBoard()
        {
            Dashboard objdash = new Dashboard();

            List<Dashboard> lista = new Dashboard().RetornarDespesasDashBoard();

            string cores = "";
            string valores = "";
            string labels = "";
            var random = new Random();

            for (int i = 0; i < lista.Count; i++)
            {
                valores += "'" + lista[i].Total.ToString() + "',";
                labels += "'" + lista[i].PlanoConta.ToString() + "',";
                cores += "'" + String.Format("#{0:X6}", random.Next(0x1000000)) + "',";

            }


         
            ViewBag.Cores = cores;
            ViewBag.Labes = labels;
            ViewBag.Valores = valores;



            return View();
        }

    }
}
