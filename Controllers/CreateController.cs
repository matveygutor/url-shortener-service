﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test;
using test.Models;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        public async Task<IActionResult> Index()
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

            var link = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,LongURL,ShortURL,Date,Click")] Link link)
        {
            if (ModelState.IsValid)
            {
                link.ShortURL = "https://" + Request.Host + "/" + GenerateToken();
                link.Date = DateTime.Now;
                link.Click = 0;
                _context.Add(link);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(HomeController.Index));
                
            }
            await Response.SendFileAsync("_Layout.cshtml");
            return View(link);

        }

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
