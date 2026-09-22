// Person stores basic information about a person.
class Person
{
    // The person's name is kept private and can only be read through GetName.
    private readonly string name = string.Empty;

    // Returns the person's name.
    public string GetName()
    {
        return name;
    }

    // Stores the person's age.
    public int age;

    // Returns the person's age.
    public int getAge()
    {
        return age;
    }
}