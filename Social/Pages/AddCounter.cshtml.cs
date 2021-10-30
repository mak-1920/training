using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Social.Data;
using Social.Entity;

namespace Social.Pages
{
    public class AddCounterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddCounterModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(string sis)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("~/");
            if (sis == null)
                _context.Messages.Add(new Message
                {
                    DateTime = DateTime.Now,
                    Bro = true,
                    UserName = User.Identity.Name
                });
            else
                _context.Messages.Add(new Message
                {
                    DateTime = DateTime.Now,
                    Sis = true,
                    UserName = User.Identity.Name
                });
            await _context.SaveChangesAsync();
            return LocalRedirect("~/");
        }
    }
}
