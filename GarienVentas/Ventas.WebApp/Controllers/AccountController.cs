using ApiClients.Models.Commands.Usuario;
using ApiClients.Ventas;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Ventas.WebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly ILogger<AccountController> _logger; // <-- Agrégalo aquí
        private readonly IUsuariosApiClient _sessionService;
        public AccountController(IUsuariosApiClient sessionService, ILogger<AccountController> logger)
        {
            _sessionService = sessionService;
            _logger = logger;
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


        [HttpPost]
        public async Task<IActionResult> GetLoginUser([FromBody] UsuarioCommand item)
        {
            try
            {
                // 1. Llamamos a tu servicio que conecta con la API de Ventas
                var response = await _sessionService.GetLoginUser(item);

                if (!response.IsSuccess)
                {
                    _logger.LogError("Login fallido: {Error}", response.Error);
                    return BadRequest(new { message = response.Error ?? "Credenciales inválidas" });
                }

                // --- INICIO DE LA FIRMA DE SESIÓN ---

                // 2. Extraemos el JWT que nos devolvió la API
                var usuarioData = response.Data;
                if (usuarioData == null)
                {
                    return BadRequest("No se recibió información del usuario.");
                }
                string jwtToken = usuarioData.Token;

                // 3. Creamos la lista de "Claims" (información del usuario para la WebApp)
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, item.NombreUsuario),
            new Claim("JWToken", jwtToken) // Guardamos el token aquí para usarlo en futuras llamadas a la API
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // 4. Configuramos las propiedades de la sesión (Seguridad y Tiempo)
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Permite que la sesión persista según el tiempo configurado
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60), // Coincide con tu Program.cs
                    AllowRefresh = true // Permite Sliding Expiration
                };

                // 5. ¡PASO CLAVE! Creamos la Cookie de autenticación encriptada
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // 6. Respondemos al JS que todo salió bien
                return Ok(new { isSuccess = true, message = "Bienvenido al sistema" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción en login para usuario: {Usuario}", item.NombreUsuario);
                return StatusCode(500, "Error interno del servidor");
            }
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
