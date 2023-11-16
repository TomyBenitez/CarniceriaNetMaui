using CarniceriaNetMaui.Models;
using CarniceriaNetMaui.Viewmodels.Ventas;

namespace CarniceriaNetMaui.Views.Ventas;

public partial class NuevoEditarVentaView : ContentPage
{
    NuevoEditarVentasViewModel NuevoEditarVentasViewModel { get; set; }
    public NuevoEditarVentaView()
	{
		InitializeComponent();
        NuevoEditarVentasViewModel = this.BindingContext as NuevoEditarVentasViewModel;
    }
    public NuevoEditarVentaView(Venta ventaAEditar)
    {
        InitializeComponent();
        NuevoEditarVentasViewModel = this.BindingContext as NuevoEditarVentasViewModel;
        NuevoEditarVentasViewModel.VentaEditado = ventaAEditar;
        NuevoEditarVentasViewModel.CargarDatosEnPantalla();
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var productoId = (Producto)PickerProductos.SelectedItem;
        NuevoEditarVentasViewModel.ProductoId = productoId.Id;
    }

    private void PickerClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        var clientesId = (Cliente)PickerClientes.SelectedItem;
        NuevoEditarVentasViewModel.ClienteId = clientesId.Id;
    }
}