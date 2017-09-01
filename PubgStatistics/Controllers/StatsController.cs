using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using PUBGSharp;
using PUBGSharp.Helpers;

namespace PubgStatistics.Controllers {
    [Produces("application/json")]
    [EnableCors("AllowAll")]
    public class StatsController : Controller {
        private static readonly string _apiKey = Environment.GetEnvironmentVariable("PubgApiKey");

        [HttpGet]
        [Route("api/stats/{userId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetStats(string userId) {
            using (var client = new PUBGStatsClient(_apiKey)) {
                var stats = await client.GetPlayerStatsAsync(userId).ConfigureAwait(false);
                var na = stats.Stats.Find(s => s.Region == PUBGSharp.Data.Region.NA);

                var solo = stats.Stats.Find(s => s.Region == PUBGSharp.Data.Region.NA && s.Mode == PUBGSharp.Data.Mode.Solo);
                var duos = stats.Stats.Find(s => s.Region == PUBGSharp.Data.Region.NA && s.Mode == PUBGSharp.Data.Mode.Duo);
                var squads = stats.Stats.Find(s => s.Region == PUBGSharp.Data.Region.NA && s.Mode == PUBGSharp.Data.Mode.Squad);

                var minimalStats = new Models.MinimalStats() {
                    Solo = new List<string> {
                    solo.Stats.Find(x => x.Stat == Stats.KDR).Value,
                    solo.Stats.Find(x => x.Stat == Stats.Wins).Value
                    },
                    Duos = new List<string> {
                        duos.Stats.Find(x => x.Stat == Stats.KDR).Value,
                        duos.Stats.Find(x => x.Stat == Stats.Wins).Value
                    },
                    Squads = new List<string> {
                        squads.Stats.Find(x => x.Stat == Stats.KDR).Value,
                        squads.Stats.Find(x => x.Stat == Stats.Wins).Value
                    }
                };

                var json = JsonConvert.SerializeObject(minimalStats);
                return Ok(json);
            };
        }
    }
}