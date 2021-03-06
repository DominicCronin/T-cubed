﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace Tridion.Extensions.Testing.Templates.Setup
{
    [TcmTemplateParameterSchema(ParameterSchema = "resource:Tridion.Extensions.Testing.Templates.Resources.UpdateItemByXPath.xsd")]
    [TcmTemplateTitle("Update item by XPath")]
    public class UpdateItemByXPath : BaseSetup
    {
        public override void Setup()
        {
            Item xPathItem = Package.GetByName("xpath");
            if (xPathItem == null)
            {
                throw new ArgumentNullException("xpath parameter is not set");
            }
            
            string xpath = xPathItem.GetAsString();
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("XPath parameter may not be empty.","xpath");
            }

            Item newValueItem = Package.GetByName("newValue");
            if (newValueItem == null)
            {
                throw new ArgumentNullException("newValue parameter is not set");
            }
            string newValue = newValueItem.GetAsString();

            ContentType inputItemType = null; 

            Item item = null;
            if (Package.GetByName("packageItemName") != null)
            {
                string packageItemName = Package.GetByName("packageItemName").GetAsString();
                item = Package.GetByName(packageItemName);
                inputItemType = ContentType.Xml;
            }
            else
            {
                item = this.Package.GetByType(ContentType.Component);
                if (item != null)
                {
                    inputItemType = ContentType.Component;
                }
                else
                {
                    item = this.Package.GetByType(ContentType.Page);
                    if (item != null)
                    {
                        inputItemType = ContentType.Page;
                    }
                    else
                    {
                        throw new UnexpectedInputItemTypeException("Input item was neither a Component nor a Page");
                    }
                }
            }
            this.Package.Remove(item);

            // if extra namespaces are configured, load them into the NamespaceManager
            if (Package.GetByName("namespaces") != null)
            {

                foreach (string nsdef in Package.GetByName("namespaces").GetAsString().Split(','))
                {
                    string[] n = nsdef.Trim().Split('=');
                    NamespaceManager.AddNamespace(n[0], n[1]);
                }
            }

            var itemDoc = item.GetAsXmlDocument();
            var titleElement = itemDoc.SelectSingleNode(xpath, NamespaceManager);
            titleElement.InnerText = newValue;

            var modifiedItem = this.Package.CreateXmlDocumentItem(inputItemType, itemDoc);
            this.Package.PushItem(Package.ComponentName, modifiedItem);
            
        }

        [Serializable]
        public class UnexpectedInputItemTypeException : Exception
        {
            public UnexpectedInputItemTypeException() { }
            public UnexpectedInputItemTypeException(string message) : base(message) { }
            public UnexpectedInputItemTypeException(string message, Exception inner) : base(message, inner) { }
            protected UnexpectedInputItemTypeException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
        }
    }
}