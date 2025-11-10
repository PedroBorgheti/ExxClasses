using System;

namespace OO.Template.Pedidos
{
    public sealed class PedidoInternacionalProcessor : PedidoProcessor
    {
        protected override decimal CalcularFrete(Pedido p)
        {
            return p.Destino switch
            {
                "BR" => 50m,
                _ => 150m
            };
        }

        protected override string GerarConfirmacao(ResultadoProcessamento resultado)
            => $"Confirmation (International): {(resultado.Sucesso ? "OK" : "FAIL")}";

        protected override void AposReservaEstoque(Pedido p)
        {
            base.AposReservaEstoque(p);
            Console.WriteLine($"[TRACK] Tracking internacional criado para pedido {p.Id}");
        }
    }
}
