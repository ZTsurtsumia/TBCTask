using Bogus;
using Dapper;
using PersonDirectory.Application.Data;
using System.Text.Json;

internal static class SeedDataExtensions
{
    private static readonly string[] Cities = { "Tbilisi", "Sokhumi", "Batumi", "Rustavi" };

    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var users = GenerateUsers(10);

        const string sql = @"
            INSERT INTO dbo.Persons 
            ([FirstName], [LastName], [Sex], [PersonalN], [DateOfBirth], [City], [ConnectedPersons], [MobilePhones])
            VALUES 
            (@FirstName, @LastName, @Sex, @PersonalN, @DateOfBirth, @City, @ConnectedPersons, @MobilePhones);";

        connection.Execute(sql, users);
    }

    private static List<AddPersonModel> GenerateUsers(int count)
    {
        var faker = new Faker<AddPersonModel>()
                    .RuleFor(p => p.FirstName, GenerateName)
                    .RuleFor(p => p.LastName, GenerateName)
                    .RuleFor(p => p.Sex, f => (Sex)f.Random.Int(0, 1))
                    .RuleFor(p => p.DateOfBirth, f => f.Date.Past(80, DateTime.UtcNow.AddYears(-18)))
                    .RuleFor(p => p.PersonalN, f => f.Random.String2(11, "0123456789"))
                    .RuleFor(p => p.ConnectedPersons, GenerateConnectedPersonsJson)
                    .RuleFor(p => p.MobilePhones, f => GenerateMobilePhonesJson(f))
                    .RuleFor(p => p.City, f => f.PickRandom(Cities));

        return faker.Generate(count);
    }

    private static string GenerateName(Faker f)
    {
        bool useGeorgian = f.Random.Bool();
        return useGeorgian
            ? f.Random.String2(f.Random.Int(2, 50), "აბგდევზთიკლმნოპრსტუფქღყშჩცძწჭხჯჰ")
            : f.Name.FirstName();
    }

    private static string GenerateConnectedPersonsJson(Faker f, AddPersonModel currentPerson)
    {
        int count = f.Random.Int(1, 3);
        var connections = new List<CP>();

        for (int i = 0; i < count; i++)
        {
            var connectedPersonId = GetUniqueConnectedPersonId(f, currentPerson.PersonalN.GetHashCode());
            connections.Add(new CP
            {
                Type = f.PickRandom<CPType>(),
                ConnectedPersonId = connectedPersonId
            });
        }

        return JsonSerializer.Serialize(connections);
    }

    private static int GetUniqueConnectedPersonId(Faker f, int currentPersonId)
    {
        int connectedPersonId;
        do
        {
            connectedPersonId = f.Random.Int(1, 10);
        }
        while (connectedPersonId == currentPersonId); // Ensure it's not the same as current person
        return connectedPersonId;
    }

    private static string GenerateMobilePhonesJson(Faker f)
    {
        int count = f.Random.Int(1, 3);
        var phones = new List<PhoneNumber>();

        for (int i = 0; i < count; i++)
        {
            phones.Add(new PhoneNumber
            {
                Type = f.PickRandom<MobilePhoneType>(),
                Number = f.Random.String2(f.Random.Int(4, 50), "0123456789")
            });
        }

        return JsonSerializer.Serialize(phones);
    }
}

public class AddPersonModel
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Sex Sex { get; set; }
    public string PersonalN { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string City { get; set; }
    public string MobilePhones { get; set; } // Store MobilePhones as a JSON string
    public string ConnectedPersons { get; set; } // JSON String
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}

public class CP
{
    public CPType Type { get; set; }
    public int ConnectedPersonId { get; set; }
}

public enum CPType
{
    Friend,
    Relative,
    Colleague
}

public enum Sex
{
    Male = 0,
    Female = 1
}

public class PhoneNumber
{
    public MobilePhoneType Type { get; set; }
    public string Number { get; set; }
}

public enum MobilePhoneType
{
    Home,
    Office,
    Personal
}
