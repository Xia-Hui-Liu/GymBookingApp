using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymBookingApp.Data;
using GymBookingApp.Models;
using GymBookingApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using GymBookingApp.Services;

namespace GymBookingApp.Controllers
{
    [Authorize]
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;
        private readonly IGymClassService _service;
        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper, IGymClassService service) // inject UserManager<ApplicationUser> into GymClassesController
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _service = service;
        }

        [AllowAnonymous]
        // GET: GymClasses
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        
        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));

            var gymClassWithAttendees = await _context.GymClasses
                .Where(g => g.Id == id)
                .Include(c => c.ApplicationUsers)
                .ThenInclude(u => u.ApplicationUser).FirstOrDefaultAsync();

            if (gymClassWithAttendees == null)
                return RedirectToAction(nameof(Index));

            return View(gymClassWithAttendees);
        }

        [Authorize(Roles = "Admin")]
        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        [Authorize(Roles = "Admin")]
        // GET: GymClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        [Authorize(Roles = "Admin")]
        // GET: GymClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        [Authorize(Roles = "Admin")]
        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass != null)
            {
                _context.GymClasses.Remove(gymClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
            return _context.GymClasses.Any(e => e.Id == id);
        }


        public async Task<IActionResult> BookingToggle(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);

            var attending = await _context.ApplicationUserGymClasses.FindAsync(userId, id);

            if (attending == null)
            {
                var booking = new ApplicationUserGymClass
                {
                    ApplicationUserId = userId,
                    GymClassId = (int)id
                };
                _context.ApplicationUserGymClasses.Add(booking);
            }
            else
            {
                _context.Remove(attending);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> History()
        {
            var gymClasses = await _context.GymClasses
                .Include(g => g.ApplicationUsers)
                .Where(g => g.StartTime < DateTime.Now)
                .ToListAsync();

            var model = _mapper.Map<IndexViewModel>(gymClasses);

            return View(model);
        }

        public async Task<IActionResult> MyHistory()
        {
            var userId = _userManager.GetUserId(User);

            var historicalClasses = await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.GymClasses!)
                .Select(ac => ac.GymClass)
                .Where(g => g.StartTime < DateTime.Now)
                .ToListAsync();

            var model = _mapper.Map<IndexViewModel>(historicalClasses);

            return View(model);
        }
        public async Task<IActionResult> MyBookings()
        {

            var userId = _userManager.GetUserId(User);

            var bookedClasses = await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.GymClasses!)
                .Select(ac => ac.GymClass)
                .Where(g => g.StartTime > DateTime.Now)
                .ToListAsync();

            var model = _mapper.Map<IndexViewModel>(bookedClasses);

            return View(model);
        }

    }

}
