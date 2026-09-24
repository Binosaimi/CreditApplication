using credit.identity.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace credit.identity.Data;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<UsersRoles> UsersRoles { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Institutes> Institutes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsersRoles>(entity =>
        {
            entity.ToTable("users_roles");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
        });
        
        modelBuilder.Entity<UsersRoles>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });
        
        modelBuilder.Entity<Users>(entity =>
        {
            entity.ToTable("users");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.InstituteId).HasColumnName("institution_id");
        });
        
        modelBuilder.Entity<Institutes>(entity =>
        {
            entity.ToTable("institutes");
            entity.Property(e => e.InstituteId).HasColumnName("institute_id");
            entity.Property(e => e.InstituteName).HasColumnName("name");
        });
        
        modelBuilder.Entity<Roles>(entity =>
        {
            entity.ToTable("roles");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName).HasColumnName("name");
        });
        
    }
    
}