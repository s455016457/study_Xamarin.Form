using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace XamarinForm.Models
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 父级菜单
        /// </summary>
        public MenuItem ParentMenuItem { get; set; }
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
        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource Icon { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public IList<MenuItem> ChildrenMenu { get; set; }

        /// <summary>
        /// 构造一个菜单项
        /// </summary>
        /// <param name="MenuItemId">菜单ID</param>
        /// <param name="Title">菜单标题</param>
        /// <param name="Icon">菜单图标</param>
        public MenuItem(String MenuItemId, String Title, ImageSource Icon)
        {
            if (String.IsNullOrWhiteSpace(MenuItemId))
                throw new ArgumentNullException("菜单ID不能为空！");
            if (String.IsNullOrWhiteSpace(Title))
                throw new ArgumentNullException("菜单标题不能为空！");
            if (Icon==null)
                throw new ArgumentNullException("菜单图标不能为空！");
            //if (!Path.IsPathRooted(Icon))
            //    throw new IOException("菜单图标路径不正确！");

            this.MenuItemId = MenuItemId;
            this.Title = Title;
            this.Icon = Icon;
        }

        /// <summary>
        /// 菜单拥有子菜单
        /// </summary>
        /// <returns></returns>
        public bool HasChildren()
        {
            return ChildrenMenu != null && ChildrenMenu.Count > 0;
        }

        /// <summary>
        /// 菜单路径
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public String ToMenuPath(char separator)
        {
            if (ParentMenuItem != null)
            {
                String parentMenuItemId = ParentMenuItem.ToMenuPath(separator);
                return String.Format("{0}{1}{2}", parentMenuItemId, separator, MenuItemId);
            }
            else
                return MenuItemId;
        }
    }
}
