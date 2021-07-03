using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberDataAccess;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Stats> Get()
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Stats.ToList();
        }

        [HttpGet("{id}")]
        public Stats Get(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Stats.FirstOrDefault(e => e.stats_id == id);
        }

        [HttpPost]
        public void Post([FromBody] Stats stats)
        {
            using var entities = new cyberdbEntities();
            entities.Stats.Add(stats);
            entities.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Stats.Remove(entities.Stats.FirstOrDefault(e => e.stats_id == id) ??
                                   throw new InvalidOperationException());
            entities.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Stats stats)
        {
            using var entities = new cyberdbEntities();
            var entity = entities.Stats.FirstOrDefault(e => e.stats_id == id);
            if (entity != null)
            {
                entity.stats_median = stats.stats_median;
                entity.stats_mode = stats.stats_mode;
                entity.stats_mean = stats.stats_mean;
                entity.stats_stdev = stats.stats_stdev;
                entity.health_id = stats.health_id;
            }

            entities.SaveChanges();
        }
    }
}
