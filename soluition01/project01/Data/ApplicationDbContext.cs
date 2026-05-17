using Microsoft.EntityFrameworkCore;
using project01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01.Data
{
    // DbContext مسؤول عن الاتصال بقاعدة البيانات
    public class ApplicationDbContext : DbContext
    {
        // كل DbSet يمثل Table داخل Database

        public DbSet<User> Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Watchlist> Watchlists { get; set; }

        public DbSet<Review> Reviews { get; set; }

        // إعداد الاتصال بقاعدة البيانات
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=MovieStreamingDB;Trusted_Connection=True;"
            );
        }

        // هنا نعرف العلاقات الخاصة بالجداول
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite Key
            // لأن جدول Watchlist يحتوي مفتاحين مع بعض
            modelBuilder.Entity<Watchlist>()
                .HasKey(w => new { w.UserId, w.MovieId });

            // العلاقة بين User و Watchlist
            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Watchlists)
                .HasForeignKey(w => w.UserId);

            // العلاقة بين Movie و Watchlist
            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.Movie)
                .WithMany(m => m.Watchlists)
                .HasForeignKey(w => w.MovieId);
        }
    }
}
