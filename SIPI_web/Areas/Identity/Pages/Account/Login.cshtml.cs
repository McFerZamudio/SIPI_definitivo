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
using SIPI_web.Servicios;
using SIPI_web.Interface;
using SIPI_web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SIPI_web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SIPI_dbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager, SIPI_dbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Nombre de Usuario")]
            public string userName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Clave")]
            public string Password { get; set; }

            [Display(Name = "Recoradarme en el portal?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.userName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    var idUser = _context.AspNetUsers
                    .Include(x => x.AspNetUserRoles)
                    .Where(x => x.UserName.Equals(Input.userName)).FirstOrDefault();

                    asignaRolEstudiante(idUser.Id);

                    HttpContext.Session.SetString("idUser", idUser.Id);
                    HttpContext.Session.SetString("userName", Input.userName);

                    // *** Validacion Universal para todos los usuarios HUMANOS o NO HUMANOS *** //
                    usuarioServices _usuario = new();
                    var _existeUsuario = await ((Iusuario)_usuario).existeUsuario(idUser.Id, _context);
                    if (_existeUsuario == false)
                    {
                        return RedirectToAction("create", "Usuario");
                    }

                    // *** TODO: Se debe validar si es HUMANO y entre aqui *** //
                    personaServices _persona = new();
                    var _existePersona = await ((Ipersona)_persona).existePersona(idUser.Id, _context);
                    if (_existePersona == false)
                    {
                        return RedirectToAction("create", "persona");
                    }

                    if (personaServices.esEstudiante(idUser.Id, _context) == true)
                    {
                        estudianteServices _estudiante = new();
                        var _existeEstudiante = await ((Iestudiante)_estudiante).existeEstudiante(idUser.Id, _context);
                        if (_existeEstudiante == false)
                        {
                            return RedirectToAction("create", "estudiante");
                        }
                    }



                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private void asignaRolEstudiante(string _idUser)
        {

            var _validaRoles = _context.AspNetUserRoles.Any(x => x.UserId.Equals(_idUser));

            if (_validaRoles == false)
            {
                AspNetUserRole agregaRole = new();

                agregaRole.RoleId = "15b55cf3-dd3f-4a43-8647-39ce15986988";
                agregaRole.UserId = _idUser;

                _context.Add(agregaRole);
                _context.SaveChanges();

            }


        }
    }
}
