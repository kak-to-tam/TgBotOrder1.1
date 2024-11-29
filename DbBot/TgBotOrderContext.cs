using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace tgBotOrderV11.DbBot;

public partial class TgBotOrderContext : DbContext
{
    public TgBotOrderContext()
    {
    }

    public TgBotOrderContext(DbContextOptions<TgBotOrderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<Referal> Referals { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=tg_bot_order;uid=root;pwd=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.3.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("operations");

            entity.HasIndex(e => e.UserId, "FK_blacklist_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.To).HasColumnName("to");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Referal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("referal");

            entity.HasIndex(e => e.Father1Id, "FK_referal_user");

            entity.HasIndex(e => e.Father2Id, "FK_referal_user_2");

            entity.HasIndex(e => e.Father3Id, "FK_referal_user_3");

            entity.HasIndex(e => e.UserId, "user_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Father1Id).HasColumnName("father1_id");
            entity.Property(e => e.Father2Id).HasColumnName("father2_id");
            entity.Property(e => e.Father3Id).HasColumnName("father3_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.TgId, "tg_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.TgId).HasColumnName("tg_id");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("wallet");

            entity.HasIndex(e => e.UserId, "FK_wallet_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("address");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.PrivateKey)
                .HasMaxLength(150)
                .HasDefaultValueSql("''")
                .HasColumnName("private_key");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
