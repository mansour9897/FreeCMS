using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.System;

namespace FreeCMS.Repository.System
{
    public class SocialNetworkRepository:BaseRepository<SocialNetwork,int>,ISocialNetworkRepository
    {
        public SocialNetworkRepository(FreeCMSContext uow)
            :base(uow)
            {}
    }
}