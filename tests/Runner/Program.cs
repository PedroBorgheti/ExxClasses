using System;
using OO.Template.Importacao;
using OO.Template.Pedidos;
using OO.Template.Pagamento;
using OO.Template.Sync;

class Program
{
    static void Main()
    {
        Console.WriteLine("--- Importacao ---");
        var impA = new ImportacaoAlunos();
        var rA = impA.Executar("dummy");
        Console.WriteLine($"Alunos: Erros={rA.Erros.Count}");
        foreach(var e in rA.Erros) Console.WriteLine("  " + e);
        foreach(var kv in rA.TotaisPorCategoria) Console.WriteLine($"  {kv.Key}: {kv.Value}");

        var impP = new ImportacaoProdutos();
        var rP = impP.Executar("dummy");
        Console.WriteLine($"Produtos: Erros={rP.Erros.Count}");
        foreach(var e in rP.Erros) Console.WriteLine("  " + e);

        Console.WriteLine("\n--- Pedidos ---");
        var pedido = new Pedido("PED-1", new System.Collections.Generic.List<PedidoItem>{ new PedidoItem("SKU-1",2) }, "BR");
        var pn = new PedidoNacionalProcessor();
        var resn = pn.Processar(pedido);
        Console.WriteLine(string.Join("\n", resn.Mensagens));

        var pi = new PedidoInternacionalProcessor();
        var resi = pi.Processar(new Pedido("PED-2", new System.Collections.Generic.List<PedidoItem>{ new PedidoItem("SKU-2",1) }, "US"));
        Console.WriteLine(string.Join("\n", resi.Mensagens));

        Console.WriteLine("\n--- Pagamento ---");
        var brFlow = new BrPaymentFlow();
        var pr = brFlow.Processar(new PedidoPagamento("PAY-1", 120m, "BRL"));
        Console.WriteLine(string.Join("\n", pr.Mensagens));

        var usFlow = new UsPaymentFlow();
        var pu = usFlow.Processar(new PedidoPagamento("PAY-2", 200m, "USD"));
        Console.WriteLine(string.Join("\n", pu.Mensagens));

        Console.WriteLine("\n--- Sync ---");
        var erp = new SyncErpFlow();
        var s1 = erp.Executar("scope1");
        Console.WriteLine(string.Join("\n", s1.Mensagens));

        var mkt = new SyncMarketplaceFlow();
        var s2 = mkt.Executar("scope2");
        Console.WriteLine(string.Join("\n", s2.Mensagens));

        Console.WriteLine("\nExecução runner concluída.");
    }
}
