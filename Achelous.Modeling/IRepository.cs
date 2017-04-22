using System;

namespace Achelous.DomainModeling
{
    public interface IRepository
    {
        IResult Create(IEntity entity);
        IResult Retrieve(IRetrieveQuery query);
        IResult Update(IRetrieveQuery query, IEntity entity);
        IResult Delete(IRetrieveQuery query);
    }
}
