using ApplicationLayer.DTOs;
using ApplicationLayer.ViewModels.Base;
using Domain.Entities.Interface;
using Domain.Interfaces.Services.ApiServices.Movimientos;
using Domain.SDK_Comercial;
using System.IO.Ports;

namespace ApplicationLayer.ViewModels
{
    public class VMCaptureUnidades : ViewModelBase
    {
        private readonly IMovimientoService _movimientoService = null!;
        private Movimiento _movimiento = null!;
        private ProductoDTO _producto = null!;
        private SDKSettings _settings = null!;

        public VMCaptureUnidades(IMovimientoService movimientoService, SDKSettings sDKSettings)
        {
            _movimientoService = movimientoService;
            _settings = sDKSettings;
        }

        public VMCaptureUnidades() { }

        public Movimiento Movimiento
        {
            get => _movimiento;
            set
            {
                _movimiento = value;
                OnPropertyChanged(nameof(Movimiento));
            }
        }

        public ProductoDTO Producto
        {
            get => _producto;
            set
            {
                _producto = value;
                OnPropertyChanged(nameof(Producto));
            }
        }

        public string LeerPeso()
        {
            //Configuracion para RHINO BAR-6x
            SerialPort serialPort = new SerialPort(_settings.PuertoBascula, _settings.BaudRateBascula, Parity.None, _settings.DataBitsBascula, StopBits.One);
            serialPort.Handshake = Handshake.None;
            serialPort.WriteTimeout = 500;
            serialPort.ReadTimeout = 500;

            try
            {
                serialPort.Open();

                serialPort.Write(_settings.WriteCommandBascula);

                Thread.Sleep(500);

                // Lee la respuesta de la báscula
                string response = serialPort.ReadExisting();
                if (response != null)
                {
                    response = response.Replace(_settings.SufijoBascula, "");
                    return response.Trim();
                }
                throw new Exception("No se recibió respuesta de la báscula");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer el peso de la báscula {ex.Message}", ex);
            }
            finally
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
        }

        public async Task UpdateMovimiento()
        {
            await _movimientoService.UpdateMovimientos(new List<Movimiento> { Movimiento });
        }
    }
}
