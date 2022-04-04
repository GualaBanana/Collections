namespace Collections;

public enum Pattern
{
    Fill,
    Ascending,
    Descending,
}

public static class PatternExtensions
{
    public static IEnumerable<int> GetEnumerable(this Pattern pattern, int seed) => pattern switch
    {
        Pattern.Fill => Enumerable.Repeat(seed, int.MaxValue),
        Pattern.Ascending => Enumerable.Range(seed, int.MaxValue),
        Pattern.Descending => Enumerable.Range(seed, 1),
        _ => throw new NotImplementedException()
    };
}
