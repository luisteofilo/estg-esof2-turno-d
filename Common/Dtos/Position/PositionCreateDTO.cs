namespace Common.Dtos.Position
{
    public class PositionCreateDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BillingType { get; set; } // Pode ser semanal, mensal ou por hora
        
    }
}
