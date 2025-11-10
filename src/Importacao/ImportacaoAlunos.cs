using System.Collections.Generic;

namespace OO.Template.Importacao
{
    public class ImportacaoAlunos : ImportacaoBase
    {
        protected override List<Registro> Ler(string caminho)
        {
            return new List<Registro>
            {
                new Registro(new[]{ "1001","Ana", "22", "Eng" }),
                new Registro(new[]{ "1002","", "20", "Math" }), // nome ausente
                new Registro(new[]{ "1003","Carlos", "-1", "Bio" }) // idade inválida
            };
        }

        protected override List<string> ValidarRegistro(Registro r)
        {
            var erros = new List<string>();
            if (r.Campos.Length < 4) { erros.Add("Registro incompleto"); return erros; }
            if (string.IsNullOrWhiteSpace(r.Campos[1])) erros.Add($"Aluno {r.Campos[0]}: nome ausente");
            if (!int.TryParse(r.Campos[2], out var idade) || idade <= 0) erros.Add($"Aluno {r.Campos[0]}: idade inválida '{r.Campos[2]}'");
            return erros;
        }

        protected override void PosConsolidacao(Relatorio rel)
        {
            base.PosConsolidacao(rel);
            rel.TotaisPorCategoria["AlunosProcessados"] = 3;
            rel.TotaisPorCategoria["AlunosComErros"] = rel.Erros.Count;
        }
    }
}
