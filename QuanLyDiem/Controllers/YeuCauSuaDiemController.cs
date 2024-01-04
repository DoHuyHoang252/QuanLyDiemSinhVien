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
    public class YeuCauSuaDiemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YeuCauSuaDiemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: YeuCauSuaDiem
public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query =_context.YeuCauSuaDiem.Include(y => y.BangDiem).Include(y => y.GiangVien).Include(y => y.HocPhan).Include(y => y.SinhVien).AsQueryable();
            
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.MaHocPhan.Contains(searchText) || d.MaSinhVien.Contains(searchText));
            }
            var YeuCauSuaDiem = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = YeuCauSuaDiem.Count;
            }

            var totalCount = YeuCauSuaDiem.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(YeuCauSuaDiem.ToPagedList(pageNumber, actualPageSize));
        }
        // GET: YeuCauSuaDiem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.YeuCauSuaDiem == null)
            {
                return NotFound();
            }

            var yeuCauSuaDiem = await _context.YeuCauSuaDiem
                .Include(y => y.BangDiem)
                .Include(y => y.GiangVien)
                .Include(y => y.HocPhan)
                .Include(y => y.SinhVien)
                .FirstOrDefaultAsync(m => m.MaYeuCauSuaDiem == id);
            if (yeuCauSuaDiem == null)
            {
                return NotFound();
            }

            return View(yeuCauSuaDiem);
        }

        // GET: YeuCauSuaDiem/Create
        public IActionResult Create()
        {
            ViewData["DiemChuyenCan"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemChuyenCan");
            ViewData["DiemKiemTra"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemKiemTra");
            ViewData["MaGiangVien"] = new SelectList(_context.GiangVien, "MaGiangVien", "TenGiangVien");
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan");
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien");
            return View();
        }

        // POST: YeuCauSuaDiem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaYeuCauSuaDiem,MaGiangVien,MaSinhVien,MaHocPhan,DiemChuyenCanMoi,DiemKiemTraMoi,LyDo")] YeuCauSuaDiem yeuCauSuaDiem)
        {
            if (ModelState.IsValid)
            {
                var selectedDiem = await _context.BangDiem
                .Where(e => e.MaHocPhan == yeuCauSuaDiem.MaHocPhan && e.MaSinhVien == yeuCauSuaDiem.MaSinhVien) // Specify your condition here
                .FirstOrDefaultAsync();
                if (selectedDiem != null)
                {
                    if(yeuCauSuaDiem.MaSinhVien == selectedDiem.MaSinhVien && yeuCauSuaDiem.MaHocPhan == selectedDiem.MaHocPhan)
                    {
                        yeuCauSuaDiem.DiemChuyenCan = selectedDiem.DiemChuyenCan;
                        yeuCauSuaDiem.DiemKiemTra = selectedDiem.DiemKiemTra;
                    }
                }
                _context.Add(yeuCauSuaDiem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiemChuyenCan"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemChuyenCan", yeuCauSuaDiem.DiemChuyenCan);
            ViewData["DiemKiemTra"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemKiemTra", yeuCauSuaDiem.DiemChuyenCan);
            ViewData["MaGiangVien"] = new SelectList(_context.GiangVien, "MaGiangVien", "TenGiangVien", yeuCauSuaDiem.MaGiangVien);
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", yeuCauSuaDiem.MaHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien", yeuCauSuaDiem.MaSinhVien);
            return View(yeuCauSuaDiem);
        }

        // GET: YeuCauSuaDiem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.YeuCauSuaDiem == null)
            {
                return NotFound();
            }

            var yeuCauSuaDiem = await _context.YeuCauSuaDiem.FindAsync(id);
            if (yeuCauSuaDiem == null)
            {
                return NotFound();
            }
            ViewData["DiemChuyenCan"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemChuyenCan", yeuCauSuaDiem.DiemChuyenCan);
            ViewData["MaGiangVien"] = new SelectList(_context.GiangVien, "MaGiangVien", "TenGiangVien", yeuCauSuaDiem.MaGiangVien);
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", yeuCauSuaDiem.MaHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien", yeuCauSuaDiem.MaSinhVien);
            return View(yeuCauSuaDiem);
        }

        // POST: YeuCauSuaDiem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaYeuCauSuaDiem,MaGiangVien,MaSinhVien,MaHocPhan,DiemChuyenCan,DiemKiemTra,DiemChuyenCanMoi,DiemKiemTraMoi,LyDo,TrangThai")] YeuCauSuaDiem yeuCauSuaDiem)
        {
            if (id != yeuCauSuaDiem.MaYeuCauSuaDiem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yeuCauSuaDiem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YeuCauSuaDiemExists(yeuCauSuaDiem.MaYeuCauSuaDiem))
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
            ViewData["DiemChuyenCan"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemChuyenCan", yeuCauSuaDiem.DiemChuyenCan);
            ViewData["MaGiangVien"] = new SelectList(_context.GiangVien, "MaGiangVien", "TenGiangVien", yeuCauSuaDiem.MaGiangVien);
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", yeuCauSuaDiem.MaHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien", yeuCauSuaDiem.MaSinhVien);
            return View(yeuCauSuaDiem);
        }

        // GET: YeuCauSuaDiem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.YeuCauSuaDiem == null)
            {
                return NotFound();
            }

            var yeuCauSuaDiem = await _context.YeuCauSuaDiem
                .Include(y => y.BangDiem)
                .Include(y => y.GiangVien)
                .Include(y => y.HocPhan)
                .Include(y => y.SinhVien)
                .FirstOrDefaultAsync(m => m.MaYeuCauSuaDiem == id);
            if (yeuCauSuaDiem == null)
            {
                return NotFound();
            }

            return View(yeuCauSuaDiem);
        }

        // POST: YeuCauSuaDiem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.YeuCauSuaDiem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.YeuCauSuaDiem'  is null.");
            }
            var yeuCauSuaDiem = await _context.YeuCauSuaDiem.FindAsync(id);
            if (yeuCauSuaDiem != null)
            {
                _context.YeuCauSuaDiem.Remove(yeuCauSuaDiem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YeuCauSuaDiemExists(int id)
        {
          return (_context.YeuCauSuaDiem?.Any(e => e.MaYeuCauSuaDiem == id)).GetValueOrDefault();
        }
    }
}
