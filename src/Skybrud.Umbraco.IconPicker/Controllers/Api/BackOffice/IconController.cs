using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using Skybrud.WebApi.Json;
using Umbraco.Web.WebApi;

namespace Skybrud.Umbraco.IconPicker.Controllers.Api.BackOffice {
    
    [JsonOnlyConfiguration]
    public class IconController : UmbracoAuthorizedApiController {
        
        [HttpGet]
        public object GetIcons(string path = "") {
            
            List<string> items = new List<string>();

            string folder = HttpContext.Current.Server.MapPath(string.Format("~{0}", path));

            DirectoryInfo d = new DirectoryInfo(folder);
            foreach (var file in d.GetFiles("*.svg")) {
                items.Add(path + file.Name);
            }
            
            return items;

        }
    
    }

}