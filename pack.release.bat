dotnet pack src/Ornament.Domain -c:release -o:./
dotnet pack src/Ornament -c:release -o:./
dotnet pack src/Uow/Ornament.Uow.DbConnection -c:release -o:./
dotnet pack src/Ornament.Uow.Web -c:release -o:./
dotnet pack src/Uow/Ornament.Uow.Ef -c:release -o:./
copy *.nupkg c:\inetpub\wwwroot\packages /y