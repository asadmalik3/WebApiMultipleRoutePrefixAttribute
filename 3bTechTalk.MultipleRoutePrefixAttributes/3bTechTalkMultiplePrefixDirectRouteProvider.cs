using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace _3bTechTalk.MultipleRoutePrefixAttributes
{
    public class _3bTechTalkMultiplePrefixDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<RouteEntry> GetActionDirectRoutes(HttpActionDescriptor actionDescriptor, IReadOnlyList<IDirectRouteFactory> factories, IInlineConstraintResolver constraintResolver)
        {
            return CreateRouteEntries(GetRoutePrefixes(actionDescriptor.ControllerDescriptor), factories, new[] { actionDescriptor }, constraintResolver, true);
        }

        protected override IReadOnlyList<RouteEntry> GetControllerDirectRoutes(HttpControllerDescriptor controllerDescriptor, IReadOnlyList<HttpActionDescriptor> actionDescriptors, IReadOnlyList<IDirectRouteFactory> factories, IInlineConstraintResolver constraintResolver)
        {
            return CreateRouteEntries(GetRoutePrefixes(controllerDescriptor), factories, actionDescriptors, constraintResolver, false);
        }

        private IEnumerable<string> GetRoutePrefixes(HttpControllerDescriptor controllerDescriptor)
        {
            Collection<IRoutePrefix> attributes = controllerDescriptor.GetCustomAttributes<IRoutePrefix>(false);
            if (attributes == null)
                return new string[] { null };

            var prefixes = new List<string>();
            foreach (var attribute in attributes)
            {
                if (attribute == null)
                    continue;

                string prefix = attribute.Prefix;
                if (prefix == null)
                    throw new InvalidOperationException("Prefix can not be null. Controller: " + controllerDescriptor.ControllerType.FullName);
                if (prefix.EndsWith("/", StringComparison.Ordinal))
                    throw new InvalidOperationException("Invalid prefix" + prefix + " in " + controllerDescriptor.ControllerName);

                prefixes.Add(prefix);
            }

            if (prefixes.Count == 0)
                prefixes.Add(null);

            return prefixes;
        }


        private IReadOnlyList<RouteEntry> CreateRouteEntries(IEnumerable<string> prefixes, IReadOnlyCollection<IDirectRouteFactory> factories, IReadOnlyCollection<HttpActionDescriptor> actions, IInlineConstraintResolver constraintResolver, bool targetIsAction)
        {
            var entries = new List<RouteEntry>();

            foreach (var prefix in prefixes)
            {
                foreach (IDirectRouteFactory factory in factories)
                {
                    RouteEntry entry = CreateRouteEntry(prefix, factory, actions, constraintResolver, targetIsAction);
                    entries.Add(entry);
                }
            }

            return entries;
        }


        private static RouteEntry CreateRouteEntry(string prefix, IDirectRouteFactory factory, IReadOnlyCollection<HttpActionDescriptor> actions, IInlineConstraintResolver constraintResolver, bool targetIsAction)
        {
            DirectRouteFactoryContext context = new DirectRouteFactoryContext(prefix, actions, constraintResolver, targetIsAction);
            RouteEntry entry = factory.CreateRoute(context);
            ValidateRouteEntry(entry);

            return entry;
        }


        private static void ValidateRouteEntry(RouteEntry routeEntry)
        {
            if (routeEntry == null)
                throw new ArgumentNullException("routeEntry");

            var route = routeEntry.Route;
            if (route.Handler != null)
                throw new InvalidOperationException("Direct route handler is not supported");
        }
    }
}