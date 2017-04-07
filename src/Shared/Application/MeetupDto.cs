using System;
using Shared.Entity;

namespace Shared.Application
{
    public class MeetupDto
    {
        public MeetupDto(Meetup meetup)
        {
            Id = meetup.Id;
            Name = meetup.Name.ToString();
            Description = meetup.Description.ToString();
            ScheduledFor = meetup.ScheduledFor;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset ScheduledFor { get; private set; }
    }
}