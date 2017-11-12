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
    public class IndexModel : PageModel
    {
        private readonly RazorMvcGarage.Models.RepairContext _context;

        public IndexModel(RazorMvcGarage.Models.RepairContext context)
        {
            _context = context;
        }

        public IList<Repair> Repair { get;set; }

        public async Task OnGetAsync()
        {
            Repair = await _context.Repair.ToListAsync();
        }
    }
}
