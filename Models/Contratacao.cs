namespace WorkerContrRendaFixa.Models
{
    public class Contratacao
    {
        public int Id { get; set; }
        public DateTime DataContratacao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
        public bool Pago { get; set; }
        public int ContratanteId { get; set; }
        public int ProdutoId { get; set; }
        public ICollection<Pagamento> Pagamentos { get; set; }
    }
}
