using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.System;
using Microsoft.EntityFrameworkCore;

namespace FreeCMS.Repository.System
{
    public class MenuRepository : BaseRepository<Menu, int>, IMenuRepository
    {
        FreeCMSContext _context;
        public MenuRepository(FreeCMSContext uow)
            : base(uow)
        {
            _context = uow;
        }
        public Menu GetByName(string name)
        {
            return _context.Set<Menu>().Include(m => m.MenuItems).Where(m => m.Name == name).FirstOrDefault();
        }
    }

}