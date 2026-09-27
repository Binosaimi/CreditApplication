using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using credit.identity.Data;
using credit.identity.Data.Entities;
using credit.identity.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace credit.identity.Services;

public class AuthService(
    IdentityDbContext db,
    IPasswordHasher<Users> passwordHasher,
    IConfiguration configuration)
{
    public async Task<LoginResponse> AuthenticateAsync(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = await db.Users
            .SingleOrDefaultAsync(
                u => u.Username == request.Username,
                cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException(
                "Invalid username or password");

        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException(
                "Invalid username or password");

        var roles = await db.UsersRoles
            .Where(ur => ur.UserId == user.UserId)
            .Select(ur => ur.Role.RoleName)
            .ToListAsync(cancellationToken);

        var expiresAt = DateTime.UtcNow.AddMinutes(
            configuration.GetValue<int>("Jwt:ExpirationMinutes"));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Username),
            new("institute_id", user.InstituteId.ToString())
        };

        claims.AddRange(
            roles.Select(role => new Claim("role", role)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return new LoginResponse(
            accessToken,
            expiresAt);
    }

    public async Task<SignupResponse> RegisterAsync(
        SignupRequest request,
        CancellationToken cancellationToken)
    {
        var exists = await db.Users
            .AnyAsync(u => u.Username == request.Username, cancellationToken);

        if (exists)
            throw new InvalidOperationException("Username already exists");

        var user = new Users
        {
            UserId = Guid.NewGuid(),
            Username = request.Username,
            InstituteId = request.InstituteId,
            Institute = null!,
            PasswordHash = string.Empty
        };

        user.PasswordHash = passwordHasher.HashPassword(
            user,
            request.Password);
        try
        {
            db.Users.Add(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
            // throw CannotSignUpException;
        }

        await db.SaveChangesAsync(cancellationToken);

        return new SignupResponse(
            user.UserId,
            user.InstituteId,
            user.Username);
    }

    public async Task<String> CreateRole(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var role = new Roles
        {
            RoleId = Guid.NewGuid(),
            RoleName = request.RoleName
        };
        try
        {
            db.Roles.Add(role);
            await db.SaveChangesAsync(cancellationToken);
            return role.RoleName;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AssignRole(
        AssignRoleRequest request,
        CancellationToken cancellationToken)
    {
        Users? user;
        try
        {
            user = await db.Users
                .SingleOrDefaultAsync(
                    u => u.Username == request.Username,
                    cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        if (user is null)
            throw new ArgumentException("User not found");

        Roles? role;
        try
        {
            role = await db.Roles
                .SingleOrDefaultAsync(
                    r => r.RoleName == request.RoleName,
                    cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


        if (role is null)
            throw new ArgumentException("Role not found");

        bool alreadyAssigned;
        try
        {
            alreadyAssigned = await db.UsersRoles
                .AnyAsync(
                    ur => ur.UserId == user.UserId &&
                          ur.RoleId == role.RoleId,
                    cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


        if (alreadyAssigned)
            throw new InvalidOperationException(
                "Role already assigned to user");

        try
        {
            db.UsersRoles.Add(new UsersRoles
            {
                UserId = user.UserId,
                RoleId = role.RoleId
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        await db.SaveChangesAsync(cancellationToken);
    }
}