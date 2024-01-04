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
    public class ChuyenNganhController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChuyenNganhController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChuyenNganh
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query = _context.ChuyenNganh.Include(c => c.Khoa).AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.TenChuyenNganh.Contains(searchText) || d.MaKhoa.Contains(searchText));
            }
            var chuyenNganh = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = chuyenNganh.Count;
            }

            var totalCount = chuyenNganh.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(chuyenNganh.ToPagedList(pageNumber, actualPageSize));
        }
        // GET: ChuyenNganh/Create
        public IActionResult Create()
        {
            ViewData["MaKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa");
            return View();
        }

        // POST: ChuyenNganh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChuyenNganh,TenChuyenNganh,MaKhoa")] ChuyenNganh chuyenNganh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chuyenNganh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", chuyenNganh.MaKhoa);
            return View(chuyenNganh);
        }

        // GET: ChuyenNganh/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ChuyenNganh == null)
            {
                return NotFound();
            }

            var chuyenNganh = await _context.ChuyenNganh.FindAsync(id);
            if (chuyenNganh == null)
            {
                return NotFound();
            }
            ViewData["MaKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", chuyenNganh.MaKhoa);
            return View(chuyenNganh);
        }

        // POST: ChuyenNganh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaChuyenNganh,TenChuyenNganh,MaKhoa")] ChuyenNganh chuyenNganh)
        {
            if (id != chuyenNganh.MaChuyenNganh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuyenNganh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuyenNganhExists(chuyenNganh.MaChuyenNganh))
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
            ViewData["MaKhoa"] = new SelectList(_context.Khoa, "MaKhoa", "TenKhoa", chuyenNganh.MaKhoa);
            return View(chuyenNganh);
        }

        // GET: ChuyenNganh/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ChuyenNganh == null)
            {
                return NotFound();
            }

            var chuyenNganh = await _context.ChuyenNganh
                .Include(c => c.Khoa)
                .FirstOrDefaultAsync(m => m.MaChuyenNganh == id);
            if (chuyenNganh == null)
            {
                return NotFound();
            }

            return View(chuyenNganh);
        }

        // POST: ChuyenNganh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ChuyenNganh == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ChuyenNganh'  is null.");
            }
            var chuyenNganh = await _context.ChuyenNganh.FindAsync(id);
            if (chuyenNganh != null)
            {
                _context.ChuyenNganh.Remove(chuyenNganh);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuyenNganhExists(string id)
        {
          return (_context.ChuyenNganh?.Any(e => e.MaChuyenNganh == id)).GetValueOrDefault();
        }
    }
}
