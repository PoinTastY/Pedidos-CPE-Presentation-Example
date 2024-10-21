using PedidosCPE.Views.Documentos;
using PedidosCPE.Views.Productos;

namespace PedidosCPE
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateDocumentoView));
        }

        private async void BtnVerPedidosPendientes_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DispatchDocumentosPendientesView));
        }
    }
}
