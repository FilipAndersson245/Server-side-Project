namespace Repository
{
    using System.Data.Entity;

    public partial class dbGrupp3 : DbContext
    {
        public dbGrupp3()
            : base("name=dbGroup3")
        {
        }

        public virtual DbSet<ADMIN> ADMINS { get; set; }
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