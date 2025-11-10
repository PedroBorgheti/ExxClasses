namespace OO.Template.Pedidos
{
    public class PedidoNacionalProcessor : PedidoProcessor
    {
        protected override decimal CalcularFrete(Pedido p) => 20m;

        protected override string GerarConfirmacao(ResultadoProcessamento resultado)
            => $"Confirmação (Nacional): {(resultado.Sucesso ? "OK" : "FALHA")} - {string.Join("; ", resultado.Mensagens)}";
    }
}
