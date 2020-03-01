using FluentAssertions;
using NerdStore.Core;
using System;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class ItemTests
    {
        [Fact]
        [Trait("Item", "AdicionarItem_ItemAcimaDe15Unidades_DeveRetornarException")]
        public void AdicionarItem_ItemAbaixoDoPermitido_DeveRetornarException()
        {
            // Arrange            
            Action act = () => new Item(Guid.NewGuid(), "Item 1", 0, 100);

            // Act & Assert
           act.Should().Throw<DomainException>().WithMessage($"Quantidade de itens inválida. É necessário ao menos uma unidade do produto.");
        }
    }
}