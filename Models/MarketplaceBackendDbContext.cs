using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace marketplace_backend.Models;

public partial class MarketplaceBackendDbContext : DbContext
{
    public MarketplaceBackendDbContext()
    {
    }

    public MarketplaceBackendDbContext(DbContextOptions<MarketplaceBackendDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BaseItem> BaseItems { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartLine> CartLines { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemImage> ItemImages { get; set; }

    public virtual DbSet<ItemUserView> ItemUserViews { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<User> Users { get; set; }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer()
                        .UseLazyLoadingProxies()
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging();
    */
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json");
        var config = builder.Build();
        var connectionString = config.GetConnectionString("DBConnectionString");

        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(connectionString)
                .UseLazyLoadingProxies()
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.IdAddress).HasName("PK__addresse__5A7A75D9CDC32E63");

            entity.ToTable("addresses");

            entity.Property(e => e.IdAddress).HasColumnName("id_address");
            entity.Property(e => e.AppartmentNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("appartment_number");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.StreetName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street_name");
            entity.Property(e => e.StreetNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street_number");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("zip_code");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__addresses__id_us__41EDCAC5");
        });

        modelBuilder.Entity<BaseItem>(entity =>
        {
            entity.HasKey(e => e.IdBaseItem).HasName("PK__base_ite__AF119C8D50C82685");

            entity.ToTable("base_items");

            entity.Property(e => e.IdBaseItem).HasColumnName("id_base_item");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.BaseItems)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__base_item__id_ca__5224328E");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__carts__C71FE3173344229F");

            entity.ToTable("carts");

            entity.Property(e => e.IdCart).HasColumnName("id_cart");
            entity.Property(e => e.BoughtDate)
                .HasColumnType("datetime")
                .HasColumnName("bought_date");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IsBought).HasColumnName("is_bought");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__carts__id_user__44CA3770");
        });

        modelBuilder.Entity<CartLine>(entity =>
        {
            entity.HasKey(e => new { e.IdCart, e.IdItem }).HasName("PK__cart_lin__6F63772F8A6F92BF");

            entity.ToTable("cart_lines");

            entity.Property(e => e.IdCart).HasColumnName("id_cart");
            entity.Property(e => e.IdItem).HasColumnName("id_item");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.IsPackageSent).HasColumnName("is_package_sent");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("((1))")
                .HasColumnName("quantity");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.CartLines)
                .HasForeignKey(d => d.IdCart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart_line__id_ca__5D95E53A");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.CartLines)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart_line__id_it__5E8A0973");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__categori__E548B673FD00C622");

            entity.ToTable("categories");

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UrlImage)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("url_image");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.IdItem).HasName("PK__items__87C9438B34AA7FE1");

            entity.ToTable("items");

            entity.Property(e => e.IdItem).HasColumnName("id_item");
            entity.Property(e => e.Color)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.IdBaseItem).HasColumnName("id_base_item");
            entity.Property(e => e.Price)
                .HasColumnType("smallmoney")
                .HasColumnName("price");
            entity.Property(e => e.Size)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("size");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdBaseItemNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdBaseItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__items__id_base_i__55F4C372");
        });

        modelBuilder.Entity<ItemImage>(entity =>
        {
            entity.HasKey(e => e.IdImage).HasName("PK__item_ima__C28C621CB9BF60BD");

            entity.ToTable("item_images");

            entity.Property(e => e.IdImage).HasColumnName("id_image");
            entity.Property(e => e.IdItem).HasColumnName("id_item");
            entity.Property(e => e.IsMain).HasColumnName("is_main");
            entity.Property(e => e.UrlImage)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("url_image");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.ItemImages)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__item_imag__id_it__59C55456");
        });

        modelBuilder.Entity<ItemUserView>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdItem }).HasName("PK__item_use__7AADD20FA7E01224");

            entity.ToTable("item_user_views");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdItem).HasColumnName("id_item");
            entity.Property(e => e.DateView)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_view");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.ItemUserViews)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__item_user__id_it__662B2B3B");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ItemUserViews)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__item_user__id_us__65370702");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.IdNotification).HasName("PK__notifica__925C842FB207372B");

            entity.ToTable("notifications");

            entity.Property(e => e.IdNotification).HasColumnName("id_notification");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.IdCart).HasColumnName("id_cart");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.NotificationText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("notification_text");
            entity.Property(e => e.WasSeen).HasColumnName("was_seen");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdCart)
                .HasConstraintName("FK__notificat__id_ca__4A8310C6");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__notificat__id_us__498EEC8D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__users__D2D146374F29A3DB");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E61649F66936D").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Role)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasDefaultValueSql("('member')")
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
