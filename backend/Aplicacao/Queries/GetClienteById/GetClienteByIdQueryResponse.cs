using Clientes.Dominio.Dtos;
using Clientes.Dominio.ObjetosDeValor;
using System;

namespace Clientes.Aplicacao.Queries.GetClienteById
{
    public class GetClienteByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string InscricaoEstadual { get; set; }
        public bool Isento { get; set; }

        public static GetClienteByIdQueryResponse MapFrom(Clientes.Dominio.Entidades.Cliente cliente)
        {
            return new GetClienteByIdQueryResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Documento = cliente.Documento?.Numero,
                TipoDocumento = cliente.Documento?.Tipo ?? TipoDocumento.CPF,
                DataNascimento = cliente.DataNascimento,
                Telefone = cliente.Telefone?.Numero,
                Email = cliente.Email?.Endereco,
                Cep = cliente.Endereco?.Cep,
                Logradouro = cliente.Endereco?.Logradouro,
                Numero = cliente.Endereco?.Numero,
                Bairro = cliente.Endereco?.Bairro,
                Cidade = cliente.Endereco?.Cidade,
                Estado = cliente.Endereco?.Estado,
                InscricaoEstadual = cliente.InscricaoEstadual,
                Isento = cliente.Isento
            };
        }
    }
}