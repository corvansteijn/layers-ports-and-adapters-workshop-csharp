using System;
using Microsoft.AspNetCore.Mvc;
using Shared.Application;
using Shared.Application.Command;
using Web.Models;

namespace Web.Controllers
{
    public class MeetupController : Controller
    {
        private readonly ScheduleMeetupCommandHandler scheduleMeetupCommandHandler;
        private readonly ScheduleMeetupProvider provider;

        public MeetupController(ScheduleMeetupCommandHandler scheduleMeetupCommandHandler, ScheduleMeetupProvider provider)
        {
            this.scheduleMeetupCommandHandler = scheduleMeetupCommandHandler;
            this.provider = provider;
        }

        public IActionResult Index()
        {
            var now = DateTime.Now;

            var upcomingMeetups = provider.GetUpcomingMeetups(now);
            var pastMeetups = provider.GetPastMeetups(now);

            return View(new MeetupOverview{
                UpcomingMeetups = upcomingMeetups,
                PastMeetups = pastMeetups
            });
        }

        public IActionResult Details(long id)
        {
            MeetupDto meetup = provider.GetMeetup(id);

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

            scheduleMeetupCommandHandler.Handle(
                meetup.Name,
                meetup.Description,
                meetup.ScheduledFor);

            return RedirectToAction("Index");
        }
    }
}
