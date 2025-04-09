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
    public class LoginController : Controller
    {
        [Route("POST", "/v4/player/Test")]
        public async void TestJsonConversion()
        {
            byte[] data = null;

            Dictionary<string, object> dict = new Dictionary<string, object>();

            PlayerData player = new();
            ClientConfig config = new();
            Tournament tournament = new();
            Event currentEvent = new();
            List<PlanetVersionModel> PlanetVesions = new();

            //playerdata

            //tournament
            tournament.tournamentId = "testTournamentID";
            tournament.minigameId = "testMinigameID";
            tournament.ccCap = 12.4f;
            tournament.prizeCoins = 999;
            tournament.acceptingNewScores = true;
            tournament.ownerId = "testOwnerId";
            tournament.ownerName = "testOwnerName";
            tournament.claimed = false;

            List<Dictionary<string, object>> tournamentUris = new List<Dictionary<string, object>>
            {
                        new Dictionary<string, object> { { "uri", "http://example.com/1" } },
                        new Dictionary<string, object> { { "uri", "http://example.com/2" } },
                        new Dictionary<string, object> { { "uri", "http://example.com/3" } }
            };

            //Event


            //Planet paths
            PlanetVesions.Add(new() { planet = "AdventureMotorcycle", version = 2 });
            PlanetVesions.Add(new() { planet = "RacingOffroadCar", version = 2 });
            PlanetVesions.Add(new() { planet = "AdventureOffroadCar", version = 2 });
            PlanetVesions.Add(new() { planet = "RacingMotorcycle", version = 2 });
            PlanetVesions.Add(new() { planet = "Metadata", version = 2 });

            LoginResponseModel model = new();
            model.PlayerData = player;
            model.ClientConfig = config;
            model.Tournament = tournament;
            model.Event = currentEvent;
            model.planetVersions = PlanetVesions;

            var dictionary = model.ToDictionary();
            var json = JsonConvert.SerializeObject(dictionary);

            data = Encoding.Default.GetBytes(json);

            //send the request
            ResponseHelper.PrepareRequest(data, RawUrl, _response, _request);
            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();

        }

        [Route("POST", "/v5/player/login")]//id 510 in adventureinitialdata = the silver chest after node 10
        public async void PlayerLogin()
        {
            Log.Information("Received Login Request");
            byte[] data = null;

            string Query = _request.Url.Query;
            //string param = Query.Split("&")[0];
            //string value = param.Split("=")[1];

            string sessionId = sessionsManager.AddNewSessionId();

            Dictionary<string, object> dict = new Dictionary<string, object>();

            if (true) //check value == "0", setting it to true for now
            {
                Log.Verbose("New User Creation");

                dict.Add("PLAY_STATUS", "OK");
                dict.Add("sessionId", "IdkWhatSessionThisIsFor");
                dict.Add("clientVersion", 1);
                dict.Add("versionInfo", "1");

                Dictionary<string, object> settings = new Dictionary<string, object>();

                settings.Add("superLikeRefreshMinutes", 12);
                settings.Add("carRefreshMinutes", 6);
                settings.Add("freshFreeInterval", 15);
                settings.Add("fbConnectReward", 20);
                settings.Add("dailyGemAmount", 10);
                settings.Add("videoAdCount", 2);
                settings.Add("videoAdCoolDown", 3600);
                settings.Add("freshFreeCount", 3);
                settings.Add("freshFreeCoolDown", 1800);
                settings.Add("inRaceDiamondSpawnProbability", 25);
                settings.Add("boltsAtStart", 0);
                settings.Add("coinsAtStart", 1000);
                settings.Add("diamondsAtStart", 50);
                settings.Add("keysAtStart", 5);
                settings.Add("offerCooldownMinutes", 4320);
                settings.Add("offerDurationMinutes", 60);
                settings.Add("minimumTournamentNitros", 5);
                settings.Add("creatorRank1", 100);
                settings.Add("creatorRank2", 1000);
                settings.Add("creatorRank3", 10000);
                settings.Add("creatorRank4", 100000);
                settings.Add("creatorRank5", 1000000);
                settings.Add("creatorRank6", 10000000);
                settings.Add("triesForAd", 0);
                settings.Add("triesForGems", 0);
                settings.Add("triesGemPrice", 0);
                settings.Add("gemShopEnabled", true);

                Dictionary<string, object> tdict = new Dictionary<string, object>();
                tdict.Add("tournamentId", "testTournamentID");
                tdict.Add("minigameId", "testMinigameID");
                tdict.Add("ccCap", 12.4f);
                tdict.Add("prizeCoins", 999);
                tdict.Add("acceptingNewScores", true);
                tdict.Add("ownerId", "testOwnerId");
                tdict.Add("ownerName", "testOwnerName");
                tdict.Add("claimed", false);

                List<Dictionary<string, object>> turis = new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object> { { "uri", "http://example.com/1" } },
                        new Dictionary<string, object> { { "uri", "http://example.com/2" } },
                        new Dictionary<string, object> { { "uri", "http://example.com/3" } }
                    };

                Dictionary<string, object> adpdict = new Dictionary<string, object>();
                adpdict.Add("planet", "AdventureMotorcycle");
                adpdict.Add("version", 2);

                Dictionary<string, object> tpdict = new Dictionary<string, object>(); //add in another one that is not adventure, maybe it will hit the callback
                tpdict.Add("planet", "RacingOffroadCar");
                tpdict.Add("version", 2);

                Dictionary<string, object> ofcarpdict = new Dictionary<string, object>(); //add in another one that is not adventure, maybe it will hit the callback
                ofcarpdict.Add("planet", "AdventureOffroadCar");
                ofcarpdict.Add("version", 2);

                Dictionary<string, object> rmcarpdict = new Dictionary<string, object>(); //add in another one that is not adventure, maybe it will hit the callback
                rmcarpdict.Add("planet", "RacingMotorcycle");
                rmcarpdict.Add("version", 2);

                Dictionary<string, object> ldcarpdict = new Dictionary<string, object>(); //add in another one that is not adventure, maybe it will hit the callback
                ldcarpdict.Add("planet", "LadderOffroadCar");
                ldcarpdict.Add("version", 2);

                Dictionary<string, object> metadatadict = new Dictionary<string, object>(); //add in another one that is not adventure, maybe it will hit the callback
                metadatadict.Add("planet", "Metadata");
                metadatadict.Add("version", 2);

                //planet paths, neccesary, kt crashes without them for some reason
                List<object> nodes = new List<object>();
                Dictionary<string, object> nodesdict = new Dictionary<string, object>();

                nodesdict.Add("id", "1234");
                nodesdict.Add("levelNumber", "12345");
                nodesdict.Add("score", "123456");
                nodesdict.Add("rented", false);

                nodes.Add(nodesdict);

                List<object> paths = new List<object>();
                Dictionary<string, object> path = new Dictionary<string, object>();
                path.Add("name", "MainPath");
                path.Add("lane", 17);
                path.Add("currentNode", 24);
                path.Add("type", "main");
                path.Add("nodes", nodes);
                path.Add("startNode", 10);
                path.Add("overwrite", true);
                path.Add("planet", "AdventureOffroadCar");

                paths.Add(path);

                //add tournament things, looks like it is neccesary to do this idk why
                Dictionary<string, object> edict = new Dictionary<string, object>();
                edict.Add("eventName", "TestEvemt");
                edict.Add("eventType", "Tournament");
                edict.Add("messageId", "testMessageID");
                edict.Add("id", "123456");
                edict.Add("header", "eventHeader");
                edict.Add("message", "eventMessage");
                edict.Add("label", "eventLabel");
                edict.Add("popup", true);
                edict.Add("newsFeed", true);
                edict.Add("startTime", Convert.ToInt64(DateTime.Now.Ticks));
                edict.Add("endTime", Convert.ToInt64(new DateTime(2025, 12, 4).Ticks));
                edict.Add("eventData", tdict);
                edict.Add("uris", turis);

                Dictionary<string, object> dataload = new Dictionary<string, object>();
                dataload.Add("sessionExpiration", false);
                dataload.Add("adsConfig", new List<object>());
                dataload.Add("seasonConfig", new Dictionary<string, object>());
                dataload.Add("iapConfig", new Dictionary<string, object>());
                dataload.Add("gemPriceConfig", new Dictionary<string, object>());
                dataload.Add("editorConfig", new Dictionary<string, object>());
                dataload.Add("achievements", new List<object>());
                dataload.Add("numOfPurchases", 10);
                dataload.Add("bossBattleConfig", new Dictionary<string, object>());
                dataload.Add("numOfSessions", 11);
                dataload.Add("publishedMinigameCount", 11);
                dataload.Add("tournamentRewardShares", new List<object>());
                dataload.Add("lastPathSync", "what");

                dict.Add("playerId", "123456789");
                dict.Add("developer", true);
                dict.Add("countryCode", "KP");
                dict.Add("clientConfig", settings);
                dict.Add("teamid", "0");
                dict.Add("teamName", "BigTeamNameIDK");
                //dict.Add("teamRole", Enums.TeamRole.Creator.ToString());
                dict.Add("hasJoinedTeam", true);
                dict.Add("youtubeName", "TempYtName");
                dict.Add("youtubeId", "ytid");

                dict.Add("eventMessage", edict);
                dict.Add("eventList", new List<object> { edict });
                dict.Add("activeTournament", tdict);

                //dict.Add("playerId", Guid.NewGuid().ToString());
                dict.Add("name", "Jurij15");
                dict.Add("tag", "JurijG");
                dict.Add("itemDbVersion", 0);
                dict.Add("AcceptNotifications", true);
                dict.Add("nameChangesDone", 0);
                dict.Add("level", 100);

                dict.Add("mcRank", 4000);
                dict.Add("carRank", 4000);

                dict.Add("coins", 1000000);
                dict.Add("tokenCoin", 10000);
                dict.Add("diamonds", 1000000);
                dict.Add("copper", 1000000);
                dict.Add("shards", 1000000);
                dict.Add("stars", 1000000);
                dict.Add("vip", true);

                dict.Add("xp", 100000);
                dict.Add("totalLikes", 10);
                dict.Add("totalCoinsEarned", 100000);
                dict.Add("carTrophies", 21000);
                dict.Add("curCar", 0);
                dict.Add("carUnlock", new List<object> { 0, 1, 2 });//you cant switch cars for some reason maybe needs db

                //Adding Hats and Trails.
                // The Trail Values seem to be quite easy to add.
                dict.Add("OffroadCarVisual", new Dictionary<string, object>());

                // Hat Values not finished for now.
                var offroadCarVisual = (Dictionary<string, object>)dict["OffroadCarVisual"];
                offroadCarVisual.Add("PaperBag", false);
                offroadCarVisual.Add("OrangeHat", false);
                offroadCarVisual.Add("WinterHat", false);
                offroadCarVisual.Add("PinkHat", false);
                offroadCarVisual.Add("ReindeerHat", false);
                offroadCarVisual.Add("HawkMask", false);
                offroadCarVisual.Add("BaconHair", false);
                offroadCarVisual.Add("RobotHat", false);
                offroadCarVisual.Add("LorpHeadband", false);
                offroadCarVisual.Add("GoldenCarHelmet", true);
                offroadCarVisual.Add("BuilderHat", false);
                offroadCarVisual.Add("AnniversaryCandleHat", false);
                offroadCarVisual.Add("AnniversaryPartyHat", false);
                offroadCarVisual.Add("AnglerFishHat", false);
                offroadCarVisual.Add("BarbarianHelmet", false);
                offroadCarVisual.Add("BaseballHat", false);
                offroadCarVisual.Add("BobbleHat", false);
                offroadCarVisual.Add("BootHat", false);
                offroadCarVisual.Add("CatHat", false);
                offroadCarVisual.Add("CowboyHat", false);
                offroadCarVisual.Add("SteelMask", false);
                offroadCarVisual.Add("TimeTravellerHat", false);
                offroadCarVisual.Add("Helmet", false);
                offroadCarVisual.Add("MotocrossHelmet", false);
                offroadCarVisual.Add("VRgoggles", false);
                offroadCarVisual.Add("MilkJugHat", false);
                offroadCarVisual.Add("DealWithItGlasses", false);
                offroadCarVisual.Add("ReversalCrown", false);
                offroadCarVisual.Add("ToadHat", false);
                offroadCarVisual.Add("PowerHelmet", false);
                offroadCarVisual.Add("MushroomHat", false);
                offroadCarVisual.Add("FishHat", false);
                offroadCarVisual.Add("WerewolfMask", false);
                offroadCarVisual.Add("trail_anniversary", false);
                offroadCarVisual.Add("trail_bubble", false);
                offroadCarVisual.Add("trail_cash", false);
                offroadCarVisual.Add("trail_death", true);
                offroadCarVisual.Add("trail_feather", false);
                offroadCarVisual.Add("trail_fire", false);
                offroadCarVisual.Add("trail_kittypaw", false);
                offroadCarVisual.Add("trail_rainbow", false);
                offroadCarVisual.Add("trail_singular", false);
                offroadCarVisual.Add("trail_snow", false);
                offroadCarVisual.Add("trail_scifi", true);
                offroadCarVisual.Add("trail_bat", false);
                offroadCarVisual.Add("ChickenHat", false);
                offroadCarVisual.Add("WinterCap", false);

                dict.Add("OffroadCarUpgrades", new Dictionary<string, object>());

                var UpgradeDict = (Dictionary<string, object>)dict["OffroadCarUpgrades"];
                UpgradeDict.Add("CarGrip1", "10");
                UpgradeDict.Add("CarGrip2", "12");
                UpgradeDict.Add("CarGrip3", "10");
                UpgradeDict.Add("CarGrip4", "8");
                UpgradeDict.Add("CarGrip5", "12");
                UpgradeDict.Add("CarGrip6", "8");
                UpgradeDict.Add("CarSpeed1", "12");
                UpgradeDict.Add("CarSpeed2", "10");
                UpgradeDict.Add("CarSpeed3", "8");
                UpgradeDict.Add("CarSpeed4", "12");
                UpgradeDict.Add("CarSpeed5", "10");
                UpgradeDict.Add("CarSpeed6", "8");
                UpgradeDict.Add("CarHandling1", "12");
                UpgradeDict.Add("CarHandling2", "10");
                UpgradeDict.Add("CarHandling3", "12");
                UpgradeDict.Add("CarHandling4", "10");
                UpgradeDict.Add("CarHandling5", "8");
                UpgradeDict.Add("CarHandling6", "8");
                UpgradeDict.Add("CarSpecial1", "8");
                UpgradeDict.Add("CarSpecial2", "8");
                UpgradeDict.Add("CarSpecial3", "12");
                UpgradeDict.Add("CarSpecial4", "12");
                UpgradeDict.Add("CarSpecial5", "10");
                UpgradeDict.Add("CarSpecial6", "10");
                UpgradeDict.Add("NewCarSpeed1", "3");
                UpgradeDict.Add("NewCarSpeed2", "3");
                UpgradeDict.Add("NewCarHandling1", "3");
                UpgradeDict.Add("NewCarHandling2", "3");
                UpgradeDict.Add("NewCarSpeed4", "3");
                UpgradeDict.Add("NewCarSpeed3", "3");
                UpgradeDict.Add("NewCarGrip1", "3");
                UpgradeDict.Add("NewCarGrip2", "3");

                dict.Add("planetVersions", new List<object> { tpdict, ofcarpdict, metadatadict, ldcarpdict });

                dict.Add("paths", paths);


                data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dict));
            }
            else
            {
                //Log.Verbose($"Value is {value}");
            }

            Log.Verbose("All Headers:");
            foreach (string item in _request.Headers)
            {
                Log.Verbose(item);
            }
            ResponseHelper.PrepareRequest(data, RawUrl, _response, _request);
            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v2/player/data/change")]
        public async void SetPlayerData()
        {
            byte[] data = null;

            Dictionary<string, object> path = new Dictionary<string, object>();
            path.Add("lastPathSync", "what");

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(path));

            Console.WriteLine("request headers");
            foreach (var item in _request.Headers)
            {
                Console.WriteLine(item);
            }


            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v2/player/changeName")]
        public async void ChangeName()
        {
            byte[] data = null;

            Dictionary<string, object> path = new Dictionary<string, object>();

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(path));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/player/friends")]
        public async void GetFriends()
        {
            byte[] data = null;

            Dictionary<string, object> friends = new Dictionary<string, object>();
            friends.Add("followees", new List<object>());
            friends.Add("friends", new List<object>());
            friends.Add("followers", new List<object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(friends));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

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

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v1/rating/save")]//post request suck
        public async void SaveRating()
        {
            byte[] data = null;

            Dictionary<string, object> Rating = new Dictionary<string, object>();

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(Rating));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            Console.WriteLine(this.RequestBodyAsync().Result);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/player/specialOffer")]
        public async void GetSpecialOffers()
        {
            byte[] data = null;

            Dictionary<string, object> specialoffer = new Dictionary<string, object>();

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(specialoffer));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();

        }

        [Route("GET", "/v2/player/getAchievementReward")]
        public async void GetRaceActivityAchievementRewards()
        {
            byte[] data = null;

            Dictionary<string, object> achievementtrophies = new Dictionary<string, object>();
            achievementtrophies.Add("trophies", new List<object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(achievementtrophies));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v2/player/getRaceFund")]
        public async void GetRaceActivityRaceFund()
        {
            byte[] data = null;

            Dictionary<string, object> racefund = new Dictionary<string, object>();
            racefund.Add("trophies", new List<object>());
            racefund.Add("isPay", false);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(racefund));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v2/player/getLastLadder")]
        public async void GetLadderReward()
        {
            byte[] data = null;

            Dictionary<string, object> ladderReward = new Dictionary<string, object>();
            ladderReward.Add("lastLadderScore", 999999);
            ladderReward.Add("lastLadderEndTime", Convert.ToUInt64(new DateTime(2024, 1, 1).Ticks));
            ladderReward.Add("lastLadderReward", true);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(ladderReward));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v2/player/getLadderScore")]
        public async void GetLadderScore()
        {
            byte[] data = null;

            Dictionary<string, object> ladderscore = new Dictionary<string, object>();
            ladderscore.Add("ladderScore", 12345);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(ladderscore));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v2/player/getLastTimeLimitedCompetitionReward")]
        public async void GetLastTimeLimitedCompetitionReward()
        {
            byte[] data = null;

            Dictionary<string, object> cancelcompetitionreward = new Dictionary<string, object>();
            cancelcompetitionreward.Add("lastEndTime", Convert.ToUInt64(new DateTime(2025, 12, 4).Ticks));
            cancelcompetitionreward.Add("lastRank", 24);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(cancelcompetitionreward));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v2/player/isJoinTimeLimitedCompetition")]
        public async void GetJoinTimeLimitedCompetition()
        {
            byte[] data = null;

            Dictionary<string, object> GetJoinTimeLimitedCompetition = new Dictionary<string, object>();
            GetJoinTimeLimitedCompetition.Add("res_code", 12345);
            GetJoinTimeLimitedCompetition.Add("endTime", Convert.ToInt64(new DateTime(2025, 12, 4).Ticks));
            GetJoinTimeLimitedCompetition.Add("isJoin", false);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(GetJoinTimeLimitedCompetition));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v2/player/getServerTime")]
        public async void GetServerTime()
        {
            byte[] data = null;

            Dictionary<string, object> ServerTime = new Dictionary<string, object>();
            ServerTime.Add("currentTime", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(ServerTime));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v2/player/sevensigned")]
        public async void SevenDaySignin()
        {
            byte[] data = null;

            Dictionary<string, object> ParseSevenSignInData = new Dictionary<string, object>();
            ParseSevenSignInData.Add("errorcode", 0);
            ParseSevenSignInData.Add("sevenSignTimes", 1);
            ParseSevenSignInData.Add("lastSevenSignTime", 1);
            ParseSevenSignInData.Add("nowTime", 123);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(ParseSevenSignInData));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v2/player/setGuideReward")]
        public async void SetOperation()
        {
            byte[] data = null;

            Dictionary<string, object> guidereward = new Dictionary<string, object>();

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(guidereward));

            Console.WriteLine(this.RequestBodyAsync().Result);

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
