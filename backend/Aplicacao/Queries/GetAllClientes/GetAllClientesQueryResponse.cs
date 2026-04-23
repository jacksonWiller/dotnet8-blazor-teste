using Ardalis.Result;
using Clientes.Dominio.Dtos;

namespace Clientes.Aplicacao.Queries.GetAllClientes;

public class GetAllClientesQueryResponse()
{
    public PagedInfo PagedInfo { get; set; }
    public List<ClienteDto> Clientes { get; set; }
}
