using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace XamarinForm.Utilities
{
    public class ClassAndSubclassesFactory
    {
        static Assembly xamarinFormsAssembly;
        public static ClassAndSubclasses Create(Type t) 
        {
            List<TypeInformation> classList = new List<TypeInformation>();

            //获取此程序集
            xamarinFormsAssembly = t.Assembly;
            //循环此程序集中定义的公共类型的集合，这些公共类型在程序集外可见。
            foreach (Type type in xamarinFormsAssembly.ExportedTypes)
            {
                if (type.IsPublic && !type.IsInterface)
                    classList.Add(new TypeInformation(type));
            }

            //获取此程序集中公共类的父类
            int index = 0;
            do
            {
                TypeInformation childType = classList[index++];

                if (childType.Type.Equals(typeof(object)))
                {
                    continue;
                }

                Boolean hasBaseType = false;
                foreach (TypeInformation parentType in classList)
                {
                    if (childType.IsDerivedDirectlyFrom(parentType.Type))
                    {
                        hasBaseType = true;
                        break;
                    }
                }

                if (!hasBaseType && !childType.Type.Equals(typeof(object)))
                    classList.Add(new TypeInformation(childType.BaseType));

            } while (index < classList.Count);

            //根据类型名称排序
            classList.OrderBy(p => p.Type.Name);
            ClassAndSubclasses rootClass =  new ClassAndSubclasses(typeof(Object), typeof(Object).FullName);
            AddChildrenToParent(rootClass, classList);
            return rootClass;
        }

        /// <summary>
        /// 组装类继承树状结构
        /// </summary>
        /// <param name="parentClass">父类</param>
        /// <param name="classList">类列表</param>
        static void AddChildrenToParent(ClassAndSubclasses parentClass, List<TypeInformation> classList)
        {
            foreach (TypeInformation typeInformation in classList)
            {
                if (typeInformation.IsDerivedDirectlyFrom(parentClass.Type))
                {
                    String name = typeInformation.Type.Name;
                    if (typeInformation.Type.Assembly != xamarinFormsAssembly)
                    {
                        name = typeInformation.Type.FullName;
                    }
                    if (typeInformation.Type.IsGenericType&& typeInformation.GenericTypeParameters!=null)
                    {
                        name = name.Substring(0, name.Length - 2);
                        name += "<";

                        for (int i = 0; i < typeInformation.GenericTypeParameters.Length; i++)
                        {
                            name += typeInformation.GenericTypeParameters[i].Name;
                            if (i < typeInformation.GenericTypeParameters.Length - 1)
                                name += ", ";
                        }
                        name += ">";
                    }
                    ClassAndSubclasses subClass = new ClassAndSubclasses(typeInformation.Type, name);
                    parentClass.Subclasses.Add(subClass);
                    AddChildrenToParent(subClass, classList);
                }
            }
        }
    }
}
