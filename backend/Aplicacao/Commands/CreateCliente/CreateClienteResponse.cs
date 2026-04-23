using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Aplicacao.Commands.CreateCliente
{
    public class CreateClienteResponse(Guid id)
    {
        public Guid Id { get; } = id;
    }
}
