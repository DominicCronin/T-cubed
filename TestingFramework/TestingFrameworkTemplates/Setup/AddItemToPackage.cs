using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace Tridion.Extensions.Testing.Templates.Setup
{
    [TcmTemplateParameterSchema(ParameterSchema = "resource:Tridion.Extensions.Testing.Templates.Resources.AddItemToPackage.xsd")]
    [TcmTemplateTitle("Add item to package")]
    public class AddItemToPackage : BaseSetup
    {
        public override void Setup()
        {
            if (Package.GetByName("variableName") == null)
            {
                throw new Exception("variableName parameter is not set");
            }

            if (Package.GetByName("newValue") == null)
            {
                throw new Exception("newValue parameter is not set");
            }

            SetPackageItem(Package.GetByName("variableName").GetAsString(),Package.GetByName("newValue").GetAsString(), true);
//            Package.PushItem(Package.GetByName("variableName").GetAsString(), Package.CreateStringItem(ContentType.Text, Package.GetByName("newValue").GetAsString()));
        }
    }
}
