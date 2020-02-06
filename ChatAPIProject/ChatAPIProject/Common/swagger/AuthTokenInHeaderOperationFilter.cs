using System;
using System.Web;
using System.Linq;
using System.Web.Http;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace ChatAPIProject.Common.swagger
{
    public class AuthTokenInHeaderOperationFilter : IOperationFilter
    {
        

        public AuthTokenInHeaderOperationFilter()
        {
            
        }

        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            var isAuthorized = filterPipeline
                                             .Select(filterInfo => filterInfo.Instance)
                                             .Any(filter => filter is System.Web.Http.AuthorizeAttribute);

            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<System.Web.Http.AllowAnonymousAttribute>().Any();

            if (!isAuthorized || allowAnonymous)
            {
                return;
            }

            if (operation == null)
            {
                return;
            }

            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            string token = string.Empty;
            
            try
            {
                token = HttpContext.Current.Request.Cookies["Token"].Value;
            }
            catch (Exception)
            {
            }

            var parameter = new Parameter
            {
                description = "The authorization token",
                @in = "header",
                name = "Authorization",
                required = true,
                type = "string",
                @default = "Bearer " + token,
            };

            if (apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                parameter.required = false;
            }

            operation.parameters.Add(parameter);
        }
    }
}