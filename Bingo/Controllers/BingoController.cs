using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bingo.Controllers
{
    // Ok - Sortear um número entre 1 e 75
    // Precisamos colocar o texto a frente do número
    // Precisamos validar se o número já foi sorteado
    // Precisamos verificar se já sorteamos todos os números possiveis

    [Route("api/[controller]")]
    [ApiController]
    public class BingoController : ControllerBase
    {
        static List<int> sorteados = new List<int>();

        [HttpGet("Sorteio")]
        public string Sorteio()
        {
            int numeroSorteado;
            bool duplicado = true;

            if (sorteados.Count >= 75)
            {
                return "Jogo finalizado, todos os números já foram sorteados!!! ";
            }


            Random randomico = new Random();


            do
            {
                numeroSorteado = randomico.Next(1, 76);

                if (sorteados.Contains(numeroSorteado))
                {
                    duplicado = true;
                }
                else
                {
                    sorteados.Add(numeroSorteado);
                    duplicado = false;
                }



            } while (duplicado);

                return ConcatenarComLetra(numeroSorteado);
        }


        [HttpGet("IniciarNovoJogo")]

        public string IniciarNovoJogo()
        {
            sorteados.Clear();
            return "Novo jogo iniciado";
        }


        [HttpGet("ObterPenultimaBolaSorteada")]

        public string ObterPenultimaBolaSorteada()
        {
       
            return ConcatenarComLetra(sorteados[sorteados.Count - 2]);
        }

        private static string ConcatenarComLetra(int numeroSorteado)
        {
            if ((numeroSorteado >= 1) && (numeroSorteado <= 15))
            {
                return $"B-{numeroSorteado}";
            }
            else if ((numeroSorteado >= 16) && (numeroSorteado <= 30))
            {
                return $"I-{numeroSorteado}";
            }
            else if ((numeroSorteado >= 31) && (numeroSorteado <= 45))
            {
                return $"N-{numeroSorteado}";
            }
            else if ((numeroSorteado >= 46) && (numeroSorteado <= 60))
            {
                return $"G-{numeroSorteado}";
            }
            else if ((numeroSorteado >= 61) && (numeroSorteado <= 75))
            {
                return $"O-{numeroSorteado}";
            }
            else
            {
                return $"Erro: {numeroSorteado}";
            }
        }


        [HttpGet("ObterTodosNumerosSorteados")]
        public List<string> ObterTodosNumerosSorteados()
        {
            List<string> lista = new List<string>();

            foreach(var item in sorteados)
            {
                lista.Add(ConcatenarComLetra(item));
                
            }
            return lista;
        }
        [HttpGet("ObterTodosNumerosSorteadosOrdenado")]
        public List<string> ObterTodosNumerosSorteadosOrdenado()
        {
            List<string> lista = new List<string>();

            foreach (var item in sorteados.OrderBy(x => x))
            {
                lista.Add(ConcatenarComLetra(item));

            }
            return lista;
        }

        [HttpGet("VerificarSeNumeroSaiu/{numero}")]

        public string VerificarSeNumeroSaiu(int numero)
        {
            if (sorteados.Contains(numero))
            {
                return $"{ConcatenarComLetra(numero)} - JÁ FOI SORTEADO";
            }
            else
            {
                return $"{ConcatenarComLetra(numero)} - AINDA NÃO FOI SORTEADO";
            }
        }


    }
}
