﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace Tridion.Extensions.Testing.Templates
{
    [TcmTemplateParameterSchema(ParameterSchema = "resource:Tridion.Extensions.Testing.Templates.Resources.AssertHtmlTag.xsd")]
    [TcmTemplateTitle("Assert html element has correct value")]
    public class AssertHtmlElement : BaseTest
    {
        public override void Test()
        {
            if (Package.GetByName("xpath") == null)
            {
                Fail("xpath parameter is not set");
            }
            if (Package.GetByName("expectedValue") == null)
            {
                Fail("expectedValue parameter is not set");
            }


            var outputName = "Output";
            if (Package.GetByName("outputName") != null) { outputName = Package.GetByName("outputName").GetAsString(); }

            if (Package.GetByName(outputName) == null)
            {
                Fail(string.Format("Output variable {0} not found in the package", outputName));
            }

            var html = new HtmlDocument();
            html.LoadHtml(Package.GetByName(outputName).GetAsString());

            var tag = html.DocumentNode.SelectSingleNode(Package.GetByName("xpath").GetAsString());
            if (tag == null)
            {
                Fail(string.Format("No element matching {0}", Package.GetByName("xpath").GetAsString()));
            }

            AreEqual(Package.GetByName("expectedValue").GetAsString(), tag.InnerHtml);
        }
    }
}

