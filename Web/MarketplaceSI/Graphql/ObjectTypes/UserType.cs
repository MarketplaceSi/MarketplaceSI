using MarketplaceSI.Core.Domain.Entities;
using MarketplaceSI.Core.Domain.Repositories.Interfaces;

namespace MarketplaceSI.Web.Api.Graphql.ObjectTypes;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor
            .Ignore(f => f.CreatedAt)
            .Ignore(f => f.CreatedBy)
            .Ignore(f => f.LastModifiedBy)
            .Ignore(f => f.LastModifiedAt)
            .Ignore(f => f.RefreshToken)
            .Ignore(f => f.RefreshTokenExpiry)
            .Ignore(f => f.NormalizedEmail)
            .Ignore(f => f.NormalizedUserName)
            .Ignore(f => f.PhoneNumberConfirmed)
            .Ignore(f => f.AccessFailedCount)
            .Ignore(f => f.Activated)
            .Ignore(f => f.ActivationKey)
            .Ignore(f => f.ConcurrencyStamp)
            .Ignore(f => f.EmailConfirmed)
            .Ignore(f => f.LockoutEnabled)
            .Ignore(f => f.LockoutEnd)
            .Ignore(f => f.PasswordHash)
            .Ignore(f => f.ResetDate)
            .Ignore(f => f.ResetKey)
            .Ignore(f => f.SecurityStamp)
            .Ignore(f => f.TwoFactorEnabled)
            .Ignore(f => f.UserRoles);
        descriptor
            .Field("fullName")
            .Resolve(context =>
            {
                return $"{context.Parent<User>().FirstName} {context.Parent<User>().LastName}";
            });
        descriptor
            .Field("address")
            .Resolve(context =>
            {
                return context.Parent<User>()?.Addresses?.FirstOrDefault(a => a.IsDefault);
            });
    }
}