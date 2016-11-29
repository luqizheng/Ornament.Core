using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Ornament.Uow.DbConnection;

namespace Ornament.Uow
{
    public static class DbConnectionExtension
    {
        public static IServiceCollection AddDbUowForSqlServer(this IServiceCollection services,
            string connectionstring, bool transcation = false)
        {
            services.AddScoped(sp => new DbUow(new SqlConnection(connectionstring), transcation));
           return services; 
        }
    }
}
