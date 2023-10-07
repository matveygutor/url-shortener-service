using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using test.Models;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        LinkContext db;
        

        public HomeController(ILogger<HomeController> logger, LinkContext context)
        {
            _logger = logger;
            db = context;
        }


        [Route("{token:minlength(7)?}")]
        public async Task<IActionResult> Index(string? token)
        {
            if (token != null)
            {
                bool any = await db.Links.AnyAsync(p => p.Token == token);
                if (!any)
                {
                    return Problem($"Token '{token}' is not registered in the system");
                }
                else
                {
                    try
                    {
                        Link URL = new();
                        URL = await db.Links.SingleAsync(p => p.Token == token);
                        URL.Click++;
                        URL.LastDate = DateTime.Now;
                        db.Links.Update(URL);
                        await db.SaveChangesAsync();
                        return RedirectPermanent(URL.LongURL);
                    }
                    catch (Exception ex)
                    {
                        return Problem($"{ex.Message}");
                    }
                }
            }
            return View(await db.Links.ToListAsync());
        }


        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Links == null)
            {
                return NotFound();
            }

            var link = await db.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }


        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Links == null)
            {
                return Problem("Entity set is null.");
            }

            var link = await db.Links.FindAsync(id);

            if (link != null)
            {
                db.Links.Remove(link);
            }

            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Links == null)
            {
                return NotFound();
            }

            var link = await db.Links.FindAsync(id);

            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Link link)
        {
            if (id != link.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Links.Update(link);

                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkExists(link.Id))
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
            return View(link);
        }


        private bool LinkExists(int id)
        {
            return (db.Links?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}