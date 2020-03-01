using NerdStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain
{
    public class Pedido
    {
        public static int QUANTIDADE_MAXIMA_PEDIDOS = 15;

        public Guid ClientId { get; private set; }
        public decimal ValorTotal => _itens.Sum(i => i.CalcularValor());

        public PedidoStatus Status { get; set; }

        private List<Item> _itens { get; set; }

        public IReadOnlyCollection<Item> Itens => _itens;

        protected Pedido()
        {
            _itens = new List<Item>();
        }

        public void RemoverItem(Item item)
        {
            ValidarItemPedidoInexistente(item);

            _itens.RemoveAll(a => a.Id == item.Id);
        }

        public void AdicionarItem(Item item)
        {
            ValidarQuantidadeItensPedido(item);

            if (_itens.Any(a => a.Id == item.Id))
            {
                var itemExistente = _itens.First(a => a.Id == item.Id);
                itemExistente.AdicionarQuantidade(item.Quantidade);

                item = itemExistente;

                _itens.Remove(itemExistente);
            }

            _itens.Add(item);
        }

        public void AtualizarItem(Item item)
        {
            ValidarItemPedidoInexistente(item);
            ValidarQuantidadeItensPedido(item);

            _itens.RemoveAll(a => a.Id == item.Id);

            _itens.Add(item);
        }

        private void ValidarQuantidadeItensPedido(Item item)
        {
            if (!QuantidadeItensPedidoValido(item))
                throw new DomainException($"Quantidade de acima do permitido.");
        }

        private void ValidarItemPedidoInexistente(Item item)
        {
            if (!ItemExistente(item))
                throw new DomainException($"Item inexistente.");
        }

        private bool ItemExistente(Item item)
        {
            return _itens.Any(a => a.Id == item.Id);
        }

        private bool QuantidadeItensPedidoValido(Item item)
        {
            return item.Quantidade <= QUANTIDADE_MAXIMA_PEDIDOS;
        }

        private void TornarRascunho()
        {
            Status = PedidoStatus.Rascunho;
        }

        public static class PedidoFactory
        {
            public static Pedido CriarNovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClientId = clienteId
                };

                pedido.TornarRascunho();

                return pedido;
            }
        }
    }
}
