using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace SDSK.API.Constraints
{
    internal class VersionConstraint : IHttpRouteConstraint
    {
        public const string VersionHeaderName = "api-version";
        private const int DefaultVersion = 1;
        public VersionConstraint(int allowedVersion = 1)
        {
            AllowedVersion = allowedVersion;
        }
        public int AllowedVersion
        {
            get;
            private set;
        }
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                int version = ifVersionHeader(request) ? 2 : DefaultVersion;
                if (version == AllowedVersion)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ifVersionHeader(HttpRequestMessage request)
        {
            string versionAsString;
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues(VersionHeaderName, out headerValues) && headerValues.Count() == 1)
            {
                versionAsString = headerValues.First();
                if (versionAsString != null)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }


    }

    internal class VersionedRoute : RouteFactoryAttribute
    {
        public VersionedRoute(string template, int allowedVersion = 1 )
            : base(template)
        {
            AllowedVersion = allowedVersion;
        }
        public int AllowedVersion
        {
            get;
            private set;
        }
        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary();
                constraints.Add("version", new VersionConstraint(AllowedVersion));
                return constraints;
            }
        }
    }

}