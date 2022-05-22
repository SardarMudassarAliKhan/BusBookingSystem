using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBS.Domain.Data.Domain;
using CVBank.Data;
using BusBookingSystem.ViewModes;

namespace BusBookingSystem.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            List<TripsVM> tripsVM = new List<TripsVM>();
            var data = await _context.Trips.ToListAsync();
            var locationsList = await _context.Locations.ToListAsync();
            foreach (var item in data)
            {
                try
                {
                    var locations = locationsList.FirstOrDefault(x => x.Id == item.Id);
                    if (locations == null)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        tripsVM.Add(new TripsVM()
                        {
                            Id = item.Id,
                            FromLocation = locations.Name,
                            ToLocation = locations.Name,
                            DurationMinHour = item.DurationMinHour,
                            DurationMaxHour = item.DurationMaxHour,
                            Rate13Seat = item.Rate13Seat,
                            Rate23Seat = item.Rate23Seat,
                            Rate35Seat = item.Rate35Seat,
                            Rate53Seat = item.Rate53Seat
                        });
                    }

                    
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            return View(tripsVM);
        }
        //FromLocation
        public Object FromLocation()
        {
            return (_context.Locations.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToList().Where(x => x.Name != null && x.Address != null));
        }
        //ToLocation
        public Object ToLocation()
        {
            return (_context.Locations.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToList().Where(x => x.Name != null && x.Address != null));
        }
        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trips = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trips == null)
            {
                return NotFound();
            }

            return View(trips);
        }

        // GET: Trips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FromLocation,ToLocation,DurationMinHour,DurationMaxHour,Rate13Seat,Rate23Seat,Rate35Seat,Rate53Seat")] Trips trips)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trips);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trips);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trips = await _context.Trips.FindAsync(id);
            if (trips == null)
            {
                return NotFound();
            }
            return View(trips);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromLocation,ToLocation,DurationMinHour,DurationMaxHour,Rate13Seat,Rate23Seat,Rate35Seat,Rate53Seat")] Trips trips)
        {
            if (id != trips.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trips);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripsExists(trips.Id))
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
            return View(trips);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trips = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trips == null)
            {
                return NotFound();
            }

            return View(trips);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trips = await _context.Trips.FindAsync(id);
            _context.Trips.Remove(trips);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripsExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}
