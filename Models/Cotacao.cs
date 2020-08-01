using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAlgoThiago.Models
{
    public class Cotacao
    {
        public double CotacaoCompra { get; set; }
        public DateTime DataHoraCotacao { get; set; }

        public override string ToString()
        {
            return $"Cotacao: {CotacaoCompra}\tData da Cotacao: {DataHoraCotacao.ToString("dd/MM/yyyy HH:mm:ss")}";
        }
    }
}
