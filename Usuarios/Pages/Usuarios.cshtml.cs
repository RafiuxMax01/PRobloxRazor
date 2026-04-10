using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usuarios.Modelos;

namespace Usuarios.Pages
{
    // Modelo de la página Razor para gestionar el CRUD de Desarrolladores
    public class UsuariosModel : PageModel
    {
        // Variable privada de solo lectura para manejar el contexto de la base de datos
        private readonly DBContext _context;

        // Lista que almacenará todos los registros para mostrarlos en la tabla HTML (Consulta)
        public List<Desarrollador> Desarrolladores { get; set; } = new List<Desarrollador>();

        // [BindProperty] conecta automáticamente los datos del formulario HTML con este objeto en C#
        [BindProperty]
        public Desarrollador NuevoDesarrollador { get; set; } = new Desarrollador();

        // Constructor de la clase: Recibe el contexto de la base de datos mediante Inyección de Dependencias
        public UsuariosModel(DBContext context)
        {
            _context = context;
        }

        // Método OnGet: Se ejecuta cuando la página carga o se hace una petición GET
        public void OnGet(int? editarId)
        {
            // Trae todos los desarrolladores de la tabla en SQL Server y los asigna a la lista
            Desarrolladores = _context.Desarrolladores.ToList();

            // Si recibe un ID por la URL (cuando el usuario da clic a "Editar"), busca ese registro
            if (editarId.HasValue)
            {
                var dev = _context.Desarrolladores.Find(editarId.Value);
                if (dev != null)
                {
                    // Si lo encuentra, llena el objeto con los datos de ese desarrollador para mostrarlos en el formulario
                    NuevoDesarrollador = dev;
                }
            }
        }

        // Método OnPost: Se ejecuta cuando enviamos el formulario (Botón "Guardar")
        public IActionResult OnPost()
        {
            // Si el ID es 0, significa que es un usuario nuevo (Operación de Alta)
            if (NuevoDesarrollador.Id == 0)
            {
                _context.Desarrolladores.Add(NuevoDesarrollador);
            }
            else
            {
                // Operación de Actualización (Update)
                // Verificamos por seguridad si el ID realmente existe en la base de datos
                bool existe = _context.Desarrolladores.Any(d => d.Id == NuevoDesarrollador.Id);

                if (existe)
                {
                    // Si existe, actualizamos sus datos de forma segura
                    _context.Desarrolladores.Update(NuevoDesarrollador);
                }
                else
                {
                    // Si el registro no existe (evitamos el crash), lo tratamos como un registro nuevo
                    NuevoDesarrollador.Id = 0;
                    _context.Desarrolladores.Add(NuevoDesarrollador);
                }
            }

            // Guardamos los cambios físicos en la base de datos SQL Server
            _context.SaveChanges();

            // Recargamos la página para limpiar el formulario y actualizar la tabla visualmente
            return RedirectToPage();
        }

        // Método OnPostEliminar: Se ejecuta cuando presionamos el botón rojo de "Eliminar" (Operación de Baja)
        public IActionResult OnPostEliminar()
        {
            // Nos aseguramos de que el ID a eliminar no esté vacío
            if (NuevoDesarrollador.Id != 0)
            {
                // Buscamos el registro exacto en la base de datos usando el ID
                var devParaEliminar = _context.Desarrolladores.Find(NuevoDesarrollador.Id);

                if (devParaEliminar != null)
                {
                    // Removemos el desarrollador de la tabla y guardamos los cambios
                    _context.Desarrolladores.Remove(devParaEliminar);
                    _context.SaveChanges();
                }
            }

            // Recargamos la página para mostrar la tabla actualizada
            return RedirectToPage();
        }
    }
}