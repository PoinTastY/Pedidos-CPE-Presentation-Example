using ApplicationLayer.ViewModels;
using ApplicationLayer.ViewModels.BindableObjects;
using CommunityToolkit.Maui.Views;
using Domain.Entities.Interface;
using PedidosCPE.Views.Movimientos;

namespace PedidosCPE.Views.Documentos;

public partial class DispatchDocumentosPendientesView : ContentPage
{
	private readonly VMDispatchDocumentosPendientes _viewModel;
    private readonly IDispatcherTimer _timer;

    public DispatchDocumentosPendientesView(VMDispatchDocumentosPendientes viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = _viewModel;
        
        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(15);
        _timer.Tick += async (s, e) =>
        {
            try
            {
                if (_viewModel.ProductosUnidades.Count != 0)
                {
                    return;
                }
                await _viewModel.LoadDocumentosPendientes();
                await _viewModel.FetchMovimientosAndProductos();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        };
        _timer.Start();
    }
    public DispatchDocumentosPendientesView() : this(MauiProgram.ServiceProvider.GetRequiredService<VMDispatchDocumentosPendientes>())
    {
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(500);
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await _viewModel.LoadDocumentosPendientes();
            await _viewModel.FetchMovimientosAndProductos();
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

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _timer.Stop();
    }

    private async void productoSeleccionado_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (productoSeleccionado.SelectedItem == null)
        {
            return;
        }
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            var elementoSeleccionado = (ViewProductoUnidades)productoSeleccionado.SelectedItem;
            var movimiento = _viewModel.Movimientos.FirstOrDefault(m => m.CodigoProducto == elementoSeleccionado.Producto.CCODIGOPRODUCTO);
            if (movimiento == null)
            {
                throw new Exception("No se encontr� el movimiento");
            }

            var capturar = new CaptureUnidadesView(elementoSeleccionado.Producto, movimiento);
            await Shell.Current.Navigation.PushAsync(capturar);
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

    private async void documentosList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if(documentosList.SelectedItem == null)
        {
            return;
        }
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            _viewModel.DocumentoSeleccionado = (Documento)documentosList.SelectedItem;
            await _viewModel.FetchMovimientosAndProductos();
            BtnCompletarDocumento.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
            BtnCompletarDocumento.IsVisible = false;
        }
        finally
        {
            popup.Close();
        }
    }

    private void searchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        documentosList.ItemsSource = _viewModel.DocumentosPendientes.Where(d => d.RazonSocial.Contains(searchBar.Text));
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            documentosList.ItemsSource = _viewModel.DocumentosPendientes;
            return;
        }
    }

    private async void BtnCompletarDocumento_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await _viewModel.SaveDocumentAndMovementsOnSDK();
            BtnCompletarDocumento.IsVisible = false;
            //todo: then print a ticket
            await DisplayAlert("�xito", "Documento enviado exitosamente :)", "Ok");
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
}