using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.Model
{
    public class Proposal
    {
        public int Id { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        [DisplayName("职位")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Email不能为空")]
        public string Email { get; set; }

        [Required(ErrorMessage = "电话/手机不能为空")]
        [DisplayName("电话/手机")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "公司名称不能为空")]
        [DisplayName("公司名称")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "项目名称不能为空")]
        [DisplayName("项目名称")]
        public string ProjectTitle { get; set; }

        [DisplayName("使用技术")]
        public string Technology { get; set; }

        [DisplayName("描述")]
        public string Description { get; set; }

        [DisplayName("其他说明")]
        public string OtherComments { get; set; }

        [DisplayName("文档(一)")]
        public string UploadDocument1 { get; set; }

        [DisplayName("文档(二)")]
        public string UploadDocument2 { get; set; }
    }

    public class ProposalFilter
    {
        /// <summary>
        /// 模糊匹配：Name, Email, Telephone, CompanyName, ProjectTitle
        /// </summary>
        public string FuzzyItem { get; set; }

        public string GetFilterString()
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(FuzzyItem))
            {
                result += string.Format(" AND ([Name] LIKE '%{0}%' OR [Email] LIKE '%{0}%' OR [Telephone] LIKE '%{0}%' OR [CompanyName] LIKE '%{0}%' OR [ProjectTitle] LIKE '%{0}%')", FuzzyItem);
            }

            return result;
        }
    }
}
