using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Aplicacao.Commands.UpadateCliente;

public class UpdateClienteResponse(Guid id)
{
    public Guid Id { get; } = id;
}
