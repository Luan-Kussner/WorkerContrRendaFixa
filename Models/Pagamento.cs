namespace WorkerContrRendaFixa.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public int ContratacaoId { get; set; }
        public Contratacao Contratacao { get; set; }
    }
}
