using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAlgoThiago.Models
{
    public class FundoInvestimento
    {
        public double BalanceReal { get; set; }
        public double BalanceDolar { get; set; }

        public FundoInvestimento(double balance = 1000)
        {
            this.BalanceReal = balance;
            this.BalanceDolar = 0;
        }

        public decimal GetPatrimonio(double currentValue)
        {

            var result = BalanceReal == 0
                ? (decimal) (BalanceDolar * currentValue)
                : (decimal) BalanceReal;


            return decimal.Round(result, 2, MidpointRounding.AwayFromZero);
        }
    }
}
