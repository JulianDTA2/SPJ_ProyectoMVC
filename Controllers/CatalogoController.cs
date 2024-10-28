using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPJ_ProyectoMVC.Data;
using SPJ_ProyectoMVC.Models;

namespace SPJ_ProyectoMVC.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly SPJ_ProyectoMVCContext _context;

        public CatalogoController(SPJ_ProyectoMVCContext context)
        {
            _context = context;
        }

        // GET: Catalogo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Catalogo.ToListAsync());
        }

        // GET: Catalogo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogo
                .FirstOrDefaultAsync(m => m.CatalogoId == id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return View(catalogo);
        }

        // GET: Catalogo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catalogo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogoId,Marca,Modelo,Usado,Precio,IVA")] Catalogo catalogo, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    // Verifica y crea la carpeta wwwroot/images si no existe
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    // Genera un nombre único para el archivo
                    var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
                    var filePath = Path.Combine(imagePath, fileName);

                    // Guarda el archivo en el sistema
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Asigna la ruta de la imagen al campo ImagePath
                    catalogo.ImagePath = "/images/" + fileName;
                }

                // Agrega el catálogo a la base de datos y guarda los cambios
                _context.Add(catalogo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalogo);
        }




        // GET: Catalogo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogo.FindAsync(id);
            if (catalogo == null)
            {
                return NotFound();
            }
            return View(catalogo);
        }

        // POST: Catalogo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatalogoId,Marca,Modelo,Usado,Precio,IVA")] Catalogo catalogo, IFormFile image)
        {
            if (id != catalogo.CatalogoId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Captura y muestra los errores en consola
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                // Si hay errores, retorna la vista con el catálogo actual para mostrar validaciones en la UI
                return View(catalogo);
            }

            try
            {
                var existingCatalogo = await _context.Catalogo.FindAsync(id);
                if (existingCatalogo == null)
                {
                    return NotFound();
                }

                existingCatalogo.Marca = catalogo.Marca;
                existingCatalogo.Modelo = catalogo.Modelo;
                existingCatalogo.Usado = catalogo.Usado;
                existingCatalogo.Precio = catalogo.Precio;
                existingCatalogo.IVA = catalogo.IVA;

                if (image != null && image.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingCatalogo.ImagePath))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCatalogo.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    existingCatalogo.ImagePath = "/images/" + fileName;
                }

                _context.Update(existingCatalogo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoExists(catalogo.CatalogoId))
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

        private bool CatalogoExists(int catalogoId)
        {
            throw new NotImplementedException();
        }

        // GET: Catalogo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogo
                .FirstOrDefaultAsync(m => m.CatalogoId == id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return View(catalogo);
        }

        // POST: Catalogo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogo = await _context.Catalogo.FindAsync(id);
            if (catalogo != null)
            {
                if (!string.IsNullOrEmpty(catalogo.ImagePath))
                {
                    var filePath = Path.Combine("wwwroot", catalogo.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Catalogo.Remove(catalogo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
