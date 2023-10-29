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
    public class ProductosRepository
    {
        const string Url = "https://carniceriadbtomy.azurewebsites.net/api/productos";

        public async Task<Producto> AddAsync(string nombre, decimal monto)
        {
            Producto producto = new Producto()
            {
                Nombre = nombre,
                Monto = monto
            };
            HttpClient client = Helper.ObtenerClienteHttp();

            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(producto),
                    Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Producto>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> RemoveAsync(int id)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.DeleteAsync(Url + "/" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, string nombre, decimal monto)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            Producto producto = new Producto()
            {
                Id = id,
                Nombre = nombre,
                Monto = monto
            };
            var response = await client.PutAsync(Url + "/" + id, new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Producto>>(response);
        }

        public async Task<Producto> GetById(int id)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.GetStringAsync($"{Url}/{id}");
            return JsonConvert.DeserializeObject<Producto>(response);
        }


    }
}
