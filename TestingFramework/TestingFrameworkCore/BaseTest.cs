using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace Tridion.Extensions.Testing
{
    public abstract class BaseTest : BaseGeneric
    {


        public override void Transform()
        {
            Test();
        }

        public abstract void Test();


        #region Assertion methods
        protected bool AssertPackageContains(string name, string value = null)
        {
            if (value == null)
                return Package.GetByName(name) != null;
            return Package.GetByName(name) != null ? Package.GetByName(name).GetAsString().Equals(value) : false;
        }
        #endregion
    }
}
