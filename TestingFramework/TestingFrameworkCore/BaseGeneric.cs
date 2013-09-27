using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
