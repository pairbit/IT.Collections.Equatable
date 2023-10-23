using IT.Collections.Factory;
using IT.Collections.Factory.Factories;
using System;

namespace IT.Collections.Equatable.Factory;

public class EquatableHashSetFactory : HashSetFactory
{
    public override Type EnumerableType => typeof(EquatableHashSet<>);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableHashSet<T> Empty<T>(in Comparers<T> comparers = default) => new(comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableHashSet<T> New<T>(int capacity, in Comparers<T> comparers = default) => new(
#if !NET461 && !NETSTANDARD2_0
            capacity,
#endif
            comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableHashSet<T> New<T>(int capacity, EnumerableBuilder<T> builder, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var hashSet = new EquatableHashSet<T>(
#if !NET461 && !NETSTANDARD2_0
            capacity,
#endif
            comparers.EqualityComparer);

        builder(hashSet.Add);

        return hashSet;
    }

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableHashSet<T> New<T, TState>(int capacity, EnumerableBuilder<T, TState> builder, in TState state, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var hashSet = new EquatableHashSet<T>(
#if !NET461 && !NETSTANDARD2_0
            capacity,
#endif
            comparers.EqualityComparer);

        builder(hashSet.Add, in state);

        return hashSet;
    }

#if !NET5_0_OR_GREATER
    protected override System.Collections.Generic.HashSet<T> NewSet<T>(int capacity, in Comparers<T> comparers) =>
#if !NET461 && !NETSTANDARD2_0
        capacity == 0 ? new EquatableHashSet<T>(comparers.EqualityComparer) : new EquatableHashSet<T>(capacity, comparers.EqualityComparer);
#else
        new EquatableHashSet<T>(comparers.EqualityComparer);
#endif
#endif
}