# Ornament #
## Unit of work##

###Uow for NHibernate setting ###

install packages from nuget 

install-package `Ornament.Uow.NHibernate` and `Microsoft.NETCore.Portable.Compatibility`

change project.json
``` xml
"frameworks": {
    "netcoreapp1.0": {
      "imports": [
        "dotnet5.6",
        "portable-net45+win8",
        "net461" //add net461 
      ]
    }
  },
```


```csharp
  var uowFactory = services
                .MsSql2008(option => { 
                    option.ConnectionString(Configuration.GetConnectionString("default")); 
                    })
  uowFactory.AddAssemblyOf(typeof(Startup)); //add FluentMappingClass from Assembly.
```
