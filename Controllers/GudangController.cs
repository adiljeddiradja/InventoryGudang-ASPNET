using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Data;
using System.Threading.Tasks;
using inventoryApp.Models;

namespace InventoryApp.Controllers
{
    public class GudangController : Controller
    {
        private readonly AppDbContext _context;

        public GudangController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Gudangs.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gudang = await _context.Gudangs
                .FirstOrDefaultAsync(m => m.KodeGudang == id);
            if (gudang == null)
            {
                return NotFound();
            }

            return View(gudang);
        }

    
        public IActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KodeGudang,NamaGudang")] Gudang gudang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gudang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gudang);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gudang = await _context.Gudangs.FindAsync(id);
            if (gudang == null)
            {
                return NotFound();
            }
            return View(gudang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KodeGudang,NamaGudang")] Gudang gudang)
        {
            if (id != gudang.KodeGudang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gudang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GudangExists(gudang.KodeGudang))
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
            return View(gudang);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gudang = await _context.Gudangs
                .FirstOrDefaultAsync(m => m.KodeGudang == id);
            if (gudang == null)
            {
                return NotFound();
            }

            return View(gudang);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gudang = await _context.Gudangs.FindAsync(id);
            _context.Gudangs.Remove(gudang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GudangExists(int id)
        {
            return _context.Gudangs.Any(e => e.KodeGudang == id);
        }
    }
}
