using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerCodeGenerator
{
    /// <summary>
    /// 动态服务描述
    /// </summary>
    public class DynamicServiceDescriptor
    {
        public DynamicServiceDescriptor(string businessName)
            : this()
        {
            BusinessName = businessName;
            LBusinessName = businessName.Hump();
        }
        public DynamicServiceDescriptor()
        {
            MethodDescriptorList = new List<MethodDescriptor>();
        }
        /// <summary>
        /// 业务名称
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// 小写开头业务名称
        /// </summary>
        public string LBusinessName { get; set; }

        /// <summary>
        /// 方法描述对象
        /// </summary>
        public List<MethodDescriptor> MethodDescriptorList { get; set; }
    }
}
