using System.Collections.Generic;

namespace OO.Template.Importacao
{
    public record Registro(string[] Campos);

    public record Relatorio(List<string> Erros, Dictionary<string,int> TotaisPorCategoria)
    {
        public Relatorio() : this(new List<string>(), new Dictionary<string,int>()) {}
    }

    public abstract class ImportacaoBase
    {
        public Relatorio Executar(string caminho)
        {
            var registros = Ler(caminho);
            var rel = new Relatorio();
            foreach(var r in registros)
            {
                var erros = ValidarRegistro(r);
                if (erros?.Count > 0) rel.Erros.AddRange(erros);
            }
            Consolidar(rel);
            PosConsolidacao(rel);
            return rel;
        }

        protected virtual List<Registro> Ler(string caminho) => new List<Registro>();

        protected abstract List<string> ValidarRegistro(Registro r);

        protected virtual void PosConsolidacao(Relatorio rel) { }

        protected void Consolidar(Relatorio rel)
        {
            // base consolidation - subclasses may add totals in PosConsolidacao
        }
    }
}
