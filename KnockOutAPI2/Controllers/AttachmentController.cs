using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace KnockOutAPI2.Controllers
{
    public class AttachmentController : ApiController
    {

        [HttpPost]
        [Route("api/Upload")]
        public async Task<IHttpActionResult> UploadAsync()
        {


            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var streamProvider = new MultipartMemoryStreamProvider();


            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();

                foreach (string item in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[item];

                    var filePath = HttpContext.Current.Server.MapPath("~/Files/" + postedFile.FileName);

                    postedFile.SaveAs(filePath);

                    docfiles.Add(filePath);

                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, docfiles));

            }
            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, "BadRequest"));
            }

           
        }
    }
}
