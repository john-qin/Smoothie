using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Smoothie.Domain.Dto;

namespace Smoothie.Web.Infrastructure
{
    public static class HtmlHelpers
    {
        public static string Pager(this HtmlHelper helper, PageListDto pageList)
        {
            var u = new UrlHelper(helper.ViewContext.RequestContext);

            var sb = new StringBuilder();
            sb.Append("<div class=\"pagination\"><ul>");

            if (pageList.HasPreviousPage)
            {
                sb.Append("<li><a href=\"#\" class=\"disabled\">Prev</a></li>");
            }
            else
            {
                sb.Append("<li><a href=\"#\" class=\"disabled\">Prev</a></li>");
            }


            sb.Append("<li><a href=\"#\">Next</a></li>");
            sb.Append("</ul></div>");

            return sb.ToString();
        }
    }
}