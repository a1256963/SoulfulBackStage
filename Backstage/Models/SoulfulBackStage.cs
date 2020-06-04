namespace Backstage.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SoulfulBackStage : DbContext
    {
        public SoulfulBackStage()
            : base("name=SoulfulBackStage")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Like> Like { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Singer> Singer { get; set; }
        public virtual DbSet<Song> Song { get; set; }
        public virtual DbSet<Style> Style { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.AspNetUsers_Id);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.AspNetUsers_Id);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Like)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.AspNetUsers_Id);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Order)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.AspNetUsers_Id);
        }
    }
}
