namespace Ornament.Domain.Uow
{
    /// <summary>
    /// Interface IUnitOfWorkFactory
    /// </summary>
    public interface IUnitOfWorkFactory
    {
       
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>IUnitOfWork.</returns>
        IUnitOfWork Create();
    }
}