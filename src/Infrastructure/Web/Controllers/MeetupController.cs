using System;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Application;
using Shared.Entity;
using Web.Models;

namespace Web.Controllers
{
    public class MeetupController : Controller
    {
        private readonly ScheduleMeetupService _service;
        private readonly MeetupRepository meetupRepository;

        public MeetupController(ScheduleMeetupService service, MeetupRepository meetupRepository)
        {
            _service = service;
            this.meetupRepository = meetupRepository;
        }

        public IActionResult Index()
        {
            var now = DateTime.Now;

            var upcomingMeetups = meetupRepository.GetUpcomingMeetups(now);
            var pastMeetups = meetupRepository.GetPastMeetups(now);

            return View(new MeetupOverview{
                UpcomingMeetups = upcomingMeetups,
                PastMeetups = pastMeetups
            });
        }

        public IActionResult Details(long id)
        {
            var meetup = meetupRepository.GetById(id);

            return View(meetup);
        }

        [HttpGet]
        public IActionResult Schedule()
        {
            return View(new ScheduleMeetup());
        }

        [HttpPost]
        public IActionResult Schedule(ScheduleMeetup meetup)
        {
            if(string.IsNullOrEmpty(meetup.Name))
            {
                ModelState.AddModelError("Name", "Name cannot be empty");
            }

            if(string.IsNullOrEmpty(meetup.Description))
            {
                ModelState.AddModelError("Description", "Description cannot be empty");
            }    
            
            if(!ModelState.IsValid)
            {
                return View(meetup);
            }

            _service.ScheduleMeetup(new MeetupScheduleContext
            {
                Name = meetup.Name,
                Description = meetup.Description,
                ScheduledFor = meetup.ScheduledFor,
            });

            return RedirectToAction("Index");
        }
    }
}
