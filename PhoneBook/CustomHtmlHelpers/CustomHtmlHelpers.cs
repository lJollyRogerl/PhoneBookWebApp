using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.CustomHtmlHelpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString LinkedImage(this HtmlHelper helper, string imgSrc, object htmlAttributes = null)
        {
            TagBuilder imgTb = new TagBuilder("img");
            imgTb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(imgSrc));

            TagBuilder linkTb = new TagBuilder("a");
            linkTb.InnerHtml = imgTb.ToString(TagRenderMode.SelfClosing);

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                linkTb.MergeAttributes(attributes);
            }
            return new MvcHtmlString(linkTb.ToString(TagRenderMode.Normal));
        }
    }
}