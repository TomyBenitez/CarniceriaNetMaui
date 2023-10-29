using CarniceriaNetMaui.Viewmodels.Cobradores;
using CarniceriaNetMaui.Views.Cobradores;
using CommunityToolkit.Mvvm.Messaging;

namespace CarniceriaNetMaui.Views;

public partial class CobradoresView : ContentPage
{
    public CobradoresView()
    {
        InitializeComponent();
        //código para preparar la recepción de mensajes y la llamada al método RecibirMensaje
        WeakReferenceMessenger.Default.Register<MiMensaje>(this, (r, m) =>
        {
            AlRecibirMensaje(m);
        });
    }
    private async void AlRecibirMensaje(MiMensaje m)
    {
        if (m.Value == "AbrirNuevoEditarCobrador")
        {
            await Navigation.PushAsync(new NuevoEditarCobradorView());
        }
        if (m.Value == "EditarCobrador")
        {
            await Navigation.PushAsync(new NuevoEditarCobradorView(m.CobradorAEditar));
        }
        if (m.Value == "VolverACobradorView")
        {
            await Navigation.PopAsync();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewmodel = this.BindingContext as CobradoresViewModel;
        viewmodel.ObtenerCobradores(this);
        viewmodel.CobradorSeleccionado = null;
    }
}