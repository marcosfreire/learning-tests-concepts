using FluentAssertions;
using NerdStore.Core;
using System;
using System.Linq;
using Xunit;
using static NerdStore.Vendas.Domain.Pedido;

namespace NerdStore.Vendas.Domain.Tests.Pedido
{
    public class DeletarPedidoTests
    {
        [Fact]
        [Trait("DeletarPedido", "RemoverItemInexistente_LancarDomainException")]
        public void RemoverItemInexistente_LancarDomainException()
        {
            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());

            var item = new Item(Guid.NewGuid(), "Item 1", 1, 5);
            Action act = () => pedido.RemoverItem(item);

            // Act & Assert
            act.Should().Throw<DomainException>().WithMessage("Item inexistente.");
        }

        [Fact]
        [Trait("DeletarPedido", "RemoverItem_AtualizarValorPedido")]
        public void RemoverItem_AtualizarValorPedido()
        {
            // Arrange
            var pedido = PedidoFactory.CriarNovoPedidoRascunho(Guid.NewGuid());

            var item1 = new Item(Guid.NewGuid(), "Item 1", 2, 5);
            pedido.AdicionarItem(item1);

            var idItem2 = Guid.NewGuid();
            var item2 = new Item(idItem2, "Item 1", 2, 5);

            pedido.AdicionarItem(item2);

            pedido.Itens.Should().HaveCount(2);
            pedido.ValorTotal.Should().Be(20, "Existem 2 itens com 2 quantidades e valor de R$5,00.");

            // Act
            pedido.RemoverItem(item2);
            
            // Assert
            pedido.Itens.Should().HaveCount(1,"Foi removido um item.");
            pedido.ValorTotal.Should().Be(10,"Existe somente o item 1 com 2 quantidades no valor de R$5,00.");
            pedido.Itens.First().Id.Should().NotBe(idItem2,"O item foi removido.");
        }
    }
}
