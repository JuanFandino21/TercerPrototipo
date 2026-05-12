using POCSuscripcionCliente.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace POCSuscripcionCliente.Services
{
    public class ApiService
    {
        private readonly HttpClient client;
        private readonly string baseUrl = "http://localhost:8090";

        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiService()
        {
            client = new HttpClient();
        }

        // USUARIOS
        public async Task<List<Usuario>> GetUsuarios()
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl + "/usuarios/all");

            if (!response.IsSuccessStatusCode)
            {
                return new List<Usuario>();
            }

            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Usuario>();
            }

            return JsonSerializer.Deserialize<List<Usuario>>(json, jsonOptions);
        }

        public async Task<Usuario> GetUsuarioByCodigo(int codigo)
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl + "/usuarios/find/" + codigo);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            return JsonSerializer.Deserialize<Usuario>(json, jsonOptions);
        }

        public async Task<List<Usuario>> BuscarUsuarioPorNombre(string nombre)
        {
            HttpResponseMessage response = await client.GetAsync(
                baseUrl + "/usuarios/buscar?nombre=" + Uri.EscapeDataString(nombre)
            );

            if (!response.IsSuccessStatusCode)
            {
                return new List<Usuario>();
            }

            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Usuario>();
            }

            return JsonSerializer.Deserialize<List<Usuario>>(json, jsonOptions);
        }

        public async Task<string> CrearUsuario(Usuario usuario)
        {
            string json = JsonSerializer.Serialize(usuario, jsonOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(baseUrl + "/usuarios/", content);
            string respuesta = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return "OK";
            }

            return respuesta;
        }

        public async Task<string> ActualizarUsuario(int codigo, Usuario usuario)
        {
            string json = JsonSerializer.Serialize(usuario, jsonOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(baseUrl + "/usuarios/update/" + codigo, content);
            string respuesta = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return "OK";
            }

            return respuesta;
        }

        public async Task<string> EliminarUsuario(int codigo)
        {
            HttpResponseMessage response = await client.DeleteAsync(baseUrl + "/usuarios/delete/" + codigo);
            string respuesta = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return "OK";
            }

            return respuesta;
        }


        // SUSCRIPCIONES

        public async Task<List<SuscripcionStreaming>> GetSuscripciones()
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl + "/streaming/all");

            if (!response.IsSuccessStatusCode)
            {
                return new List<SuscripcionStreaming>();
            }

            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<SuscripcionStreaming>();
            }

            return JsonSerializer.Deserialize<List<SuscripcionStreaming>>(json, jsonOptions);
        }

        public async Task<SuscripcionStreaming> GetSuscripcionById(int id)
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl + "/streaming/find/" + id);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            return JsonSerializer.Deserialize<SuscripcionStreaming>(json, jsonOptions);
        }

        public async Task<string> CrearSuscripcion(SuscripcionStreaming suscripcion)
        {
            string json = JsonSerializer.Serialize(suscripcion, jsonOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(baseUrl + "/streaming/", content);
            string respuesta = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return "OK";
            }

            return respuesta;
        }

        public async Task<string> ActualizarSuscripcion(int id, SuscripcionStreaming suscripcion)
        {
            string json = JsonSerializer.Serialize(suscripcion, jsonOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(baseUrl + "/streaming/update/" + id, content);
            string respuesta = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return "OK";
            }

            return respuesta;
        }

        public async Task<string> EliminarSuscripcion(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(baseUrl + "/streaming/delete/" + id);
            string respuesta = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return "OK";
            }

            return respuesta;
        }
    }
}