#pragma checksum "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8e1aa42ec07dbf19e424e4e7955c592cd1565b6b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UploadSong_ViewSongs), @"mvc.1.0.view", @"/Views/UploadSong/ViewSongs.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8e1aa42ec07dbf19e424e4e7955c592cd1565b6b", @"/Views/UploadSong/ViewSongs.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4fc0d20841662a0d746d64a4ea346b619fe8e08", @"/Views/_ViewImports.cshtml")]
    public class Views_UploadSong_ViewSongs : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Amazon.S3.Model.S3Object>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DownloadImage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteImage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <!--To bring the result into the website since they are stored in S3 Object type-->\r\n\r\n");
#nullable restore
#line 4 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
  
    ViewData["Title"] = "ViewImages";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>View Images</h1>\r\n\r\n");
#nullable restore
#line 10 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
 if (ViewBag.msg != "")
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>alert(\"");
#nullable restore
#line 12 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
              Write(ViewBag.msg);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");</script>\r\n");
#nullable restore
#line 13 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<center>
    <h1>View Uploaded Images</h1>
    <br />
    <hr />
    <br />
    <table border=""1"">
        <tr bgcolor=""Yellow"">
            <th>Product Image</th>
            <th>Product Name</th>
            <th>Product Image Size</th>
            <th>Action</th>
        </tr>
");
#nullable restore
#line 26 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
           int i = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
         foreach (var image in Model)
        {
            string link = "https://" + image.BucketName + ".s3.amazonaws.com/" + image.Key;

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td><img");
            BeginWriteAttribute("src", " src=\"", 779, "\"", 801, 1);
#nullable restore
#line 31 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
WriteAttributeValue("", 785, ViewBag.URLs[i], 785, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"300px\" height=\"300px\" /></td>\r\n                <td><img");
            BeginWriteAttribute("src", " src=\"", 865, "\"", 876, 1);
#nullable restore
#line 32 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
WriteAttributeValue("", 871, link, 871, 5, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"300px\" height=\"300px\" /></td>\r\n                <td>");
#nullable restore
#line 33 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
               Write(image.Key);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 34 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
               Write(image.Size);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8e1aa42ec07dbf19e424e4e7955c592cd1565b6b7163", async() => {
                WriteLiteral("<button>Download</button>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-FileName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 36 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
                                                          WriteLiteral(image.Key);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["FileName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-FileName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["FileName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    <br />\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8e1aa42ec07dbf19e424e4e7955c592cd1565b6b9438", async() => {
                WriteLiteral("<button>Delete</button>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-FileName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 38 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
                                                        WriteLiteral(image.Key);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["FileName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-FileName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["FileName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 41 "C:\Users\yongf\source\repos\DDACAssignment\DDACAssignment\Views\UploadSong\ViewSongs.cshtml"
            i++;
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n</center>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Amazon.S3.Model.S3Object>> Html { get; private set; }
    }
}
#pragma warning restore 1591
