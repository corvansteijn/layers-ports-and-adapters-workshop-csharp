using System;
using Shared.Entity;

namespace Shared.Application
{
    public class ScheduleMeetupService
    {
        private readonly MeetupRepository meetupRepository;

        public ScheduleMeetupService(MeetupRepository meetupRepository)
        {
            this.meetupRepository = meetupRepository;
        }

        public void ScheduleMeetup(MeetupScheduleContext meetup)
        {
            meetupRepository.Add(Meetup.Schedule(
                Name.FromString(meetup.Name),
                Description.FromString(meetup.Description),
                meetup.ScheduledFor));

        }
    }

    public class MeetupScheduleContext
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ScheduledFor { get; set; }
    }
}