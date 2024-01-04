using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyDiem.Data;
using QuanLyDiem.Models;
using X.PagedList;

namespace QuanLyDiem.Controllers
{
    [Authorize]
    public class KhoaHocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhoaHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KhoaHoc
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query =_context.KhoaHoc.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.TenKhoaHoc.Contains(searchText));
            }
            var khoaHoc = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = khoaHoc.Count;
            }

            var totalCount = khoaHoc.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(khoaHoc.ToPagedList(pageNumber, actualPageSize));
        }


        // GET: KhoaHoc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhoaHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKhoaHoc,TenKhoaHoc,NamBatDau,NamKetThuc")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khoaHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khoaHoc);
        }

        // GET: KhoaHoc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.KhoaHoc == null)
            {
                return NotFound();
            }

            var khoaHoc = await _context.KhoaHoc.FindAsync(id);
            if (khoaHoc == null)
            {
                return NotFound();
            }
            return View(khoaHoc);
        }

        // POST: KhoaHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKhoaHoc,TenKhoaHoc,NamBatDau,NamKetThuc")] KhoaHoc khoaHoc)
        {
            if (id != khoaHoc.MaKhoaHoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khoaHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhoaHocExists(khoaHoc.MaKhoaHoc))
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
            return View(khoaHoc);
        }

        // GET: KhoaHoc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.KhoaHoc == null)
            {
                return NotFound();
            }

            var khoaHoc = await _context.KhoaHoc
                .FirstOrDefaultAsync(m => m.MaKhoaHoc == id);
            if (khoaHoc == null)
            {
                return NotFound();
            }

            return View(khoaHoc);
        }

        // POST: KhoaHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.KhoaHoc == null)
            {
                return Problem("Entity set 'ApplicationDbContext.KhoaHoc'  is null.");
            }
            var khoaHoc = await _context.KhoaHoc.FindAsync(id);
            if (khoaHoc != null)
            {
                _context.KhoaHoc.Remove(khoaHoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhoaHocExists(string id)
        {
          return (_context.KhoaHoc?.Any(e => e.MaKhoaHoc == id)).GetValueOrDefault();
        }
    }
}
