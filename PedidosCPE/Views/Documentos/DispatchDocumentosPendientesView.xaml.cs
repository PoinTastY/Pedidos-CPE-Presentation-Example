using ApplicationLayer.ViewModels;
using CommunityToolkit.Maui.Views;
using Domain.Entities.Interface;

namespace PedidosCPE.Views.Documentos;

public partial class DispatchDocumentosPendientesView : ContentPage
{
	private readonly VMDispatchDocumentosPendientes _viewModel;
    public DispatchDocumentosPendientesView(VMDispatchDocumentosPendientes viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    public DispatchDocumentosPendientesView() : this(MauiProgram.ServiceProvider.GetRequiredService<VMDispatchDocumentosPendientes>())
    {
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(1000);
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await _viewModel.LoadDocumentosPendientes();
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

    private void productoSeleccionado_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

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

    private void searchBar_SearchButtonPressed(object sender, EventArgs e)
    {

    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}