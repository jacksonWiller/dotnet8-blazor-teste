using Clientes.Dominio.ObjetosDeValor;

namespace Clientes.Dominio.Entidades
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public Guid DocumentoId { get; private set; }
        public Guid EmailId { get; private set; }
        public Guid EnderecoId { get; private set; }
        public Guid TelefoneId { get; private set; }

        public DateTime DataNascimento { get; private set; }
        public Documento Documento { get; private set; }
        public Telefone Telefone { get; private set; }
        public Email Email { get; private set; }
        public Endereco Endereco { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public bool Isento { get; private set; }
        public bool Removido { get; private set; } = false;

        protected Cliente() { }

        public Cliente(string nome, 
            Documento documento, 
            DateTime dataNascimento,
            Telefone telefone, 
            Email email, 
            Endereco endereco,
            string inscricaoEstadual = null, bool isento = false)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Documento = documento;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
            InscricaoEstadual = inscricaoEstadual;
            Isento = isento;
            
            // Definir as chaves estrangeiras
            DocumentoId = documento.Id;
            EmailId = email.Id;
            EnderecoId = endereco.Id;
            TelefoneId = telefone.Id;
        }

        public void AtualizarDados(
            string nome, 
            Documento documento, 
            DateTime dataNascimento,
            Telefone telefone, 
            Email email, 
            Endereco endereco,
            string inscricaoEstadual = null, bool isento = false)
        {
            Nome = nome;
            Documento = documento;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
            InscricaoEstadual = inscricaoEstadual;
            Isento = isento;
            
            // Atualizar as chaves estrangeiras
            DocumentoId = documento.Id;
            EmailId = email.Id;
            EnderecoId = endereco.Id;
            TelefoneId = telefone.Id;
        }

        public void Remover() => Removido = true;
    }
}