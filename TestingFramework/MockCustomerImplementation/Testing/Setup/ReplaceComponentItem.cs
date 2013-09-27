using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;
using Tridion.Extensions.Testing;

namespace MockCustomerImplementation.Testing.Setup
{
    [TcmTemplateTitle("Replace component item from assembly")]
    public class ReplaceComponentItem : BaseSetup
    {
        public override void Setup()
        {
            AddEmbeddedResourceToPackage(Package.ComponentName, "MockCustomerImplementation.Testing.Setup.ComponentXml.xml", ContentType.Component);
        }
    }
}
