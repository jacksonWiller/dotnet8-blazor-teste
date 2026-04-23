namespace Clientes.Dominio.ObjetosDeValor
{
    public class Documento
    {
        public Guid Id { get; private set; }
        public string Numero { get; private set; }
        public TipoDocumento Tipo { get; private set; }

        protected Documento() { }

        public Documento(string numero, TipoDocumento tipo)
        {
            Numero = numero;
            Tipo = tipo;
        }

        public override string ToString()
        {
            if (Tipo == TipoDocumento.CPF)
            {
                return Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00");
            }

            return Convert.ToUInt64(Numero).ToString(@"00\.000\.000\/0000\-00");
        }
    }

    public enum TipoDocumento
    {
        CPF,
        CNPJ
    }
}
