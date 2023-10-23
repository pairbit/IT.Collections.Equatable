# IT.Collections.Equatable.Factory
[![NuGet version (IT.Collections.Equatable.Factory)](https://img.shields.io/nuget/v/IT.Collections.Equatable.Factory.svg)](https://www.nuget.org/packages/IT.Collections.Equatable.Factory)
[![NuGet pre version (IT.Collections.Equatable.Factory)](https://img.shields.io/nuget/vpre/IT.Collections.Equatable.Factory.svg)](https://www.nuget.org/packages/IT.Collections.Equatable.Factory)

Implementation of collections factory for equatable

## Register factories

```csharp
var registry = new EnumerableFactoryRegistry();
//var registry = new ConcurrentEnumerableFactoryRegistry();
registry.RegisterFactoriesDefault();
registry.RegisterFactoriesEquatable(RegistrationBehavior.OverwriteExisting);
```

## New list and equals

```csharp
var listFactory = registry.GetFactory<ListFactory>();
Assert.That(listFactory is EquatableListFactory, Is.True);

var comparers = StringComparer.OrdinalIgnoreCase.ToComparers();

var list = listFactory.Empty(in comparers);
Assert.That(list is EquatableList<string?>, Is.True);

list.Add("ignoreCase");

var list2 = listFactory.Empty(in comparers);
list2.Add("IgNoReCaSe");
        
Assert.That(list, Is.EqualTo(list2));
Assert.That(list.GetHashCode(), Is.EqualTo(list2.GetHashCode()));
```