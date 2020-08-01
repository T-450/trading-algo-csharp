using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TradingAlgoThiago.DataAccess;
using TradingAlgoThiago.Models;

namespace TradingAlgoThiago.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly JsonFileReader _cotacoesObjects = new JsonFileReader();

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var cotacoesObjects = new JsonFileReader().Deserialize();

            // Lista<ValoresQueVoceComprou> { ValorDeCompraQueVoceComprou }
            // Lista.Add(val);
            // var val = lista.OrderBy(c => c.ValorQueVoceComprou).First();
            // if(val == valorAtual) // faca alguma coisa
            // lista.add(valorAtual);


            return Ok(cotacoesObjects);
        }


        [HttpPost]
        public ActionResult<IEnumerable<Models.JsonResult>> Post(User user)
        {
            var dataSet = _cotacoesObjects.Deserialize();

            var fundoInvestimento = new FundoInvestimento(user.patrimonioInicial);
            var agente = new Agent(user.precoVenda, user.precoCompra, fundoInvestimento);
            List<Models.JsonResult> jsonResult = Calculator.CalcularMediaMovel(agente, dataSet);

            return Ok(jsonResult);


        }
    }
}
