using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.Extensions.Testing;

namespace MockCustomerImplementation
{
    public class BreadCrumbTest : BaseTest
    {
        public override void Test()
        {
            Item item = Package.GetByName(Package.PageName);
            if (item == null)
            {
                Logger.Error("no page found (is this a component template?)");
            }
            Page page = (Page)Engine.GetObject(item.GetAsSource().GetValue("ID"));
            Item actualItem = Package.GetByName("breadcrumbs");


            if (page.FileName.ToLower().Equals("index") && page.OrganizationalItem.Id.Equals(((Publication)page.ContextRepository).RootStructureGroup.Id))
            {
                if (actualItem != null || actualItem.GetAsString() != String.Empty)
                {
                    Fail("home page should not contain a bread crumb trail");
                }

                AssertPackageContains("breadcrumbs", "test");
            }
        }
    }
}
