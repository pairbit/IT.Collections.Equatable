﻿using IT.Collections.Factory;
using IT.Collections.Factory.Factories;
using System;

namespace IT.Collections.Equatable.Factory;

public class EquatableListFactory : ListFactory
{
    public override Type EnumerableType => typeof(EquatableList<>);

    public override EnumerableKind Kind => base.Kind | EnumerableKind.Equatable;

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableList<T> Empty<T>(in Comparers<T> comparers = default) => new(comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableList<T> New<T>(int capacity, in Comparers<T> comparers = default) => new(capacity, comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableList<T> New<T>(int capacity, EnumerableBuilder<T> builder, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var list = new EquatableList<T>(capacity, comparers.EqualityComparer);

        builder(item => { list.Add(item); return true; });

        return list;
    }

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableList<T> New<T, TState>(int capacity, EnumerableBuilder<T, TState> builder, in TState state, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var list = new EquatableList<T>(capacity, comparers.EqualityComparer);

        builder(item => { list.Add(item); return true; }, in state);

        return list;
    }

#if !NET5_0_OR_GREATER
    protected override System.Collections.Generic.List<T> NewList<T>(int capacity, in Comparers<T> comparers)
        => capacity == 0 ? new EquatableList<T>(comparers.EqualityComparer) : new EquatableList<T>(capacity, comparers.EqualityComparer);
#endif
}