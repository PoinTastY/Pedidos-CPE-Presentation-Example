using PedidosCPE.Views.ClientesProveedores;
using PedidosCPE.Views.Documentos;
using PedidosCPE.Views.Productos;

namespace PedidosCPE
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //routing
            Routing.RegisterRoute(nameof(SearchProductosView), typeof(SearchProductosView));
            Routing.RegisterRoute(nameof(CreateDocumentoView), typeof(CreateDocumentoView));
            Routing.RegisterRoute(nameof(SearchClientesProveedoresView), typeof(SearchClientesProveedoresView));
        }
    }
}
