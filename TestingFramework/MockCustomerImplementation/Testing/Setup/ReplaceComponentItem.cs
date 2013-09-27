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

namespace TridionImplementationTestingSystem
{
    [TcmTemplateTitle("ReplaceComponentItemAssembly")]
    public class ReplaceComponentItem : BaseSetup
    {
        public override void Setup()
        {
            XmlDocument itemDoc = new XmlDocument();

            string resourceName = "MockCustomerImplementation.Testing.Setup.ComponentXml.xml";
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (XmlTextReader reader = new XmlTextReader(manifestResourceStream))
            {
                itemDoc.Load(reader);
            }

            var componentItem = this.Package.GetByType(ContentType.Component);
            this.Package.Remove(componentItem);

            var modifiedComponentItem = this.Package.CreateXmlDocumentItem(ContentType.Component, itemDoc);
            this.Package.PushItem(Package.ComponentName, modifiedComponentItem);
        }

    }
}
