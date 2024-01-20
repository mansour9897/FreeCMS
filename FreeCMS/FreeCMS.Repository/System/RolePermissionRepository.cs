using FreeCMS.DAL;
using FreeCMS.DomainModels.Identity;

namespace FreeCMS.Repository.System
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly FreeCMSContext _context;
        public RolePermissionRepository(FreeCMSContext context)
        {
            _context = context;
        }
        public IList<RolePermission> GetAll()
        {
            return _context.Set<RolePermission>().ToList();
        }

        public IList<RolePermission> GetByRoleId(string roleId)
        {
            return _context.Set<RolePermission>().Where(r => r.RoleId == roleId).ToList();
        }
    }
}
