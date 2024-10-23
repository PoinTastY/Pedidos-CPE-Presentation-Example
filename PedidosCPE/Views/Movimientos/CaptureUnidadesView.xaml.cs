using ApplicationLayer.DTOs;
using ApplicationLayer.ViewModels;
using CommunityToolkit.Maui.Views;
using Domain.Entities.Interface;

namespace PedidosCPE.Views.Movimientos;

public partial class CaptureUnidadesView : ContentPage
{
    private readonly VMCaptureUnidades _viewModel;

    public CaptureUnidadesView(VMCaptureUnidades vMDispatchDocumentosPendientes)
    {
        InitializeComponent();
        _viewModel = vMDispatchDocumentosPendientes;
        BindingContext = _viewModel;
    }

    public CaptureUnidadesView(ProductoDTO producto, Movimiento movimiento) : this(MauiProgram.ServiceProvider.GetRequiredService<VMCaptureUnidades>())
    {
        _viewModel.Producto = producto;
        _viewModel.Movimiento = movimiento;
        LblUnidades.Text = "Unidades: " + _viewModel.Movimiento.Surtidas.ToString("F3") + "/" + _viewModel.Movimiento.Unidades.ToString("F3");
    }
    
    public CaptureUnidadesView() : this(MauiProgram.ServiceProvider.GetRequiredService<VMCaptureUnidades>())
    {
	}

    private async void BtnCapturarPeso_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await Task.Run(() =>
            {
                var peso = _viewModel.LeerPeso();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    EntryPeso.Text = peso;
                });
            });

            var valorCapturado = EntryPeso.Text;

            if (string.IsNullOrEmpty(valorCapturado))
            {
                throw new Exception("No se captur� ning�n valor");
            }

            var peso = double.Parse(valorCapturado);
            var addOrReplace = await DisplayAlert("Capturar Peso", $"�Desea Sumar o Reemplazar el valor actual?", "Sumar", "Reemplazar");
            if (addOrReplace)
            {
                _viewModel.Movimiento.Surtidas += peso;
            }
            else
            {
                _viewModel.Movimiento.Surtidas = peso;
            }
            LblUnidades.Text = "Unidades: " + _viewModel.Movimiento.Surtidas.ToString("F3") + "/" + _viewModel.Movimiento.Unidades.ToString("F3");
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

    private async void BtnTerminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            await _viewModel.UpdateMovimiento();
            await Shell.Current.Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
        }
    }
}