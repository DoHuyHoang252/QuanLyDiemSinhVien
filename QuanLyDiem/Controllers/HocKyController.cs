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
    public class HocKyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HocKyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HocKy
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query = _context.HocKy.Include(h => h.KhoaHoc).AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.TenHocKy.Contains(searchText) || d.MaKhoaHoc.Contains(searchText));
            }
            var hocKy = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = hocKy.Count;
            }

            var totalCount = hocKy.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(hocKy.ToPagedList(pageNumber, actualPageSize));
        }

        // GET: HocKy/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.HocKy == null)
            {
                return NotFound();
            }

            var hocKy = await _context.HocKy
                .Include(h => h.KhoaHoc)
                .FirstOrDefaultAsync(m => m.MaHocKy == id);
            if (hocKy == null)
            {
                return NotFound();
            }

            return View(hocKy);
        }

        // GET: HocKy/Create
        public IActionResult Create()
        {
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHoc, "MaKhoaHoc", "MaKhoaHoc");
            return View();
        }

        // POST: HocKy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKhoaHoc,MaHocKy,TenHocKy")] HocKy hocKy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hocKy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHoc, "MaKhoaHoc", "MaKhoaHoc", hocKy.MaKhoaHoc);
            return View(hocKy);
        }

        // GET: HocKy/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.HocKy == null)
            {
                return NotFound();
            }

            var hocKy = await _context.HocKy.FindAsync(id);
            if (hocKy == null)
            {
                return NotFound();
            }
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHoc, "MaKhoaHoc", "MaKhoaHoc", hocKy.MaKhoaHoc);
            return View(hocKy);
        }

        // POST: HocKy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKhoaHoc,MaHocKy,TenHocKy")] HocKy hocKy)
        {
            if (id != hocKy.MaHocKy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocKy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocKyExists(hocKy.MaHocKy))
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
            ViewData["MaKhoaHoc"] = new SelectList(_context.KhoaHoc, "MaKhoaHoc", "MaKhoaHoc", hocKy.MaKhoaHoc);
            return View(hocKy);
        }

        // GET: HocKy/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.HocKy == null)
            {
                return NotFound();
            }

            var hocKy = await _context.HocKy
                .Include(h => h.KhoaHoc)
                .FirstOrDefaultAsync(m => m.MaHocKy == id);
            if (hocKy == null)
            {
                return NotFound();
            }

            return View(hocKy);
        }

        // POST: HocKy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HocKy == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HocKy'  is null.");
            }
            var hocKy = await _context.HocKy.FindAsync(id);
            if (hocKy != null)
            {
                _context.HocKy.Remove(hocKy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HocKyExists(string id)
        {
          return (_context.HocKy?.Any(e => e.MaHocKy == id)).GetValueOrDefault();
        }
    }
}
