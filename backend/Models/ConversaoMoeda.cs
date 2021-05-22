namespace backend.Models
{
    public class ConversaoMoeda
    {
        public int moedaOrigemId { get; set; }
        public int moedaDestinoId { get; set; }
        public decimal valor { get; set; }
    }
}