namespace Common.Dtos.Profile
{
    public class PositionDTO
    {
        public int PositionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BillingType { get; set; } // Pode ser semanal, mensal ou por hora
        public int JobId { get; set; }
        // Você pode incluir outros campos relevantes do Job se necessário
    }
}
