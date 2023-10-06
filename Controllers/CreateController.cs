using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Models;
using System.Text;

namespace test.Controllers
{
    public class CreateController : Controller
    {
        private readonly LinkContext _context;
        private Random _random;

        public CreateController(LinkContext context)
        {
            _context = context;
            _random = new Random();
        }

        // GET: Create
        public IActionResult Index()
        {
            return View();
        }

        // GET: Create/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Links == null)
            {
                return NotFound();
            }

            Link link = await _context.Links.FirstAsync(m => m.Id == id);
            
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // GET: Create/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Create/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Link link)
        {
            bool result = _context.Links.Any(u => u.LongURL == link.LongURL);
            
            if (!result)
            {
                if (ModelState.IsValid)
                {
                    link.Token = GenerateToken();
                    
                    GenerateShortURL(link);
                    
                    link.Date = DateTime.Now;
                    
                    link.Click = 0;
                    
                    _context.Add(link);
                    
                    await _context.SaveChangesAsync();

                    return Redirect("https://" + Request.Host);
                }
            }
            else
            {
                var searchId = _context.Links.First(l => l.LongURL == link.LongURL);
                
                return RedirectToAction(nameof(Details), nameof(Create), new { id = searchId.Id });
            }

            return View(link);

        }

        private string GenerateShortURL(Link link) =>
            link.ShortURL = "https://" + Request.Host + "/" + link.Token;
        

        private string GenerateToken()
        {
            string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            
            var sb = new StringBuilder();
            
            for (int i = 0; i < 7; i++)
            {
                sb.Append(chars[_random.Next(0, chars.Length)]);
            }
            
            return sb.ToString();
        }

    }
}
