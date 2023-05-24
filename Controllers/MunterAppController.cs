using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MunterService;
using MunterService.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MunterServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MunterAppController : ControllerBase
    {

        //private readonly ILogger<MunterAppController> _logger;

        //public MunterAppController(ILogger<MunterAppController> logger)
        //{
        //    _logger = logger;
        //}



        /// <summary>
        /// <description>
        /// GET api/gifs/?searchText={} or api/gifs
        /// </description>
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns>JSONResponse</returns>
        [Route("api/gifs")]
        [HttpGet]
        public JSONResponse GetGifsList(string searchText)
        {
            string myName = NlogLogger.InitMethodName();
            NlogLogger.Log.Info($"{myName} searchText:[{searchText}]");
            var response = new JSONResponse() { Status = HttpStatusCode.BadRequest };

            try
            {
                string message = string.Empty;
                string respGifsData = string.Empty;

                
                if (!string.IsNullOrEmpty(searchText))
                {
                    //Search in gifs/search or from                    
                    if (Functions.cacheDic.ContainsKey(searchText))
                    {
                        response.Status = HttpStatusCode.OK;
                        response.Data = JsonConvert.DeserializeObject<Original[]>(Functions.cacheDic[searchText]);
                    }
                    else
                    {
                        string searchUrl = "https://api.giphy.com/v1/gifs/search?api_key=2bwcVkYS3ndztWiWLJmNVRCA7u9DwDPq&q=" + searchText;

                        respGifsData = Functions.GetGifsListFromGiphyCom(searchUrl).ToString();

                        GiphyResponse giphyResponse = JsonConvert.DeserializeObject<GiphyResponse>(respGifsData);

                        bool addToDic = true;
                        response.Data = ReturnUrlsList(giphyResponse, searchText, addToDic);
                    }
                }
                else
                {
                    //Search in gifs/trending
                    string searchTrendingUrl = "https://api.giphy.com/v1/gifs/trending?api_key=2bwcVkYS3ndztWiWLJmNVRCA7u9DwDPq";

                    respGifsData = Functions.GetGifsListFromGiphyCom(searchTrendingUrl).ToString();
                    if (!string.IsNullOrEmpty(respGifsData))
                    {
                        response.Status = HttpStatusCode.OK;
                        GiphyResponse giphyResponse = JsonConvert.DeserializeObject<GiphyResponse>(respGifsData);

                        response.Data = ReturnUrlsList(giphyResponse);
                    }
                    else
                    {
                        response.Status = HttpStatusCode.NotFound;
                        response.Message = "Not found data in Giphy.com";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                NlogLogger.Log.Error($"{myName} error:{ex.Message}");

            }

            return response;
        }

        private static Original[] ReturnUrlsList(GiphyResponse giphyResponse, string searchText = "", bool addToDic = false)
        {
            string myName = NlogLogger.InitMethodName();
            NlogLogger.Log.Info($"{myName} searchText:[{searchText}]");

            Original[] response = null;
            try
            {
                string newJsonData = string.Empty;
                foreach (var gif in giphyResponse.Data)
                {
                    newJsonData += "{\"url\":\"" + gif.Images.Original.Url + "\"},";
                }

                var urlsList = @"[" + newJsonData.TrimEnd(',') + "]";

                //Save to cache if not tranding
                if (addToDic)
                {
                    Functions.cacheDic.Add(searchText, urlsList);
                }

                response = JsonConvert.DeserializeObject<Original[]>(urlsList);
            }
            catch (Exception ex)
            {
                NlogLogger.Log.Error($"{myName} error:{ex.Message}");
            }

            return response;
        }    
    }
}
