using Microsoft.AspNetCore.Mvc;

namespace SuaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        // Modelo de Pessoa
        public class Pessoa
        {
            public string Nome { get; set; }
            public float Peso { get; set; }
            public float Altura { get; set; }
        }

        // Ação para calcular o IMC
        [HttpPost("calcular-imc")]
        public IActionResult CalcularIMC([FromBody] Pessoa pessoa)
        {
            if (pessoa.Altura <= 0)
                return BadRequest("Altura deve ser maior que zero.");

            var imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);
            return Ok(new { Nome = pessoa.Nome, IMC = imc });
        }

        // Ação para consultar a tabela de IMC
        [HttpGet("consulta-tabela-imc/{imc}")]
        public IActionResult ConsultaTabelaIMC(float imc)
        {
            string descricao;

            if (imc < 18.5)
                descricao = "Abaixo do peso";
            else if (imc < 24.9)
                descricao = "Peso normal";
            else if (imc < 29.9)
                descricao = "Sobrepeso";
            else if (imc < 34.9)
                descricao = "Obesidade Grau I";
            else if (imc < 39.9)
                descricao = "Obesidade Grau II";
            else
                descricao = "Obesidade Grau III";

            return Ok(new { IMC = imc, Descricao = descricao });
        }
    }
}
