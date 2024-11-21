using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace tgBotOrder_v11.DbBot;

public partial class TgBotOrderContext : DbContext
{
    public TgBotOrderContext()
    {
    }

    public TgBotOrderContext(DbContextOptions<TgBotOrderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blacklist> Blacklists { get; set; }

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

        modelBuilder.Entity<Blacklist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("blacklist");

            entity.HasIndex(e => e.UserId, "FK_blacklist_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Blacklists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_blacklist_user");
        });

        modelBuilder.Entity<Referal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("referal");

            entity.HasIndex(e => e.Father1Id, "FK__user");

            entity.HasIndex(e => e.Father2Id, "FK__user_2");

            entity.HasIndex(e => e.Father3Id, "FK__user_3");

            entity.HasIndex(e => e.UserId, "user_id").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Father1Id).HasColumnName("father1_id");
            entity.Property(e => e.Father2Id).HasColumnName("father2_id");
            entity.Property(e => e.Father3Id).HasColumnName("father3_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Father1).WithMany(p => p.ReferalFather1s)
                .HasForeignKey(d => d.Father1Id)
                .HasConstraintName("FK__user");

            entity.HasOne(d => d.Father2).WithMany(p => p.ReferalFather2s)
                .HasForeignKey(d => d.Father2Id)
                .HasConstraintName("FK__user_2");

            entity.HasOne(d => d.Father3).WithMany(p => p.ReferalFather3s)
                .HasForeignKey(d => d.Father3Id)
                .HasConstraintName("FK__user_3");

            entity.HasOne(d => d.User).WithOne(p => p.ReferalUser)
                .HasForeignKey<Referal>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_5");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.WalletId, "wallet_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.TgId).HasColumnName("tg_id");
            entity.Property(e => e.WalletId).HasColumnName("wallet_id");

            entity.HasOne(d => d.Wallet).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.WalletId)
                .HasConstraintName("FK_user_wallet");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("wallet");

            entity.HasIndex(e => e.UserId, "FK_wallet_user");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("adress");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wallet_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
