using IT.Collections.Factory;
using IT.Collections.Factory.Factories;
using System;

namespace IT.Collections.Equatable.Factory;

public class EquatableQueueFactory : QueueFactory
{
    public override Type EnumerableType => typeof(EquatableQueue<>);

    public override EnumerableKind Kind => base.Kind | EnumerableKind.Equatable;

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableQueue<T> Empty<T>(in Comparers<T> comparers = default) => new(comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableQueue<T> New<T>(int capacity, in Comparers<T> comparers = default) => new(capacity, comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableQueue<T> New<T>(int capacity, EnumerableBuilder<T> builder, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var queue = new EquatableQueue<T>(capacity, comparers.EqualityComparer);

        builder(item => { queue.Enqueue(item); return true; });

        return queue;
    }

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableQueue<T> New<T, TState>(int capacity, EnumerableBuilder<T, TState> builder, in TState state, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var queue = new EquatableQueue<T>(capacity, comparers.EqualityComparer);

        builder(item => { queue.Enqueue(item); return true; }, in state);

        return queue;
    }

#if !NET5_0_OR_GREATER
    protected override System.Collections.Generic.Queue<T> NewQueue<T>(int capacity, in Comparers<T> comparers)
        => capacity == 0 ? new EquatableQueue<T>(comparers.EqualityComparer) : new EquatableQueue<T>(capacity, comparers.EqualityComparer);
#endif
}