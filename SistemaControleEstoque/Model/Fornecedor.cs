namespace SistemaControleEstoque.Model
{
    public class Fornecedor
    {
        public int IdFornecedor { get; set; }
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}