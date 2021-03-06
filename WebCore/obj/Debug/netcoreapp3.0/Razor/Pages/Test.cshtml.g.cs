#pragma checksum "C:\Users\MR.Z\OneDrive\repos\项目\GitHub项目\WebCore\WebCore\Pages\Test.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8e3281747b0da6a639bdff287693b720161c2275"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(WebCore.Pages.Pages_Test), @"mvc.1.0.razor-page", @"/Pages/Test.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Test.cshtml", typeof(WebCore.Pages.Pages_Test), null)]
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
#line 1 "C:\Users\MR.Z\OneDrive\repos\项目\GitHub项目\WebCore\WebCore\Pages\_ViewImports.cshtml"
using WebCore;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8e3281747b0da6a639bdff287693b720161c2275", @"/Pages/Test.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5123c2fd7f4c65ab918803232fbc907481ddd091", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Test : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div>
    <a id=""clickTest"" onclick=""getList()"">获取列表</a>
    <a id=""clickTest2"" onclick=""postObject()"">提交对象</a>
    <a id=""clickTest3"" onclick=""postObject2()"">提交对象2</a>
    <a id=""clickTest3"" onclick=""clickHandle4()"">获取服务器表单数据</a>
    <a id=""clickTest4"" onclick=""uploadFile()"">上传</a>
    <a id=""clickTest5"" onclick=""clickHandle5()"">获取对象restful</a>
    <a id=""clickTest6"" onclick=""clickHandle6()"">获取对象fromQuery</a>
    <a id=""clickTest7"" onclick=""clickHandle7()"">获取对象fromHeader</a>
    <a id=""clickTest8"" onclick=""clickHandle8()"">获取对象(直接上传JSON字符串)</a>
    <input id=""fileInput"" name=""file"" type=""file"" multiple=""multiple"" accept=""image/png,image/gif,image/jpeg"" onchange=""uploadFile()"" />
    <input id=""fileInput2"" name=""file"" type=""file"" multiple=""multiple"" accept=""image/png,image/gif,image/jpeg"" onchange=""uploadFile2()"" />
    <input id=""fileInput3"" name=""file"" type=""file"" multiple=""multiple"" accept=""image/png,image/gif,image/jpeg"" onchange=""uploadFile3()"" />
    <a id=""clickTest5"" onclick=""GetImg()"">获");
            WriteLiteral(@"取图片流</a>
    <a id=""clickTest5"" onclick=""downloadFile()"">下载图片</a>
    <img src=""api/Download/GetImg"" alt=""Alternate Text"" />
    <img src=""api/Download/GetImg/a.jpg"" alt=""Alternate Text"" />
    <img src=""staticfiles/a.jpg"" alt=""Alternate Text"" />
    <a id=""clickTest9"" onclick=""clickHandle9()"">测试重定向Action</a>
    <a id=""clickTest9"" onclick=""testMyContentResult()"">测试MyContentResult</a>

</div>

<script>
    function getList() {
        axios.get('api/GetMsg/GetProductList')
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    function postObject() {
        axios.post('api/GetMsg/Send_LX', {
            PhoneNum: 'Fred',
            Msg: 'Flintstone',
            Id: 1,
            cookie: ''
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
      ");
            WriteLiteral(@"          console.log(error);
            });
    }

    function postObject2() {
        axios.post('api/GetMsg/Send_MI', {
            PhoneNum: 'Fred',
            Msg: 'Flintstone',
            Id: 1,
            cookie: ''
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    function clickHandle4() {
        axios.get('api/GetMsg/GetFormData')
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    function clickHandle5() {
        axios.get('api/GetMsg/GetModel/1/jom/12/jom的商品')
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    function cli");
            WriteLiteral(@"ckHandle6() {
        axios.get('api/GetMsg/GetModel?id=1&name=jom2&price=13&description=jom2的商品')
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    function clickHandle7() {
        axios.get('api/GetMsg/GetModel2?id=1&name=jom2&price=13&description=jom2的商品')
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    function clickHandle8() {
        axios.get('api/GetMsg/GetModel3', {
            params: {
                ""product"": JSON.stringify({
                    id: 1,
                    name: ""jom2"",
                    price: 13,
                    description: ""jom2的商品""
                })
            }
        })
            .then(function (response) {
                console.log(response);
");
            WriteLiteral(@"            })
            .catch(function (error) {
                console.log(error);
            });
    }

    function clickHandle9() {
        axios.get('/api/redirect/redirectAction?url=https://www.baidu.com')
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    function uploadFile() {
        let files = document.getElementById(""fileInput"").files;
        let formdata1 = new FormData();// 创建form对象
        formdata1.append('file001', files[0]);
        formdata1.append('msg', ""hello"");
        formdata1.append('file002', files[1]);
        axios.post('api/Upload/UploadFile', formdata1, {
            headers: { 'Content-Type': 'multipart/form-data' }
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                alert(error);
            }");
            WriteLiteral(@");
    }

    function uploadFile2() {
        let files = document.getElementById(""fileInput2"").files;
        let formdata1 = new FormData();// 创建form对象
        formdata1.append('file001', files[0]);
        formdata1.append('msg', ""hello"");
        formdata1.append('file002', files[1]);
        axios.post('api/Upload/UploadFile2', formdata1, {
            headers: { 'Content-Type': 'multipart/form-data' }
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                alert(error);
            });
    }

    function uploadFile3() {
        let files = document.getElementById(""fileInput3"").files;
        let formdata1 = new FormData();// 创建form对象
        formdata1.append('file001', files[0]);
        formdata1.append('msg', ""hello"");
        formdata1.append('file002', files[1]);
        axios.post('api/Upload/UploadFile3', formdata1, {
            headers: { 'Content-Type': 'multipart/");
            WriteLiteral(@"form-data' }
        })
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                alert(error);
            });
    }

    function testMyContentResult() {
        axios.get(""api/test/TestMyContentResult"").then(response => {
            console.log(response);
        }).catch(error => {
            console.log(error);
        });
    }


    function GetImg() {
        axios.get('api/Download/GetImg')
            .then(function (response) {
                console.log(response);
            })
            .catch(function (error) {
                console.log(response);
            });
    }

    function downloadFile() {
        this.location.href = `${this.location.origin}/api/Download/downloadFile`;
    }
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TestModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TestModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TestModel>)PageContext?.ViewData;
        public TestModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
