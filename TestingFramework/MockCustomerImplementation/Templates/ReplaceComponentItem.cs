using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace TridionImplementationTestingSystem
{
    [TcmTemplateTitle("ReplaceComponentItemAssembly")]
    public class ReplaceComponentItem : ITemplate
    {
        public void Transform(Engine engine, Package package)
        {
            var component = package.GetByType(ContentType.Component);
        }
    }
}
