using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.Identity;

namespace FreeCMS.Repository.System
{
    public class PermissionRepository:BaseRepository<Permission,int>,IPermissionRepository
    {
        public PermissionRepository(FreeCMSContext uow)
            :base(uow)
        {}
        
    }
    
}