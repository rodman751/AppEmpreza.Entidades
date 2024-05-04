using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEmpreza.Entidades
{
    public class Empleados
    {
        [Key]
        public int Id { get; set; } //PK
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string? Direcion { get; set; }
        public DateTime? FechaNacimietno { get; set; }
        public double? Salario { get; set; }
        public double? Comisiono { get; set; }
     
        public int CargoId { get; set; }
        public Cargo? Cargos {  get; set; }
        public int DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }
    }
}
