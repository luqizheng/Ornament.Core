dotnet build src/Ornament.Domain -c:Release -o:.\Bin\net461 --framework:net461
dotnet build src/Ornament -c:Release -o:.\Bin\net461 --framework:net461
dotnet build src/Uow/Ornament.Uow.DbConnection -c:Release -o:.\Bin\net461 --framework:net461
dotnet build src/Ornament.Uow.Web -c:Release -o:.\Bin\net461 --framework:net461

dotnet build src/Ornament.Domain -c:Release -o:.\Bin\netstandard1.6 --framework:netstandard1.6
dotnet build src/Ornament -c:Release -o:.\Bin\netstandard1.6 --framework:netstandard1.6
dotnet build src/Uow/Ornament.Uow.DbConnection -c:Release -o:.\Bin\net461 --framework:netstandard1.6
dotnet build src/Ornament.Uow.Web -c:Release -o:.\Bin\netstandard1.6 --framework:netstandard1.6