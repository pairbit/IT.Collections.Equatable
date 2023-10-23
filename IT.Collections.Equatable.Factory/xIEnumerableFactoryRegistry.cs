using IT.Collections.Factory;
using IT.Collections.Factory.Factories;

namespace IT.Collections.Equatable.Factory;

public static class xIEnumerableFactoryRegistry
{
    public static bool TryRegisterFactoriesEquatableOnlyClasses(this IEnumerableFactoryRegistry registry, RegistrationBehavior behavior)
        => registry.TryRegisterFactory<EquatableListFactory>(behavior) &
           registry.TryRegisterFactory<EquatableLinkedListFactory>(behavior) &
           registry.TryRegisterFactory<EquatableHashSetFactory>(behavior) &
           registry.TryRegisterFactory<EquatableSortedSetFactory>(behavior) &
           registry.TryRegisterFactory<EquatableStackFactory>(behavior) &
           registry.TryRegisterFactory<EquatableQueueFactory>(behavior) &
           registry.TryRegisterFactory<EquatableDictionaryFactory>(behavior);

    public static bool TryRegisterFactoriesEquatableOnlyClassesOverride(this IEnumerableFactoryRegistry registry, RegistrationBehavior behavior)
        => registry.TryRegisterFactory<ListFactory, EquatableListFactory>(behavior) &
           registry.TryRegisterFactory<LinkedListFactory, EquatableLinkedListFactory>(behavior) &
           registry.TryRegisterFactory<HashSetFactory, EquatableHashSetFactory>(behavior) &
           registry.TryRegisterFactory<SortedSetFactory, EquatableSortedSetFactory>(behavior) &
           registry.TryRegisterFactory<StackFactory, EquatableStackFactory>(behavior) &
           registry.TryRegisterFactory<QueueFactory, EquatableQueueFactory>(behavior) &
           registry.TryRegisterFactory<DictionaryFactory, EquatableDictionaryFactory>(behavior);

    public static bool TryRegisterFactoriesEquatableOnlyInterfaces(this IEnumerableFactoryRegistry registry, RegistrationBehavior behavior)
        => registry.TryRegisterFactory<IEnumerableFactory, EquatableLinkedListFactory>(behavior) &
           registry.TryRegisterFactory<ICollectionFactory, EquatableLinkedListFactory>(behavior) &
           registry.TryRegisterFactory<IListFactory, EquatableListFactory>(behavior) &
           registry.TryRegisterFactory<ISetFactory, EquatableHashSetFactory>(behavior) &
           registry.TryRegisterFactory<IDictionaryFactory, EquatableDictionaryFactory>(behavior);

    public static bool TryRegisterFactoriesEquatable(this IEnumerableFactoryRegistry registry, RegistrationBehavior behavior)
        => registry.TryRegisterFactoriesEquatableOnlyClasses(behavior) &
           registry.TryRegisterFactoriesEquatableOnlyClassesOverride(behavior) &
           registry.TryRegisterFactoriesEquatableOnlyInterfaces(behavior);

    public static IEnumerableFactoryRegistry RegisterFactoriesEquatableOnlyClasses(this IEnumerableFactoryRegistry registry,
        RegistrationBehavior behavior = RegistrationBehavior.ThrowOnExisting)
    {
        registry.TryRegisterFactoriesEquatableOnlyClasses(behavior);
        return registry;
    }

    public static IEnumerableFactoryRegistry RegisterFactoriesEquatableOnlyClassesOverride(this IEnumerableFactoryRegistry registry,
        RegistrationBehavior behavior = RegistrationBehavior.ThrowOnExisting)
    {
        registry.TryRegisterFactoriesEquatableOnlyClassesOverride(behavior);
        return registry;
    }

    public static IEnumerableFactoryRegistry RegisterFactoriesEquatableOnlyInterfaces(this IEnumerableFactoryRegistry registry,
        RegistrationBehavior behavior = RegistrationBehavior.ThrowOnExisting)
    {
        registry.TryRegisterFactoriesEquatableOnlyInterfaces(behavior);
        return registry;
    }

    public static IEnumerableFactoryRegistry RegisterFactoriesEquatable(this IEnumerableFactoryRegistry registry,
        RegistrationBehavior behavior = RegistrationBehavior.ThrowOnExisting)
    {
        registry.TryRegisterFactoriesEquatable(behavior);
        return registry;
    }
}