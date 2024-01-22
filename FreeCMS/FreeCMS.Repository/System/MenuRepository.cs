using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.System;

namespace FreeCMS.Repository.System
{
    public class MenuRepository:BaseRepository<Menu,int>,IMenuRepository
    {
        public MenuRepository(FreeCMSContext uow)
            :base(uow)
            {}
        public Menu GetByName(string name)
        {
            return this.List(mt => mt.Name == name).FirstOrDefault();
        }
    }
    
}