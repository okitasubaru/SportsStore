using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;
using System.Collections.Generic;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; } // thuộc tính của thẻ div bên List sử dụng 
        public string PageAction { get; set; } // thuộc tính của thẻ div bên List sử dụng 

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; }
                       = new Dictionary<string, object>();
        public bool PageClassesEnabled { get; set; } = false; // trang cho phép click mặc định bằng false vì đang ở trang đó không được click vô trang đó nữa

        public string PageClass { get; set; } 
        public string PageClassNormal { get; set; } 
        public string PageClassSelected { get; set; } // thay đổi về màu sắc hay nền để biết trang nào đang được chọn 
        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div"); // dùng taghelper tạo ra 1 thẻ div 
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a"); // dùng taghelper tạo ra nhiều thẻ a 
                PageUrlValues["productPage"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues); 

                tag.Attributes["href"] = urlHelper.Action(PageAction,
                    new { productPage = i });
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage
                    ? PageClassSelected : PageClassNormal); 
                }
                tag.InnerHtml.Append(i.ToString()); // truyền tag Inner truyền số vào thẻ a 
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
