using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.Utilities
{
    class TypeInformation
    {
        public Type Type { get; private set; }
        public Type BaseType { get; private set; }

        public Type[] GenericTypeParameters { get; private set; }

        /// <summary>
        /// 父类型为泛型
        /// </summary>
        Boolean isBaseGenericType;
        /// <summary>
        /// 父类泛类型Type
        /// </summary>
        Type baseGenericTypeDef;

        public TypeInformation(Type type)
        {
            Type = type;
            BaseType = type.BaseType;
            if(Type.IsGenericType)
                GenericTypeParameters = Type.GetTypeInfo().GenericTypeParameters;

            if (BaseType != null)
            {
                isBaseGenericType = BaseType.IsGenericType;
                if (isBaseGenericType)
                {
                    baseGenericTypeDef = BaseType.GetGenericTypeDefinition();
                }
            }
        }

        /// <summary>
        /// 是否是继承至parentType
        /// </summary>
        /// <param name="parentType">父类型</param>
        /// <returns></returns>
        public Boolean IsDerivedDirectlyFrom(Type parentType)
        {
            if (BaseType == null) return false;

            if (isBaseGenericType)
            {
                return baseGenericTypeDef.Equals(parentType);
            }
            return BaseType.Equals(parentType);
        }
    }
}
