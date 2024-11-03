using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Domain.Entities;
using System.Collections.Generic;

public class ReservaController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public ReservaController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync("api/Reservas");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var reservas = JsonSerializer.Deserialize<List<Reserva>>(jsonData);
                return View(reservas);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return View(new List<Reserva>()); // Retorna lista vacía si hay error
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Reserva reserva)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(reserva);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Reservas/crear", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al crear reserva: " + response.ReasonPhrase);
            }
        }

        return View(reserva);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync($"api/Reservas/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var reserva = JsonSerializer.Deserialize<Reserva>(jsonData);
                return View(reserva);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Reserva reserva)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(reserva);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Reservas/editar/{reserva.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al editar reserva: " + response.ReasonPhrase);
            }
        }

        return View(reserva);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.DeleteAsync($"api/Reservas/eliminar/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        Console.WriteLine("Error al eliminar reserva: " + response.ReasonPhrase);
        return NotFound();
    }
}
