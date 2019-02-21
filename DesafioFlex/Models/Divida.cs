using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioFlex.Models
{
    public class Divida
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Motivo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDivida { get; set; }
        public DateTime? ExcluidoEm { get; set; }
    }
}