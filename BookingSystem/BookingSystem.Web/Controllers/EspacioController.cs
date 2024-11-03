using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Domain.Entities;
using System.Collections.Generic;

public class EspacioController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public EspacioController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync("api/Espacios");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonData); // Verifica que este JSON tenga datos
            try
            {
                var espacios = JsonSerializer.Deserialize<List<Espacio>>(jsonData);
                Console.WriteLine($"Número de espacios obtenidos: {espacios.Count}"); // Verifica la cantidad de espacios

                // Detalles de cada espacio
                foreach (var espacio in espacios)
                {
                    Console.WriteLine($"ID: {espacio.Id}, Nombre: {espacio.Nombre}, Descripción: {espacio.Descripcion}");
                }

                return View(espacios);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return View(new List<Espacio>()); // Retorna lista vacía si hay error
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Espacio espacio)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(espacio);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Espacios/crear", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al crear espacio: " + response.ReasonPhrase);
            }
        }

        return View(espacio);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync($"api/Espacios/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var espacio = JsonSerializer.Deserialize<Espacio>(jsonData);
                return View(espacio);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Espacio espacio)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(espacio);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Espacios/editar/{espacio.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al editar espacio: " + response.ReasonPhrase);
            }
        }

        return View(espacio);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.DeleteAsync($"api/Espacios/eliminar/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        Console.WriteLine("Error al eliminar espacio: " + response.ReasonPhrase);
        return NotFound();
    }
}
