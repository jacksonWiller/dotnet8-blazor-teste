using Ardalis.Result;
using Clientes.Dominio.ObjetosDeValor;
using MediatR;

namespace Clientes.Aplicacao.Commands.CreateCliente
{
    public class CreateClienteCommand : IRequest<Result<CreateClienteResponse>>
    {
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
    }
}
