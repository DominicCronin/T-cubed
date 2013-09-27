using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.Templating;

namespace Tridion.Extensions.Testing.Templates.Setup
{
    public class AddItemToPackage : BaseSetup
    {
        public override void Setup()
        {
            Package.PushItem("test", Package.CreateStringItem(ContentType.Text, this.GetType().FullName));
        }
    }
}
