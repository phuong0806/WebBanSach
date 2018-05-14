namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext()
            : base("name=BookStoreDbContext")
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<BookCatalog> BookCatalogs { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<MenuGroup> MenuGroups { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .HasMany(e => e.Businesses)
                .WithMany(e => e.Administrators)
                .Map(m => m.ToTable("Administrator_Business").MapLeftKey("Username").MapRightKey("Business"));

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Books)
                .WithMany(e => e.Authors)
                .Map(m => m.ToTable("Author_Book").MapLeftKey("AuthorID").MapRightKey("BookID"));

            modelBuilder.Entity<BookCatalog>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<BookCatalog>()
                .HasMany(e => e.BookCategories)
                .WithOptional(e => e.BookCatalog)
                .HasForeignKey(e => e.CatalogID);

            modelBuilder.Entity<BookCategory>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<BookCategory>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.BookCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Book>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Books)
                .Map(m => m.ToTable("Books_Tags").MapLeftKey("BookID").MapRightKey("TagID"));

            modelBuilder.Entity<Business>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<MenuGroup>()
                .HasMany(e => e.Menus)
                .WithOptional(e => e.MenuGroup)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Target)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PostCategory>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<PostCategory>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.PostCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Post>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("Posts_Tags").MapLeftKey("PostID").MapRightKey("TagID"));

            modelBuilder.Entity<Publisher>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Publisher>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Type)
                .IsUnicode(false);
        }
    }
}
