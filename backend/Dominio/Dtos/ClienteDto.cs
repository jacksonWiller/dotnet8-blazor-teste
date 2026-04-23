using Clientes.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Dominio.Dtos
{
    public class ClienteDto
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
    }
}
