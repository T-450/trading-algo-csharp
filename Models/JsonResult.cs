using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAlgoThiago.Models
{
    public class JsonResult
    {
        public double CotacaoAtual { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public double MediaMovel { get; set; }
        public double EMA { get; set; }
        public double BalanceReal { get; set; }
        public double BalanceDolar { get; set; }

        public AgentAction Action { get; set; }
        public double patrimonioTotal { get; set; }
  
    }
}
