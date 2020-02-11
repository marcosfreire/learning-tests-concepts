using Learning.Tests.Concepts.Tests.Fixtures;
using Xunit;

namespace Learning.Tests.Concepts.Tests.UnitTests
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTestsTests
    {
        private readonly ClienteTestsFixture _clienteFixture;

        public ClienteTestsTests(ClienteTestsFixture clienteFixture)
        {
            _clienteFixture = clienteFixture;            
        }

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Fixture Tests")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            var cliente = _clienteFixture.GerarClienteValido();
            var clienteValido = cliente.Valido();

            Assert.True(clienteValido);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fixture Tests")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            var cliente = _clienteFixture.GerarClienteInvalido();
            var clienteValido = cliente.Valido();

            Assert.False(clienteValido);
        }
    }
}
