using CarniceriaNetMaui.Enums;
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
    public class CobradoresRepository
    {
        const string Url = "https://carniceriadbtomy.azurewebsites.net/api/cobradores";

        public async Task<Cobrador> AddAsync(string nombre, string direccion, string telefono, string username, string password, TipoCobradorEnum tipocobradorenum)
        {
            Cobrador cobrador = new Cobrador()
            {
                Apellido_Nombre = nombre,
                Dirección = direccion,
                Teléfono = telefono,
                Username = username,
                Password = password,
                Tipocobrador = tipocobradorenum
            };
            HttpClient client = Helper.ObtenerClienteHttp();

            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(cobrador),
                    Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Cobrador>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> RemoveAsync(int id)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.DeleteAsync(Url + "/" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id,string nombre, string direccion, string telefono, string username, string password, TipoCobradorEnum tipocobradorenum)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            Cobrador cobrador = new Cobrador()
            {
                Id = id,
                Apellido_Nombre = nombre,
                Dirección = direccion,
                Teléfono = telefono,
                Username = username,
                Password = password,
                Tipocobrador = tipocobradorenum
            };
            var response = await client.PutAsync(Url + "/" + id, new StringContent(JsonConvert.SerializeObject(cobrador), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateNotPassAsync(int id, string nombre, string direccion, string telefono, string username,string password, TipoCobradorEnum tipocobradorenum)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            Cobrador cobrador = new Cobrador()
            {
                Id = id,
                Apellido_Nombre = nombre,
                Dirección = direccion,
                Teléfono = telefono,
                Username = username,
                Password = password,
                Tipocobrador = tipocobradorenum
            };
            var response = await client.PutAsync(Url + "/" + id, new StringContent(JsonConvert.SerializeObject(cobrador), Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Cobrador>> GetAllAsync()
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Cobrador>>(response);
        }

        public async Task<Cobrador> GetById(int id)
        {
            HttpClient client = Helper.ObtenerClienteHttp();
            var response = await client.GetStringAsync($"{Url}/{id}");
            return JsonConvert.DeserializeObject<Cobrador>(response);
        }


    }
}
