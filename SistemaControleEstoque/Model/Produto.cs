using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaControleEstoque.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoCusto { get; set; }
        public int EstoqueMinimo { get; set; }
        public string Categoria { get; set; }

        // --- NOVAS PROPRIEDADES (Sintaxe para .NET 4.7) ---
        public byte[] Foto { get; set; } // Pode ser nulo por padrão
        public string LocalizacaoEstoque { get; set; } // Pode ser nulo por padrão
        public DateTime DataCadastro { get; set; }
        public DateTime? DataVencimento { get; set; } // '?' é usado para tipos de valor (como DateTime) que podem ser nulos
    }
}
