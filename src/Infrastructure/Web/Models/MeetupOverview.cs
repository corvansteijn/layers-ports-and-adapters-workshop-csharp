using System.Collections.Generic;
using Shared.Application;

namespace Web.Models
{
    public class MeetupOverview
    {
        public IEnumerable<MeetupDto> UpcomingMeetups{get;set;}
        public IEnumerable<MeetupDto> PastMeetups{get;set;}
    }
}