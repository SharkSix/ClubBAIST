#pragma checksum "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4dd5e8b523710b6bba222f7712e6ccb7eea00d3c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ClubBAIST.Pages.Pages_ReviewMonthlyHandicapReport), @"mvc.1.0.razor-page", @"/Pages/ReviewMonthlyHandicapReport.cshtml")]
namespace ClubBAIST.Pages
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
#line 1 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\_ViewImports.cshtml"
using ClubBAIST;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4dd5e8b523710b6bba222f7712e6ccb7eea00d3c", @"/Pages/ReviewMonthlyHandicapReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"118610665f19a6bdbd00be5dc8934b469f41e581", @"/Pages/_ViewImports.cshtml")]
    public class Pages_ReviewMonthlyHandicapReport : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("ReportForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onload", new global::Microsoft.AspNetCore.Html.HtmlString("validDate()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
  
    ViewData["Title"] = "ReviewMonthlyHandicapReport";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html lang=\"en\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4dd5e8b523710b6bba222f7712e6ccb7eea00d3c4608", async() => {
                WriteLiteral(@"
    <meta charset=""utf-8"">
    <style type=""text/css"">
        table.imagetable {
            width: 100%;
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #999999;
            border-collapse: collapse;
        }

            table.imagetable th {
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #999999;
            }

            table.imagetable td {
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #999999;
            }
    </style>
    <script type=""text/javascript"">

    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4dd5e8b523710b6bba222f7712e6ccb7eea00d3c6382", async() => {
                WriteLiteral("\r\n    <header><h1>Review Membership Application</h1></header>\r\n    <nav></nav>\r\n    <main>\r\n        <div>\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4dd5e8b523710b6bba222f7712e6ccb7eea00d3c6765", async() => {
                    WriteLiteral("\r\n                <table>\r\n                    <tr>\r\n                        <td>Date</td>\r\n                        <td><input type=\"month\" name=\"Time\" /></td>\r\n                    </tr>\r\n                </table>\r\n");
                    WriteLiteral("                <table>\r\n                    <tr>\r\n                        <td>\r\n                            <input type=\"submit\" value=\"Submit\" />\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                <table class=""imagetable"">
                    <thead>
                        <tr>
                            <th>
                                Member Name
                            </th>
                            <th>
                                Handicap
                            </th>
                            <th>
                                Average
                            </th>
                            <th>
                                Best 10 Average
                            </th>
                            <th>
                                Last 20 Rounds
                            </th>
                        </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 82 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                         if (Model.MonthlyHandicapReport != null)
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 84 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                             foreach (var item in Model.MonthlyHandicapReport)
                            {
                                if (item.Last20Rounds != null)
                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <tr>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 90 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.MemberName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 93 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Handicap));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 96 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Average));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        ");
#nullable restore
#line 99 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                                   Write(Html.DisplayFor(modelItem => item.Best10Average));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                    </td>\r\n                                    <td>\r\n                                        <table>\r\n                                            <tr>\r\n");
#nullable restore
#line 104 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                                                 foreach (var number in item.Last20Rounds)
                                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                    <td style=\"column-width:50px;\">\r\n                                                        ");
#nullable restore
#line 107 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                                                   Write(number);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                                    </td>\r\n");
#nullable restore
#line 109 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                                                }

#line default
#line hidden
#nullable disable
                WriteLiteral("                                            </tr>\r\n                                        </table>\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 114 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                                }
                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 115 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
                             
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </tbody>\r\n                </table>\r\n            <div>\r\n                <h4>");
#nullable restore
#line 120 "C:\Users\Jiawei\Desktop\ClubBAIST\Pages\ReviewMonthlyHandicapReport.cshtml"
               Write(Model.Message);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h4>\r\n            </div>\r\n        </div>\r\n    </main>\r\n    <footer>\r\n    </footer>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n</html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ClubBAIST.Pages.ReviewMonthlyHandicapReportModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ClubBAIST.Pages.ReviewMonthlyHandicapReportModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ClubBAIST.Pages.ReviewMonthlyHandicapReportModel>)PageContext?.ViewData;
        public ClubBAIST.Pages.ReviewMonthlyHandicapReportModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
