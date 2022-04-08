#pragma checksum "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\Gestao\Vendas.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "50732f9d8a57f06aea72c0d001ec0087db8da077"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Gestao_Vendas), @"mvc.1.0.view", @"/Views/Gestao/Vendas.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\_ViewImports.cshtml"
using bluemarket;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\_ViewImports.cshtml"
using bluemarket.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"50732f9d8a57f06aea72c0d001ec0087db8da077", @"/Views/Gestao/Vendas.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1e19a84f7efc264618e9863f08602145aacce355", @"/Views/_ViewImports.cshtml")]
    public class Views_Gestao_Vendas : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<bluemarket.Models.Venda>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\Gestao\Vendas.cshtml"
  
    Layout = "_LayoutAdmin";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<script>
    $(document).ready( function () {
        $('#vendas').DataTable({

            ""language"": {
                ""lengthMenu"": ""Mostrando _MENU_ registros por página"",
                ""zeroRecords"": ""Nada encontrado, desculpa!"",
                ""info"": ""Mostrando página _PAGE_ de _PAGES_"",
                ""infoEmpty"": ""Nenhum registro disponível"",
                ""search"": ""Buscar"",
                ""paginate"": {
                    ""first"": ""Primeiro"",
                    ""last"": ""Último"",
                    ""next"": ""Próximo"",
                    ""previous"": ""Anterior""
                }
            }
        });
    });
</script>

<h2>Relatório de vendas</h2>
<hr />

<table id=""vendas"" class=""table table-striped table-bordered"">
    <thead>
        <tr>
            <th>Id</th>
            <th>Data</th>
            <th>Total</th>
            <th>Valor pago</th>
            <th>Troco</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 41 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\Gestao\Vendas.cshtml"
         foreach (var venda in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>");
#nullable restore
#line 44 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\Gestao\Vendas.cshtml"
               Write(venda.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 45 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\Gestao\Vendas.cshtml"
               Write(venda.Data);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 46 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\Gestao\Vendas.cshtml"
               Write(venda.Total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 47 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\Gestao\Vendas.cshtml"
               Write(venda.ValorPago);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 48 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\Gestao\Vendas.cshtml"
               Write(venda.Troco);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            </tr>\n");
#nullable restore
#line 50 "C:\Users\mnia\OneDrive - GFT Technologies SE\Documents\Workspace\Atividades\Desafio MVC\Treinamento MVC\bluemarket\Views\Gestao\Vendas.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<bluemarket.Models.Venda>> Html { get; private set; }
    }
}
#pragma warning restore 1591
