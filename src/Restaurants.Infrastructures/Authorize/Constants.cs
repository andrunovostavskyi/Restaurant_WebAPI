namespace Restaurants.Infrastructures.Authorize;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string HasRequiredAge = "HasRequiredAge";
    public const string HasMinimalNumberRestaurants = "HasMinimalNumberRestaurants";
}


public static class AppClaimsType
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
}