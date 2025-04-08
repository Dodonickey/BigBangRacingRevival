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
    public class ChatController : Controller //chats dont work 
    {
        [Route("GET", "/v1/global/chat/find")]
        public async void GetComments()
        {
            byte[] data = null;

            Dictionary<string, object> commentData = new Dictionary<string, object>();
            commentData.Add("data", new List<object>());

            List<object> list = commentData["data"] as List<object>;

            Dictionary<string, object> commentData2 = new Dictionary<string, object>();
            commentData2.Add("playerId", "123456798");
            commentData2.Add("comment", "placeholdertext");
            commentData2.Add("message", "inserttext");
            commentData2.Add("name", "testguy1");
            commentData2.Add("tag", "tg1");
            commentData2.Add("facebookId", "f612dg82dq87");
            commentData2.Add("gameCenterId", "g37gr74g73428");
            commentData2.Add("admin", true);
            commentData2.Add("publishTime", new Dictionary<string, object>());

            Dictionary<string, object> commentData3 = (Dictionary<string, object>)commentData2["publishTime"];
            commentData3.Add("$date", 1518220800000L);

            commentData2.Add("type", "whatdoesthismean");
            commentData2.Add("customData", new Dictionary<string, object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(commentData));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v1/global/chat/save")]
        public async void SaveComment()
        {
            byte[] data = null;

            Dictionary<string, object> savecommentData = new Dictionary<string, object>();

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(savecommentData));



            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
