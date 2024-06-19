using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Data;
using System.Linq;
using System.Threading.Tasks;
using inventoryApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryApp.Controllers
{
    public class BarangController : Controller
    {
        private readonly AppDbContext _context;

        public BarangController(AppDbContext context)
        {
            _context = context;
        }

  
        public async Task<IActionResult> Index()
        {
            var barang = await _context.Barangs
                .Include(b => b.Gudang)
                .ToListAsync();
            return View(barang);
        }

 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barang = await _context.Barangs
                .Include(b => b.Gudang)
                .FirstOrDefaultAsync(m => m.KodeBarang == id);
            if (barang == null)
            {
                return NotFound();
            }

            return View(barang);
        }

        public IActionResult Create()
        {
            ViewData["KodeGudang"] = new SelectList(_context.Gudangs, "KodeGudang", "NamaGudang");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KodeBarang,NamaBarang,Harga,Jumlah,ExpiredDate,KodeGudang")] Barang barang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(barang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KodeGudang"] = new SelectList(_context.Gudangs, "KodeGudang", "NamaGudang", barang.KodeGudang);
            return View(barang);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barang = await _context.Barangs.FindAsync(id);
            if (barang == null)
            {
                return NotFound();
            }
            ViewData["KodeGudang"] = new SelectList(_context.Gudangs, "KodeGudang", "NamaGudang", barang.KodeGudang);
            return View(barang);
        }

        // POST: Barang/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KodeBarang,NamaBarang,Harga,Jumlah,ExpiredDate,KodeGudang")] Barang barang)
        {
            if (id != barang.KodeBarang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarangExists(barang.KodeBarang))
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
            ViewData["KodeGudang"] = new SelectList(_context.Gudangs, "KodeGudang", "NamaGudang", barang.KodeGudang);
            return View(barang);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barang = await _context.Barangs
                .Include(b => b.Gudang)
                .FirstOrDefaultAsync(m => m.KodeBarang == id);
            if (barang == null)
            {
                return NotFound();
            }

            return View(barang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barang = await _context.Barangs.FindAsync(id);
            _context.Barangs.Remove(barang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarangExists(int id)
        {
            return _context.Barangs.Any(e => e.KodeBarang == id);
        }
    }
}
