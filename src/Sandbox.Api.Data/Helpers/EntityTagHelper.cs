using System.Security.Cryptography;
using System.Text;
using Sandbox.Api.Data.Entities;

namespace Sandbox.Api.Data.Helpers;

public static class EntityTagHelper
{
    /// <summary>
    /// Generates the EntityTag (a.k.a. "ETag") for a given entity
    /// </summary>
    /// <param name="entity">The entity for which to generate an EntityTag</param>
    /// <typeparam name="T">Any type which extends <see cref="BaseEntity"/></typeparam>
    /// <returns></returns>
    public static string GenerateEtagForEntity<T>(this T entity) where T : BaseEntity
    {
        // Use the identity as base (Type-Guid)
        var eTagIdentityComponent = string.Concat(typeof(T).Name, "-", entity.Id.ToString("N"));
        
        // Append the modified timestamp in `YYYY-MM-DD HH:mm:ssZ` format
        var eTagTimestampComponent = entity.Modified.ToString("u");
       
        // Use the hash of the entities Guid in 32 digit format 
        var hashedIdentity = GetHashString(eTagIdentityComponent);
        
        // Return the hash of the combined result
        return GetHashString(hashedIdentity + eTagTimestampComponent);
    }
    
    public static bool IsETagValid<T>(this T entity, string eTag) where T: BaseEntity
        => eTag.Trim('"').Equals(entity.GenerateEtagForEntity(), StringComparison.OrdinalIgnoreCase);

    private static string GetHashString(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        var byteHash = MD5.HashData(bytes);
        return string.Concat(byteHash.Select(b => $"{b:x2}"));
    }
}