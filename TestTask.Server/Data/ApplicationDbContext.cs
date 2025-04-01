using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestTask.Server.Data.Entities;

namespace TestTask.Server.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    public DbSet<AboutContent> AboutContent { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ShortenedUrl>()
            .HasIndex(u => u.ShortCode)
            .IsUnique();

        builder.Entity<ShortenedUrl>()
            .HasOne(s => s.CreatedBy)
            .WithMany(u => u.ShortenedUrls)
            .HasForeignKey(s => s.CreatedById)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AboutContent>()
            .HasOne(a => a.ModifiedBy)
            .WithMany(u => u.ModifiedAboutContents)
            .HasForeignKey(a => a.ModifiedById)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}