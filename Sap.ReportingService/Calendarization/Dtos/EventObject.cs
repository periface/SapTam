using System;
using System.Collections.Generic;

namespace Sap.ReportingService.Calendarization.Dtos
{
    public class EventObject
    {
        public IEnumerable<EventDto> EventsDto { get; set; }
    }

    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Start => (long)(StartDate - new DateTime(1970, 1, 1)).TotalMilliseconds;
        public decimal End => (long)(EndDate - new DateTime(1970, 1, 1)).TotalMilliseconds;
        public string Url { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
