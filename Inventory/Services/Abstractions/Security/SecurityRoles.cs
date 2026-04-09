namespace Services.Abstractions.Security;

public static class SecurityRoles
{
    public const string Admin = "Admin";
    public const string Manager = "Manager";
    public const string Employee = "Employee";

    public static readonly string[] All = [Admin, Manager, Employee];
}