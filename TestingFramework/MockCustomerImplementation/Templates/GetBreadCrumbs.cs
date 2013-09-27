using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace MockCustomerImplementation.Templates
{
    [TcmTemplateTitle("Get bread crumbs")]
    public class GetBreadCrumbs : ITemplate
    {
        private TemplatingLogger _logger = null;
        protected virtual TemplatingLogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = TemplatingLogger.GetLogger(this.GetType());
                }
                return _logger;
            }
        }

        public void Transform(Tridion.ContentManager.Templating.Engine engine, Tridion.ContentManager.Templating.Package package)
        {
            Item item = package.GetByName(Package.PageName);
            if (item == null)
            {
                Logger.Error("no page found (is this a component template?)");
            }
            Page page = (Page)engine.GetObject(item.GetAsSource().GetValue("ID"));
            
            StringBuilder sb = new StringBuilder();
            sb.Append(page.Title);
            StructureGroup sg = page.OrganizationalItem as StructureGroup;
            while (sg.Id != ((Publication)page.ContextRepository).RootStructureGroup.Id)
            {
                sb.Insert(0, sg.Title + " > ");
                sg = sg.OrganizationalItem as StructureGroup;
            }
            package.CreateStringItem(ContentType.Html, sb.ToString());

        }


    }
}
