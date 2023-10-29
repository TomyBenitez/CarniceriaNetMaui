using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Utils;
using CarniceriaNetMaui.Viewmodels.Cobradores;

namespace CarniceriaNetMaui.Views.Cobradores;

public partial class NuevoEditarCobradorView : ContentPage
{
	NuevoEditarCobradorViewModel NuevoEditarCobradorViewModel { get; set; }
	public NuevoEditarCobradorView()
	{
		InitializeComponent();
	}

    public NuevoEditarCobradorView(Cobrador cobradorAEditar)
    {
        InitializeComponent();
        NuevoEditarCobradorViewModel = this.BindingContext as NuevoEditarCobradorViewModel;
        NuevoEditarCobradorViewModel.CobradorEditado = cobradorAEditar;
        NuevoEditarCobradorViewModel.CargarDatosEnPantalla();
    }

    private void myPickerOfTypeUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        Helper.TypeUserSelection = selectedIndex;
    }
}