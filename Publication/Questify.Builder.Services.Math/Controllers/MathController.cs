using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Svg;

namespace Questify.Builder.Services.Math.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<ActionResult> Render([FromServices]INodeServices nodeServices, [FromBody]string mathMl)
        {
            var result = await nodeServices.InvokeAsync<string>("./Scripts/renderMathMl", mathMl);

            string base64String = string.Empty;
            var byteArray = Encoding.ASCII.GetBytes(result);
            using (var stream = new MemoryStream(byteArray))
            using (var pngStream = new MemoryStream())
            {
                var svgDocument = SvgDocument.Open<SvgDocument>(stream);
                var bitmap = svgDocument.Draw();
                bitmap.Save(pngStream, ImageFormat.Png);
                base64String = Convert.ToBase64String(pngStream.ToArray());
            }

            return new JsonResult(base64String);
        }
    }
}
