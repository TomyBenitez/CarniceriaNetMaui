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

namespace CarniceriaNetMaui.Viewmodels.Cobradores
{
    public class CobradoresViewModel : ObjectNotification
    {
        CobradoresRepository cobradoresRepository = new CobradoresRepository();

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

        private Cobrador cobradorSeleccionado;

        public Cobrador CobradorSeleccionado
        {
            get { return cobradorSeleccionado; }
            set
            {
                cobradorSeleccionado = value;
                OnPropertyChanged();
                EliminarCobradorCommand.ChangeCanExecute();
                EditarCobradorCommand.ChangeCanExecute();
                AgregarCobradorCommand.ChangeCanExecute();

            }
        }
        private ObservableCollection<Cobrador> cobradores;

        public ObservableCollection<Cobrador> Cobradores
        {
            get { return cobradores; }
            set
            {
                cobradores = value;
                OnPropertyChanged();
            }
        }

        public Command ObtenerCobradoresCommand { get; }
        public Command EditarCobradorCommand { get; }
        public Command EliminarCobradorCommand { get; }
        public Command AgregarCobradorCommand { get; }




        public CobradoresViewModel()
        {
            Cobradores = new ObservableCollection<Cobrador>();
            ObtenerCobradoresCommand = new Command(ObtenerCobradores);
            EditarCobradorCommand = new Command(EditarCobrador, PermitirEditar);
            EliminarCobradorCommand = new Command(EliminarCobrador, PermitirEliminar);
            AgregarCobradorCommand = new Command(AgregarCobrador);
        }



        private bool PermitirEditar(object arg)
        {
            return cobradorSeleccionado != null;
        }

        private bool PermitirEliminar(object arg)
        {
            return cobradorSeleccionado != null;
        }

        private void AgregarCobrador(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("AbrirNuevoEditarCobrador"));
        }

        private async void EliminarCobrador(object obj)
        {
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Eliminar un Cobrador", $"¿Está seguro que desea eliminar el cobrador {cobradorSeleccionado.Apellido_Nombre}?", "Si", "No");
            if (respuesta)
            {
                ActividadRealizandose = true;
                await cobradoresRepository.RemoveAsync(cobradorSeleccionado.Id);
                ObtenerCobradores(this);
                ActividadRealizandose = false;
                cobradorSeleccionado = null;
            }
        }

        private void EditarCobrador(object obj)
        {
            WeakReferenceMessenger.Default.Send(new MiMensaje("EditarCobrador") { CobradorAEditar = CobradorSeleccionado });
        }

        public async void ObtenerCobradores(object obj)
        {
            ActividadRealizandose = true;
            Cobradores.Clear();
            var cobradores = await cobradoresRepository.GetAllAsync();
            foreach (var cobrador in cobradores)
            {
                Cobradores.Add(cobrador);
            }
            ActividadRealizandose = false;
        }
    }
}
