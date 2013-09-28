using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace Tridion.Extensions.Testing
{
    public abstract class BaseGeneric : ITemplate
    {
        protected Engine Engine { get; set; }
        protected Package Package { get; set; }
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

        public void Transform(Engine engine, Package package)
        {
            Engine = engine;
            Package = package;
            Transform();
        }
        public abstract void Transform();

        public void LogMessage(string message)
        {
            Logger.Warning("[Testing Framework] " + message);
        }

        public XmlNamespaceManager NamespaceManager
        {
            get
            {
                var nsm = new XmlNamespaceManager(new NameTable());                     
                nsm.AddNamespace("tcm", "http://www.tridion.com/ContentManager/5.0");
                nsm.AddNamespace("xlink", "http://www.w3.org/1999/xlink");
                return nsm;
            }
        }
    }
}
