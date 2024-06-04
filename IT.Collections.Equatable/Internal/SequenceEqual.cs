using System.Collections.Generic;

namespace IT.Collections.Equatable.Internal;

internal static class SequenceEqual
{
    public static bool List<T>(IList<T> x, IList<T> y, IEqualityComparer<T>? equalityComparer)
    {
        if (equalityComparer == null)
        {
            // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
            if (typeof(T).IsValueType)
            {
                for (int i = 0; i < x.Count; i++)
                {
                    if (!EqualityComparer<T>.Default.Equals(x[i], y[i])) return false;
                }
                return true;
            }

            equalityComparer = EqualityComparer<T>.Default;
        }

        for (int i = 0; i < x.Count; i++)
        {
            if (!equalityComparer.Equals(x[i], y[i])) return false;
        }

        return true;
    }

    public static bool Enumerable<T>(IEnumerable<T> x, IEnumerable<T> y,
        IEqualityComparer<T>? equalityComparer)
    {
        using var ex = x.GetEnumerator();
        using var ey = y.GetEnumerator();

        if (equalityComparer == null)
        {
            // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
            if (typeof(T).IsValueType)
            {
                while (ex.MoveNext())
                {
                    if (!(ey.MoveNext() && EqualityComparer<T>.Default.Equals(ex.Current, ey.Current))) return false;
                }

                return !ey.MoveNext();
            }

            equalityComparer = EqualityComparer<T>.Default;
        }

        while (ex.MoveNext())
        {
            if (!(ey.MoveNext() && equalityComparer.Equals(ex.Current, ey.Current))) return false;
        }

        return !ey.MoveNext();
    }

    public static bool EnumerableRequired<T>(IEnumerable<T> x, IEnumerable<T> y,
        IEqualityComparer<T> equalityComparer)
    {
        using var ex = x.GetEnumerator();
        using var ey = y.GetEnumerator();

        // ValueType: Devirtualize with EqualityComparer<TValue>.Default intrinsic
        if (typeof(T).IsValueType && equalityComparer == EqualityComparer<T>.Default)
        {
            while (ex.MoveNext())
            {
                if (!(ey.MoveNext() && EqualityComparer<T>.Default.Equals(ex.Current, ey.Current))) return false;
            }

            return !ey.MoveNext();
        }

        while (ex.MoveNext())
        {
            if (!(ey.MoveNext() && equalityComparer.Equals(ex.Current, ey.Current))) return false;
        }

        return !ey.MoveNext();
    }

    public static bool EnumerableRequired<T>(IEnumerable<T> x, IEnumerable<T> y,
        IComparer<T> comparer)
    {
        using var ex = x.GetEnumerator();
        using var ey = y.GetEnumerator();

        while (ex.MoveNext())
        {
            if (!(ey.MoveNext() && comparer.Compare(ex.Current, ey.Current) == 0)) return false;
        }

        return !ey.MoveNext();
    }

    public static bool Enumerable<TKey, TValue>(
        IEnumerable<KeyValuePair<TKey, TValue>> x, IEnumerable<KeyValuePair<TKey, TValue>> y,
        IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue>? valueComparer)
    {
        using var ex = x.GetEnumerator();
        using var ey = y.GetEnumerator();

        if (valueComparer == null)
        {
            if (typeof(TValue).IsValueType)
            {
                if (typeof(TKey).IsValueType && keyComparer == EqualityComparer<TKey>.Default)
                {
                    while (ex.MoveNext())
                    {
                        if (!ey.MoveNext()) return false;

                        var xCurrent = ex.Current;
                        var yCurrent = ey.Current;

                        if (!(EqualityComparer<TKey>.Default.Equals(xCurrent.Key, yCurrent.Key) &&
                              EqualityComparer<TValue>.Default.Equals(xCurrent.Value, yCurrent.Value))) return false;
                    }

                    return !ey.MoveNext();
                }

                while (ex.MoveNext())
                {
                    if (!ey.MoveNext()) return false;

                    var xCurrent = ex.Current;
                    var yCurrent = ey.Current;

                    if (!(keyComparer.Equals(xCurrent.Key, yCurrent.Key) &&
                          EqualityComparer<TValue>.Default.Equals(xCurrent.Value, yCurrent.Value))) return false;
                }

                return !ey.MoveNext();
            }

            valueComparer = EqualityComparer<TValue>.Default;
        }

        if (typeof(TKey).IsValueType && keyComparer == EqualityComparer<TKey>.Default)
        {
            while (ex.MoveNext())
            {
                if (!ey.MoveNext()) return false;

                var xCurrent = ex.Current;
                var yCurrent = ey.Current;

                if (!(EqualityComparer<TKey>.Default.Equals(xCurrent.Key, yCurrent.Key) &&
                      valueComparer.Equals(xCurrent.Value, yCurrent.Value))) return false;
            }

            return !ey.MoveNext();
        }

        while (ex.MoveNext())
        {
            if (!ey.MoveNext()) return false;

            var xCurrent = ex.Current;
            var yCurrent = ey.Current;

            if (!(keyComparer.Equals(xCurrent.Key, yCurrent.Key) &&
                  valueComparer.Equals(xCurrent.Value, yCurrent.Value))) return false;
        }

        return !ey.MoveNext();
    }
}