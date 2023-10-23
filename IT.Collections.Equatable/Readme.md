# IT.Collections.Equatable
[![NuGet version (IT.Collections.Equatable)](https://img.shields.io/nuget/v/IT.Collections.Equatable.svg)](https://www.nuget.org/packages/IT.Collections.Equatable)
[![NuGet pre version (IT.Collections.Equatable)](https://img.shields.io/nuget/vpre/IT.Collections.Equatable.svg)](https://www.nuget.org/packages/IT.Collections.Equatable)

Implementation of equatable collections

## Equals record with collections

```csharp
record SimpleRecord
{
    public EquatableList<string> List { get; set; }
}

var record1 = new SimpleRecord
{
    List = new EquatableList<string> { "a", "b", "c" }
};

var record2 = new SimpleRecord
{
    List = new EquatableList<string> { "a", "b", "c" }
};

Assert.That(record1.GetHashCode(), Is.EqualTo(record2.GetHashCode()));
Assert.That(record1, Is.EqualTo(record2));
```

## Set IEqualityComparer

```csharp
var record3 = new SimpleRecord
{
    List = new EquatableList<string>(StringComparer.OrdinalIgnoreCase)
    { "test", "abc", "8" }
};

var record4 = new SimpleRecord
{
    List = new EquatableList<string>(StringComparer.OrdinalIgnoreCase)
    { "TeSt", "aBc", "8" }
};

Assert.That(record1.GetHashCode(), Is.Not.EqualTo(record3.GetHashCode()));
Assert.That(record1, Is.Not.EqualTo(record4));

Assert.That(record3.GetHashCode(), Is.EqualTo(record4.GetHashCode()));
Assert.That(record3, Is.EqualTo(record4));
```

## Empty collections with different comparators are not equal

```csharp
var record5 = new SimpleRecord
{
    List = new EquatableList<string>(StringComparer.OrdinalIgnoreCase)
};

var record6 = new SimpleRecord
{
    List = new EquatableList<string>(StringComparer.Ordinal)
};

Assert.That(record5.GetHashCode(), Is.Not.EqualTo(record6.GetHashCode()));
Assert.That(record5, Is.Not.EqualTo(record6));
```