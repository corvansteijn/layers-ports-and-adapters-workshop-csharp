using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Shared.Application;
using Shared.Entity;

namespace Shared.Infrastructure
{
    public class MeetupRepository : IMeetupRepository
    {
        private string filePath;

        public MeetupRepository(string filePath) 
        {
            this.filePath = filePath;
        }

        public void Add(Meetup meetup)
        {
            var meetups = GetPersistedMeetups();

            meetups.Add(meetup);

            PersistMeetups(meetups);
        }

        public Meetup GetById(Guid id)
        {
            var meetups = GetPersistedMeetups();

            var meetup = meetups.SingleOrDefault(x => x.Id == id);

            if(meetup == null)
            {
                throw new ArgumentException("Meetup not found");
            }

            return meetup;
        }

        public IEnumerable<Meetup> GetUpcomingMeetups(DateTime now)
        {
            var meetups = GetPersistedMeetups();

            return meetups.Where(x => x.IsUpcoming(now));
        }

        public IEnumerable<Meetup> GetPastMeetups(DateTime now)
        {
            var meetups = GetPersistedMeetups();

            return meetups.Where(x => !x.IsUpcoming(now));
        }

        private void PersistMeetups(List<Meetup> meetups) 
        {
            using(var fileStream = File.OpenWrite(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<Meetup>));

                serializer.Serialize(fileStream, meetups);
            }
        }

        private List<Meetup> GetPersistedMeetups()
        {
            if(!File.Exists(filePath))
            {
                return new List<Meetup>();
            }

            using(var fileStream = File.OpenRead(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<Meetup>));

                return (List<Meetup>)serializer.Deserialize(fileStream);
            }
        }
    }
}