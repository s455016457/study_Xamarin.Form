using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.Utilities
{
    public class ClassAndSubclasses
    {
        public ClassAndSubclasses(Type parent,String ShowName)
        {
            Type = parent;
            this.ShowName = ShowName;
            Subclasses = new List<ClassAndSubclasses>();
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public String ShowName { get; private set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { private set; get; }
        /// <summary>
        /// 子类
        /// </summary>
        public List<ClassAndSubclasses> Subclasses { private set; get; }
    }
}
