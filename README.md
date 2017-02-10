# Ornament #
## Unit of work##

###Uow for NHibernate setting ###

install package from nuget 
`install-package Ornament.Uow.NHibernate'

```csharp
  var uowFactory = services
                .MsSql2008(option => { option.ConnectionString(Configuration.GetConnectionString("default")); })
  uowFactory.AddAssemblyOf(typeof(Startup)); //add FluentMappingClass from Assembly.
```
