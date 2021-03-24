using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SD.Toolkits.WebApi.Core.Extensions
{
    /// <summary>
    /// Allows API methods to accept multiple parameters via a POST operation. If a method is marked
    /// with <see cref="WrapPostParameterBinding"/> and supports HTTP POSTs, clients must pass
    /// a single object with a property for each parameter. Both JSON and standard query string
    /// posting is supported. The parameters can be of any type.
    /// </summary>
    public class WrapPostParameterBinding : IModelBinder
    {
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            HttpContext context = bindingContext.HttpContext;

            #region # Check

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            if (context.Request.Method != HttpMethod.Post.ToString())
            {
                throw new InvalidOperationException("Only post method available !");
            }
            if (!context.Request.ContentType.StartsWith("application/json"))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            #endregion

            #region # Precompile

#if (NETSTANDARD2_1 || NETCOREAPP3_0)
            context.Request.EnableBuffering();
#else
            context.Request.EnableRewind();
#endif

            #endregion

            NameValueCollection parameters = await this.ParseParametersFromBody(context.Request);
            string stringValue = parameters[bindingContext.FieldName];
            if (!string.IsNullOrWhiteSpace(stringValue))
            {
                object paramValue;
                if (bindingContext.ModelType == typeof(string))
                {
                    paramValue = stringValue;
                }
                else if (bindingContext.ModelType.IsEnum)
                {
                    paramValue = Enum.Parse(bindingContext.ModelType, stringValue);
                }
                else if (bindingContext.ModelType.IsPrimitive || bindingContext.ModelType.IsValueType)
                {
                    paramValue = Convert.ChangeType(stringValue, bindingContext.ModelType);
                }
                else
                {
                    paramValue = JsonSerializer.Deserialize(stringValue, bindingContext.ModelType);
                }

                bindingContext.Result = ModelBindingResult.Success(paramValue);
            }

            // rewind
            context.Request.Body.Position = 0;
        }

        /// <summary>
        /// Read parameters from the body into a collection.
        /// </summary>
        private async Task<NameValueCollection> ParseParametersFromBody(HttpRequest request)
        {
            // deserialize to Dictionary and convert to NameValueCollection
            using StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, false, 4096, true);
            string body = await reader.ReadToEndAsync();
            IDictionary<string, JsonElement> jsonDictionary = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(body);
            NameValueCollection result = jsonDictionary.Aggregate(new NameValueCollection(), (seed, current) =>
            {
                seed.Add(current.Key, current.Value.ToString());
                return seed;
            });

            return result;
        }
    }
}
