
class Character
{
    public ulong Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string FirstAppearances { get; set; } = string.Empty;
    public int YearCreated { get; set; } // Removed the incorrect assignment

    public string Display()
    {
        return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nSpecies: {Species}\nFirst Appearance: {FirstAppearances}\nYear Created: {YearCreated}\n";
    }

    internal List<string> ConvertAll(Func<object, int> value)
    {
        throw new NotImplementedException();
    }
}
