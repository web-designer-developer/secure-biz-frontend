using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SecurityServices.AwsServices;

namespace SecurityServices.Models
{
    public partial class serviceContext : DbContext
    {
        public serviceContext()
        {
        }

        public serviceContext(DbContextOptions<serviceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Producttransactionhistory> Producttransactionhistories { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Usercontact> Usercontacts { get; set; } = null!;
        public virtual DbSet<Usertoken> Usertokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseMySql("server=service-db.cfguwcyiea8b.us-east-2.rds.amazonaws.com;database=service;user=admin;password=2Lzj!1lO5L!7[aaIgz<m-*o$:-dH", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));
                optionsBuilder.UseMySql(Rds.GetConnectionString(), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.Code, "code_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .HasColumnName("id");

                entity.Property(e => e.Code)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("code");

                entity.Property(e => e.Isavailable)
                    .HasColumnName("isavailable")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Producttransactionhistory>(entity =>
            {
                entity.ToTable("producttransactionhistory");

                entity.HasIndex(e => e.Productid, "productid_idx");

                entity.HasIndex(e => e.Transactionid, "transactionid_idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .HasColumnName("id");

                entity.Property(e => e.Productid)
                    .HasMaxLength(64)
                    .HasColumnName("productid");

                entity.Property(e => e.Productname)
                    .HasMaxLength(255)
                    .HasColumnName("productname");

                entity.Property(e => e.Transactionid)
                    .HasMaxLength(64)
                    .HasColumnName("transactionid");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Producttransactionhistories)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("productid");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Producttransactionhistories)
                    .HasForeignKey(d => d.Transactionid)
                    .HasConstraintName("transactionid");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.HasIndex(e => e.Userid, "userid_idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2)
                    .HasColumnName("amount");

                entity.Property(e => e.Authdetails)
                    .HasMaxLength(2048)
                    .HasColumnName("authdetails");

                entity.Property(e => e.Clientname)
                    .HasMaxLength(64)
                    .HasColumnName("clientname");

                entity.Property(e => e.Companyemail)
                    .HasMaxLength(64)
                    .HasColumnName("companyemail");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime");

                entity.Property(e => e.Limitation)
                    .HasMaxLength(2048)
                    .HasColumnName("limitation");

                entity.Property(e => e.Otherservices)
                    .HasMaxLength(2048)
                    .HasColumnName("otherservices");

                entity.Property(e => e.Schedule)
                    .HasMaxLength(2048)
                    .HasColumnName("schedule");

                entity.Property(e => e.Scope)
                    .HasMaxLength(2048)
                    .HasColumnName("scope");

                entity.Property(e => e.Transactionid)
                    .HasMaxLength(128)
                    .HasColumnName("transactionid");

                entity.Property(e => e.Transactionstatus)
                    .HasMaxLength(64)
                    .HasColumnName("transactionstatus");

                entity.Property(e => e.Userid)
                    .HasMaxLength(64)
                    .HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userid");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Username, "username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(64);

                entity.Property(e => e.Companyname)
                    .HasMaxLength(255)
                    .HasColumnName("companyname");

                entity.Property(e => e.Emailverified).HasColumnName("emailverified");

                entity.Property(e => e.Failedloginattempts).HasColumnName("failedloginattempts");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("firstname");

                entity.Property(e => e.Hash)
                    .HasMaxLength(1024)
                    .HasColumnName("hash");

                entity.Property(e => e.Lastfailedattempt)
                    .HasColumnType("datetime")
                    .HasColumnName("lastfailedattempt");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");

                entity.Property(e => e.Lastotp)
                    .HasMaxLength(64)
                    .HasColumnName("lastotp");

                entity.Property(e => e.Lastotpsent)
                    .HasColumnType("datetime")
                    .HasColumnName("lastotpsent");

                entity.Property(e => e.Salt)
                    .HasMaxLength(1024)
                    .HasColumnName("salt");

                entity.Property(e => e.Username).HasColumnName("username");
            });

            modelBuilder.Entity<Usercontact>(entity =>
            {
                entity.ToTable("usercontact");

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .HasColumnName("id");

                entity.Property(e => e.Company)
                    .HasMaxLength(64)
                    .HasColumnName("company");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .HasColumnName("email");

                entity.Property(e => e.Message)
                    .HasMaxLength(2048)
                    .HasColumnName("message");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Usertoken>(entity =>
            {
                entity.ToTable("usertoken");

                entity.HasIndex(e => e.Sessionid, "sessionid_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Userid, "userid_idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .HasColumnName("id");

                entity.Property(e => e.Expiresat)
                    .HasColumnType("datetime")
                    .HasColumnName("expiresat");

                entity.Property(e => e.Generatedat)
                    .HasColumnType("datetime")
                    .HasColumnName("generatedat");

                entity.Property(e => e.Sessionid)
                    .HasMaxLength(64)
                    .HasColumnName("sessionid");

                entity.Property(e => e.Token)
                    .HasMaxLength(2048)
                    .HasColumnName("token");

                entity.Property(e => e.Userid)
                    .HasMaxLength(64)
                    .HasColumnName("userid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
