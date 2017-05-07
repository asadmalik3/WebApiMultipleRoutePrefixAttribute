using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace _3bTechTalk.MultipleRoutePrefixAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class _3bTechTalkRoutePrefix : RoutePrefixAttribute
    {
        public int Order { get; set; }

        public _3bTechTalkRoutePrefix(string prefix) : this(prefix, 0) { }

        public _3bTechTalkRoutePrefix(string prefix, int order) : base(prefix)
		{
            Order = order;
        }        
    }
}