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
    public class YeuCauPhucKhaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YeuCauPhucKhaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: YeuCauPhucKhao
        public async Task<IActionResult> Index(int? page, int? PageSize, string searchText)
        {
            int pageNumber = (page ?? 1); 
            int defaultPageSize = 5; 
            int actualPageSize = PageSize ?? defaultPageSize;
            var query =_context.YeuCauPhucKhao.Include(y => y.BangDiem).Include(y => y.HocPhan).Include(y => y.SinhVien).AsQueryable();
            
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(d => d.MaHocPhan.Contains(searchText) || d.MaSinhVien.Contains(searchText));
            }
            var YeuCauPhucKhao = await query.ToListAsync();
            if (actualPageSize == -1)
            {
                actualPageSize = YeuCauPhucKhao.Count;
            }

            var totalCount = YeuCauPhucKhao.Count;

            ViewBag.CurrentPage = pageNumber;
            ViewBag.pageSize = actualPageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SearchTerm = searchText;
            return View(YeuCauPhucKhao.ToPagedList(pageNumber, actualPageSize));
        }

        // GET: YeuCauPhucKhao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.YeuCauPhucKhao == null)
            {
                return NotFound();
            }

            var yeuCauPhucKhao = await _context.YeuCauPhucKhao
                .Include(y => y.BangDiem)
                .Include(y => y.HocPhan)
                .Include(y => y.SinhVien)
                .FirstOrDefaultAsync(m => m.MaYeuCauPhucKhao == id);
            if (yeuCauPhucKhao == null)
            {
                return NotFound();
            }

            return View(yeuCauPhucKhao);
        }

        // GET: YeuCauPhucKhao/Create
        public IActionResult Create()
        {
            ViewData["DiemThi"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemThi");
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan");
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien");
            return View();
        }

        // POST: YeuCauPhucKhao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaYeuCauPhucKhao,MaSinhVien,MaHocPhan,LyDo")] YeuCauPhucKhao yeuCauPhucKhao)
        {
            if (ModelState.IsValid)
            {
                var selectedDiemThi = await _context.BangDiem
                .Where(e => e.MaHocPhan == yeuCauPhucKhao.MaHocPhan && e.MaSinhVien == yeuCauPhucKhao.MaSinhVien) // Specify your condition here
                .FirstOrDefaultAsync();
                if (selectedDiemThi != null)
                {
                    if(yeuCauPhucKhao.MaSinhVien == selectedDiemThi.MaSinhVien && yeuCauPhucKhao.MaHocPhan == selectedDiemThi.MaHocPhan)
                    {
                        yeuCauPhucKhao.DiemThi = selectedDiemThi.DiemThi;
                    }
                }
                _context.Add(yeuCauPhucKhao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiemThi"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemThi", yeuCauPhucKhao.DiemThi);
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", yeuCauPhucKhao.MaHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien", yeuCauPhucKhao.MaSinhVien);
            return View(yeuCauPhucKhao);
        }

        // GET: YeuCauPhucKhao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.YeuCauPhucKhao == null)
            {
                return NotFound();
            }

            var yeuCauPhucKhao = await _context.YeuCauPhucKhao.FindAsync(id);
            if (yeuCauPhucKhao == null)
            {
                return NotFound();
            }
            ViewData["DiemThi"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemThi", yeuCauPhucKhao.DiemThi);
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", yeuCauPhucKhao.MaHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien", yeuCauPhucKhao.MaSinhVien);
            return View(yeuCauPhucKhao);
        }

        // POST: YeuCauPhucKhao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaYeuCauPhucKhao,MaSinhVien,MaHocPhan,DiemThi,LyDo,TrangThai")] YeuCauPhucKhao yeuCauPhucKhao)
        {
            if (id != yeuCauPhucKhao.MaYeuCauPhucKhao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yeuCauPhucKhao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YeuCauPhucKhaoExists(yeuCauPhucKhao.MaYeuCauPhucKhao))
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
            ViewData["DiemThi"] = new SelectList(_context.BangDiem, "MaBangDiem", "DiemThi", yeuCauPhucKhao.DiemThi);
            ViewData["MaHocPhan"] = new SelectList(_context.HocPhan, "MaHocPhan", "TenHocPhan", yeuCauPhucKhao.MaHocPhan);
            ViewData["MaSinhVien"] = new SelectList(_context.SinhVien, "MaSinhVien", "TenSinhVien", yeuCauPhucKhao.MaSinhVien);
            return View(yeuCauPhucKhao);
        }

        // GET: YeuCauPhucKhao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.YeuCauPhucKhao == null)
            {
                return NotFound();
            }

            var yeuCauPhucKhao = await _context.YeuCauPhucKhao
                .Include(y => y.BangDiem)
                .Include(y => y.HocPhan)
                .Include(y => y.SinhVien)
                .FirstOrDefaultAsync(m => m.MaYeuCauPhucKhao == id);
            if (yeuCauPhucKhao == null)
            {
                return NotFound();
            }

            return View(yeuCauPhucKhao);
        }

        // POST: YeuCauPhucKhao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.YeuCauPhucKhao == null)
            {
                return Problem("Entity set 'ApplicationDbContext.YeuCauPhucKhao'  is null.");
            }
            var yeuCauPhucKhao = await _context.YeuCauPhucKhao.FindAsync(id);
            if (yeuCauPhucKhao != null)
            {
                _context.YeuCauPhucKhao.Remove(yeuCauPhucKhao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YeuCauPhucKhaoExists(int id)
        {
          return (_context.YeuCauPhucKhao?.Any(e => e.MaYeuCauPhucKhao == id)).GetValueOrDefault();
        }
    }
}
