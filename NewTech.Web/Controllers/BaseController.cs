using Lifepoem.Foundation.Utilities.DBHelpers;
using NewTech.BLL;
using NewTech.Model;
using NewTech.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewTech.Web.Controllers
{
    public class BaseController : Controller, IDisposable
    {
        #region Properties

        private NewTechBll _bll = null;

        protected NewTechBll bll
        {
            get
            {
                if (_bll == null)
                {
                    _bll = new NewTechBll(AppConfig.SQLConnString);
                }
                return _bll;
            }
        }

        /// <summary>
        /// Current logon user
        /// </summary>
        public User LogonUser
        {
            get
            {
                var item = SessionHelper.Get<User>(Session, SessionKey.LOGON_USER);
                if (item == null)
                {
                    throw new Exception("Session过期, 请重新登陆。");
                }
                return item;
            }
        }


        #endregion

        #region Paging Option

        protected int PageSize5 = 5;
        protected int PageSize15 = 15;

        protected PagingOption GetPagingOption(int page)
        {
            return GetPagingOption(page, PageSize5);
        }

        protected PagingOption GetPagingOption15(int page)
        {
            return GetPagingOption(page, PageSize15);
        }

        protected PagingOption GetPagingOption(int page, int pageSize)
        {
            PagingOption option = new PagingOption();
            option.Start = (page - 1) * pageSize + 1;
            option.Length = pageSize;
            option.GetRecordCount = true;
            return option;
        }


        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_bll != null)
                {
                    _bll.Dispose();
                    _bll = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Public Methods

        public void HtmlTextWriteExcel<T>(T dtData, string filename)
        {
            HtmlTextWriteExcel<T>(dtData, filename, null, null, null);
        }

        public void HtmlTextWriteExcel<T>(T dtData, string filename, DataGrid dgExport, List<BoundColumn> columns, Action<DataGrid> formatHandler)
        {
            // 设置编码和附件格式 
            System.Web.HttpContext current = System.Web.HttpContext.Current;
            current.Response.ContentType = "application/vnd.ms-excel";
            current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=gb2312>");
            current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            current.Response.Charset = "GB2312";
            current.Response.AppendHeader("content-disposition", "inline; filename=" + filename + ".xls");
            current.Response.Flush();

            // 导出excel文件 
            StringWriter strWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(strWriter);

            // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid 
            if (dgExport == null)
            {
                dgExport = new DataGrid();
            }
            if (columns != null && columns.Count > 0)
            {
                dgExport.AutoGenerateColumns = false;
                foreach (var column in columns)
                {
                    dgExport.Columns.Add(column);
                }
            }
            dgExport.DataSource = dtData;
            dgExport.AllowPaging = false;
            dgExport.DataBind();
            if (formatHandler != null)
            {
                formatHandler(dgExport);
            }
            dgExport.RenderControl(htmlWriter);

            current.Response.Write(strWriter.ToString());
            current.Response.End();
        }

        #endregion

    }
}