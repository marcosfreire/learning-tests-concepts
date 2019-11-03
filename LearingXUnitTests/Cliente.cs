using FluentValidation;
using System;

namespace LearingXUnitTests
{
    public class Cliente
    {
        public Cliente(Guid id, string nome, string sobreNome, DateTime dataNascimento, string email, bool ativo, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            SobreNome = sobreNome;
            DataNascimento = dataNascimento;
            Email = email;
            Ativo = ativo;
            DataCadastro = dataCadastro;
        }

        public Guid Id { get; private set; }

        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public string Email { get; private set; }
        public bool Ativo { get; private set; }

        public DateTime DataCadastro { get; private set; }

        public string NomeCompleto()
        {
            return $"{Nome} {SobreNome}";
        }

        public bool Especial()
        {
            return DataCadastro < DateTime.Now.AddYears(-3) && Ativo;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        public bool Valido()
        {
            return false;
        }
    }

    public class ClienteValidacao : AbstractValidator<Cliente>
    {
        public ClienteValidacao()
        {
            RuleFor(a => a.Nome).NotEmpty().WithMessage("O campo nome é Obrigatório");
            RuleFor(a => a.Nome).NotEmpty().WithMessage("O campo nome é Obrigatório");
        }
    }
}
