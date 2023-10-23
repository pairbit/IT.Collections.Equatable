using IT.Collections.Factory;
using IT.Collections.Factory.Factories;
using System;

namespace IT.Collections.Equatable.Factory;

public class EquatableLinkedListFactory : LinkedListFactory
{
    public override Type EnumerableType => typeof(EquatableLinkedList<>);

    public override EnumerableKind Kind => base.Kind | EnumerableKind.Equatable;

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableLinkedList<T> Empty<T>(in Comparers<T> comparers = default) => new(comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableLinkedList<T> New<T>(int _, in Comparers<T> comparers = default) => new(comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableLinkedList<T> New<T>(int capacity, EnumerableBuilder<T> builder, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var list = new EquatableLinkedList<T>(comparers.EqualityComparer);

        builder(item => { list.AddLast(item); return true; });

        return list;
    }

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableLinkedList<T> New<T, TState>(int capacity, EnumerableBuilder<T, TState> builder, in TState state, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var list = new EquatableLinkedList<T>(comparers.EqualityComparer);

        builder(item => { list.AddLast(item); return true; }, in state);

        return list;
    }

#if !NET5_0_OR_GREATER
    protected override System.Collections.Generic.LinkedList<T> NewList<T>(int capacity, in Comparers<T> comparers)
        => new EquatableLinkedList<T>(comparers.EqualityComparer);
#endif
}