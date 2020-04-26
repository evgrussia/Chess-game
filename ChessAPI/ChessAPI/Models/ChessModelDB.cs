namespace ChessAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    public partial class ChessModelDB : DbContext
    {
        public ChessModelDB()
            : base("name=ChessModelDB")
        {
        }
        public virtual DbSet<Game> Games { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .Property(e => e.FEN)
                .IsUnicode(false);
            modelBuilder.Entity<Game>()
                .Property(e => e.Status)
                .IsUnicode(false);
        }
    }
}
