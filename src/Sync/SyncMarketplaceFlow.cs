using System.Collections.Generic;

namespace OO.Template.Sync
{
    public class SyncMarketplaceFlow : SyncBase
    {
        protected override object ColetarBruto(string escopo) => new List<string>{ "MKT_A", "MKT_B", "MKT_C" };

        protected override string GerarRelatorio(SyncStatus status) => "Relatorio-MKT: 3 itens processados";

        protected override void PosAplicacao(SyncStatus status)
        {
            base.PosAplicacao(status);
            Console.WriteLine("[METRICS] Enviando métricas (Marketplace)");
        }
    }
}
