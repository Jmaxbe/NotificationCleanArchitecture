namespace Domain.ValueObjects;

public sealed class Gender : ValueObject
{
    static Gender(){}
    
    private Gender(){}

    private Gender(string gender)
    {
        Value = gender;
    }

    public string Value { get; private set; } = "N/A";
    
    public static Gender Male => new("Male");
    public static Gender Female => new("Female");
    public static Gender NA => new("N/A");
    
    protected static IEnumerable<Gender> SupportedGenders
    {
        get
        {
            yield return Male;
            yield return Female;
        }
    }
    
    public static implicit operator string(Gender gender)
    {
        return gender;
    }
    
    public static explicit operator Gender(string gender)
    {
        return From(gender);
    }
    
    public static Gender From(string value)
    {
        var gender = new Gender { Value = value };

        if (!SupportedGenders.Contains(gender))
        {
            throw new UnsupportedGenderException(gender);
        }

        return gender;
    } 
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }
}