using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Domain.Entities;
using System.Collections.Generic;

public class EventoController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public EventoController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync("api/Eventos");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var eventos = JsonSerializer.Deserialize<List<Evento>>(jsonData);
                return View(eventos);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return View(new List<Evento>()); // Retorna lista vacía si hay error
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Evento evento)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(evento);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Eventos/crear", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al crear evento: " + response.ReasonPhrase);
            }
        }

        return View(evento);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync($"api/Eventos/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var evento = JsonSerializer.Deserialize<Evento>(jsonData);
                return View(evento);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Evento evento)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(evento);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Eventos/editar/{evento.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al editar evento: " + response.ReasonPhrase);
            }
        }

        return View(evento);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.DeleteAsync($"api/Eventos/eliminar/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        Console.WriteLine("Error al eliminar evento: " + response.ReasonPhrase);
        return NotFound();
    }
}
