using Xunit;

namespace LearningTests.tests.test_fixtures
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTesteInvalido
    {
        readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteTesteInvalido(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Trait("Cliente", "Cliente_NovoCliente_DeveEstarInvalido")]
        [Fact]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var valido = cliente.Valido();

            // Assert
            Assert.False(valido);
            Assert.Single(cliente.ValidationResult.Errors);
        }
    }
}
