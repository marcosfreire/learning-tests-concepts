using FluentValidation;
using System;

namespace LearingXUnitTests
{
    public class Cliente : Entity
    {
        public Cliente(Guid id, string nome, string sobreNome, DateTime dataNascimento, string email)
        {
            Id = id;
            Nome = nome;
            SobreNome = sobreNome;
            DataNascimento = dataNascimento;
            Email = email;
            Ativo = true;
            DataCadastro = DateTime.Now;
        }

        public string Nome { get; private set; }
        public string SobreNome { get; private set; }

        public DateTime DataCadastro { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public string Email { get; private set; }
        public bool Ativo { get; private set; }

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
            var validator = new ClienteValidacao();
            ValidationResult = validator.Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class ClienteValidacao : AbstractValidator<Cliente>
    {
        public ClienteValidacao()
        {
            RuleFor(a => a.Nome).NotEmpty().WithMessage("O campo Nome é Obrigatório");

            RuleFor(a => a.SobreNome).NotEmpty().WithMessage("O campo SobreNome é Obrigatório");

            RuleFor(a => a.Email).NotEmpty().WithMessage("O campo Email é Obrigatório").EmailAddress().WithMessage("Email inválido");
        }
    }
}
