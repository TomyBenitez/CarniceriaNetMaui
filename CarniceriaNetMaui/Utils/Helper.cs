using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Utils
{
    public class Helper
    {
        public static HttpClient ObtenerClienteHttp()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        const int typeUserSelection = 0;

        public static int TypeUserSelection
        {
            get => Preferences.Get(nameof(TypeUserSelection), typeUserSelection);
            set => Preferences.Set(nameof(TypeUserSelection), value);
        }

        const int loginCobradorId = 0;

        public static int LoginCobradorId
        {
            get => Preferences.Get(nameof(LoginCobradorId), loginCobradorId);
            set => Preferences.Set(nameof(LoginCobradorId), value);
        }

        public static string ObtenerHashSha256(string textoAEncriptar)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(textoAEncriptar));
            StringBuilder hashObtenido = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                hashObtenido.Append(bytes[i].ToString("x2"));
            }
            return hashObtenido.ToString();
        }
    }
}
