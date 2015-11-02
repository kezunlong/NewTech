using Lifepoem.Foundation.Utilities.Helpers;
using Lifepoem.Foundation.Web.Helpers;
using Lifepoem.Foundation.Web.MVC;
using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NewTech.Web.Infrastructure
{
    public static class CustomHelper
    {
        #region Dicts/Objects to SelectListItems

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };
        private static readonly SelectListItem[] SingleEmptyNumberItem = new[] { new SelectListItem { Text = "", Value = "0" } };
        public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<Dict> list, bool addEmptyItem = false)
        {
            var result = list.Select(item => new SelectListItem { Text = item.Name, Value = item.Id });
            if (addEmptyItem)
            {
                result = SingleEmptyItem.Concat(result);
            }
            return result;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<TElement>(this IEnumerable<TElement> list, bool addEmptyItem = false)
        {
            var result = list.Select(item => new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            if (addEmptyItem)
            {
                result = SingleEmptyItem.Concat(result);
            }
            return result;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<TElement>(this IEnumerable<TElement> list, Func<TElement, string> text, Func<TElement, string> value, bool addEmptyItem = false)
        {
            var result = list.Select(item => new SelectListItem { Text = text(item), Value = value(item) });
            if (addEmptyItem)
            {
                result = SingleEmptyItem.Concat(result);
            }
            return result;
        }

        #endregion

        #region Form Layout Help Methods

        /// <summary>
        /// Output an actionlink with disabled attribute or not.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString CDCActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, bool disabled)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, disabled ? (object)new { @class = "btn btn-default", @disabled = "disabled" } : (object)new { @class = "btn btn-default" });
        }
        public static MvcHtmlString CDCActionLinkSM(this HtmlHelper htmlHelper, string linkText, string actionName, bool disabled)
        {
            return htmlHelper.ActionLink(linkText, actionName, null, disabled ? (object)new { @class = "btn btn-default btn-sm", @disabled = "disabled" } : (object)new { @class = "btn btn-default btn-sm" });
        }

        /// <summary>
        ///     @Html.LabelFor(expression, new { @class = "col-sm-2 control-label" })
        ///     <div class="col-sm-4">
        ///         @Html.TextBoxFor(expression, new { @class = "form-control input-sm" })
        ///     </div>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isReadOnly = false)
        {
            object textBoxAttributes;
            if (isReadOnly)
            {
                textBoxAttributes = new { @class = "form-control input-sm", @readonly = "readonly" };
            }
            else
            {
                textBoxAttributes = new { @class = "form-control input-sm" };
            }

            return TextBoxWithLabelFor(htmlHelper, expression, textBoxAttributes);
        }
        public static MvcHtmlString TextBoxWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object textBoxAttributes)
        {
            var labelAttributes = new { @class = "col-sm-2 control-label" };
            var divClass = "col-sm-4";

            return TextBoxWithLabelFor(htmlHelper, expression, labelAttributes, textBoxAttributes, divClass);
        }
        public static MvcHtmlString TextBoxWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object labelAttributes, object textBoxAttributes, string divClass)
        {
            string result = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();

            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass(divClass);
            tag.InnerHtml = htmlHelper.TextBoxFor(expression, textBoxAttributes).ToHtmlString();

            result += tag.ToString();
            return new MvcHtmlString(result);
        }

        public static MvcHtmlString DropDownListWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper
            , Expression<Func<TModel, TProperty>> expression
            , IEnumerable<SelectListItem> list
            , bool isReadOnly = false)
        {
            object attributes;
            if (isReadOnly)
            {
                attributes = new { @class = "form-control input-sm", @readonly = "readonly" };
            }
            else
            {
                attributes = new { @class = "form-control input-sm" };
            }

            var labelAttributes = new { @class = "col-sm-2 control-label" };
            var divClass = "col-sm-4";

            string result = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();

            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass(divClass);
            tag.InnerHtml = htmlHelper.DropDownListFor(expression, list, attributes).ToHtmlString();

            result += tag.ToString();
            return new MvcHtmlString(result);
        }

        public static MvcHtmlString TextAreaWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isReadOnly = false)
        {
            object attributes;
            if (isReadOnly)
            {
                attributes = new { @class = "form-control input-sm", rows = "4", @readonly = "readonly" };
            }
            else
            {
                attributes = new { @class = "form-control input-sm", rows = "4" };
            }

            return TextAreaWithLabelFor(htmlHelper, expression, attributes);
        }

        public static MvcHtmlString TextAreaWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object textAreaAttributes)
        {
            var labelAttributes = new { @class = "col-sm-2 control-label" };
            var divClass = "col-sm-10";

            return TextAreaWithLabelFor(htmlHelper, expression, labelAttributes, textAreaAttributes, divClass);
        }

        public static MvcHtmlString TextAreaWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object labelAttributes, object textAreaAttributes, string divClass)
        {
            string result = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();

            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass(divClass);
            tag.InnerHtml = htmlHelper.TextAreaFor(expression, textAreaAttributes).ToHtmlString();

            result += tag.ToString();
            return new MvcHtmlString(result);
        }


        #endregion

    }
}