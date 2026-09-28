using credit.identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace credit.identity.Data;

public static class IdentityDbSeeder
{
    public static async Task SeedAsync(
        IdentityDbContext db,
        IPasswordHasher<Users> passwordHasher)
    {
        var institutionId =
            Guid.Parse("10000000-0000-0000-0000-000000000001");

        var adminUserId =
            Guid.Parse("70000000-0000-0000-0000-000000000001");

        var readerUserId =
            Guid.Parse("70000000-0000-0000-0000-000000000002");

        var writerUserId =
            Guid.Parse("70000000-0000-0000-0000-000000000003");

        var adminRoleId =
            Guid.Parse("90000000-0000-0000-0000-000000000001");

        var readerRoleId =
            Guid.Parse("90000000-0000-0000-0000-000000000002");

        var writerRoleId =
            Guid.Parse("90000000-0000-0000-0000-000000000003");

        if (!await db.Institutes.AnyAsync())
        {
            db.Institutes.Add(new Institutes
            {
                InstituteId = institutionId,
                InstituteName = "KBANK"
            });

            await db.SaveChangesAsync();
        }

        if (!await db.Roles.AnyAsync())
        {
            db.Roles.AddRange(
                new Roles
                {
                    RoleId = adminRoleId,
                    RoleName = "Admin"
                },
                new Roles
                {
                    RoleId = readerRoleId,
                    RoleName = "Reader"
                },
                new Roles
                {
                    RoleId = writerRoleId,
                    RoleName = "Writer"
                });

            await db.SaveChangesAsync();
        }

        if (!await db.Users.AnyAsync())
        {
            var admin = CreateUser(
                adminUserId,
                "admin",
                institutionId,
                passwordHasher);

            var reader = CreateUser(
                readerUserId,
                "reader",
                institutionId,
                passwordHasher);

            var writer = CreateUser(
                writerUserId,
                "writer",
                institutionId,
                passwordHasher);

            db.Users.AddRange(admin, reader, writer);

            await db.SaveChangesAsync();
        }

        if (!await db.UsersRoles.AnyAsync())
        {
            db.UsersRoles.AddRange(
                // Admin gets everything
                new UsersRoles
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new UsersRoles
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new UsersRoles
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                },

                // Reader can only read
                new UsersRoles
                {
                    UserId = readerUserId,
                    RoleId = readerRoleId
                },

                // Writer can read + write
                new UsersRoles
                {
                    UserId = writerUserId,
                    RoleId = readerRoleId
                },
                new UsersRoles
                {
                    UserId = writerUserId,
                    RoleId = writerRoleId
                });

            await db.SaveChangesAsync();
        }
    }

    private static Users CreateUser(
        Guid userId,
        string username,
        Guid institutionId,
        IPasswordHasher<Users> passwordHasher)
    {
        var user = new Users
        {
            UserId = userId,
            Username = username,
            InstituteId = institutionId,
            Institute = null!,
            PasswordHash = string.Empty
        };

        user.PasswordHash =
            passwordHasher.HashPassword(user, "Password123!");

        return user;
    }
}