#pragma checksum "C:\Users\SG422\source\repos\WebCore2\WebCore\Pages\Filter.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4933505a27d2fcf083f99dcfaf512ffdc796f5e5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(WebCore.Pages.Pages_Filter), @"mvc.1.0.razor-page", @"/Pages/Filter.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Filter.cshtml", typeof(WebCore.Pages.Pages_Filter), null)]
namespace WebCore.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\SG422\source\repos\WebCore2\WebCore\Pages\_ViewImports.cshtml"
using WebCore;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4933505a27d2fcf083f99dcfaf512ffdc796f5e5", @"/Pages/Filter.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5123c2fd7f4c65ab918803232fbc907481ddd091", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Filter : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(34, 1174, true);
            WriteLiteral(@"
<input type=""button"" value=""测试过滤器"" onclick=""TestFileter()"" />
<input type=""button"" value=""测试资源拦截过滤器"" onclick=""TestShortCircuiting()"" />
<input type=""button"" value=""测试异常过滤器"" onclick=""TestExceptionFileter()"" />


<script>
    function TestFileter() {
        axios.get(""api/Fileter/GetFileterExcutedMsg"")
            .then(function (response) {
                console.log(response);
            })
            .catch(function (response) {
                console.log(response);
            });
    }

    function TestShortCircuiting() {
        axios.get(""api/Fileter/ShortCircuitingResource"")
            .then(function (response) {
                console.log(response);
            })
            .catch(function (response) {
                console.log(response);
            });
    }

    function TestExceptionFileter() {
        axios.get(""api/Fileter/TestExceptionFileter"")
            .then(function (response) {
                console.log(response);
            })
            .c");
            WriteLiteral("atch(function (response) {\r\n                console.log(response);\r\n            });\r\n    }\r\n   \r\n</script>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FilterModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<FilterModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<FilterModel>)PageContext?.ViewData;
        public FilterModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
