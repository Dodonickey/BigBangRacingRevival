﻿using BBRRevival.Services.API;
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

            //everything here can be removed, i dont think its usefull anymore
            Dictionary<string, object> datas2 = new Dictionary<string, object>();
            datas2.Add("nodeType", "9");//undefined
            datas2.Add("itemName", "9");//undefined
            datas2.Add("itemIcon", "item_player_monster_car_icon");//undefined
            datas2.Add("itemDescription", "9");//undefined

            Dictionary<string, object> datas = new Dictionary<string, object>();
            datas.Add("id", 0);//undefined
            datas.Add("name", "RacingMotorcycle");//undefined
            datas.Add("x", 24);//undefined
            datas.Add("y", 24);//undefined
            datas.Add("data", datas2);

            Dictionary<string, object> datas3 = new Dictionary<string, object>();
            datas3.Add("nodeType", "13");//undefined
            datas3.Add("itemType", "6");//undefined
            datas3.Add("itemName", "EDITOR_GUI_VEHICLE_NAME_CAR");//undefined
            datas3.Add("itemIcon", "item_player_monster_car_icon");//undefined
            datas3.Add("itemDescription", "EDITOR_GUI_VEHICLE_DESC_CAR");//undefined

            Dictionary<string, object> unlockabledatas = new Dictionary<string, object>();
            unlockabledatas.Add("id", 13);//undefined
            unlockabledatas.Add("name", "EDITOR_GUI_VEHICLE_NAME_CAR");//undefined
            unlockabledatas.Add("x", 4);//undefined
            unlockabledatas.Add("y", 4);//undefined
            unlockabledatas.Add("data", datas3);

            Dictionary<string, object> datas4 = new Dictionary<string, object>();
            datas4.Add("nodeType", "2");//undefined
            datas4.Add("itemType", "6");//undefined
            datas4.Add("itemName", "EDITOR_GUI_VEHICLE_NAME_CAR");//undefined
            datas4.Add("itemIcon", "item_player_monster_car_icon");//undefined
            datas4.Add("itemDescription", "EDITOR_GUI_VEHICLE_DESC_CAR");//undefined

            Dictionary<string, object> unlockablegamemodedatas = new Dictionary<string, object>();
            unlockablegamemodedatas.Add("id", 13);//undefined
            unlockablegamemodedatas.Add("name", "EDITOR_GUI_VEHICLE_NAME_CAR");//undefined
            unlockablegamemodedatas.Add("x", 4);//undefined
            unlockablegamemodedatas.Add("y", 4);//undefined
            unlockablegamemodedatas.Add("data", datas4);

            Dictionary<string, object> datadict = new Dictionary<string, object>();
            datadict.Add("nodes", new List<object>() {datas, unlockabledatas, unlockablegamemodedatas });

            string json = JsonConvert.SerializeObject(datadict, Formatting.None);
            //Console.WriteLine(json);

            //string cleanJson  = Regex.Replace(Encoding.Default.GetString(bytes), @"[\x00-\x1F\x7F]", ""); //string pattern = @"[\x00-\x1F\x7F]";

            //byte[] bytes = FilePacker.ZipBytes(Encoding.UTF8.GetBytes(json));

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
