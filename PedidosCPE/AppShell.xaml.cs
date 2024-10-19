using PedidosCPE.Presentation.Views;

namespace PedidosCPE
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //register routes
            Routing.RegisterRoute(nameof(SearchProductosView), typeof(SearchProductosView));
        }
    }
}
