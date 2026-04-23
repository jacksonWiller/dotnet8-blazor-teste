namespace Clientes.Dominio.ObjetosDeValor
{
    public class Email
    {
        public Guid Id { get; private set; }
        public string Endereco { get; private set; }

        protected Email() { }

        public Email(string endereco)
        {
            Endereco = endereco;
        }

        public override string ToString()
        {
            return Endereco;
        }
    }
}
