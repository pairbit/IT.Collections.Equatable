using IT.Collections.Equatable.Factory;
using IT.Collections.Factory;
using IT.Collections.Factory.Factories;

namespace IT.Collections.Equatable.Tests;

public class Tests
{
    private IEnumerableFactoryRegistry _registry;

    [SetUp]
    public void Setup()
    {
        _registry = new EnumerableFactoryRegistry().RegisterFactoriesEquatable();
    }

    [Test]
    public void RegistryTest()
    {
        FactoryStringTest<EquatableHashSetFactory>();
        FactoryStringTest<EquatableLinkedListFactory>();
        FactoryStringTest<EquatableListFactory>();
        FactoryStringTest<EquatableQueueFactory>();
        FactoryStringTest<EquatableSortedSetFactory>();
        FactoryStringTest<EquatableStackFactory>();

        FactoryStringTest<HashSetFactory>();
        FactoryStringTest<LinkedListFactory>();
        FactoryStringTest<ListFactory>();
        FactoryStringTest<QueueFactory>();
        FactoryStringTest<SortedSetFactory>();
        FactoryStringTest<StackFactory>();

        FactoryStringTest<IEnumerableFactory>();
        FactoryStringTest<ICollectionFactory>();
        FactoryStringTest<IListFactory>();
        FactoryStringTest<ISetFactory>();
    }

    public void FactoryStringTest<TFactory>() where TFactory : IEnumerableFactory
    {
        var comparers = StringComparer.OrdinalIgnoreCase.ToComparers();
        var comparers2 = StringComparer.Ordinal.ToComparers();
        try
        {
            FactoryTest(_registry.GetFactory<TFactory>(), Builder, in comparers, in comparers2);
        }
        catch (Exception)
        {
            Console.WriteLine($"{typeof(TFactory).FullName} error");
            throw;
        }
    }

    public void FactoryTest<T>(IEnumerableFactory factory, EnumerableBuilder<T, int> builder,
        in Comparers<T> comparers, in Comparers<T> comparers2)
    {
        Assert.That(comparers, Is.Not.EqualTo(comparers2));

        SequenceEqual(
            factory.Empty(in comparers),
            factory.Empty(in comparers));

        SequenceEqual(
            factory.New(2, builder, 1, in comparers),
            factory.New(2, builder, 1, in comparers));

        SequenceEqual(
            factory.New(10, builder, 5, in comparers),
            factory.New(10, builder, 5, in comparers));

        SequenceNotEqual(
            factory.Empty(in comparers),
            factory.Empty(in comparers2));

        SequenceNotEqual(
            factory.New(2, builder, 1, in comparers),
            factory.New(2, builder, 1, in comparers2));

        SequenceNotEqual(
            factory.New(10, builder, 5, in comparers),
            factory.New(10, builder, 5, in comparers2));

        SequenceNotEqual(
            factory.Empty(in comparers),
            factory.New(2, builder, 1, in comparers));

        SequenceNotEqual(
            factory.New(2, builder, 1, in comparers),
            factory.New(10, builder, 5, in comparers));

        SequenceNotEqual(
            factory.Empty(in comparers),
            factory.New(2, builder, 1, in comparers2));

        SequenceNotEqual(
            factory.New(2, builder, 1, in comparers),
            factory.New(10, builder, 5, in comparers2));
    }

    private void SequenceEqual<T>(IEnumerable<T> x, IEnumerable<T> y)
    {
        Assert.That(x.GetHashCode(), Is.EqualTo(y.GetHashCode()));

        Assert.That(x.Equals(y), Is.True);
        Assert.That(y.Equals(x), Is.True);
    }

    private void SequenceNotEqual<T>(IEnumerable<T> x, IEnumerable<T> y)
    {
        Assert.That(x.GetHashCode(), Is.Not.EqualTo(y.GetHashCode()));

        Assert.That(x.Equals(y), Is.False);
        Assert.That(y.Equals(x), Is.False);
    }

    static bool flag;

    private static void Builder(TryAdd<string?> tryAdd, in int size)
    {
        for (int i = 0; i < size; i++)
        {
            if (flag)
            {
                tryAdd($"test {i}");
            }
            else
            {
                tryAdd($"tEsT {i}");
            }
            flag = !flag;
        }
    }
}