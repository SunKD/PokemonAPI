using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace pokeinfo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            return View("Intro");
        }

        [HttpGet]
        [Route("pokemon/{pokeid}")]
        public IActionResult QueryPoke(int pokeid)
        {
            var PokeInfo = new Dictionary<string, object>();
            WebRequest.GetPokemonDataAsync(pokeid, ApiResponse =>
                {
                    PokeInfo = ApiResponse;
                }
            ).Wait();
            // Other code
            ViewBag.Name = PokeInfo["name"];
            JArray alltypes = (JArray)PokeInfo["types"];
            JObject obj = (JObject)PokeInfo["sprites"];
            foreach(var types in alltypes){
                if((int)types["slot"] ==1){
                    ViewBag.Type = types["type"]["name"];
                }
                else if ((int)types["slot"] ==2){
                    ViewBag.Type2 = types["type"]["name"];
                }
            }
            ViewBag.Sprite = obj["front_default"];
            ViewBag.Weight = PokeInfo["weight"];
            ViewBag.Height = PokeInfo["height"];
            return View("Index");
        }

    }

}