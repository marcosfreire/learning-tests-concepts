using LearingXUnitTests;
using System;
using Xunit;

namespace LearningTests.tests.test_fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    {
    }

    public class ClienteTestsFixture
    {
        public Cliente GerarClienteValido()
        {
            return new Cliente(
               Guid.NewGuid(),
               "Marcos",
               "Freire",
               new DateTime(1990, 8, 28),
               "marcos.aurelio.freire@outlook.com");
        }

        public Cliente GerarClienteInvalido()
        {
            return new Cliente(
                Guid.NewGuid(),
                "",
                "Freire",
                new DateTime(1990, 8, 28),
                "marcos.aurelio.freire@outlook.com");
        }
    }
}
