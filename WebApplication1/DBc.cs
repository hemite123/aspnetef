
namespace WebApplication1
{
    using System;
    using System.Data.Entity;
    using Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    public class DBc : DbContext
    {

        public DBc() 
            : base(@"Data Source = .\SQLEXPRESS ; user id = admin; password = admin123;initial catalog = userDB")
        { 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DBc>(null);
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<UserAccount> Users { get; set; }
        public virtual DbSet<Layar> Layar { get; set; }
        public virtual DbSet<Kantor> Kantor { get; set; }
        public virtual DbSet<RoleData> RoleData { get; set; }

    }
}
