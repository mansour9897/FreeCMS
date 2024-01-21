using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.System;

namespace FreeCMS.Repository.System
{
    public class MenuItemRepository:BaseRepository<MenuItem,Guid>,IMenuItemRepository
    {
        public MenuItemRepository(FreeCMSContext uow)
            :base(uow)
            {}
        
    }
    
}