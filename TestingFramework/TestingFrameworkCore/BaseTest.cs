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
        public bool AssertPackageContains(string name, string value = null)
        {
            if (value == null)
                return Package.GetByName(name) != null;
            return Package.GetByName(name) != null ? Package.GetByName(name).GetAsString().Equals(value) : false;
        }

        protected void AreEqual(object expected, object actual, string errorMessage = null)
        {
            if (expected is string)
            {
                if (expected.ToString() != actual.ToString())
                {
                    throw new Exception(string.Format("Expected: {0} Actual: {1} {2}", expected, actual, errorMessage));
                }
            }
            else
            {
                if (expected != actual)
                {
                    throw new Exception(string.Format("Expected: {0} Actual: {1} {2}", expected, actual, errorMessage));
                }
            }
            LogMessage(string.Format("Expected: {0} Actual: {1}", expected, actual));
        }

        protected void AreNotEqual(object expected, object actual, string errorMessage = null)
        {
            if (expected is string)
            {
                if (expected.ToString() == actual.ToString())
                {
                    throw new Exception(string.Format("Expected something other than: {0} Actual: {1} {2}", expected, actual, errorMessage));
                }
            }
            else
            {
                if (expected != actual)
                {
                    throw new Exception(string.Format("Expected something other than: {0} Actual: {1} {2}", expected, actual, errorMessage));
                }
            }
            LogMessage(string.Format("Expected something other than: {0} Actual: {1}", expected, actual));
        }

        protected void Fail(string errorMessage = null)
        {
            if (errorMessage == null)
                errorMessage = "test failed without message";
            throw new Exception(errorMessage);
        }

        #endregion
    }
}
