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

namespace Tridion.Extensions.Testing
{
    public abstract class BaseSetup : BaseGeneric
    {


        public override void Transform()
        {
            Setup();
        }

        public abstract void Setup();


        #region Package manipulation methods
        public void SetPackageItem(string name, string value)
        {
            Package.GetByName(name).SetAsString(value);
        }

        public void AddEmbeddedResourceToPackage(string packageItemName, string resourcePath, ContentType contentType)
        {
            Item packageItem;
            if (contentType == ContentType.Xml || contentType == ContentType.Component || contentType == ContentType.Page)
            {
                XmlDocument itemDoc = new XmlDocument();
                using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
                using (XmlTextReader reader = new XmlTextReader(manifestResourceStream))
                {
                    itemDoc.Load(reader);
                    packageItem = Package.CreateXmlDocumentItem(contentType, itemDoc);
                }
            }
            else
            {
                using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
                using (TextReader reader = new StreamReader(manifestResourceStream))
                {
                    string s = reader.ReadToEnd();
                    packageItem = Package.CreateStringItem(contentType, s);
                }
            }

            // if there is already an item by the specified name, remove it from the package first
            var originalItem = this.Package.GetByName(packageItemName);
            if (originalItem != null)
                this.Package.Remove(originalItem);

            // push item into the package
            this.Package.PushItem(packageItemName, packageItem);

        }
        #endregion
    }
}
