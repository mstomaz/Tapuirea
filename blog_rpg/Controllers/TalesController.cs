using blog_rpg.Models;
using blog_rpg.Models.ViewModel;
using blog_rpg.Services;
using Microsoft.AspNetCore.Mvc;

namespace blog_rpg.Controllers
{
    public class TalesController : Controller
    {
        private readonly TaleService _taleService;
        private readonly UserService _userService;

        public TalesController(TaleService taleService, UserService userService)
        {
            _taleService = taleService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetAsync(1);
            var tales = await _taleService.GetAllAsync();

            var viewModel = new TalesViewModel
            {
                Tales = tales,
                User = user
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            return null;
        }

        [Route("/Tales/Create", Name = "talecreation")]
        public IActionResult Create()
        {
            //ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Tales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuthorId,Title,Story,PostDate,EditDate")] Tale tale)
        {
            return null;
        }

        // GET: Tales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return null;
        }

        // POST: Tales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorId,Title,Story,PostDate,EditDate")] Tale tale)
        {
            return null;
        }

        // GET: Tales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return null;
        }

        // POST: Tales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return null;
        }

        /*
        private bool TaleExists(int id)
        {
            return _taleService.GetAll().Any(x => x.Id == id);
        }
        */
    }
}
