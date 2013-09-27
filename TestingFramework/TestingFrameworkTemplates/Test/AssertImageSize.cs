using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace Tridion.Extensions.Testing.Templates
{
    [TcmTemplateParameterSchema(ParameterSchema = "resource:Tridion.Extensions.Testing.Templates.Resources.AssertImageSize.xsd")]
    [TcmTemplateTitle("Assert image is of correct size")]
    public class AssertImageSize : BaseTest
    {
        public override void Test()
        {
            if (Package.GetByName("variableName") == null)
            {
                Fail("variableName parameter is not set");
            }
            var isHeightSet = true;
            var isWidthSet = true;
            if (Package.GetByName("height") == null)
            {
                isHeightSet = false;
            }
            if (Package.GetByName("width") == null)
            {
                isWidthSet = false;
            }
            
            if (!isHeightSet && !isWidthSet)
            {
                Fail("Either height or width should be set");
            }


            if (Package.GetByName("variableName") == null)
            {
                Fail("variableName parameter should be set");
            }

            var variableName = Package.GetByName("variableName").GetAsString();

            var image = Package.GetByName(variableName);

            if (!image.ContentType.Value.Contains("image"))
            {
                Fail(String.Format("ContentType is {0} can't be converted into image", image.ContentType.Value));
            }
            
            var imageStream = image.GetAsStream();
            var bitmap = new Bitmap(imageStream);
            imageStream.Dispose();

            if (isHeightSet)
            {
                AreEqual(Package.GetByName("height").GetAsString(), bitmap.Height);
            }

            if (isWidthSet)
            {
                AreEqual(Package.GetByName("width").GetAsString(), bitmap.Width);
            }
        }
    }
}

