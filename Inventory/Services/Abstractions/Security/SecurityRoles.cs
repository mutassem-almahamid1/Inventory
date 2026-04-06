namespace Services.Abstractions.Security;

public static class SecurityRoles
{
    public const string Employee = "Employee";
    public const string Manager = "Manager";

    public static readonly string[] All = [Employee, Manager];
}