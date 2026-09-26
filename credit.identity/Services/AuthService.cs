using credit.identity.Data;
using credit.identity.Data.Entities;
using credit.identity.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace credit.identity.Services;

public class AuthService(
    IdentityDbContext db,
    IPasswordHasher<Users> passwordHasher)
{
    public async Task<Users> AuthenticateAsync(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var user = await db.Users
            .Include(u => u.Institute)
            .SingleOrDefaultAsync(
                u => u.Username == request.Username,
                cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException("Invalid username or password");

        var result = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedAccessException("Invalid username or password");

        return user;
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

        db.Users.Add(user);

        await db.SaveChangesAsync(cancellationToken);

        return new SignupResponse(
            user.UserId,
            user.InstituteId,
            user.Username);
    }
}