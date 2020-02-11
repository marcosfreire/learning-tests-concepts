using App.Domain.Enums;
using System;

namespace App.Domain.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public bool Ativo { get; set; }

        public Cliente(Guid id, string nome, string sobreNome, DateTime dataNascimento, Sexo sexo, bool ativo)
        {
            Id = id;
            Nome = nome;
            SobreNome = sobreNome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Ativo = ativo;
        }

        public bool Valido()
        {
            var valido = Id != Guid.Empty;
            valido &= !string.IsNullOrEmpty(Nome);
            valido &= !string.IsNullOrEmpty(SobreNome);
            valido &= DataNascimento < DateTime.Now.AddYears(-18);

            return valido;
        }
    }
}
