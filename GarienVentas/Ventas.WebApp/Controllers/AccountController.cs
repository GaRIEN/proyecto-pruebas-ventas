using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;


namespace Ventas.WebApp.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
            
        }

        [HttpGet]
        [Route("/")] // Esto hace que sea la página principal al abrir el sitio
        [Route("Login")]
        public IActionResult Login()
        {
            // Si el usuario ya está logueado, lo mandamos al Home
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    //if (!ModelState.IsValid) return View(model);

        //    //try
        //    //{
        //    //    // A. Llamar a tu API para obtener el Token
        //    //    // Asumo que tu ApiClient devuelve un string (el token) o un objeto con el token
        //    //    string tokenJwt = await _authService.LoginAsync(model.Usuario, model.Password);

        //    //    if (string.IsNullOrEmpty(tokenJwt))
        //    //    {
        //    //        ViewBag.Error = "Credenciales incorrectas";
        //    //        return View(model);
        //    //    }

        //    //    // B. Decodificar el token para leer los datos (Claims)
        //    //    var handler = new JwtSecurityTokenHandler();
        //    //    var jwtSecurityToken = handler.ReadJwtToken(tokenJwt);

        //    //    // C. Crear la identidad del usuario para la WebApp
        //    //    var claims = new List<Claim>(jwtSecurityToken.Claims);

        //    //    // Importante: Guardar el token en los claims para usarlo en futuras llamadas a la API
        //    //    claims.Add(new Claim("access_token", tokenJwt));

        //    //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    //    var authProperties = new AuthenticationProperties
        //    //    {
        //    //        IsPersistent = true, // Mantener sesión abierta
        //    //        ExpiresUtc = jwtSecurityToken.ValidTo // Sincronizar expiración con el token
        //    //    };

        //    //    // D. Iniciar sesión en la WebApp (crea la cookie de autenticación)
        //    //    await HttpContext.SignInAsync(
        //    //        CookieAuthenticationDefaults.AuthenticationScheme,
        //    //        new ClaimsPrincipal(claimsIdentity),
        //    //        authProperties);

        //    //    return RedirectToAction("Index", "Home");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    ViewBag.Error = "Error al intentar iniciar sesión: " + ex.Message;
        //    //    return View(model);
        //    //}
        //}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }
}
