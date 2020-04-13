using System;
using Xunit;
using SIS_ControleFinanceiro_MVC.Models;


namespace TesteControleFinanceiro
{
    public class UnitTestModel
    {
        [Fact]
        public void TestUsuarioLogin()
        {
            UsuarioModel usuermodel = new UsuarioModel();
            usuermodel.Email = "markosvolg@gmail.com";
            usuermodel.Senha = "123456";
            bool result = usuermodel.ValidarLogin();
            Assert.True(result);

        }
    }
}
