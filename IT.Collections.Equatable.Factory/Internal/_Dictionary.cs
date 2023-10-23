#if NETSTANDARD2_0 || NET461_OR_GREATER

namespace System.Collections.Generic;

internal static class _Dictionary
{
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key)) return false;

        dictionary.Add(key, value);
        return true;
    }
}

#endif