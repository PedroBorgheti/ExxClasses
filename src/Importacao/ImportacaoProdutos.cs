using System.Collections.Generic;

namespace OO.Template.Importacao
{
    public class ImportacaoProdutos : ImportacaoBase
    {
        protected override List<Registro> Ler(string caminho)
        {
            return new List<Registro>
            {
                new Registro(new[]{ "SKU-1","Caneta","2.5","Papelaria" }),
                new Registro(new[]{ "SKU-2","","15.0","Informatica" }),
                new Registro(new[]{ "SKU-3","Teclado","-50","Informatica" })
            };
        }

        protected override List<string> ValidarRegistro(Registro r)
        {
            var erros = new List<string>();
            if (r.Campos.Length < 4) { erros.Add("Registro incompleto"); return erros; }
            if (string.IsNullOrWhiteSpace(r.Campos[1])) erros.Add($"Produto {r.Campos[0]}: nome ausente");
            if (!decimal.TryParse(r.Campos[2], out var preco) || preco <= 0) erros.Add($"Produto {r.Campos[0]}: preço inválido '{r.Campos[2]}'");
            return erros;
        }
    }
}
