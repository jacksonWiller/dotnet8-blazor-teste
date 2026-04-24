using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infra.Contexto;
using Fop;
using Fop.FopExpression;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly GoodHamburgerContext _dataContext;

        public ClienteRepository(GoodHamburgerContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> ExisteClienteComEmailAsync(string email, Guid clienteId)
        {
            return await _dataContext.Clientes
                .AnyAsync(c => c.Email.Endereco == email && c.Id != clienteId && !c.Removido);
        }

        public async Task<(List<ClienteDto>, int)> GetAllClientesAsync(
            string filter = null,
            string order = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var fopRequest = FopExpressionBuilder<Cliente>.Build(filter, order, pageNumber, pageSize);

            var query = _dataContext.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Telefone)
                .Where(x => !x.Removido);

            var (filteredClientes, totalRecords) = query
                .AsNoTracking()
                .ApplyFop(fopRequest);

            var clientesLista = await filteredClientes.ToListAsync();

            var clientesListaDto = clientesLista.Select(c => new ClienteDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Documento = c.Documento.Numero,
                TipoDocumento = (Dominio.ObjetosDeValor.TipoDocumento)(int)c.Documento.Tipo,
                DataNascimento = c.DataNascimento,
                Telefone = c.Telefone.Numero,
                Email = c.Email.Endereco,
                Cep = c.Endereco.Cep,
                Logradouro = c.Endereco.Logradouro,
                Numero = c.Endereco.Numero,
                Bairro = c.Endereco.Bairro,
                Cidade = c.Endereco.Cidade,
                Estado = c.Endereco.Estado,
                InscricaoEstadual = c.InscricaoEstadual,
                Isento = c.Isento
            }).ToList();

            return (clientesListaDto, totalRecords);
        }

        public async Task<Cliente[]> GetClientesByNomeAsync(string nome)
        {
            var query = _dataContext.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Telefone)
                .Where(c => c.Nome.ToLower().Contains(nome.ToLower()) && !c.Removido)
                .AsNoTracking()
                .OrderBy(c => c.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(Guid clienteId)
        {
            var query = _dataContext.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Telefone)
                .Where(c => c.Id == clienteId && !c.Removido)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClienteByDocumentoAsync(string documento)
        {
            var query = _dataContext.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Telefone)
                .Where(c => c.Documento.Numero == documento && !c.Removido)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClienteByEmailAsync(string email)
        {
            var query = _dataContext.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Telefone)
                .Where(c => c.Email.Endereco == email && !c.Removido)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExisteClienteComDocumentoAsync(string documento)
        {
            return await _dataContext.Clientes
                .AnyAsync(c => c.Documento.Numero == documento && !c.Removido);
        }

        public async Task<bool> ExisteClienteComEmailAsync(string email)
        {
            return await _dataContext.Clientes
                .AnyAsync(c => c.Email.Endereco == email && !c.Removido);
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            _dataContext.Clientes.Add(cliente);
            await _dataContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _dataContext.Clientes.Update(cliente);
            await _dataContext.SaveChangesAsync();
        }

        public Task<Cliente[]> GetAllClientesAsync()
        {
            throw new NotImplementedException();
        }
    }
}