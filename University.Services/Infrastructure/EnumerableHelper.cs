namespace University.Services.Infrastructure;

public static class EnumerableHelper
{
    public static void ForEach<T>(
        this IEnumerable<T> source, Action<T> action)
    {
        using var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            action(enumerator.Current);
        }
    }

    public static bool IsEnumerable(
        Type type) => IsEnumerable(type, out _);
    public static bool IsEnumerable(Type type, out Type underlyingType)
    {
        underlyingType = type.IsInterface &&
                         type.IsGenericType &&
                         type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
            ? type.GetGenericArguments()[0]
            : type.GetInterfaces().FirstOrDefault(IsEnumerable)?.GetGenericArguments()[0];
        return underlyingType != null;
    }

}