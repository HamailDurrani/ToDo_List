using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoDemo.Models;
using ToDos.Data;
namespace ToDoDemo.Controllers
{
    public class HomeController(ToDoContext _context) : Controller
    {
        private readonly ToDoContext context = _context;

        public IActionResult Index(string id, int page = 1)
        {
            var filters = new Filters(id);

            ViewBag.Filters = filters;

            ViewBag.Categories = context.Categories.ToList();

            ViewBag.Statuses = context.Statuses.ToList();

            ViewBag.DueFilters = Filters.DueFilterValues;

            int pageSize = 5;
            int totalDue = 5;
            int totalCompleted = 5;

            //ApplyFilters

            IQueryable<ToDo> query = context.ToDos.Include(t => t.Category).Include(t => t.Status);

            if (filters.HasCategory)
            {
                query = query.Where(t => t.CategoryId == filters.CategoryId);
            }
            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }
            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                {
                    query = query.Where(t => t.DueDate < today);
                }
                else if (filters.IsFuture)
                {

                    query = query.Where(t => t.DueDate > today);
                }
                else if (filters.IsToday)
                {

                    query = query.Where(t => t.DueDate == today);
                }
            }

            //Add Pages
            var totalItems = query.Count();
            //var totalDues = (int)Math.Ceiling((double)totalItems / totalDue);
            //var totalCompletes = (int)Math.Ceiling((double)totalItems / totalCompleted);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            var tasks = query.OrderBy(t => t.DueDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = context.Categories.ToList();

            ViewBag.Statuses = context.Statuses.ToList();

            var task = new ToDo { StatusId = "open" };
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = context.Categories.ToList();
                ViewBag.Statuses = context.Statuses.ToList();
                return View(task);
            }

        }
        //filters working
        [HttpPost]
        public IActionResult ApplyFilters(string categoryId, string due, string statusId)
        {
            string id = $"{categoryId}-{due}-{statusId}";
            return RedirectToAction("Index", new { id });
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
        {
            selected = context.ToDos.Find(selected.Id)!;

            if (selected != null)
            {
                selected.StatusId = "closed";
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { ID = id });
        }
        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            var toDelete = context.ToDos.Where(t => t.StatusId == "closed").ToList();

            context.ToDos.RemoveRange(toDelete);

            context.SaveChanges();
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = context.ToDos.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();

            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(ToDo task)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Update(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            return View(task);
        }
    }
}
