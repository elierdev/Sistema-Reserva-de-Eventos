using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Domain.Entities;
using System.Collections.Generic;

public class ServicioController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public ServicioController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync("api/Servicios");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var servicios = JsonSerializer.Deserialize<List<Servicio>>(jsonData);
                return View(servicios);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return View(new List<Servicio>());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Servicio servicio)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(servicio);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Servicios/crear", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al crear servicio: " + response.ReasonPhrase);
            }
        }

        return View(servicio);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync($"api/Servicios/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var servicio = JsonSerializer.Deserialize<Servicio>(jsonData);
                return View(servicio);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Servicio servicio)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(servicio);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Servicios/editar/{servicio.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al editar servicio: " + response.ReasonPhrase);
            }
        }

        return View(servicio);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync($"api/Servicios/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var servicio = JsonSerializer.Deserialize<Servicio>(jsonData);
                return View(servicio);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Servicio servicio)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.DeleteAsync($"api/Servicios/eliminar/{servicio.Id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        Console.WriteLine("Error al eliminar servicio: " + response.ReasonPhrase);
        return NotFound();
    }
}
