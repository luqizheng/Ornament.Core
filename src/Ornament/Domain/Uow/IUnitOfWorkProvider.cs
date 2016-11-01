namespace Ornament.Domain.Uow
{
    public interface IUnitOfWorkProvider
    {
                IUnitOfWork Get();        
    }
}