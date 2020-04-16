using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.Models
{
    public class DataItem
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public String MenuItemId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }
        public DataItem(String MenuItemId, String Title, String Description)
        {
            this.MenuItemId = MenuItemId;
            this.Title = Title;
            this.Description = Description;
        }
    }
}
