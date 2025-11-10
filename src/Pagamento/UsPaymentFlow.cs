using System;

namespace OO.Template.Pagamento
{
    public class UsPaymentFlow : PaymentFlow
    {
        protected override decimal CalcularImpostos(PedidoPagamento p) => Math.Round(p.Valor * 0.07m, 2);

        protected override string FormatarRecibo(ResultadoProcessamentoPagamento resultado)
            => $"Receipt-US | Status: {(resultado.Sucesso ? "OK" : "FAIL")}";

        protected override void AntesDeRegistrar(PedidoPagamento p, decimal subtotal, decimal impostos)
        {
            base.AntesDeRegistrar(p, subtotal, impostos);
            Console.WriteLine("[LOG] Compliance check (US) antes do registro");
        }
    }
}
