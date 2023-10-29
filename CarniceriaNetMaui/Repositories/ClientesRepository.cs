using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Repositories
{
    public class ClientesRepository
    {
        const string Url = "https://carniceriadbtomy.azurewebsites.net/api/clientes";

        public async Task<Cliente> AddAsync(string apellido_nombre, string direccion ,string telefono)
        {
            Cliente cliente = new Cliente()
            {
                Apellido_Nombre = apellido_nombre,
                Dirección = direccion,
                Teléfono = telefono
            };
            HttpClient client = Helper.ObtenerClienteHttp();

            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(cliente),
                    Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Cliente>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> RemoveAsync(int id)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.DeleteAsync(Url + "/" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, string apellido_nombre, string direccion, string telefono)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            Cliente cliente = new Cliente()
            {
                Id = id,
                Apellido_Nombre = apellido_nombre,
                Dirección = direccion,
                Teléfono = telefono
            };
            var response = await client.PutAsync(Url + "/" + id, new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Cliente>>(response);
        }

        public async Task<Cliente> GetById(int id)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.GetStringAsync($"{Url}/{id}");
            return JsonConvert.DeserializeObject<Cliente>(response);
        }


    }
}
