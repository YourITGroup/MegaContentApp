using MegaContentApp.Core.Models.Content;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Umbraco.Core.Models;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi.Filters;

namespace MegaContentApp.Core.Controllers
{
    [PluginController("MegaContentApp")]
    public class ProductApiController : UmbracoAuthorizedJsonController
    {
        //[HttpGet]
        //public PagedResult<Product> GetProducts(int id, [ModelBinder(typeof(HttpQueryStringModelBinder))]FormDataCollection queryStrings)
        //{

        //}
    }
}
