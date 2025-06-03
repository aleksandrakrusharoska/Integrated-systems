using LibraryDomain.Domain;
using LibraryDomain.Identity;
using LibraryDomain.Relationship;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Data;

public class ApplicationDbContext : IdentityDbContext<LibraryUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<BooksInShoppingCart> BooksInShoppingCarts { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<BooksInOrder> BooksInOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(book =>
        {
            book.HasKey(b => b.Id);
        });

        modelBuilder.Entity<ShoppingCart>(cart =>
        {
            cart.HasKey(c => c.Id);

            cart.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            cart.HasOne(c => c.LibraryUser)
                .WithOne(c => c.UserShoppingCart)
                .HasForeignKey<ShoppingCart>(c => c.OwnerId);
        });

        modelBuilder.Entity<BooksInShoppingCart>(booksInCart =>
        {
            booksInCart.HasKey(b => b.Id);

            booksInCart.HasOne(b => b.Book)
                .WithMany(b => b.BooksInShoppingCart)
                .HasForeignKey(b => b.BookId);

            booksInCart.HasOne(b => b.ShoppingCart)
                .WithMany(s => s.BooksInShoppingCart)
                .HasForeignKey(b => b.ShoppingCartId);
        });

        modelBuilder.Entity<Order>(order =>
        {
            order.HasKey(o => o.Id);
            order.HasOne(o => o.LibraryUser)
                .WithMany()
                .HasForeignKey(o => o.OwnerId);
        });

        modelBuilder.Entity<BooksInOrder>(booksInOrder =>
        {
            booksInOrder.HasKey(b => b.Id);

            booksInOrder.HasOne(b => b.Book)
                .WithMany()
                .HasForeignKey(b => b.BookId);

            booksInOrder.HasOne(b => b.Order)
                .WithMany(s => s.BooksInOrder)
                .HasForeignKey(b => b.OrderId);
        });
    }
}