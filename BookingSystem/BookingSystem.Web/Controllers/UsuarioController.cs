using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Domain.Entities;
using System.Collections.Generic;

public class UsuarioController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public UsuarioController(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync("api/Usuarios");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var usuarios = JsonSerializer.Deserialize<List<Usuario>>(jsonData);
                return View(usuarios);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return View(new List<Usuario>());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Usuarios/crear", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al crear usuario: " + response.ReasonPhrase);
            }
        }

        return View(usuario);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync($"api/Usuarios/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var usuario = JsonSerializer.Deserialize<Usuario>(jsonData);
                return View(usuario);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            var client = _clientFactory.CreateClient("BookingSystemAPI");
            var jsonData = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Usuarios/editar/{usuario.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine("Error al editar usuario: " + response.ReasonPhrase);
            }
        }

        return View(usuario);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.GetAsync($"api/Usuarios/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            try
            {
                var usuario = JsonSerializer.Deserialize<Usuario>(jsonData);
                return View(usuario);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error deserializando JSON: {jsonEx.Message}");
            }
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Usuario usuario)
    {
        var client = _clientFactory.CreateClient("BookingSystemAPI");
        var response = await client.DeleteAsync($"api/Usuarios/eliminar/{usuario.Id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        Console.WriteLine("Error al eliminar usuario: " + response.ReasonPhrase);
        return NotFound();
    }
}
