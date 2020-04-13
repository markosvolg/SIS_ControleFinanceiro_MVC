using SIS_ControleFinanceiro_MVC.Util;
using System.Data;

namespace SIS_ControleFinanceiro_MVC.Models
{
    public class HomeModel
    {

        public string LerUsuario()
        {

            DAL objdal = new DAL();
            DataTable dt = objdal.retDataTable("select * from usuario");

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["nome"].ToString();
                }
            }

            return "Objeto não encontrado";


        }


    }
}
