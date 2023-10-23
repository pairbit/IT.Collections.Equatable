using IT.Collections.Factory;
using IT.Collections.Factory.Factories;
using System;

namespace IT.Collections.Equatable.Factory;

public class EquatableStackFactory : StackFactory
{
    public override Type EnumerableType => typeof(EquatableStack<>);

    public override EnumerableKind Kind => base.Kind | EnumerableKind.Equatable;

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableStack<T> Empty<T>(in Comparers<T> comparers = default) => new(comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableStack<T> New<T>(int capacity, in Comparers<T> comparers = default) => new(capacity, comparers.EqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableStack<T> New<T>(int capacity, EnumerableBuilder<T> builder, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var stack = new EquatableStack<T>(capacity, comparers.EqualityComparer);

        builder(item => { stack.Push(item); return true; });

        return stack;
    }

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableStack<T> New<T, TState>(int capacity, EnumerableBuilder<T, TState> builder, in TState state, in Comparers<T> comparers = default)
    {
        if (capacity == 0) return new(comparers.EqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var stack = new EquatableStack<T>(capacity, comparers.EqualityComparer);

        builder(item => { stack.Push(item); return true; }, in state);

        return stack;
    }

#if !NET5_0_OR_GREATER
    protected override System.Collections.Generic.Stack<T> NewStack<T>(int capacity, in Comparers<T> comparers)
        => capacity == 0 ? new EquatableStack<T>(comparers.EqualityComparer) : new EquatableStack<T>(capacity, comparers.EqualityComparer);
#endif
}