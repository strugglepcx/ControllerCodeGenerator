using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace ControllerCodeGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Type serviceType = null;
            string businessName;
            var serviceFileName = ConfigurationManager.AppSettings["ServiceFileName"];
            do
            {
                Console.WriteLine("请输入业务模块名称");

                businessName = Console.ReadLine();
                try
                {
                    serviceType = Assembly.LoadFrom(serviceFileName).GetTypes().FirstOrDefault(type => type.Name == $"I{businessName}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (serviceType.IsNull())
                {
                    Console.WriteLine("未能找到该业务类型！");
                }

            } while (serviceType.IsNull());

            var dynamicServiceDescriptor = new DynamicServiceDescriptor(businessName);
            foreach (var method in serviceType.GetMethods())
            {
                dynamicServiceDescriptor.MethodDescriptorList.Add(new MethodDescriptor(method));
            }


            var config = new TemplateServiceConfiguration
            {
                DisableTempFileLocking = true,
                CachingProvider = new DefaultCachingProvider(t => { })
            };
            Engine.Razor = RazorEngineService.Create(config);

            Console.WriteLine("请选择代码类型：1.Controller代码 2.Js服务代码");
            string generatorCode = null;
            var isGenerator = false;
            do
            {
                string template;
                switch (Console.ReadLine())
                {
                    case "1":
                        try
                        {
                            template = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\ControllerTemplate.razor"));
                            generatorCode = Engine.Razor.RunCompile(template, "controller", null,
                                dynamicServiceDescriptor);

                            isGenerator = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case "2":
                        try
                        {
                            template = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\JsServiceTemplate.razor"));
                            generatorCode = Engine.Razor.RunCompile(template, "js", null,
                                dynamicServiceDescriptor);
                            isGenerator = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    default:
                        Console.WriteLine("无效的代码类型，请重新输入！");
                        break;
                }
            } while (!isGenerator);

            Console.WriteLine(generatorCode);
            try
            {
                Clipboard.SetText(generatorCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("已经复制到剪切板！");
            Console.ReadLine();
        }
    }
}
