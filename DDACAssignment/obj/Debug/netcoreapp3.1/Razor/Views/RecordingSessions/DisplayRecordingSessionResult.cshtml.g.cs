#pragma checksum "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a691726623d6b3cc726f2e4c485c74a3dd135cbc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RecordingSessions_DisplayRecordingSessionResult), @"mvc.1.0.view", @"/Views/RecordingSessions/DisplayRecordingSessionResult.cshtml")]
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
#line 1 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\_ViewImports.cshtml"
using DDACAssignment;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\_ViewImports.cshtml"
using DDACAssignment.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a691726623d6b3cc726f2e4c485c74a3dd135cbc", @"/Views/RecordingSessions/DisplayRecordingSessionResult.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4fc0d20841662a0d746d64a4ea346b619fe8e08", @"/Views/_ViewImports.cshtml")]
    public class Views_RecordingSessions_DisplayRecordingSessionResult : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DDACAssignment.Models.RecordingSession>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
  
    ViewData["Title"] = "DisplayRecordingSessionResult";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
 if (ViewBag.msg != "")
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>alert(\"");
#nullable restore
#line 8 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
              Write(ViewBag.msg);

#line default
#line hidden
#nullable disable
            WriteLiteral("\")</script>\r\n");
#nullable restore
#line 9 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<center>
    <h1>Display Flower Result</h1>
    <br />
    <hr />
    <br />
    <table border=""1"" style=""text-align:center"">
        <tr bgcolor=""yellow"">
            <th width=""150px"">Recording Session ID</th>
            <th width=""250px"">Song Name</th>
            <th width=""250px"">Start Date Time</th>
            <th width=""250px"">End Date Time</th>
            <th width=""250px"">Producer Name</th>
            <th width=""250px"">Composer Name</th>
        </tr>
");
#nullable restore
#line 24 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
          
            int i = 1; string color = "white";
            foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr");
            BeginWriteAttribute("bgcolor", " bgcolor=\"", 820, "\"", 836, 1);
#nullable restore
#line 28 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
WriteAttributeValue("", 830, color, 830, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <td>");
#nullable restore
#line 29 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
                   Write(item.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 30 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
                   Write(item.SongName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 31 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
                   Write(item.StartDateTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 32 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
                   Write(item.EndDateTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 33 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
                   Write(item.ProducerName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 34 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
                   Write(item.ComposerName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 37 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
                 if (i == 1)
                {
                    i = 0;
                    color = "lightblue";
                }
                else
                {
                    i = 1;
                    color = "white";
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 46 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\RecordingSessions\DisplayRecordingSessionResult.cshtml"
                 
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral(";\r\n    </table>\r\n</center>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DDACAssignment.Models.RecordingSession>> Html { get; private set; }
    }
}
#pragma warning restore 1591