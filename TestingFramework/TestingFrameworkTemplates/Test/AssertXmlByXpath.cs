using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace Tridion.Extensions.Testing.Templates
{
    [TcmTemplateParameterSchema(ParameterSchema = "resource:Tridion.Extensions.Testing.Templates.Resources.AssertXmlByXpath.xsd")]
    [TcmTemplateTitle("Run xpath on XML document")]
    public class AssertXmlByXpath : BaseTest
    {
        public override void Test()
        {
            if (Package.GetByName("xpath") == null)
            {
                throw new Exception("xpath parameter is not set");
            }
            if (Package.GetByName("expectedValue") == null)
            {
                throw new Exception("expectedValue parameter is not set");
            }


            var parameterName = "Output";
            if (Package.GetByName("parameterName") != null) { parameterName = Package.GetByName("parameterName").GetAsString(); }

            if (Package.GetByName(parameterName) == null)
            {
                throw new Exception(string.Format("Output variable {0} not found in the package", parameterName));
            }

            var xml = new XmlDocument();
            xml.LoadXml(Package.GetByName(parameterName).GetAsString());


            XmlNode nodeToCheck;
            if (Package.GetByName("namespaces") != null)
            {
                var nsmgr = new XmlNamespaceManager(xml.NameTable);
                foreach (string nsdef in Package.GetByName("namespaces").GetAsString().Split(','))
                {
                    string[] n = nsdef.Trim().Split('=');
                    nsmgr.AddNamespace(n[0], n[1]);
                }
                nodeToCheck = xml.SelectSingleNode(Package.GetByName("xpath").GetAsString(), nsmgr);
            }
            else
            {
                nodeToCheck = xml.SelectSingleNode(Package.GetByName("xpath").GetAsString());
            }

            if (nodeToCheck == null)
            {
                Fail(string.Format("No node matching {0}", Package.GetByName("xpath").GetAsString()));
            }

            string actual = nodeToCheck is XmlAttribute ? ((XmlAttribute)nodeToCheck).Value : ((XmlElement)nodeToCheck).InnerText;
            AreEqual(Package.GetByName("expectedValue").GetAsString(), actual);
        }
    }
}

