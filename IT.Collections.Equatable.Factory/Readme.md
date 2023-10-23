# IT.Collections.Equatable.Factory
[![NuGet version (IT.Collections.Equatable.Factory)](https://img.shields.io/nuget/v/IT.Collections.Equatable.Factory.svg)](https://www.nuget.org/packages/IT.Collections.Equatable.Factory)
[![NuGet pre version (IT.Collections.Equatable.Factory)](https://img.shields.io/nuget/vpre/IT.Collections.Equatable.Factory.svg)](https://www.nuget.org/packages/IT.Collections.Equatable.Factory)

Implementation of collections factory for equatable

## Register factories

```csharp
var registry = new EnumerableFactoryRegistry();
//var registry = new ConcurrentEnumerableFactoryRegistry();
registry.RegisterFactoriesDefault()
		.RegisterFactoriesEquatable(RegistrationBehavior.OverwriteExisting);
```