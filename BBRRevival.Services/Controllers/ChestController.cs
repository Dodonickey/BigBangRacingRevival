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
    public class ChestController : Controller
    {
        [Route("POST", "/v2/player/data/removeChest")]
        public async void RemoveChest()
        {
            byte[] data = null;

            Dictionary<string, object> remove = new Dictionary<string, object>();

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(remove));

            Console.WriteLine(this.RequestBodyAsync().Result);

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v1/player/createChest")]
        public async void CreatChest()
        {
            byte[] data = null;

            string requestBody = this.RequestBodyAsync().Result;

            // Deserialize the incoming JSON into a dictionary
            var chestData = JsonConvert.DeserializeObject<Dictionary<string, object>>(requestBody);

            chestData.Add("_id", "12");
            chestData.Add("timeLeft", 0);
            chestData.Add("notified", true);

            // Serialize the updated dictionary back to JSON
            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(chestData));


            Console.WriteLine(this.RequestBodyAsync().Result);
            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v1/player/openChest")]
        public async void OpenChest()
        {
            byte[] data = null;

            Dictionary<string, object> OpenChestData = new Dictionary<string, object>();
            OpenChestData.Add("SetData", new List<object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(OpenChestData));

            Console.WriteLine("request headers");
            foreach (var item in _request.Headers)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(this.RequestBodyAsync().Result);

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v2/player/data/updateChest")]
        public async void UpdateChest()
        {
            byte[] data = null;

            Dictionary<string, object> ChestData = new Dictionary<string, object>();
            ChestData.Add("id", 9);
            ChestData.Add("_id", "12");
            ChestData.Add("index", 82007);
            ChestData.Add("type", "SUPER");
            ChestData.Add("timeLeft", 0);
            ChestData.Add("level", 8);
            ChestData.Add("notified", true);

            Dictionary<string, object> ChestData2 = new Dictionary<string, object>();
            ChestData2.Add("id", 9);
            ChestData2.Add("_id", "12");
            ChestData2.Add("index", 11505);
            ChestData2.Add("type", "RARE");
            ChestData2.Add("timeLeft", 0);
            ChestData2.Add("level", 8);
            ChestData2.Add("notified", true);

            Dictionary<string, object> ChestData3 = new Dictionary<string, object>();
            ChestData3.Add("id", 9);
            ChestData3.Add("_id", "12");
            ChestData3.Add("index", 36006);
            ChestData3.Add("type", "EPIC");
            ChestData3.Add("timeLeft", 0);
            ChestData3.Add("level", 8);
            ChestData3.Add("notified", true);

            Dictionary<string, object> UpdatedChest = new Dictionary<string, object>();
            UpdatedChest.Add("chest", new List<object> { ChestData, ChestData2, ChestData3 });



            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(UpdatedChest));

            Console.WriteLine(this.RequestBodyAsync().Result);

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
