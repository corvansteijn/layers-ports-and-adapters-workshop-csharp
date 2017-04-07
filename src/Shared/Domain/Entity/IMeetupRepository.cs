using System;
using System.Collections;
using System.Collections.Generic;
using Shared.Application;

namespace Shared.Entity
{
    public interface IMeetupRepository
    {
        void Add(Meetup meetup);
        Meetup GetById(Guid id);
        IEnumerable<Meetup> GetUpcomingMeetups(DateTime now);
        IEnumerable<Meetup> GetPastMeetups(DateTime now);
    }
}