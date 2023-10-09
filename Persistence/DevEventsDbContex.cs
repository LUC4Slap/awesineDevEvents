using AwesineDevEvents.API.Entities;

namespace AwesineDevEvents.API.Persistence
{
    public class DevEventsDbContex
    {
        public List<DevEvents> DevEvents { get; set; }

        public DevEventsDbContex()
        {
            DevEvents = new List<DevEvents>();
        }
    }
}
