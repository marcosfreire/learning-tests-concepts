using Xunit;

namespace LearningTests.tests.test_fixtures
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTesteValido
    {
        readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteTesteValido(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Trait("Cliente", "Cliente_NovoCliente_DeveEstarValido")]
        [Fact]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var valido = cliente.Valido();

            // Assert
            Assert.True(valido);
            Assert.Empty(cliente.ValidationResult.Errors);
        }
    }
}
