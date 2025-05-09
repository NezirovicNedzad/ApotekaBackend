﻿using ApotekaBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ApotekaBackend.Data
{
    public class DataContext(DbContextOptions options) :IdentityDbContext<AppUser,AppRole,int,
        IdentityUserClaim<int>,AppUserRole,
        IdentityUserLogin<int>,IdentityRoleClaim<int>,
        IdentityUserToken<int>>(options)
    {

        public DbSet<Lek> Lekovi { get; set; }  
        public DbSet<Klijent> Klijenti { get; set; }    

        public DbSet<Recept> Recepti { get; set; }  


        public DbSet<TransakcijaDetalji> TransakcijaDetalji { get; set; }
        public DbSet<Transkacija> Transakcije{ get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().HasMany(appUser => appUser.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            builder.Entity<AppRole>().HasMany(appUser => appUser.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
            .IsRequired();

            builder.Entity<Lek>()
                .HasOne(l => l.Farmaceut)
                .WithMany(u => u.Lekovi)
                .HasForeignKey(l => l.IdFarmaceuta);
              

            builder.Entity<Klijent>()
                .HasOne(l => l.Apotekar)
                .WithMany(a => a.Klijenti)
                .HasForeignKey(s => s.IdApotekara);

            builder.Entity<Recept>()
                .HasOne(r => r.Farmaceut)
                .WithMany(r => r.Recepti)
                .HasForeignKey(s => s.IdFarmaceuta)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Recept>()
               .HasOne(k => k.Klijent)
               .WithMany(r =>r.Recepti)
               .HasForeignKey(s=>s.IdKlijenta)
              .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Recept>()
               .HasOne(r => r.Lek)
               .WithMany(l=>l.Recepti)
               .HasForeignKey(s => s.IdLeka).OnDelete(DeleteBehavior.Restrict);



            builder.Entity<TransakcijaDetalji>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Lek)
                      .WithMany(l => l.ProdajaDetalji)
                      .HasForeignKey(e => e.IdLeka)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Transkacija)
                      .WithMany(p => p.ProdajaDetalji)
                      .HasForeignKey(e => e.IdTransakcije)
                      .OnDelete(DeleteBehavior.Cascade);

               

                entity.HasOne(e => e.Recept)
                      .WithMany(r => r.ProdajaDetalji)
                      .HasForeignKey(e => e.ReceptId)
                      .OnDelete(DeleteBehavior.Restrict);

            });
       

            builder.Entity<Transkacija>().
                HasOne(t => t.Klijent).
                WithMany(t => t.Transakcije).
                HasForeignKey(k => k.KlijentId);          


        }


    }
}
