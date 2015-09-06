using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewTech.Web.Models
{
    public class AlertMessage
    {
        public AlertCategory Category { get; set; }
        public string Message { get; set; }
        public string CategoryCSS
        {
            get
            {
                switch (Category)
                {
                    case AlertCategory.Success: return "alert alert-success";
                    case AlertCategory.Info: return "alert alert-info";
                    case AlertCategory.Warning: return "alert alert-warning";
                    case AlertCategory.Error: return "alert alert-danger";
                    default: return "alert alert-info";
                }

            }
        }

        public AlertMessage(string message)
            : this(AlertCategory.Success, message)
        {
        }

        public AlertMessage(AlertCategory category, string message)
        {
            this.Category = category;
            this.Message = message;
        }
    }

    public enum AlertCategory
    {
        Success,
        Info,
        Warning,
        Error
    }
}