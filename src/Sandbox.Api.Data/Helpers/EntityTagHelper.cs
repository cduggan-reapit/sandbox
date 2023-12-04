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
        // Use the hash of the entities Guid in 32 digit format 
        var hashValue = GetHashString(entity.Id.ToString("N"));
        
        // Append the modified timestamp in `YYYY-MM-DD HH:mm:ssZ` format
        hashValue += entity.Modified.ToString("u");
        
        // Return the hash of the combined result
        return GetHashString(hashValue);
    }

    private static string GetHashString(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        var byteHash = SHA256.HashData(bytes);
        return Encoding.UTF8.GetString(byteHash);
    }
}