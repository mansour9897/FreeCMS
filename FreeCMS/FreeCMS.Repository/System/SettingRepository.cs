using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.System;

namespace FreeCMS.Repository.System
{
    public class SettingRepository : BaseRepository<Setting, int>, ISettingRepository
    {
        public SettingRepository(FreeCMSContext uow)
            : base(uow)
        { }
    }

}