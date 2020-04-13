using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIS_ControleFinanceiro_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIS_ControleFinanceiro_MVC.Controllers
{
    public class PlanoContaController : Controller
    {

        IHttpContextAccessor HttpContextAccessor;


        public PlanoContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {

            PlanoContaModel objmodal = new PlanoContaModel(HttpContextAccessor);

            ViewBag.ListaPlanoConta = objmodal.ListaPlanoConta();

            return View();
        }

        [HttpGet]
        public IActionResult CriarPlanoConta(int? id)
        {
            if (id != null)
            {
                PlanoContaModel objmodal = new PlanoContaModel(HttpContextAccessor);
                ViewBag.Registro = objmodal.CarregaRegistro(id);
            }


            return View();
        }


        [HttpPost]
        public IActionResult CriarPlanoConta(PlanoContaModel planoconta)
        {
            if (ModelState.IsValid)
            {
                planoconta.HttpContextAccessor = HttpContextAccessor;
                planoconta.AdicionarConta();
                return RedirectToAction("Index");
            }


            return View();
        }


        public IActionResult ExcluirPlanoConta(int id)
        {
            PlanoContaModel objmodal = new PlanoContaModel(HttpContextAccessor);
            objmodal.ExcluirConta(id);
            return RedirectToAction("Index");
        }


    }
}
