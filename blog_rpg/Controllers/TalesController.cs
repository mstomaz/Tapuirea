using blog_rpg.Data;
using blog_rpg.Models;
using blog_rpg.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace blog_rpg.Controllers
{
    public class TalesController : Controller
    {
        private readonly BlogContext _context;

        public TalesController(BlogContext context)
        {
            _context = context;
        }

        // GET: Tales
        public IActionResult Index()
        {
            var user = _context.Users.FirstOrDefault()!;
            var tales = _context.Tales.ToList();

            var viewModel = new TalesViewModel
            {
                Tales = tales.Count > 0 ? tales : [],
                User = user
            };
            return View(viewModel);
        }

        // GET: Tales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tale = await _context.Tales
                .Include(t => t.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tale == null)
            {
                return NotFound();
            }

            return View(tale);
        }

        // GET: Tales/Create
        [Route("/Tales/Create", Name = "talecreation")]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Tales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuthorId,Title,Story,PostDate,EditDate")] Tale tale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", tale.AuthorId);
            return View(tale);
        }

        // GET: Tales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tale = await _context.Tales.FindAsync(id);
            if (tale == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", tale.AuthorId);
            return View(tale);
        }

        // POST: Tales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorId,Title,Story,PostDate,EditDate")] Tale tale)
        {
            if (id != tale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaleExists(tale.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id", tale.AuthorId);
            return View(tale);
        }

        // GET: Tales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tale = await _context.Tales
                .Include(t => t.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tale == null)
            {
                return NotFound();
            }

            return View(tale);
        }

        // POST: Tales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tale = await _context.Tales.FindAsync(id);
            if (tale != null)
            {
                _context.Tales.Remove(tale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaleExists(int id)
        {
            return _context.Tales.Any(e => e.Id == id);
        }
    }
}
