using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIS_ControleFinanceiro_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIS_ControleFinanceiro_MVC.Controllers
{
    public class ContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;


        public ContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {

            ContaModel objmodal = new ContaModel(HttpContextAccessor);

            ViewBag.ListaConta = objmodal.ListaConta();

            return View();
        }

        [HttpPost]
        public IActionResult CriarConta(ContaModel conta)
        {
            if (ModelState.IsValid)
            {
                conta.HttpContextAccessor = HttpContextAccessor;
                conta.AdicionarConta();
                return RedirectToAction("Index");
            }


            return View();
        }


        [HttpGet]
        public IActionResult CriarConta()
        {
            

            return View();
        }


        public IActionResult ExcluirConta(int id)
        {
            ContaModel objmodal = new ContaModel(HttpContextAccessor);
            objmodal.ExcluirConta(id);
            return RedirectToAction("Index");
        }
    }
}
