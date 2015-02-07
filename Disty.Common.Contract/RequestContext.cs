using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Disty.Common.Contract
{
    [Serializable]
    public class RequestContext : MarshalByRefObject // Need this for now due to this:  http://msdn.microsoft.com/en-us/library/dn458353(v=vs.110).aspx and this http://stackoverflow.com/questions/15693262/serialization-exception-in-net-4-5
    {
        public const string Key = "RequestContext";

        public Guid RequestId { get; set; }
        public Guid? ApiKey { get; set; }
        public byte[] SharedSecret { get; set; }
        public int? TenantId { get; set; }
        public Guid? UserId { get; set; }
        public ApiVersion ApiVersion { get; set; }
        public string AccessToken { get; set; }
        public bool IsSuperUser { get; set; }

        public static void SetToCallContext(RequestContext requestContext)
        {
            CallContext.LogicalSetData(Key, requestContext);
        }

        public static RequestContext GetFromCallContext()
        {
            var requestContext = System.Runtime.Remoting.Messaging.CallContext.LogicalGetData(Key) as RequestContext;
            if (requestContext == null)
            {
                throw new Exception("RequestContext not set, this should be done automatically by WebApi or NServiceBus.Host");
            }

            return requestContext;
        }
    }
}
