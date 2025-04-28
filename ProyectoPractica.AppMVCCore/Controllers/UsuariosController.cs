using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoPractica.AppMVCCore.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Text;

namespace ProyectoPractica.AppMVCCore.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ProyectoPracticaContext _context;

        public UsuariosController(ProyectoPracticaContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Index(Usuario usuario, int topRegistro = 10)
        {
            var query = _context.Usuarios.AsQueryable();
            if (!string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                query = query.Where(s => s.NombreUsuario.Contains(usuario.NombreUsuario));

            if (!string.IsNullOrWhiteSpace(usuario.Email))
                query = query.Where(s => s.Email.Contains(usuario.Email));
            if (topRegistro > 0)
                query = query.Take(topRegistro);
            return View(await query.ToListAsync());
         
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        [Authorize(Roles = "ADMINISTRADOR")]
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "ADMINISTRADOR")]
        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreUsuario,Contrasena,Email,Rol,FechaRegistro")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario.Contrasena = CalcularHashMD5(usuario.Contrasena);
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


                return View(usuario);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(usuario);
            }

        }
        [Authorize(Roles = "ADMINISTRADOR")]
        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreUsuario,Contrasena,Email,Rol,FechaRegistro")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioData = await _context.Usuarios.FirstOrDefaultAsync(s => s.Id == usuario.Id);
                    if (usuarioData != null)
                    {
                        usuarioData.Email = usuario.Email;
                        usuarioData.NombreUsuario = usuario.NombreUsuario;
                        usuarioData.Rol = usuario.Rol;
                        _context.Update(usuarioData);
                        await _context.SaveChangesAsync();

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
        [Authorize(Roles = "ADMINISTRADOR,BIBLIOTECARIO")]
        public async Task<IActionResult> ChancePassword()
        {
            int idUser = int.Parse(User.FindFirst("Id").Value);
            var usuario = await _context.Usuarios.FindAsync(idUser);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMINISTRADOR,BIBLIOTECARIO")]
        public async Task<IActionResult> ChancePassword(int id, [Bind("Id,Password")] Usuario usuario, string passwordAnt)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            try
            {
                var passAnt = CalcularHashMD5(passwordAnt);
                var usuarioData = await _context.Usuarios.FirstOrDefaultAsync(s => s.Id == usuario.Id);
                if (usuarioData != null && usuarioData.Contrasena == passAnt)
                {
                    usuarioData.Contrasena = CalcularHashMD5(usuario.Contrasena);
                    _context.Update(usuarioData);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
                else
                {
                    throw new Exception("La contraseña actual es incorrecta");
                }
            }
            catch (Exception ex)
            {

                {
                    ModelState.AddModelError("", ex.Message);
                    return View(usuario);
                }

            }
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Contrasena")] Usuario usuario)
        {

            try
            {
                usuario.Contrasena = CalcularHashMD5(usuario.Contrasena);
                var usuarioAuth = await _context.Usuarios.FirstOrDefaultAsync(s => s.Email == usuario.Email && s.Contrasena == usuario.Contrasena);
                if (usuarioAuth != null && usuarioAuth.Id > 0)
                {
                    var claims = new[] {
                    new Claim(ClaimTypes.Name, usuarioAuth.NombreUsuario),
                    new Claim("Id", usuarioAuth.Id.ToString()),
                     new Claim("Email", usuarioAuth.Email),
                    new Claim(ClaimTypes.Role, usuarioAuth.Rol)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception("El email o contraseña son incorrectos");
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(usuario);
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> CerrarSession()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        private string CalcularHashMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // "x2" convierte el byte en una cadena hexadecimal de dos caracteres.
                }
                return sb.ToString();
            }
        }
    }
}