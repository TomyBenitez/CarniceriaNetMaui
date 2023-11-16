using CarniceriaNetMaui.Repositories;
using CarniceriaNetMaui.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Viewmodels.Login
{
    public class LoginViewModel : ObjectNotification
    {
        private CobradoresRepository cobradoresRepository = new CobradoresRepository();
        private bool logueado = false;

        public LoginViewModel() { }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }

        private async void Login(object obj)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Credenciales Incompletas", $"Falta rellenar el campo usuario o contraseña", "Aceptar");
            }
            else
            {
                var usuariosList = await cobradoresRepository.GetAllAsync();
                foreach (var usuarios in usuariosList)
                {
                    if (Username == usuarios.Username && Helper.ObtenerHashSha256(Password) == usuarios.Password)
                    {
                        Helper.LoginCobradorId = usuarios.Id;
                        logueado = true;
                        break;
                    }
                }
                if (logueado)
                {
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Datos Incorrectos", "Ok");
                }
            }
        }
    }
}
