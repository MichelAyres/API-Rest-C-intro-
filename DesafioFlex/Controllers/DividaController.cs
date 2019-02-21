using DesafioFlex.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DesafioFlex.Controllers
{
    public class DividaController : ApiController
    {
        // GET: api/Divida
        public List<Divida> Get()
        {
            List<Divida> dividas = new List<Divida>();
            Divida divida;
            var connection = DBConnection.Instance();
            try
            {
                if (connection.IsConnect())
                {
                    var command = new MySqlCommand("SELECT ID, CLIENTE_ID, MOTIVO, VALOR, DATA_DIVIDA FROM DIVIDAS WHERE EXCLUIDO_EM IS NULL", connection.Connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        divida = new Divida
                        {
                            Id = int.Parse(reader.GetString(0)),
                            ClienteId = int.Parse(reader.GetString(1)),
                            Motivo = reader.GetString(2),
                            Valor = decimal.Parse(reader.GetString(3)),
                            DataDivida = DateTime.Parse(reader.GetString(4))
                        };

                        dividas.Add(divida);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return dividas;
        }

        // GET: api/Divida/5
        public Divida Get(int id)
        {
            Divida divida = new Divida();
            var connection = DBConnection.Instance();
            try
            {
                if (connection.IsConnect())
                {
                    var command = new MySqlCommand("SELECT ID, CLIENTE_ID, MOTIVO, VALOR, DATA_DIVIDA FROM DIVIDAS WHERE EXCLUIDO_EM IS NULL AND ID = @val1", connection.Connection);
                    command.Parameters.AddWithValue("@val1", id);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        divida = new Divida
                        {
                            Id = int.Parse(reader.GetString(0)),
                            ClienteId = int.Parse(reader.GetString(1)),
                            Motivo = reader.GetString(2),
                            Valor = decimal.Parse(reader.GetString(3)),
                            DataDivida = DateTime.Parse(reader.GetString(4))
                        };
                        break;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return divida;
        }

        // POST: api/Divida
        public bool Post([FromBody]Divida divida)
        {
            return Salvar(divida);
        }

        // PUT: api/Divida/5
        public bool Put(int id, [FromBody]Divida divida)
        {
            return Salvar(divida);
        }

        // DELETE: api/Divida/5
        public bool Delete(int id)
        {
            bool retorno = false;
            var connection = DBConnection.Instance();
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

            return retorno;
        }

        private bool Salvar(Divida divida)
        {
            bool retorno = false;
            var connection = DBConnection.Instance();
            try
            {
                if (connection.IsConnect())
                {
                    string query;
                    if (divida.Id == 0)
                    {
                        query = "INSERT INTO DIVIDAS (CLIENTE_ID, MOTIVO, VALOR, DATA_DIVIDA, MODIFICADO_EM, CRIADO_EM) VALUES (@val1, @val2, @val3, @val4, @val5, @val6)";
                    }
                    else
                    {
                        query = "UPDATE DIVIDAS SET CLIENTE_ID = @val1, MOTIVO = @val2, VALOR = @val3, DATA_DIVIDA = @val4, MODIFICADO_EM = @val5 WHERE ID = @val6";
                    }
                    var command = new MySqlCommand(query, connection.Connection);
                    command.Parameters.AddWithValue("@val1", divida.ClienteId);
                    command.Parameters.AddWithValue("@val2", divida.Motivo);
                    command.Parameters.AddWithValue("@val3", divida.Valor);
                    command.Parameters.AddWithValue("@val4", divida.DataDivida);
                    command.Parameters.AddWithValue("@val5", DateTime.Now);
                    if (divida.Id == 0)
                    {
                        command.Parameters.AddWithValue("@val6", DateTime.Now);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@val6", divida.Id);
                    }
                    command.Prepare();
                    retorno = command.ExecuteNonQuery() > 0;
                }
            }
            finally
            {
                connection.Close();
            }
            return retorno;
        }
    }
}
