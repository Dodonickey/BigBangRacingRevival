using BBRRevival.Services.API;
using BBRRevival.Services.API.Models;
using BBRRevival.Services.API.Models.Responses;
using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class NewCar : Controller
    {
        [Route("POST", "/v2/player/changeCurCar")]
        public async void ChangeCar()
        {
            byte[] data = null;

            string requestBody = await this.RequestBodyAsync();//im stupid so i used chatgpt

            Dictionary<string, object> incomingData = JsonConvert.DeserializeObject<Dictionary<string, object>>(requestBody);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(incomingData));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            Console.WriteLine(this.RequestBodyAsync().Result);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v2/player/unLockCar")]
        public async void UnlockCar()
        {
            byte[] data = null;

            Dictionary<string, object> addcar = new Dictionary<string, object>();
            addcar.Add("carUnlock", new List<object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(addcar));

            Console.WriteLine(this.RequestBodyAsync().Result);

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
