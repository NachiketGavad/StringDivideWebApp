using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StringDivideWebApp.Areas.Identity.Data;

namespace StringDivideWebApp.Areas.Identity.Data;

public class StringDivideIdentityContext : IdentityDbContext<StringDivideWebAppUser>
{
    public StringDivideIdentityContext(DbContextOptions<StringDivideIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<StringDivideWebAppUser>
{
    public void Configure(EntityTypeBuilder<StringDivideWebAppUser> builder)
    {
        builder.Property(x => x.Firstname).HasMaxLength(255);
        builder.Property(x => x.Lastname).HasMaxLength(255);
    }
}