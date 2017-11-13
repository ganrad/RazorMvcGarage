using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RazorMvcGarage.Data;
using RazorMvcGarage.Models;

namespace RazorMvcGarage.Controllers
{
    public class RepairsController : Controller {
        private readonly RepairContext _context;
        private ILogger _logger;

        public RepairsController(RepairContext ctx, ILoggerFactory logfactory) {
            _context = ctx;
            _logger = logfactory.CreateLogger("RazorMvcGarage.Controllers.RepairsController");
        }
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            _logger.LogDebug(1,"Executing Index() method....");
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            };
            ViewData["CurrentFilter"] = searchString;

            var repairs = from s in _context.Repair select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                repairs = repairs.Where(s => s.Vehicle.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    repairs = repairs.OrderByDescending(s => s.Vehicle);
                    break;
                case "Date":
                    repairs = repairs.OrderBy(s => s.RepairDate);
                    break;
                case "date_desc":
                    repairs = repairs.OrderByDescending(s => s.RepairDate);
                    break;
                default:
                    repairs = repairs.OrderBy(s => s.Vehicle);
                    break;
            }
            // return View(await repairs.AsNoTracking().ToListAsync());
            int pageSize = 12;
            return View(await PaginatedList<Repair>.CreateAsync(repairs.AsNoTracking(), page ?? 1, pageSize));
        }
    }
}