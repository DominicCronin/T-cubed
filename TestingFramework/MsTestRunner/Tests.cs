using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tridion.ContentManager.CoreService.Client;
using System.Configuration;

namespace Tridion.Extensions.Testing.MsTestRunner
{
    [TestClass]
    [DeploymentItem(".\\TestData.xlsx")]
    public class Tests
    {
        public TestContext TestContext { get; set; }

        [TestMethod, DataSource("System.Data.Odbc", "Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};dbq=|DataDirectory|/TestData.xlsx", "RenderTests$", DataAccessMethod.Sequential)]
        public void RenderTest()
        {
            if (TestContext.DataRow["Ignore"].ToString() == "TRUE")
            {
                return;
            }

            var templateId = TestContext.DataRow["TemplateId"].ToString();
            var itemId = TestContext.DataRow["ItemId"].ToString();

            var client = new SessionAwareCoreServiceClient();
            client.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationManager.AppSettings.Get("domain");
            client.ClientCredentials.Windows.ClientCredential.Password = ConfigurationManager.AppSettings.Get("password");
            client.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationManager.AppSettings.Get("username");

            var publishInstruction = new PublishInstructionData
            {
                RenderInstruction = new RenderInstructionData { RenderMode = RenderMode.PreviewDynamic },
                ResolveInstruction = new ResolveInstructionData { StructureResolveOption = StructureResolveOption.OnlyItems }
            };

            try
            {
                client.RenderItem(itemId, templateId, publishInstruction, ConfigurationManager.AppSettings.Get("publicationTargetId"));
            }
            catch (Exception ex)
            {

                Assert.Fail("Rendering {0} with {1} failed: {2}", templateId, itemId, ex.Message);
            }
        }
    }
}
