using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppEmpreza.Entidades
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; } //PK
        [DisplayName("Nombre del departamento")]
        public string Nombre { get; set;}
        public string Ciudad { get; set;}

        public List<Empleados>? Empleados { get; set; }

    }
}
