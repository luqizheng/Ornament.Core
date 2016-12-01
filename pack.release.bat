dotnet pack src/Ornament.Domain -c:release -o:./
dotnet pack src/Uow/Ornament.Uow.DbConnection -c:release -o:./

copy *.nupkg c:\inetpub\wwwroot\packages /y