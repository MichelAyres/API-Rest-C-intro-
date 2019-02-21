using DesafioFlex.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace DesafioFlex.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Divida> model = new List<Divida>();
            string json;
            using (var wc = new System.Net.WebClient())
            {
                json = wc.DownloadString("https://jsonplaceholder.typicode.com/users/");
            }
            ViewBag.Clientes = JsonConvert.DeserializeObject<List<Cliente>>(json);

            var connection = DBConnection.Instance();
            try
            {
                if (connection.IsConnect())
                {
                    string query = "SELECT ID, CLIENTE_ID, MOTIVO, VALOR, DATA_DIVIDA FROM DIVIDAS WHERE EXCLUIDO_EM IS NULL";
                    var command = new MySqlCommand(query, connection.Connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Divida divida = new Divida();
                        divida.Id = int.Parse(reader.GetString(0));
                        divida.ClienteId = int.Parse(reader.GetString(1));
                        divida.Motivo = reader.GetString(2);
                        divida.Valor = decimal.Parse(reader.GetString(3));
                        divida.DataDivida = DateTime.Parse(reader.GetString(4));

                        model.Add(divida);
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return View(model);
        }

        public Boolean Deletar()
        {
            bool retorno = false;
            var connection = DBConnection.Instance();
            string id = Request.Form["hdnId"];
            try
            {
                if (connection.IsConnect())
                {
                    var command = new MySqlCommand("UPDATE DIVIDAS SET EXCLUIDO_EM = @val1 WHERE ID = @val2", connection.Connection);
                    command.Parameters.AddWithValue("@val1", DateTime.Now);
                    command.Parameters.AddWithValue("@val2", id);
                    command.Prepare();
                    retorno = command.ExecuteNonQuery() != 0;
                }
            }
            finally
            {
                connection.Close();
            }

            TempData["Success"] = FormatString("Registro removido com sucesso!");
            return retorno;
        }

        [HttpPost]
        public ActionResult Salvar()
        {
            string id = Request["hdnId"];
            string ddlCliente = Request["ddlCliente"];
            if (string.IsNullOrEmpty(ddlCliente))
            {
                TempData["Error"] = FormatString("Ocorreu um erro ao preencher o cliente.");
                return RedirectToAction("Index");
            }
            string txtMotivo = Request["txtMotivo"];
            if (string.IsNullOrEmpty(txtMotivo))
            {
                TempData["Error"] = FormatString("Ocorreu um erro ao preencher o motivo.");
                return RedirectToAction("Index");
            }
            decimal txtValor;
            if(!decimal.TryParse(Request["txtValor"], out txtValor))
            {
                TempData["Error"] = FormatString("Ocorreu um erro ao preencher o valor.");
                return RedirectToAction("Index");
            }
            DateTime txtData;
            if (!DateTime.TryParse(Request["txtData"], out txtData))
            {
                TempData["Error"] = FormatString("Ocorreu um erro ao preencher a data.");
                return RedirectToAction("Index");
            }

            var connection = DBConnection.Instance();
            try
            {
                if (connection.IsConnect())
                {
                    string query;
                    if (string.IsNullOrEmpty(id))
                    {
                        query = "INSERT INTO DIVIDAS (CLIENTE_ID, MOTIVO, VALOR, DATA_DIVIDA, MODIFICADO_EM, CRIADO_EM) VALUES (@val1, @val2, @val3, @val4, @val5, @val6)";
                    }
                    else
                    {
                        query = "UPDATE DIVIDAS SET CLIENTE_ID = @val1, MOTIVO = @val2, VALOR = @val3, DATA_DIVIDA = @val4, MODIFICADO_EM = @val5 WHERE ID = @val6";
                    }
                    var command = new MySqlCommand(query, connection.Connection);
                    command.Parameters.AddWithValue("@val1", ddlCliente);
                    command.Parameters.AddWithValue("@val2", txtMotivo);
                    command.Parameters.AddWithValue("@val3", txtValor);
                    command.Parameters.AddWithValue("@val4", txtData);
                    command.Parameters.AddWithValue("@val5", DateTime.Now);
                    if (string.IsNullOrEmpty(id))
                    {
                        command.Parameters.AddWithValue("@val6", DateTime.Now);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@val6", id);
                    }
                    command.Prepare();
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }

            TempData["Success"] = FormatString("Registro salvo com sucesso!");
            return RedirectToAction("Index");
        }

        private string FormatString(string str)
        {
            byte[] bStr = Encoding.Default.GetBytes(str);
            byte[] isoBytes = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, bStr);
            str = Encoding.UTF8.GetString(isoBytes);
            return str;
        }
    }
}