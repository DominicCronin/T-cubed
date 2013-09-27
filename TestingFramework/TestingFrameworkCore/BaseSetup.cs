using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void LoadXmlItemFromEmbeddedResource(string packageItemName, string resourcePath)
        {

        }
        #endregion
    }
}
