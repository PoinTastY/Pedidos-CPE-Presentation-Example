using ApplicationLayer.ViewModels;
using CommunityToolkit.Maui.Views;
using PedidosCPE.Views.ClientesProveedores;
using PedidosCPE.Views.Events;
using PedidosCPE.Views.Productos;

namespace PedidosCPE.Views.Documentos;

public partial class CreateDocumentoView : ContentPage
{
	private readonly VMCreateDocumento _viewModel;
    public CreateDocumentoView(VMCreateDocumento viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
	public CreateDocumentoView() : this(MauiProgram.ServiceProvider.GetRequiredService<VMCreateDocumento>())
    {
    }

    private async void OnProductoSelected(object? sender, ProductoSelectedEventArgs e)
    {
        if (e.ProductosSeleccionados != null)
        {
            foreach (var producto in e.ProductosSeleccionados)
            {
                //validate if the product is already in the list
                if (_viewModel.Productos.Any(p => p.CIDPRODUCTO == producto.CIDPRODUCTO))
                {
                    continue;
                }
                _viewModel.Productos.Add(producto);
            }
            await Shell.Current.Navigation.PopAsync();
        }
    }

    private async void OnClienteProveedorSelected(object? sender, CteProvSelectedEventArgs e)
    {
        if (e.ClienteProveedorSeleccionado != null)
        {
            _viewModel.ClienteProveedorSeleccionado = e.ClienteProveedorSeleccionado;
            await Shell.Current.Navigation.PopAsync();
        }
    }

    private void DatePickerFecha_DateSelected(object sender, DateChangedEventArgs e)
    {
        if(e.NewDate < DateTime.Now)
        {
            DisplayAlert("Fecha Incorrecta", "La fecha no puede ser menor a la fecha actual", "Ok");
            DatePickerFecha.Date = DateTime.Now;
            return;
        }
        _viewModel.Documento.Fecha = e.NewDate.ToString("MM/dd/yyyy");
    }

    private async void BtnAgregarProductos_Clicked(object sender, EventArgs e)
    {
        var productos = new SearchProductosView();
        productos.ProductosSeleccionados += OnProductoSelected;
        await Shell.Current.Navigation.PushAsync(productos);
    }

    private async void BtnGuardarPedido_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await _viewModel.EnviarDocumentoMovimientos();
            await DisplayAlert("Guardado", "El documento ha sido guardado correctamente", "Ok");
            await Shell.Current.Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
        }
        finally
        {
            popup.Close();
        }
    }

    private async void BtnSeleccionarSocio_Clicked(object sender, EventArgs e)
    {
        var clientes = new SearchClientesProveedoresView();
        clientes.ClienteProveedorSeleccionado += OnClienteProveedorSelected;
        await Shell.Current.Navigation.PushAsync(clientes);
    }
}