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
            // load xml document from embedded resource
            XmlDocument itemDoc = new XmlDocument();
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MockCustomerImplementation.Testing.Setup.ComponentXml.xml"))
            using (XmlTextReader reader = new XmlTextReader(manifestResourceStream))
            {
                itemDoc.Load(reader);
            }

            SetPackageItem(Package.ComponentName, itemDoc, ContentType.Component);
        }
    }
}
