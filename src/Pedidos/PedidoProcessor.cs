using System.Collections.Generic;

namespace OO.Template.Pedidos
{
    public record PedidoItem(string Sku, int Quantidade);
    public record Pedido(string Id, List<PedidoItem> Itens, string Destino);
    public record ResultadoProcessamento(bool Sucesso, List<string> Mensagens);

    public abstract class PedidoProcessor
    {
        public ResultadoProcessamento Processar(Pedido p)
        {
            var msgs = new List<string>();
            if (!ValidarItens(p, msgs)) return new ResultadoProcessamento(false, msgs);

            ReservarEstoque(p, msgs);

            var frete = CalcularFrete(p);

            var total = CalcularTotal(p, frete);
            PersistirPedido(p, total, msgs);

            var confirm = GerarConfirmacao(new ResultadoProcessamento(true, msgs));
            AposReservaEstoque(p);

            msgs.Add($"Frete calculado: {frete}");
            msgs.Add($"Total calculado: {total}");
            msgs.Add($"Confirm: {confirm}");

            return new ResultadoProcessamento(true, msgs);
        }

        protected virtual bool ValidarItens(Pedido p, List<string> msgs)
        {
            if (p.Itens == null || p.Itens.Count == 0) { msgs.Add("Pedido sem itens."); return false; }
            return true;
        }

        protected virtual void ReservarEstoque(Pedido p, List<string> msgs)
        {
            msgs.Add("Estoque reservado (simulado).");
        }

        protected abstract decimal CalcularFrete(Pedido p);

        protected virtual decimal CalcularTotal(Pedido p, decimal frete)
        {
            decimal subtotal = 0;
            foreach(var it in p.Itens) subtotal += it.Quantidade * 10; // preço fictício
            return subtotal + frete;
        }

        protected virtual void PersistirPedido(Pedido p, decimal total, List<string> msgs)
        {
            msgs.Add($"Persistido pedido {p.Id} com total {total}");
        }

        protected abstract string GerarConfirmacao(ResultadoProcessamento resultado);

        protected virtual void AposReservaEstoque(Pedido p) { }
    }
}
