using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TradingAlgoThiago.Models
{
    public static class Calculator
    {
        private static double _smaSum { get; set; } = 0;
        private static List<JsonResult> _jsonResult { get; set; } = null;
        private static Stack<double> _emaStack { get; set; } = null;

        public static List<JsonResult> CalcularMediaMovel(Agent agente, Queue<Cotacao> data, int periodo = 20)
        {
            // Inicializar campos
            _emaStack = new Stack<double>();

            _jsonResult = new List<JsonResult>
            {
                new JsonResult()
                {
                    CotacaoAtual = 0,
                    BalanceReal = (double) agente.FundoInvestimento.BalanceReal,
                    BalanceDolar = (double) agente.FundoInvestimento.BalanceDolar,
                    patrimonioTotal = agente.FundoInvestimento.BalanceReal
                }
            };

            // Calcular a primeira media movel exponencial
            var cotacaoAtual = data.Take(periodo - 1).First();
            _smaSum = (data.Take(periodo - 1)
                           .Sum(c => c.CotacaoCompra));

            _smaSum /= (periodo - 1);
            _emaStack.Push(_smaSum);
            
            do
            {
                if (periodo >= data.Count) periodo = data.Count;
                // Pega a cotação de hoje
                cotacaoAtual = data.Take(periodo).Skip(periodo - 1).First();
                // Media movel de hoje
                _smaSum = ((data.Take(periodo).Sum(c => c.CotacaoCompra)) / periodo);


                var ema = CalculaMediaMovelExponencial(cotacaoAtual.CotacaoCompra, n: (double) periodo);
                
                var action = agente.Inspecionar(ema, cotacaoAtual.CotacaoCompra);
                
                data.Dequeue();
                
                _emaStack.Push(ema);

                _jsonResult.Add(new JsonResult()
                    {
                        CotacaoAtual = cotacaoAtual.CotacaoCompra,
                        TransactionDateTime = cotacaoAtual.DataHoraCotacao,
                        MediaMovel = _smaSum,
                        EMA = ema,
                        BalanceReal = (double) decimal.Round((decimal) agente.FundoInvestimento.BalanceReal, 2, MidpointRounding.AwayFromZero),
                        BalanceDolar = (double) decimal.Round((decimal) agente.FundoInvestimento.BalanceDolar,2, MidpointRounding.AwayFromZero),
                        Action = action,
                        patrimonioTotal = (double) agente.FundoInvestimento.GetPatrimonio(cotacaoAtual.CotacaoCompra)
                    }
                );

            } while (data.Count != 0);
            _emaStack = null;

            return _jsonResult;
        }

        public static double CalculaMediaMovelExponencial(double valorDeHoje, double n)
        {
            // valor de hoje - emaDeOntem * (2/n+1) + emaDeOntem
            var emaDeOntem = _emaStack.Peek();
            var ema = (((valorDeHoje - emaDeOntem)*( ( 2/( n+1 ) ) )) + emaDeOntem);
            return ema;
        }
    }
}
