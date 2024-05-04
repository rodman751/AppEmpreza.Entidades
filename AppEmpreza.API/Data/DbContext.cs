using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppEmpreza.Entidades;

    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext (DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public DbSet<Cargo> Cargo { get; set; } = default!;

        public DbSet<Departamento> Departamento { get; set; } = default!;

        public DbSet<Empleados> Empleados { get; set; } = default!;
    }
