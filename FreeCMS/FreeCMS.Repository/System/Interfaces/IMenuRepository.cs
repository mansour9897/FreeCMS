using FreeCMS.Common.Repository;
using FreeCMS.DomainModels.System;

namespace FreeCMS.Repository.System
{
    public interface IMenuRepository:IRepository<Menu,int>
    {
        Menu GetByName(string name);
    }
    
}