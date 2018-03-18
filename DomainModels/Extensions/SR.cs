using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;

namespace DomainModels.Extensions
{
    public partial class SR
    {
        private static List<ResourceManager> managers { get; set; }
        private static CultureInfo cultureInfo { get; set; }
        private static string[] cultureNames = { "en-US", "ru-RU", "fr-FR"};

        static SR()
        {
            cultureInfo = new CultureInfo(cultureNames[0]);
            var dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyResources");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            managers = new List<ResourceManager>();
            var resourcesName = new List<string>();
            if (!System.IO.Directory.Exists(dir))
            {
                return;
            }
            foreach (var f in Directory.GetFiles(dir, "*.resources"))
            {
                if (!string.IsNullOrEmpty(f))
                {
                    var name = Path.GetFileName(f);
                    var delRes = name.Substring(0, name.IndexOf(".resources"));
                    name = delRes;
                    if (!resourcesName.Contains(name))
                    {
                        resourcesName.Add(name);
                    }
                }
            }
            foreach (var resource in resourcesName)
            {
                var mng = ResourceManager.CreateFileBasedResourceManager(resource, dir, null);
                managers.Add(mng);

            }
        }

        public static string T(string text)
        {
            string res = null;
            foreach (var resourceManager in managers.Where(r=>r.BaseName.Contains(cultureInfo.Name)))
            {
                res = resourceManager.GetString(text, cultureInfo);
                if (!string.IsNullOrEmpty(res)) break;
            }
            return res ?? text;
        }

        public static void ChangeLocation(int id)
        {
            if (id < 3 && cultureNames[id] != null)
            {
                cultureInfo = new CultureInfo(cultureNames[id]);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
        }
    }
}