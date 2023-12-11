namespace Sandbox.Api.Common.Helpers;

public class CaseInsensitiveStringEqualityComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y)
    {
        if (x == null && y == null)
            return true;

        if (x == null || y == null)
            return false;

        return x.Equals(y, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(string obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        return obj.GetHashCode();
    }
}