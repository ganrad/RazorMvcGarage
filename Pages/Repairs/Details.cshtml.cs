using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorMvcGarage.Models;

namespace RazorMvcGarage.Pages.Repairs
{
    public class DetailsModel : PageModel
    {
        private readonly RazorMvcGarage.Models.RepairContext _context;

        public DetailsModel(RazorMvcGarage.Models.RepairContext context)
        {
            _context = context;
        }

        public Repair Repair { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Repair = await _context.Repair.SingleOrDefaultAsync(m => m.ID == id);

            if (Repair == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
