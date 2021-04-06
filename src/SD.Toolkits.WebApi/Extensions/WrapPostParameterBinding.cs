using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace SD.Toolkits.WebApi.Extensions
{
    /// <summary>
    /// Allows API methods to accept multiple parameters via a POST operation. If a method is marked
    /// with <see cref="WrapPostParameterBinding"/> and supports HTTP POSTs, clients must pass
    /// a single object with a property for each parameter. Both JSON and standard query string
    /// posting is supported. The parameters can be of any type.
    /// </summary>
    /// <remarks>
    /// The default behavior of .NET Web API is to allow just 1 parameter via a POST operation.
    /// 
    /// Example:
    /// Given this web API method:
    ///		[MultiPostParameters] public string MyMethod(CustomObject param1, CustomObject param2, string param3) { ... }
    ///		
    /// a client would pass either a JSON object in this format:
    ///		{ param1: {...}, param2: {...}, param3: "..." }
    ///		
    /// or an encoded query string:
    ///		param1=...&amp;param2=...&amp;param3=...
    ///		
    /// To use this class, add the following line to Global.asax.cs in Application_Start before the call to GlobalConfiguration.Configure(WebApiConfig.Register):
    ///		GlobalConfiguration.Configuration.ParameterBindingRules.Insert(0, MultiPostParameterBinding.CreateBindingForMarkedParameters);
    /// 
    /// This class is based on a similar implementation from https://github.com/RoyiNamir/SimplePostVariableParameterBindingExtended
    /// (also based on Rick Strahl's SimplePostVariableParameterBinding class) which only supported simple types.
    /// </remarks>
    public sealed class WrapPostParameterBinding : HttpParameterBinding
    {
        #region # Constructors

        /// <summary>
        /// Allows API methods to accept multiple parameters via a POST operation. If a method is marked
        /// with <see cref="WrapPostParametersAttribute"/> and supports HTTP POSTs, clients must pass
        /// a single object with a property for each parameter. Both JSON and standard query string
        /// posting is supported. The parameters can be of any type.
        /// </summary>
        /// <remarks>
        /// The default behavior of .NET Web API is to allow just 1 parameter via a POST operation.
        /// 
        /// Example:
        /// Given this web API method:
        ///		[MultiPostParameters] public string MyMethod(CustomObject param1, CustomObject param2, string param3) { ... }
        ///		
        /// a client would pass either a JSON object in this format:
        ///		{ param1: {...}, param2: {...}, param3: "..." }
        ///		
        /// or an encoded query string:
        ///		param1=...&amp;param2=...&amp;param3=...
        ///		
        /// To use this class, add the following line to Global.asax.cs in Application_Start before the call to GlobalConfiguration.Configure(WebApiConfig.Register):
        ///		GlobalConfiguration.Configuration.ParameterBindingRules.Insert(0, MultiPostParameterBinding.CreateBindingForMarkedParameters);
        /// 
        /// This class is based on a similar implementation from https://github.com/RoyiNamir/SimplePostVariableParameterBindingExtended
        /// (also based on Rick Strahl's SimplePostVariableParameterBinding class) which only supported simple types.
        /// </remarks>
        public WrapPostParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor)
        {

        }

        /// <summary>
        /// Returns a <see cref="WrapPostParameterBinding"/> object to use for the API method parameter specified.
        /// An object is only returned if the parameter's method is marked with <see cref="WrapPostParametersAttribute"/>,
        /// otherwise null is returned.
        /// </summary>
        /// <remarks>
        /// Call this method in Global.asax.cs in Application_Start before the call to GlobalConfiguration.Configure(WebApiConfig.Register):
        ///		GlobalConfiguration.Configuration.ParameterBindingRules.Insert(0, MultiPostParameterBinding.CreateBindingForMarkedParameters);
        /// </remarks>
        public static WrapPostParameterBinding CreateBindingForMarkedAction(HttpParameterDescriptor descriptor)
        {
            // short circuit if action does not have this attribute
            if (!descriptor.ActionDescriptor.GetCustomAttributes<WrapPostParametersAttribute>().Any())
            {
                return null;
            }

            // Only apply this binder on POST and PUT operations
            ICollection<HttpMethod> supportedMethods = descriptor.ActionDescriptor.SupportedHttpMethods;
            if (supportedMethods.Contains(HttpMethod.Post) || supportedMethods.Contains(HttpMethod.Put))
            {
                return new WrapPostParameterBinding(descriptor);
            }

            return null;
        }

        #endregion

        #region # Implementations

        /// <summary>
        /// Parses the parameter value from the request body.
        /// </summary>
        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            // read request body (query string or JSON) into name/value pairs
            NameValueCollection parameters = this.ParseParametersFromBody(actionContext.Request);

            // try to get parameter value from parsed body
            string stringValue = null;
            if (parameters != null)
            {
                stringValue = parameters[Descriptor.ParameterName];
            }

            // if not found in body, try reading query string
            if (stringValue == null)
            {
                IEnumerable<KeyValuePair<string, string>> queryStringPairs = actionContext.Request.GetQueryNameValuePairs();
                if (queryStringPairs != null)
                {
                    stringValue = queryStringPairs
                        .Where(kv => kv.Key == Descriptor.ParameterName)
                        .Select(kv => kv.Value)
                        .FirstOrDefault();
                }
            }

            // if found, convert/deserialize the parameter and set the binding
            if (stringValue != null)
            {
                object paramValue;
                if (Descriptor.ParameterType == typeof(string))
                {
                    paramValue = stringValue;
                }
                else if (Descriptor.ParameterType == typeof(Guid))
                {
                    paramValue = Guid.Parse(stringValue);
                }
                else if (Descriptor.ParameterType == typeof(DateTime))
                {
                    paramValue = DateTime.Parse(stringValue);
                }
                else if (Descriptor.ParameterType.IsEnum)
                {
                    paramValue = Enum.Parse(Descriptor.ParameterType, stringValue);
                }
                else if (Descriptor.ParameterType.IsPrimitive)
                {
                    paramValue = Convert.ChangeType(stringValue, Descriptor.ParameterType);
                }
                else
                // when deserializing an object, pass in the global settings so that custom converters, etc. are honored
                {
                    paramValue = JsonConvert.DeserializeObject(stringValue, Descriptor.ParameterType);
                }

                // Set the binding result here
                base.SetValue(actionContext, paramValue);
            }

            // now, we can return a completed task with no result
            TaskCompletionSource<object> taskCompletionSources = new TaskCompletionSource<object>();
            taskCompletionSources.SetResult(default(object));
            return taskCompletionSources.Task;
        }

        /// <summary>
        /// Read parameters from the body into a collection.
        /// </summary>
        private NameValueCollection ParseParametersFromBody(HttpRequestMessage requestMessage)
        {
            object result = null;

            MediaTypeHeaderValue contentType = requestMessage.Content.Headers.ContentType;
            if (contentType != null)
            {
                switch (contentType.MediaType)
                {
                    case "application/json":
                        // deserialize to Dictionary and convert to NameValueCollection
                        string content = requestMessage.Content.ReadAsStringAsync().Result;
                        Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                        result = values.Aggregate(new NameValueCollection(), (seed, current) =>
                        {
                            seed.Add(current.Key, current.Value == null ? "" : current.Value.ToString());
                            return seed;
                        });
                        break;

                    case "application/x-www-form-urlencoded":
                        result = requestMessage.Content.ReadAsFormDataAsync().Result;
                        break;
                }
            }

            return result as NameValueCollection;
        }

        #endregion
    }
}