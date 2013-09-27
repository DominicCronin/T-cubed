using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace Tridion.Extensions.Testing.Templates
{
    [TcmTemplateParameterSchema(ParameterSchema = "resource:Tridion.Extensions.Testing.Templates.Resources.AssertString.xsd")]
    [TcmTemplateTitle("Assert string variable is correct")]
    public class AssertString : BaseTest
    {
        public override void Test()
        {
            if (Package.GetByName("variableName") == null)
            {
                Fail("variableName parameter is not set");
            }

            if (Package.GetByName("expectedValue") == null)
            {
                Fail("expectedValue parameter is not set");
            }

            var variableName = Package.GetByName("variableName").GetAsString();
            if (Package.GetByName(variableName) == null)
            {
                Fail(string.Format("Variable {0} not found in the package", variableName));
            }
            var variableValue = Package.GetByName(variableName).GetAsString();

            AreEqual(Package.GetByName("expectedValue").GetAsString(), variableValue);
        }
    }
}

