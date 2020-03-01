using FluentAssertions;
using System;
using Xunit;
using System.Linq;
using NerdStore.Core;
using static NerdStore.Vendas.Domain.Pedido;

namespace NerdStore.Vendas.Domain.Tests
{
    public class AdicionarPedidoTests
    {
        [Fact]
        [Trait("AdicionarPedido", "NovoPedido_StatusDeveSerRascunho")]
        public void NovoPedido_StatusDeveSerRascunho()
        {
            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());

            // Act & Assert
            pedido.Status.Should().Be(PedidoStatus.Rascunho, "Todo novo pedido deve ser iniciado como rascunho");
        }

        [Fact]
        [Trait("AdicionarPedido", "NovoPedido_ValorDeveSerZero")]
        public void NovoPedido_ValorDeveSerZero()
        {
            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());

            // Act & Assert
            pedido.ValorTotal.Should().Be(0, "O pedido não possui itens.");
        }

        [Fact]
        [Trait("AdicionarPedido", "AdicionarItem_NovoPedido_DeveAtualizarValor")]
        public void AdicionarItem_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());
            var item = new Item(Guid.NewGuid(), "Item 1", 2, 100);

            // Act
            pedido.AdicionarItem(item);

            // Assert
            pedido.ValorTotal.Should().Be(200, "Cada item vale R$100,00 e o pedido possui somente 1 item com 2 quantidades.");
        }

        [Fact]
        [Trait("AdicionarPedido", "AdicionarItem_ItemExistente_AdicionarQuantidadeSomarValor")]
        public void AdicionarItem_ItemExistente_AdicionarQuantidadeSomarValor()
        {
            var idItem = Guid.NewGuid();

            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());
            var item = new Item(idItem, "Item 1", 2, 100);
            pedido.AdicionarItem(item);

            var item2 = new Item(idItem, "Item 2", 3, 100);

            // Act
            pedido.AdicionarItem(item2);

            // Assert
            pedido.ValorTotal.Should().Be(500, "Cada item vale R$100,00 e o pedido possui somente 1 item com 5 quantidades.");
            pedido.Itens.Should().HaveCount(1, "Foi adicionado o mesmo item novamente.");
            pedido.Itens.FirstOrDefault(a => a.Id == idItem).Quantidade.Should().Be(5, "Foi adicionado o mesmo item novamente.");
        }

        [Fact]
        [Trait("AdicionarPedido", "AdicionarItem_ItemAcimaDoPermitido_DeveRetornarException")]
        public void AdicionarItem_ItemAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());
            var item = new Item(Guid.NewGuid(), "Item 1", Domain.Pedido.QUANTIDADE_MAXIMA_PEDIDOS + 1, 100);

            // Act & Assert
            Action act = () => pedido.AdicionarItem(item);
            act.Should().Throw<DomainException>().WithMessage($"Quantidade de acima do permitido.");
        }
    }
}