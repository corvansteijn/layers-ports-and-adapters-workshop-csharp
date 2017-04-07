using System;
using Shared.Entity;

namespace Shared.Application.Command
{
    public class ScheduleMeetupCommandHandler
    {
        private readonly IMeetupRepository meetupRepository;

        public ScheduleMeetupCommandHandler(IMeetupRepository meetupRepository)
        {
            this.meetupRepository = meetupRepository;
        }

        public void Handle(string name, string description, string scheduledFor)
        {
            meetupRepository.Add(Meetup.Schedule(
            Guid id = Guid.NewGuid();
            meetupRepository.Add(Meetup.Schedule(id,
                Name.FromString(name),
                Description.FromString(description), 
                DateTimeOffset.Parse(scheduledFor)));            
        }
    }
}