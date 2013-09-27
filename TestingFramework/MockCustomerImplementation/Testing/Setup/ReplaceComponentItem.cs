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

namespace TridionImplementationTestingSystem
{
    [TcmTemplateTitle("ReplaceComponentItemAssembly")]
    public class ReplaceComponentItem : ITemplate
    {
        public void Transform(Engine engine, Package package)
        {
            XmlDocument itemDoc = new XmlDocument();

            string resourceName = "MockCustomerImplementation.Testing.Setup.ComponentXml.xml";
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (XmlTextReader reader = new XmlTextReader(manifestResourceStream))
            {
                itemDoc.Load(reader);
            }

            var componentItem = package.GetByType(ContentType.Component);
            package.Remove(componentItem);

            var modifiedComponentItem = package.CreateXmlDocumentItem(ContentType.Component, itemDoc);
            package.PushItem(Package.ComponentName, modifiedComponentItem);
        }
    }
}
