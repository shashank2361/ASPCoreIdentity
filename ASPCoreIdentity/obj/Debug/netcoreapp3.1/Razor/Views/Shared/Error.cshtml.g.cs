#pragma checksum "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e931f2b4a3a5f607ec7b1612afd32271a00d15a0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
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
#line 1 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\_ViewImports.cshtml"
using ASPCoreIdentity.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\_ViewImports.cshtml"
using ASPCoreIdentity.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e931f2b4a3a5f607ec7b1612afd32271a00d15a0", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3536317a59dc232965a8a4809ff0e63cca35dcee", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<int>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml"
  
    ViewBag.Title = "Error";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml"
 if (ViewBag.ErrorTitle == null)
{


#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <h3>
        An occured while processing your request. The support
        team is notified and we are working on the fix
    </h3>
    <h5>Please contact us on pragim@pragimtech.com</h5>
    <hr />
    <h3>Exception Details:</h3>
    <div class=""alert alert-danger"">
        <h5>Exception Path</h5>
        <hr />
        <p>");
#nullable restore
#line 20 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml"
      Write(ViewBag.ExceptionPath);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n");
            WriteLiteral("    <div class=\"alert alert-danger\">\r\n        <h5>Exception Message</h5>\r\n        <hr />\r\n        <p>");
#nullable restore
#line 26 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml"
      Write(ViewBag.ExceptionMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n");
            WriteLiteral("    <div class=\"alert alert-danger\">\r\n        <h5>Exception Stack Trace</h5>\r\n        <hr />\r\n        <p>");
#nullable restore
#line 32 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml"
      Write(ViewBag.StackTrace);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n");
#nullable restore
#line 34 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h1 class=\"text-danger\">");
#nullable restore
#line 37 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml"
                       Write(ViewBag.ErrorTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    <h6 class=\"text-danger\">");
#nullable restore
#line 38 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml"
                       Write(ViewBag.ErrorMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n");
#nullable restore
#line 39 "E:\Projects\ASPCoreIdentity\ASPCoreIdentity\Views\Shared\Error.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<int> Html { get; private set; }
    }
}
#pragma warning restore 1591
