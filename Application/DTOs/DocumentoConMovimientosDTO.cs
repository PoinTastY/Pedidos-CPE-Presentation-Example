namespace ApplicationLayer.DTOs
{
    public class DocumentoConMovimientosDTO
    {
        public DocumentDTO Documento { get; set; } = new DocumentDTO();
        public List<MovimientoDTO> Movimientos { get; set; } = new List<MovimientoDTO>();
        public DocumentoConMovimientosDTO(DocumentDTO documento, List<MovimientoDTO> movimientos)
        {
            Documento = documento;
            Movimientos = movimientos;
        }
    }
}
