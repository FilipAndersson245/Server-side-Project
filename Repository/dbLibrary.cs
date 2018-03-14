namespace Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbLibrary : DbContext
    {
        public dbLibrary()
            : base("name=dbLibrary")
        {
        }

        public virtual DbSet<ADMIN> ADMINs { get; set; }
        public virtual DbSet<AUTHOR> AUTHORs { get; set; }
        public virtual DbSet<BOOK> BOOKs { get; set; }
        public virtual DbSet<CLASSIFICATION> CLASSIFICATIONs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ADMIN>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .Property(e => e.Salt)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<AUTHOR>()
                .HasMany(e => e.BOOKs)
                .WithMany(e => e.AUTHORs)
                .Map(m => m.ToTable("BOOK_AUTHOR").MapLeftKey("Aid").MapRightKey("ISBN"));
        }
    }
}
