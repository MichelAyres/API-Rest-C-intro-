using DesafioFlex.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
//using System.Web.Routing;
//using System.Net;
//using System.Net.Http;

namespace DesafioFlex.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Clientes = GetClientes();
            return View();
        }

        private List<Cliente> GetClientes()
        {
            string json = "";
            using (var wc = new System.Net.WebClient())
            {
                json = wc.DownloadString("https://jsonplaceholder.typicode.com/users/");
            }
            return JsonConvert.DeserializeObject<List<Cliente>>(json);
        }
    }
}