using BBRRevival.Services.API;
using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class PlanetController : Controller
    {
        [Route("GET", "/v1/path/db/find")]
        public async void FindPath()
        {
            byte[] data = null;

            Log.Verbose(_request.RawUrl);

            var planet = _request.QueryString["planet"];
            
            byte[] bytes = FilePacker.ZipBytes(Encoding.UTF8.GetBytes(File.ReadAllText($"Assets\\InitialData\\{planet}LocalInitialData.txt")));

            data = bytes;

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, false);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
