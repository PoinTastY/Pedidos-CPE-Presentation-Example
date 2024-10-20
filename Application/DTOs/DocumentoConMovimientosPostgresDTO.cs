using Domain.Entities.Interface;

namespace ApplicationLayer.DTOs
{
    public class DocumentoConMovimientosPostgresDTO
    {
        public Documento Documento { get; set; } = null!;
        public List<Movimiento> Movimientos { get; set; } = new();
    }
}
