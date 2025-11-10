using System.Collections.Generic;

namespace OO.Template.Pagamento
{
    public record PedidoPagamento(string Id, decimal Valor, string Moeda);
    public record ResultadoProcessamentoPagamento(bool Sucesso, List<string> Mensagens);

    public abstract class PaymentFlow
    {
        public ResultadoProcessamentoPagamento Processar(PedidoPagamento p)
        {
            var msgs = new List<string>();
            if (!ValidarPedido(p, msgs)) return new ResultadoProcessamentoPagamento(false, msgs);

            var subtotal = CalcularSubtotal(p);
            var impostos = CalcularImpostos(p);
            AntesDeRegistrar(p, subtotal, impostos);

            RegistrarPagamento(p, subtotal + impostos, msgs);
            AposRegistrar(new ResultadoProcessamentoPagamento(true, msgs));

            var recibo = FormatarRecibo(new ResultadoProcessamentoPagamento(true, msgs));
            msgs.Add($"Recibo: {recibo}");
            return new ResultadoProcessamentoPagamento(true, msgs);
        }

        protected virtual bool ValidarPedido(PedidoPagamento p, List<string> msgs)
        {
            if (p.Valor <= 0) { msgs.Add("Valor inválido."); return false; }
            return true;
        }

        protected virtual decimal CalcularSubtotal(PedidoPagamento p) => p.Valor;

        protected abstract decimal CalcularImpostos(PedidoPagamento p);

        protected virtual void RegistrarPagamento(PedidoPagamento p, decimal total, List<string> msgs)
        {
            msgs.Add($"Pagamento registrado {p.Id} - total {total} {p.Moeda}");
        }

        protected abstract string FormatarRecibo(ResultadoProcessamentoPagamento resultado);

        protected virtual void AntesDeRegistrar(PedidoPagamento p, decimal subtotal, decimal impostos) { }

        protected virtual void AposRegistrar(ResultadoProcessamentoPagamento resultado) { }
    }
}
