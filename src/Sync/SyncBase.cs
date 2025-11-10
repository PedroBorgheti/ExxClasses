using System.Collections.Generic;

namespace OO.Template.Sync
{
    public record SyncStatus(bool Sucesso, List<string> Mensagens);

    public abstract class SyncBase
    {
        public SyncStatus Executar(string escopo)
        {
            var msgs = new List<string>();
            var bruto = ColetarBruto(escopo);
            msgs.Add("Normalizar e reconciliar (simulado)");
            msgs.Add("Aplicar diferenças (simulado)");
            PosAplicacao(new SyncStatus(true, msgs));
            var rel = GerarRelatorio(new SyncStatus(true, msgs));
            msgs.Add($"Relatório: {rel}");
            return new SyncStatus(true, msgs);
        }

        protected abstract object ColetarBruto(string escopo);

        protected abstract string GerarRelatorio(SyncStatus status);

        protected virtual void PosAplicacao(SyncStatus status) { }
    }
}
