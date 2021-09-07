using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using AutoMapper.Internal;
using Skybrud.WebApi.Json;
using Umbraco.Core.IO;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using File = System.IO.File;

namespace Skybrud.Umbraco.IconPicker.Controllers.Api.BackOffice {
    
    [JsonOnlyConfiguration]
    [PluginController("Skybrud")]
    public class IconPickerController : UmbracoAuthorizedApiController {
        
        [HttpGet]
        public object GetIcons(string contentTypeAlias = null, string propertyAlias = null) {
            
            var contentType = Services.ContentTypeService.GetContentType(contentTypeAlias);

            var property = contentType.PropertyTypes.FirstOrDefault(x => x.Alias == propertyAlias);
            string path = "/svgs/icons/";

            if (property != null)
            {
                var prevalues = Services.DataTypeService.GetPreValuesCollectionByDataTypeId(property.DataTypeDefinitionId).PreValuesAsDictionary;

                path = prevalues["iconpath"].Value.TrimStart('~');
            }
            
            string folder = IOHelper.MapPath("~" + path);
            DirectoryInfo d = new DirectoryInfo(folder);

            return new {
                path,
                icons = (
                    from file in d.GetFiles("*.svg")
                    select Path.GetFileNameWithoutExtension(file.FullName)
                )
            };

        }
        
        [HttpGet]
        public object GetSvg(string contentTypeAlias = null, string propertyAlias = null, string icon = null) {
            
            var contentType = Services.ContentTypeService.GetContentType(contentTypeAlias);

            var property = contentType.PropertyTypes.FirstOrDefault(x => x.Alias == propertyAlias);
            bool removeFillAttributes = false;
            string path = "/svgs/icons/";

            if (property != null)
            {
                var prevalues = Services.DataTypeService.GetPreValuesCollectionByDataTypeId(property.DataTypeDefinitionId).PreValuesAsDictionary;

                if (prevalues.TryGetValue("removeFillAttributes", out PreValue pre1))
                {
                    removeFillAttributes = pre1.Value == "1";
                }

                if (prevalues.TryGetValue("iconpath", out PreValue pre2))
                {
                    path = pre2.Value.TrimStart('~');
                }
            }

            string folder = IOHelper.MapPath("~" + path);
            DirectoryInfo d = new DirectoryInfo(folder);

            var file = d.GetFiles("*.svg").FirstOrDefault(x => Path.GetFileNameWithoutExtension(x.FullName) == icon);

            string contents = File.ReadAllText(file.FullName);

            if (removeFillAttributes) contents = Regex.Replace(contents, " fill=\".+?\"", "");

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "image/svg+xml";
            HttpContext.Current.Response.Write(contents);
            HttpContext.Current.Response.End();

            return null;

        }
    
    }

}