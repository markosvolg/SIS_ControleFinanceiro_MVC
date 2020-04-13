using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIS_ControleFinanceiro_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIS_ControleFinanceiro_MVC.Controllers
{
    public class UsuarioController : Controller
    {

        [HttpGet]
        public IActionResult Login(int? id)
        {
            if (id != null)
            {
                if (id == 0)
                {
                    HttpContext.Session.SetString("UsuarioLogadoNome", string.Empty);
                    HttpContext.Session.SetString("UsuarioLogadoId", string.Empty);
                }
            }

            return View();
        }


        [HttpPost]
        public IActionResult ValidarLogin(UsuarioModel usuario)
        {

            bool login = usuario.ValidarLogin();
            if (login)
            {

                HttpContext.Session.SetString("UsuarioLogadoNome", usuario.Nome);
                HttpContext.Session.SetString("UsuarioLogadoId", usuario.Id.ToString());
                return RedirectToAction("Menu", "Home");
            }
            else
            {
                TempData["ValidaMenssagem"] = "Usuario ou Senha invalido";
                return RedirectToAction("Login");

            }
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioModel usuario)
        {

            if (ModelState.IsValid)
            {
                usuario.RegistrarUsuario();
        
                
                return RedirectToAction("Sucesso");
            }

            return View();
        }



        [HttpGet]
        public IActionResult Registrar()
        {

            return View();
        }


        public IActionResult Sucesso()
        {

            return View();
        }

    }
}
