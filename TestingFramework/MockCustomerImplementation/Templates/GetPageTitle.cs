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
    [TcmTemplateTitle("Get page title")]
    public class GetPageTitle : ITemplate
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
            package.PushItem("pagetitle", package.CreateStringItem(ContentType.Text, string.Format("{0} ({1})", page.ContextRepository.Title, page.Title)));
        }


    }
}
