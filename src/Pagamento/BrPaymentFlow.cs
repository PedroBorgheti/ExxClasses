using System;

namespace OO.Template.Pagamento
{
    public sealed class BrPaymentFlow : PaymentFlow
    {
        protected override decimal CalcularImpostos(PedidoPagamento p) => Math.Round(p.Valor * 0.18m, 2);

        protected override string FormatarRecibo(ResultadoProcessamentoPagamento resultado)
            => $"Recibo-BR | Status: {(resultado.Sucesso ? "OK" : "FALHA")} | Mensagens: {string.Join(", ", resultado.Mensagens)}";
    }
}
