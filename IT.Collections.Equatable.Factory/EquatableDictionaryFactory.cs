using IT.Collections.Factory;
using IT.Collections.Factory.Factories;
using System;
using System.Collections.Generic;

namespace IT.Collections.Equatable.Factory;

public class EquatableDictionaryFactory : DictionaryFactory
{
    public override Type EnumerableType => typeof(EquatableDictionary<,>);

    public override EnumerableKind Kind => base.Kind | EnumerableKind.EquatableValue;

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableDictionary<TKey, TValue> Empty<TKey, TValue>(in Comparers<TKey, TValue> comparers = default)
#if !NET5_0_OR_GREATER
        where TKey : notnull
#endif
        => new(comparers.KeyEqualityComparer, comparers.ValueEqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableDictionary<TKey, TValue> New<TKey, TValue>(int capacity, in Comparers<TKey, TValue> comparers = default)
#if !NET5_0_OR_GREATER
        where TKey : notnull
#endif
        => new(capacity, comparers.KeyEqualityComparer, comparers.ValueEqualityComparer);

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableDictionary<TKey, TValue> New<TKey, TValue>(int capacity, EnumerableBuilder<KeyValuePair<TKey, TValue>> builder, in Comparers<TKey, TValue> comparers = default)
#if !NET5_0_OR_GREATER
        where TKey : notnull
#endif
    {
        if (capacity == 0) return new(comparers.KeyEqualityComparer, comparers.ValueEqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var dictionary = new EquatableDictionary<TKey, TValue>(capacity, comparers.KeyEqualityComparer, comparers.ValueEqualityComparer);

        builder(item => dictionary.TryAdd(item.Key, item.Value));

        return dictionary;
    }

    public
#if NET5_0_OR_GREATER
        override
#else
        new
#endif
        EquatableDictionary<TKey, TValue> New<TKey, TValue, TState>(int capacity, EnumerableBuilder<KeyValuePair<TKey, TValue>, TState> builder, in TState state, in Comparers<TKey, TValue> comparers = default)
#if !NET5_0_OR_GREATER
        where TKey : notnull
#endif
    {
        if (capacity == 0) return new(comparers.KeyEqualityComparer, comparers.ValueEqualityComparer);
        if (builder == null) throw new ArgumentNullException(nameof(builder));

        var dictionary = new EquatableDictionary<TKey, TValue>(capacity, comparers.KeyEqualityComparer, comparers.ValueEqualityComparer);

        builder(item => dictionary.TryAdd(item.Key, item.Value), in state);

        return dictionary;
    }

#if !NET5_0_OR_GREATER
    protected override Dictionary<TKey, TValue> NewDictionary<TKey, TValue>(int capacity, in Comparers<TKey, TValue> comparers)
        => new EquatableDictionary<TKey, TValue>(capacity, comparers.KeyEqualityComparer, comparers.ValueEqualityComparer);
#endif
}