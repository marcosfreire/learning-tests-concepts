using FluentAssertions;
using NerdStore.Core;
using System;
using System.Linq;
using Xunit;
using static NerdStore.Vendas.Domain.Pedido;

namespace NerdStore.Vendas.Domain.Tests
{
    public class AtualizarPedidoTests
    {
        [Fact]
        [Trait("AtualizarPedido", "NenhumItemAdicionado_LancarDomainException")]
        public void NenhumItemAdicionado_LancarDomainException()
        {
            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());

            var item = new Item(Guid.NewGuid(), "Item 1", 1, 5);
            Action act = () => pedido.AtualizarItem(item);

            // Act & Assert
            act.Should().Throw<DomainException>().WithMessage("Item inexistente.");
        }

        [Fact]
        [Trait("AtualizarPedido", "ItemNaoExistente_LancarDomainException")]
        public void ItemNaoExistente_LancarDomainException()
        {
            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());
            var item = new Item(Guid.NewGuid(), "Item 1", 1, 5);

            pedido.AdicionarItem(item);

            var item2 = new Item(Guid.NewGuid(), "Item 1", 1, 5);
            Action act = () => pedido.AtualizarItem(item2);

            // Act & Assert
            act.Should().Throw<DomainException>().WithMessage("Item inexistente.");
        }

        [Fact]
        [Trait("AtualizarPedido", "ItemComQuantidadeDiferenteDoQueAnteriormente_AtualizarValorEQUantidade")]
        public void ItemComQuantidadeDiferenteDoQueAnteriormente_AtualizarValorEQUantidade()
        {
            var idItemPedido = Guid.NewGuid();

            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());
            var item = new Item(idItemPedido, "Item 1", 2, 5);

            pedido.AdicionarItem(item);

            var item2 = new Item(idItemPedido, "Item 1", 5, 5);

            // Act
            pedido.AtualizarItem(item2);

            // Assert
            pedido.Itens.Should().HaveCount(1);
            pedido.Itens.First().Quantidade.Should().Be(5);
            pedido.ValorTotal.Should().Be(25, "O item anterior foi atualizado para um novo item com 5 unidades de R$5,00.");
        }

        [Fact]
        [Trait("AtualizarPedido", "ItemComQuantidadeMaiorQuePermitido_LancarDomainException")]
        public void ItemComQuantidadeMaiorQuePermitido_LancarDomainException()
        {
            var idItemPedido = Guid.NewGuid();

            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());
            var item = new Item(idItemPedido, "Item 1", 15, 5);

            pedido.AdicionarItem(item);

            var item2 = new Item(idItemPedido, "Item 1", 16, 5);

            // Act
            Action act = () => pedido.AtualizarItem(item2);

            // Assert
            act.Should().Throw<DomainException>().WithMessage("Quantidade de acima do permitido.");
        }
    }
}
