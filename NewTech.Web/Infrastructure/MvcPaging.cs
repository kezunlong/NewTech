using Lifepoem.Foundation.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NewTech.Web.Infrastructure
{
    public class PagingUIOption
    {
        public string FirstPageText { get; set; }
        public string PrevPageText { get; set; }
        public string NextPageText { get; set; }
        public string LastPageText { get; set; }
        public int TotalPageLink { get; set; }
        public string CssClass { get; set; }
        public string PageCssClass { get; set; }
        public string CurrentPageCssClass { get; set; }
        public string DisablePageCssClass { get; set; }
        public string CustomInfoHTML { get; set; }
        public string CustomInfoPosition { get; set; }
        public string CustomInfoCssClass { get; set; }
    }

    public class PagingUIFactory
    {
        public static PagingUIOption GetDefaultPagingUI()
        {
            return new PagingUIOption
            {
                FirstPageText = "First",
                PrevPageText = "Previous",
                NextPageText = "Next",
                LastPageText = "Last",
                TotalPageLink = 10,
                CustomInfoHTML = string.Empty,
                CustomInfoPosition = string.Empty,
                CustomInfoCssClass = "custominfo"
            };
        }

        public static PagingUIOption GetBootstrapPagingUI()
        {
            return new PagingUIOption
            {
                FirstPageText = "<span class='icon fa fa-step-backward'></span>",
                PrevPageText = "<span class='icon fa fa-chevron-left'></span>",
                NextPageText = "<span class='icon fa fa-chevron-right'></span>",
                LastPageText = "<span class='icon fa fa-step-forward'></span>",
                CssClass = "pagination pagination-sm",
                PageCssClass = "",
                CurrentPageCssClass = "active",
                DisablePageCssClass = "disabled",
                TotalPageLink = 10,
                CustomInfoHTML = "Records: {TotalItems}, Pages: {TotalPages}",
                CustomInfoCssClass = "custominfo"
            };
        }
    }

    /// <summary>
    /// Create Pagination using:
    /// In order to support displaying custom information in left or right, we need to add table layout
    /// Because the pagination has a "display:inline-block" attribute, make it impossible to display info in the same line.
    /// 
    /// <table style="border:0; border-collapse:collapse;">
    ///     <tr>
    ///         <td>
    ///             <span>custom information</span>
    ///         </td>
    ///         <td>
    ///             <ul>
    ///                 <li><a href="#">First</a></li>
    ///                 <li><a href="#">Previous</a></li>
    ///                 <li><a href="#">...</a></li>
    ///                 <li><a href="#">1</a></li>
    ///                 <li class="active" disabled="disabled"><a href="#">2</a></li>
    ///                 <li><a href="#">3</a></li>
    ///                 <li><a href="#">4</a></li>
    ///                 <li><a href="#">5</a></li>
    ///                 <li><a href="#">...</a></li>
    ///                 <li><a href="#">Next</a></li>
    ///                 <li><a href="#">Last</a></li>
    ///             </ul>
    ///         </td>
    ///     </tr>
    /// </table>
    /// </summary>
    public class MvcPaging
    {
        public WebPagingOption PagingOption { get; set; }
        public PagingUIOption UIOption { get; set; }
        public Func<int, string> PageUrl { get; set; }

        public MvcPaging() { }

        public MvcHtmlString PageLinks()
        {
            if (PagingOption == null || PagingOption.TotalPages <= 1)
            {
                return new MvcHtmlString("");
            }

            string customInfo = GetCustomInfoHTML();

            TagBuilder ulTag = new TagBuilder("ul");
            AddCssClass(ulTag, UIOption.CssClass);

            StringBuilder links = new StringBuilder();
            links.Append(GetPageURL(1, UIOption.FirstPageText));
            links.Append(GetPageURL(PagingOption.CurrentPage - 1, UIOption.PrevPageText));

            int start = PagingOption.CurrentPage / UIOption.TotalPageLink;
            if (PagingOption.CurrentPage % UIOption.TotalPageLink == 0) start -= 1;
            start = start * UIOption.TotalPageLink + 1;
            int end = start + UIOption.TotalPageLink - 1;
            if (end > PagingOption.TotalPages) end = PagingOption.TotalPages;

            if (start > UIOption.TotalPageLink)
            {
                links.Append(GetPageURL(start - 1, "..."));
            }

            for (int i = start; i <= end; i++)
            {
                links.Append(GetPageURL(i, i.ToString()));
            }

            if (end < PagingOption.TotalPages)
            {
                links.Append(GetPageURL(end + 1, "..."));
            }
            links.Append(GetPageURL(PagingOption.CurrentPage + 1, UIOption.NextPageText));
            links.Append(GetPageURL(PagingOption.TotalPages, UIOption.LastPageText));

            ulTag.InnerHtml = links.ToString();

//            string result = @"<table style=""border:0; border-collapse:collapse;"">
//	<tr>
//		<td>{0}</td>
//		<td>{1}</td>
//	</tr>
//</table>";

            string result = "{0}{1}";
            bool isCustomerInfoLeft = UIOption.CustomInfoPosition != "right";
            result = string.Format(result, isCustomerInfoLeft ? customInfo : ulTag.ToString(), isCustomerInfoLeft ? ulTag.ToString() : customInfo);
            return new MvcHtmlString(result.ToString());
        }

        private string GetCustomInfoHTML()
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(UIOption.CustomInfoHTML))
            {
                TagBuilder tag = new TagBuilder("span");

                string customInfo = UIOption.CustomInfoHTML;
                customInfo = customInfo.Replace("{TotalItems}", PagingOption.TotalItems.ToString());
                customInfo = customInfo.Replace("{TotalPages}", PagingOption.TotalPages.ToString());
                customInfo = customInfo.Replace("{CurrentPage}", PagingOption.CurrentPage.ToString());
                customInfo = customInfo.Replace("{ItemsPerPage}", PagingOption.ItemsPerPage.ToString());
                AddCssClass(tag, UIOption.CustomInfoCssClass);
                tag.InnerHtml = customInfo;

                result = tag.ToString();
            }

            return result;
        }

        public string GetPageURL(int page, string pageText)
        {
            if (string.IsNullOrEmpty(pageText))
            {
                return string.Empty;
            }

            if (page < 1) page = 1;
            if (page > PagingOption.TotalPages) page = PagingOption.TotalPages;

            TagBuilder liTag = new TagBuilder("li");
            AddCssClass(liTag, UIOption.PageCssClass);
            if (PagingOption.CurrentPage == page)
            {
                if (page.ToString() == pageText)
                {
                    AddCssClass(liTag, UIOption.CurrentPageCssClass);
                }
                else
                {
                    AddCssClass(liTag, UIOption.DisablePageCssClass);
                }
            }

            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", PageUrl(page));
            tag.InnerHtml = string.IsNullOrEmpty(pageText) ? page.ToString() : pageText;
            if (PagingOption.CurrentPage == page)
            {
                tag.MergeAttribute("disabled", "disabled");
            }

            liTag.InnerHtml = tag.ToString();
            return liTag.ToString();
        }

        private void AddCssClass(TagBuilder tag, string cssClass)
        {
            if (!string.IsNullOrEmpty(cssClass))
            {
                tag.AddCssClass(cssClass);
            }
        }
    }
}