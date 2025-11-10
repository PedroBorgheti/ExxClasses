using System.Collections.Generic;

namespace OO.Template.Sync
{
    public sealed class SyncErpFlow : SyncBase
    {
        protected override object ColetarBruto(string escopo) => new List<string>{ "ERP_1", "ERP_2" };

        protected override string GerarRelatorio(SyncStatus status) => "Relatorio-ERP: sincronização concluída.";
    }
}
