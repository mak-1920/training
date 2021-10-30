using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Social.Data;
using Social.Entity;

namespace Social.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        public int SisCount => _context.Messages.Where(e => e.Sis).Count();
        public int BroCount => _context.Messages.Count() - SisCount;
        public Message LastMessage => _context.Messages.OrderBy(e => e.MessageId).LastOrDefault();

        public async Task<IActionResult> OnPostAsync(string sis)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("~/");
            var messageInfo = new Message
            {
                DateTime = DateTime.Now,
                UserName = User.Identity.Name
            };
            messageInfo.Bro = sis == null;
            messageInfo.Sis = !messageInfo.Bro;
            _context.Messages.Add(messageInfo);
            await _context.SaveChangesAsync();

            return LocalRedirect("~/");
        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }
    }
}
