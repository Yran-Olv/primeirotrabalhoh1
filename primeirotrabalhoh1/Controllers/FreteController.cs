using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace frete.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreteController : ControllerBase
    {
        [HttpPost]
        public IActionResult CalcularFrete([FromBody] FreteRequest request)
        {

            var tarifasPorEstado = new Dictionary<string, decimal>
            {
                { "SP", 50m },
                { "RJ", 60m },
                { "MG", 55m },
                { "OUTROS", 70m }
            };

            decimal taxaPorCm3 = 0.01m;
            decimal volume = (decimal)(request.Altura * request.Largura * request.Comprimento);
            decimal tarifaEstado;
            if (!tarifasPorEstado.TryGetValue(request.UF.ToUpper(), out tarifaEstado))
            {
                tarifaEstado = tarifasPorEstado["OUTROS"];
            }

            decimal valorFrete = volume * taxaPorCm3 + tarifaEstado;
            return Ok(new { ValorFrete = valorFrete });
        }
    }

    public class FreteRequest
    {
        public string NomeProduto { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public float Largura { get; set; }
        public float Comprimento { get; set; }
        public string UF { get; set; }
    }
}
