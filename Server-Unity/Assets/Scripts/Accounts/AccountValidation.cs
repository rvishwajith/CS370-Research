using System.Text.RegularExpressions;

public static class AccountValidation
{
    private static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

    public static bool ValidateAll(PlayerAccount account)
    {
        return true;
    }

    public static bool ValidatePassword(PlayerAccount account)
    {
        // Requirements for a valid password:
        // 1. 6 <= length <= 32.
        // 2. Contains at >=1 number and >= 1 letter.
        // 3. Contains no non-ASCII characters and no \t, \n, \r\n.
        var password = account.password;
        return true;
    }

    public static bool ValidateUsername(PlayerAccount account)
    {
        // Requirements for a valid username:
        // 1. 4 <= length <= 16
        // 2. Contains >= 1 letters.
        // 3. Contains only A-Z, a-z, 0-9, -, and _.
        // 4. Does not start with - or _.
        var username = account.username;
        return true;
    }

    public static bool ValidateEmailAddress(PlayerAccount account)
    {
        // Requirements for a valid email address:
        // 1. length >= 5 (shortest email is x@x.x)
        // 2. Passes regex validation pattern: [...]@[].[]
        var address = account.emailAddress;
        return address.Length >= 5 && EmailRegex.IsMatch(address);
    }
}

