call:pack src/Ornament.Domain 
call:pack src/Ornament 
call:pack src/Uow/Ornament.Uow.DbConnection 
call:pack src/Ornament.Uow.Web
call:pack src/Uow/Ornament.Uow.Ef 
call:pack src/Uow/Ornament.Uow.NHibernate 


:pack
dotnet restore %1
dotnet pack -c:release -o:%cd%  %1

GOTO:EOF

:publish
set /p nuget=«Î ‰»Îurl:
set /p webkey=«Î ‰»ÎapiKey:
for %%i in (*.nupkg) do (
 dotnet nuget push -s %nuget% %%i -k %webkey%
)
goto:eof