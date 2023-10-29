using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Repositories;
using CarniceriaNetMaui.Utils;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Viewmodels.Clientes
{
    public class NuevoEditarClienteViewModel : ObjectNotification
    {
        ClientesRepository clientesRepository = new ClientesRepository();
        public Cliente ClienteEditado { get; set; }

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
        public Command GuardarCommand { get; }
        public Command CancelarCommand { get; }

        public NuevoEditarClienteViewModel()
        {
            GuardarCommand = new Command(Guardar);
            CancelarCommand = new Command(Cancelar);
        }

        private void Cancelar(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("VolverAClienteView"));
        }

        private async void Guardar(object obj)
        {
            if (ClienteEditado == null)
            {
                Cliente cliente = await clientesRepository.AddAsync(apellido_Nombre, dirección, teléfono);
            }
            else
            {
                await clientesRepository.UpdateAsync(ClienteEditado.Id, apellido_Nombre, dirección, teléfono);
            }
            WeakReferenceMessenger.Default.Send(new MiMensaje("VolverAClienteView"));
        }

        public void CargarDatosEnPantalla()
        {
            Apellido_Nombre = ClienteEditado.Apellido_Nombre;
            Dirección = ClienteEditado.Dirección;
            Teléfono = ClienteEditado.Teléfono;
        }
    }
}
