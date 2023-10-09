using AwesineDevEvents.API.Entities;
using AwesineDevEvents.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesineDevEvents.API.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEvetsController : ControllerBase
    {
        private readonly DevEventsDbContex _context;

        public DevEvetsController(DevEventsDbContex context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() {
            var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();
            return Ok(devEvents);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) {
            var devEvent = _context.DevEvents.Where(d => d.Id == id).ToList();

            return devEvent == null ? NotFound() : Ok(devEvent);
        }

        [HttpPost]
        public IActionResult Post(DevEvents devEvet) {
            var devEventExists = _context.DevEvents.SingleOrDefault(d => d.Id == devEvet.Id);

            if (devEventExists != null) return BadRequest("Evento já cadastrado!!");
            _context.DevEvents.Add(devEvet);
            return CreatedAtAction(nameof(GetById), new {id = devEvet.Id}, devEvet);

        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, DevEvents devEvent) {
            var devEventFind = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEventFind == null) return NotFound();
            
            if(devEventFind.IsDeleted) return BadRequest("Não pode atulizar evento já excluido!");

            devEvent.Update(devEvent.Title, devEvent.Description, devEvent.StartDate, devEvent.EndDate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null) return NotFound();

            devEvent.Delete();

            return NoContent();
        }

        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, DevEventsSpeaker speaker) {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null) return NotFound();

            devEvent.Speakers.Add(speaker);

            return Ok(devEvent);
        }
    }
}

