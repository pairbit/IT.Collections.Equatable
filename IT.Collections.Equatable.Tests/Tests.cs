using IT.Collections.Factory;

namespace IT.Collections.Equatable.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }


    public void FactoryTest<T>(IEnumerableFactory factory, EnumerableBuilder<T, int> builder,
        in Comparers<T> comparers, in Comparers<T> comparers2)
    {
        Assert.That(comparers, Is.Not.EqualTo(comparers2));

        SequenceEqual(
            factory.Empty(in comparers), 
            factory.Empty(in comparers));

        SequenceEqual(
            factory.New(1, builder, 1, in comparers), 
            factory.New(1, builder, 1, in comparers));

        SequenceEqual(
            factory.New(9, builder, 9, in comparers), 
            factory.New(9, builder, 9, in comparers));

        SequenceNotEqual(
            factory.Empty(in comparers),
            factory.Empty(in comparers2));

        SequenceNotEqual(
            factory.New(1, builder, 1, in comparers),
            factory.New(1, builder, 1, in comparers2));

        SequenceNotEqual(
            factory.New(9, builder, 9, in comparers),
            factory.New(9, builder, 9, in comparers2));

        SequenceNotEqual(
            factory.Empty(in comparers), 
            factory.New(1, builder, 1, in comparers));

        SequenceNotEqual(
            factory.New(1, builder, 1, in comparers), 
            factory.New(9, builder, 9, in comparers));

        SequenceNotEqual(
            factory.Empty(in comparers),
            factory.New(1, builder, 1, in comparers2));

        SequenceNotEqual(
            factory.New(1, builder, 1, in comparers),
            factory.New(9, builder, 9, in comparers2));
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
}