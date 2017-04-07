using System;
using Shared.Entity;

namespace Shared.Application.Command
{
    public delegate void NotifyMeetupCreated(Guid id);
    public class ScheduleMeetupCommandHandler
    {
        private readonly IMeetupRepository meetupRepository;
        private readonly NotifyMeetupCreated created;

        public ScheduleMeetupCommandHandler(IMeetupRepository meetupRepository, NotifyMeetupCreated created)
        {
            this.meetupRepository = meetupRepository;
            this.created = created;
        }

        public void Handle(string name, string description, string scheduledFor)
        {
            Guid id = Guid.NewGuid();
            meetupRepository.Add(Meetup.Schedule(id,
                Name.FromString(name),
                Description.FromString(description), 
                DateTimeOffset.Parse(scheduledFor)));
            created(id);
        }
    }
}