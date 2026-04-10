using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usuarios.Modelos;

namespace Usuarios.Pages
{
    public class UsuariosModel : PageModel
    {
        private readonly DBContext _context;

        public List<Desarrollador> Desarrolladores { get; set; } = new List<Desarrollador>();

        [BindProperty]
        public Desarrollador NuevoDesarrollador { get; set; }

        public UsuariosModel(DBContext context)
        {
            _context = context;
        }

        // Modificado para recibir un ID si queremos editar
        public void OnGet(int? editarId)
        {
            Desarrolladores = _context.Desarrolladores.ToList();

            if (editarId.HasValue)
            {
                NuevoDesarrollador = _context.Desarrolladores.Find(editarId.Value);
            }
        }

        public IActionResult OnPost()
        {
            if (NuevoDesarrollador.Id == 0)
            {
                // Si el ID es 0, es un Alta nueva
                _context.Desarrolladores.Add(NuevoDesarrollador);
            }
            else
            {
                // Si ya tiene ID, es una Actualización
                _context.Desarrolladores.Update(NuevoDesarrollador);
            }

            _context.SaveChanges();
            return RedirectToPage();
        }
        public IActionResult OnPostEliminar()
        {
            if (NuevoDesarrollador.Id != 0)
            {
                // Busca al desarrollador por su ID
                var devParaEliminar = _context.Desarrolladores.Find(NuevoDesarrollador.Id);

                if (devParaEliminar != null)
                {
                    // Lo elimina de la base de datos
                    _context.Desarrolladores.Remove(devParaEliminar);
                    _context.SaveChanges();
                }
            }
            return RedirectToPage();
        }
    }
}