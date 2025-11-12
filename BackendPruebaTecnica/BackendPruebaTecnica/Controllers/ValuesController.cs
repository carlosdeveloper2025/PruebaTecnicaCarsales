using BackendPruebaTecnica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public PagedResponse<Episodio> Get(int page = 1, CancellationToken ct = default)
        {
            try
            {
                var url = $"episode?page={page}";
                var client = _httpClientFactory.CreateClient("RickAndMortyClient");

                // Llamar a la API de Rick and Morty 
                var response = client.GetAsync(url, ct).Result;
                response.EnsureSuccessStatusCode();

                // Leer la respuesta como una cadena JSON
                string json = response.Content.ReadAsStringAsync().Result;


                // Deserializar solo los nombres de los episodios
                var data = JsonDocument.Parse(json);
                var info = data.RootElement.GetProperty("info");

                var episodes = data.RootElement.GetProperty("results");

                var episodeList = new List<Episodio>();

                foreach (var ep in episodes.EnumerateArray())
                {
                    var episodeInfo = new Episodio
                    {
                        name = ep.GetProperty("name").GetString()!,
                        air_date = ep.GetProperty("air_date").GetString()!,
                        episode = ep.GetProperty("episode").GetString()!,
                        characters = ep.GetProperty("characters").EnumerateArray().Select(c => c.GetString()).ToList()!,
                        url = ep.GetProperty("url").GetString()!,
                        created = ep.GetProperty("created").GetString()!
                    };

                    episodeList.Add(episodeInfo);
                }

                //return episodeList;
                return new PagedResponse<Episodio> { Info = info, Results = episodeList };
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error de conexión al consumir la API: ", ex);
            }
            catch (JsonException ex)
            {
                throw new ApplicationException("Error al procesar la respuesta JSON: ", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error inesperado: ", ex);
            }
        }        

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
