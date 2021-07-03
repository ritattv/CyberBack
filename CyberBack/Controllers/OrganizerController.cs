using System;
using System.Collections.Generic;
using System.Linq;
using CyberDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CyberBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Organizer> Get()
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Organizer.ToList();
        }

        [HttpGet("{id}")]
        public Organizer Get(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Configuration.LazyLoadingEnabled = false;
            return entities.Organizer.FirstOrDefault(e => e.organizer_id == id);
        }

        [HttpPost]
        public void Post([FromBody] Organizer organizer)
        {
            using var entities = new cyberdbEntities();
            organizer.organizer_login = Encryption.GetInstance()
                .HashPassword(organizer.organizer_login, Encryption.GetInstance().GenerateSalt());
            organizer.organizer_password = Encryption.GetInstance()
                .HashPassword(organizer.organizer_password, Encryption.GetInstance().GenerateSalt());
            entities.Organizer.Add(organizer);
            entities.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using var entities = new cyberdbEntities();
            entities.Organizer.Remove(entities.Organizer.FirstOrDefault(e => e.organizer_id == id) ??
                                      throw new InvalidOperationException());
            entities.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Organizer organizer)
        {
            using var entities = new cyberdbEntities();
            var entity = entities.Organizer.FirstOrDefault(e => e.organizer_id == id);
            if (entity != null)
            {
                entity.organizer_name = organizer.organizer_name;
                entity.organizer_description = organizer.organizer_description;
                entity.organizer_login = Encryption.GetInstance()
                    .HashPassword(organizer.organizer_login, Encryption.GetInstance().GenerateSalt());
                ;
                entity.organizer_password = Encryption.GetInstance().HashPassword(organizer.organizer_password,
                    Encryption.GetInstance().GenerateSalt());
            }

            ;
            entities.SaveChanges();
        }
    }
}