using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TradingAlgoThiago.Models;

namespace TradingAlgoThiago.DataAccess
{
    public class JsonFileReader
    {
        public string jsonString { get; set; }
        private Queue<Cotacao> _cotacaoRepository { get; set; } = null;
        private List<JsonResult> _operationResult { get; set; }

        public JsonFileReader(string path = @"C:\Users\edwar\source\repos\TradingAlgoThiago\Data\COTACAO_2019.json")
        {
            jsonString = File.ReadAllText(path);
        }

        public Queue<Cotacao> Deserialize()
        {
            _cotacaoRepository = JsonConvert.DeserializeObject<Queue<Cotacao>>(jsonString);

            return _cotacaoRepository;
        }

        public string Serializer(List<JsonResult> opR)
        {
            _operationResult = opR;
            var jsonString = JsonConvert.SerializeObject(opR);
            return jsonString;
        }

    }
}
