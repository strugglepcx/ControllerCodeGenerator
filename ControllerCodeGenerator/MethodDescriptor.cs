using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ControllerCodeGenerator
{
    /// <summary>
    /// 方法描述
    /// </summary>
    public class MethodDescriptor
    {
        public MethodDescriptor(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            MethodName = methodInfo.Name;
            LMethodName = methodInfo.Name.Hump();
            ReturnTypeName = MethodInfo.ReturnType.Name;
            FirstParameterTypeName = methodInfo.GetParameters().FirstOrDefault()?.ParameterType.Name;
        }

        /// <summary>
        /// 方法元数据
        /// </summary>
        public MethodInfo MethodInfo { get; private set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// 小写开头方法名称
        /// </summary>
        public string LMethodName { get; private set; }

        /// <summary>
        /// 返回值名称
        /// </summary>
        public string ReturnTypeName { get; private set; }

        /// <summary>
        /// 第一个参数名称
        /// </summary>
        public string FirstParameterTypeName { get; private set; }

        /// <summary>
        /// 是否有参数
        /// </summary>
        public bool HasParameter => !FirstParameterTypeName.IsNullOrWhiteSpace();

        /// <summary>
        /// 是否无返回值方法
        /// </summary>
        public bool IsVoid => ReturnTypeName == "Void";

    }
}
