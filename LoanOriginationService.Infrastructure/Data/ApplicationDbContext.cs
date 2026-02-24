using LoanOriginationService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<LoanDeals> LoanDeals { get; set; }

        public DbSet<LoanTypes> LoanTypes { get; set; }

        public DbSet<DealReview> DealReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoanDeals>()
               .HasOne(ld => ld.LoanType)
               .WithMany()
               .HasForeignKey(ld => ld.loanTypeId)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<DealReview>()
                .HasOne(dr => dr.LoanDeals)
                .WithMany()
                .HasForeignKey(dr => dr.dealId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoanDeals>()
               .Property(p => p.approvedAmount)
               .HasPrecision(18, 2);

            modelBuilder.Entity<LoanDeals>()
                .Property(p => p.eligibleAmount)
                .HasPrecision(18, 2);


        }
    }
}
