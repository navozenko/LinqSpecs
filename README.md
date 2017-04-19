![](https://github.com/navozenko/LinqSpecs/blob/master/logo.png)

LinqSpec is a framework that will help you to create specifications for LINQ queries. You can read more about the specification pattern [here](http://en.wikipedia.org/wiki/Specification_pattern).

Almost all users of LINQ create specifications in their daily work, but most of them write those specifications scattered all over the code. The idea behind this project is to help the user to write, test and expose specifications as first-class objects. You will learn how to use LinqSpec in this brief document.

### Defining simple specifications

In order to define our first specification named "CustomerFromCountrySpec" we need to inherit from Specification\<T\>:

```csharp
public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();
}
```

So this is our implementation:

```csharp
using LinqSpecs;

public class CustomerFromCountrySpec : Specification<Customer>
{
    public Country { get; set; }

    public CustomerFromCountrySpec(Country country)
    {
        Country = country;
    }

    public override Expression<Func<Customer, bool>> ToExpression()
    { 
        return c => c.Country == Country;
    }
}
```

Simple as is, to use this class, your repository or DAO should implement these kind of methods:

```csharp
public IEnumerable<T> Find(Specification<T> specification)
{
    return [a queryable source].Where(specification).ToList();
}

public int Count(Specification<T> specification)
{
    return [a queryable source].Count(specification);
}
```

The usage is very simple:

```csharp
var spec = new CustomerFromCountrySpec(Countries.Argentina);
var customersFromArgentina = customerRepository.Find(spec);
```

### Alternative way to expose specifications

An alternative way of exposing specifications is with a static class:

```csharp
public static CustomerSpecs
{
    public static Specification<Customer> FromCountry(Country country) 
    { 
        return new CustomerFromCountrySpec(country);
    }

    public static Specification<Customer> EligibleForDiscount(decimal discount)
    {
        return new AdHocSpecification<Customer>(
            c => c.IsPreferred && !c.HasDebt &&
                 c.LastPurchaseDate > DateTime.Today.AddDays(-30));
    }
}
```

Usage:

```csharp
customerRepository.Find(
    CustomerSpecs.FromCountry(argentina) &&
    CustomerSpecs.EligibleForDiscount(3223));
```

### Logical operations AND, OR, NOT

One of the most interesting features of LinqSpecs is that you can combine known specifications with "!", "&&" and "||" operators. For example:

```csharp
var spec1 = new CustomerFromCountrySpec(Countries.Argentina);
var spec2 = new CustomerPreferredSpec();
var result = customerRepository.Find(spec1 && !spec2);
```

This code returns all customers from Argentina that are not preferred. The & operator work as an && (AndAlso) operator. The same for | and ||.

### Comparing

The result of and'ing, or'ing and negating specifications implements equality members. That's said:

```csharp
// This returns true
(spec1 && spec2).Equals(spec1 && spec2);

// This returns true
(spec1 && (spec2 || !spec3)).Equals(spec1 && (spec2 || !spec3));

// This returns false, because AndAlso and OrElse are not commutable operations
(spec1 && spec2).Equals(spec2 && spec1);
```

This is an useful feature when you are writing Asserts in your unit tests.

### AdHocSpecification

The AdHocSpecification is an alternative way to write a specification without writing a class. You should not abuse of them, and try to write those in a single place as explained above. Also AdHocSpecification doesn't implement Equals, so two AdHocSpecifications are equal only if they are the same instance.

```csharp
var spec = new AdHocSpecification<Customer>(c => c.IsPreferred && !c.HasDebt);
```

### TrueSpecification and FalseSpecification

The TrueSpecification is satisfied by any object. The FalseSpecification is not satisfied by any object.

```csharp
// This returns all customers
customerRepository.Find(new TrueSpecification<Customer>());

// This returns nothing
customerRepository.Find(new FalseSpecification<Customer>());
```

These specifications can be useful when you want to retrieve all items from a data source or when you are building a chain of several specifications. For example:

```csharp
Specification<Customer> spec = new FalseSpecification<Customer>();
foreach (var country in countries)
    spec |= new CustomerFromCountrySpec(country);
return spec;
```

### In-memory queries

Although the LinqSpecs is targeted towards IQueryable\<T\> data source, it is possible to use LinqSpecs specifications for filtering IEnumerable\<T\> collections and also for other checks in memory:

```csharp
IEnumerable<Customer> customers = ...
var spec = new CustomerFromCountrySpec(Country.Argentina);
var result = customers.Where(spec.ToExpression().Compile());
```

Compiling of expression tree into a delegate is a very slow operation, so it's a good idea to cache the result of a compilation for reuse if it's possible.

# License

The LinqSpecs is open-sourced software licensed under the [Microsoft Public License (MS-PL)](https://opensource.org/licenses/MS-PL).

# Contributors

LinqSpecs was created by [José F. Romaniello](https://github.com/jfromaniello) and [Sergey Navozenko](https://github.com/navozenko).
