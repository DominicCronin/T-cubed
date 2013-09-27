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
        public void SetPackageItem(string name, string value, bool overwrite = true)
        {
            SetPackageItem(name, value, ContentType.Text, overwrite);
        }
        public void SetPackageItem(string name, string value, ContentType contentType, bool overwrite = true)
        {
            // if there is already an item by the specified name, remove it from the package first
            var originalItem = this.Package.GetByName(name);
            if (originalItem != null && !overwrite)
            {
                LogMessage(string.Format("unable to set package item with name {0}, item already exists", name));
                return;
            }
            if (originalItem != null && overwrite)
                this.Package.Remove(originalItem);

            Item packageItem;



            if (contentType == ContentType.Html
                || contentType == ContentType.Text
                || contentType == ContentType.Xhtml)
            {
                packageItem = Package.CreateStringItem(contentType, value);
            }
            else
            {
                LogMessage(string.Format("unable to set package item with name {0}: unsupported content type {1}", name, contentType));
                return;
            }


            // push item into the package
            this.Package.PushItem(name, packageItem);

        }

        public void SetPackageItem(string name, XmlDocument value, ContentType contentType, bool overwrite = true)
        {
            // if there is already an item by the specified name, remove it from the package first
            var originalItem = this.Package.GetByName(name);
            if (originalItem != null && !overwrite)
            {
                LogMessage(string.Format("unable to set package item with name {0}, item already exists", name));
                return;
            }
            if (originalItem != null && overwrite)
                this.Package.Remove(originalItem);

            Item packageItem;



            if (contentType == ContentType.Xml
                || contentType == ContentType.Page
                || contentType == ContentType.Component)
            {
                packageItem = Package.CreateXmlDocumentItem(contentType, value);
            }
            else
            {
                LogMessage(string.Format("unable to set package item with name {0}: unsupported content type {1}", name, contentType));
                return;
            }


            // push item into the package
            this.Package.PushItem(name, packageItem);

        }


        #endregion
    }
}
