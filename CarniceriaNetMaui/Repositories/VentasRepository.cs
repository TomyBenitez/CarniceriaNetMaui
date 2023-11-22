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
    public class VentasRepository
    {
        const string Url = "https://carniceriadbtomy.azurewebsites.net/api/ventas";

        public async Task<Venta> AddAsync(int clienteId, int cobradorId, int productoId, int cantidad, decimal monto)
        {
            Venta venta = new Venta()
            {
                ClienteId = clienteId,
                CobradorId = cobradorId,
                ProductoId = productoId,
                Cantidad = cantidad,
                Fecha = DateTime.Now,
                MontoTotal = monto
            };
            HttpClient client = Helper.ObtenerClienteHttp();

            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(venta),
                    Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Venta>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> RemoveAsync(int id)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.DeleteAsync(Url + "/" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, int clienteId, int cobradorId, int productoId, int cantidad, decimal monto)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            Venta venta = new Venta()
            {
                Id = id,
                ClienteId = clienteId,
                CobradorId = cobradorId,
                ProductoId = productoId,
                Cantidad = cantidad,
                Fecha = DateTime.Now,
                MontoTotal = monto
            };
            var response = await client.PutAsync(Url + "/" + id, new StringContent(JsonConvert.SerializeObject(venta), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Venta>> GetAllAsync()
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Venta>>(response);
        }

        public async Task<Venta> GetById(int id)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.GetStringAsync($"{Url}/{id}");
            return JsonConvert.DeserializeObject<Venta>(response);
        }


    }
}
