using CarniceriaNetMaui.Enums;
using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Repositories;
using CarniceriaNetMaui.Utils;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Viewmodels.Cobradores
{
    public class NuevoEditarCobradorViewModel : ObjectNotification
    {
        CobradoresRepository cobradoresRepository = new CobradoresRepository();
        public IList<TipoCobradorList> RangosUsuarios { get; set; }
        public Cobrador CobradorEditado { get; set; }

        private string apellido_Nombre;

        public string Apellido_Nombre
        {
            get { return apellido_Nombre; }
            set
            {
                apellido_Nombre = value;
                OnPropertyChanged();
            }
        }
        private string dirección;

        public string Dirección
        {
            get { return dirección; }
            set
            {
                dirección = value;
                OnPropertyChanged();
            }
        }

        private string teléfono;

        public string Teléfono
        {
            get { return teléfono; }
            set
            {
                teléfono = value;
                OnPropertyChanged();
            }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private bool _permitirEdicionContraseña;
        public bool PermitirEdicionContraseña
        {
            get { return _permitirEdicionContraseña; }
            set
            {
                _permitirEdicionContraseña = value;
                OnPropertyChanged(nameof(PermitirEdicionContraseña));
            }
        }
        public bool IsContraseñaEnabled => PermitirEdicionContraseña;

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private TipoCobradorEnum tipoCobradorEnum;

        public TipoCobradorEnum TipoCobradorEnum
        {
            get { return tipoCobradorEnum; }
            set
            {
                tipoCobradorEnum = value;
                OnPropertyChanged();
            }
        }

        public Command GuardarCommand { get; }
        public Command CancelarCommand { get; }

        public NuevoEditarCobradorViewModel()
        {
            GuardarCommand = new Command(Guardar);
            CancelarCommand = new Command(Cancelar);
            RangosUsuarios = new List<TipoCobradorList>();
            RangosUsuarios.Add(new TipoCobradorList
            {
                Nombre = "Administrador"
            });
            RangosUsuarios.Add(new TipoCobradorList
            {
                Nombre = "Encargado"
            });
            RangosUsuarios.Add(new TipoCobradorList
            {
                Nombre = "Empleado"
            });
        }

        private void Cancelar(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("VolverACobradorView"));
        }

        private async void Guardar(object obj)
        {
            tipoCobradorEnum = (TipoCobradorEnum)Helper.TypeUserSelection;
            if (CobradorEditado == null)
            {
                Cobrador cobrador = await cobradoresRepository.AddAsync(apellido_Nombre,dirección,teléfono,username,Helper.ObtenerHashSha256(password),tipoCobradorEnum);
            }
            else
            {
                if (PermitirEdicionContraseña)
                {
                    await cobradoresRepository.UpdateAsync(CobradorEditado.Id, apellido_Nombre, dirección, teléfono, username, Helper.ObtenerHashSha256(password), tipoCobradorEnum);
                }
                else
                {
                    await cobradoresRepository.UpdateNotPassAsync(CobradorEditado.Id, apellido_Nombre, dirección, teléfono, username, password, tipoCobradorEnum);
                }
            }
            WeakReferenceMessenger.Default.Send(new MiMensaje("VolverACobradorView"));
        }

        public void CargarDatosEnPantalla()
        {
            Apellido_Nombre = CobradorEditado.Apellido_Nombre;
            Dirección = CobradorEditado.Dirección;
            Teléfono = CobradorEditado.Teléfono;
            Username = CobradorEditado.Username;
            Password = CobradorEditado.Password;
            TipoCobradorEnum = CobradorEditado.Tipocobrador;
        }
    }
}
