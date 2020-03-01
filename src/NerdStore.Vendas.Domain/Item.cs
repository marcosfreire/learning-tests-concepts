using NerdStore.Core;
using System;

namespace NerdStore.Vendas.Domain
{
    public class Item
    {
        public Item(Guid id, string nome, int quantidade, decimal valorUnitario)
        {
            if (quantidade <= 0)
                throw new DomainException("Quantidade de itens inválida. É necessário ao menos uma unidade do produto.");

            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; set; }

        internal decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }

        internal decimal AdicionarQuantidade(int quantidade)
        {
            return Quantidade += quantidade;
        }
    }
}