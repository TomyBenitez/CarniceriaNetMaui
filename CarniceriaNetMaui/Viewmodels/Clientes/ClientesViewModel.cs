using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Repositories;
using CarniceriaNetMaui.Utils;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarniceriaNetMaui.Viewmodels.Clientes
{
    public class ClientesViewModel : ObjectNotification
    {
        ClientesRepository clientesRepository = new ClientesRepository();

        private bool actividadRealizandose = false;

        public bool ActividadRealizandose
        {
            get { return actividadRealizandose; }
            set
            {
                actividadRealizandose = value;
                OnPropertyChanged();
            }
        }

        private Cliente clienteSeleccionado;

        public Cliente ClienteSeleccionado
        {
            get { return clienteSeleccionado; }
            set
            {
                clienteSeleccionado = value;
                OnPropertyChanged();
                EliminarClienteCommand.ChangeCanExecute();
                EditarClienteCommand.ChangeCanExecute();
                AgregarClienteCommand.ChangeCanExecute();

            }
        }
        private ObservableCollection<Cliente> clientes;

        public ObservableCollection<Cliente> Clientes
        {
            get { return clientes; }
            set
            {
                clientes = value;
                OnPropertyChanged();
            }
        }

        public Command ObtenerClienteCommand { get; }
        public Command EditarClienteCommand { get; }
        public Command EliminarClienteCommand { get; }
        public Command AgregarClienteCommand { get; }




        public ClientesViewModel()
        {
            Clientes = new ObservableCollection<Cliente>();
            ObtenerClienteCommand = new Command(ObtenerClientes);
            EditarClienteCommand = new Command(EditarCobrador, PermitirEditar);
            EliminarClienteCommand = new Command(EliminarCobrador, PermitirEliminar);
            AgregarClienteCommand = new Command(AgregarCobrador);
        }



        private bool PermitirEditar(object arg)
        {
            return clienteSeleccionado != null;
        }

        private bool PermitirEliminar(object arg)
        {
            return clienteSeleccionado != null;
        }

        private void AgregarCobrador(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("AbrirNuevoEditarCliente"));
        }

        private async void EliminarCobrador(object obj)
        {
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar un Cliente", $"¿Está seguro que desea eliminar el cliente {clienteSeleccionado.Apellido_Nombre}?", "Si", "No");
            if (respuesta)
            {
                ActividadRealizandose = true;
                await clientesRepository.RemoveAsync(clienteSeleccionado.Id);
                ObtenerClientes(this);
                ActividadRealizandose = false;
                clienteSeleccionado = null;
            }
        }

        private void EditarCobrador(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("EditarCliente") { ClienteAEditar = ClienteSeleccionado });
        }

        public async void ObtenerClientes(object obj)
        {
            Clientes.Clear();
            var clientes = await clientesRepository.GetAllAsync();
            foreach (var cliente in clientes)
            {
                Clientes.Add(cliente);
            }
        }
    }
}
