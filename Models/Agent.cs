using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradingAlgoThiago.Models
{
    public class Agent
    {
        public double SellingOrder { get; }
        public double BuyingOrder { get; }
        public FundoInvestimento FundoInvestimento { get; set; }

        public Agent(double sellingOrder, double buyingOrder, FundoInvestimento patrimony)
        {
            this.SellingOrder = sellingOrder;
            this.BuyingOrder = buyingOrder;
            this.FundoInvestimento = patrimony;
        }

        public AgentAction Comprar(double currentValue)
        {
            if (!FundoInvestimento.BalanceReal.Equals((double) 0))
            {
                FundoInvestimento.BalanceDolar = ( FundoInvestimento.BalanceReal / currentValue );
                FundoInvestimento.BalanceReal -= FundoInvestimento.BalanceReal;
                return AgentAction.Compra;
            }

            return AgentAction.Compra;
        }

        public AgentAction Vender(double currentValue)
        {
            if (!FundoInvestimento.BalanceDolar.Equals((double) 0))
            {
                FundoInvestimento.BalanceReal = (FundoInvestimento.BalanceDolar * currentValue);
                FundoInvestimento.BalanceDolar -= FundoInvestimento.BalanceDolar;
                return AgentAction.Vende;
            }

            return AgentAction.Vende;
        }

        public AgentAction Inspecionar(double avg, double currentValue)
        {
            double sellingAvg = CalculateSellingAvg(avg);
            double buyingAvg = CalculateBuyingAvg(avg);


            switch (currentValue)
            {
                case var _ when currentValue <= buyingAvg: 
                    return Comprar(currentValue);
                case var _  when currentValue >= sellingAvg: 
                    return Vender(currentValue);

                default: return AgentAction.Hold;
            }
        }

        private double CalculateSellingAvg(double avg)
        {
            var result = ((SellingOrder * (avg / 100)) + avg);

            return result;

        }

        private double CalculateBuyingAvg(double avg)
        {
            var result = (avg - (BuyingOrder * (avg / 100)));

            return result;

        }
    }
}
