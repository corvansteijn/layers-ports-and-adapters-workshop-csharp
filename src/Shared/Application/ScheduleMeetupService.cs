using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Entity;

namespace Shared.Application
{
    public class ScheduleMeetupProvider
    {
        private readonly IMeetupRepository meetupProvider;

        public ScheduleMeetupProvider(IMeetupRepository meetupProvider)
        {
            this.meetupProvider = meetupProvider;
        }

        public MeetupDto GetMeetup(Guid identifier)
        {
            return new MeetupDto(meetupProvider.GetById(identifier));
        }

        public IEnumerable<MeetupDto> GetUpcomingMeetups(DateTime now)
        {
            return meetupProvider.GetUpcomingMeetups(now)
                .Select(m => new MeetupDto(m))
                .ToArray();
        }

        public IEnumerable<MeetupDto> GetPastMeetups(DateTime now)
        {
            return meetupProvider.GetPastMeetups(now)
                .Select(m => new MeetupDto(m))
                .ToArray();
        }
    }
}