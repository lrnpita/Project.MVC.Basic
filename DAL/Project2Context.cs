namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using DTO;

    public partial class Project2Context : DbContext
    {
        public Project2Context()
            : base("name=Project2Context")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
