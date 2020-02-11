using App.Domain.Enums;
using App.Domain.Models;
using System;
using Xunit;

namespace Learning.Tests.Concepts.Tests.Fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    {
    }

    public class ClienteTestsFixture
    {
        public Cliente GerarClienteValido()
        {
            return new Cliente
                (Guid.NewGuid(),
                "Marcos",
                "Freire",
                DateTime.Now.AddYears(-20),
                Sexo.Masculino,
                true);
        }

        public Cliente GerarClienteInvalido()
        {
            return new Cliente
                (Guid.Empty,
                "",
                "",
                DateTime.Now,
                Sexo.Masculino,
                true);
        }
    }
}
