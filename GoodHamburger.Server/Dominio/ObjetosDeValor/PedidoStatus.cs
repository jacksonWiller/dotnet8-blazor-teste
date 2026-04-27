namespace Dominio.ObjetosDeValor
{
    /// <summary>
    /// Enum para representar o status de um pedido
    /// </summary>
    public enum PedidoStatus
    {
        /// <summary>
        /// Pedido criado, aguardando confirmação
        /// </summary>
        Pendente = 0,
        
        /// <summary>
        /// Pedido confirmado, em preparação
        /// </summary>
        EmPreparacao = 1,
        
        /// <summary>
        /// Pedido pronto para entrega/retirada
        /// </summary>
        Pronto = 2,
        
        /// <summary>
        /// Pedido entregue ao cliente
        /// </summary>
        Entregue = 3,
        
        /// <summary>
        /// Pedido cancelado pelo cliente ou restaurante
        /// </summary>
        Cancelado = 4
    }
}
