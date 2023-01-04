using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace MarketplaceSI.Core.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher<User>
{
    private readonly HashingSettings _settings;

    public PasswordHasher(IOptions<HashingSettings> options)
    {
        _settings = options.Value;
    }

    public string HashPassword(User user, string password)
    {
        using var algorithm = new Rfc2898DeriveBytes(
            password,
            _settings.SaltSize,
            _settings.Iterations,
            HashAlgorithmName.SHA256);

        var key = Convert.ToBase64String(algorithm.GetBytes(_settings.KeySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{_settings.Iterations}.{salt}.{key}";
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
        var parts = hashedPassword.Split('.', 3);

        var iterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        var needsUpgrade = iterations != _settings.Iterations;

        using var algorithm = new Rfc2898DeriveBytes(
          providedPassword,
          salt,
          iterations,
          HashAlgorithmName.SHA256);

        return
            algorithm.GetBytes(_settings.KeySize).SequenceEqual(key)
            ? iterations != _settings.Iterations
                ? PasswordVerificationResult.SuccessRehashNeeded
                : PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }
}