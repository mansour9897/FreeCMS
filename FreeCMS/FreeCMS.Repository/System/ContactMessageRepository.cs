using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.System;

namespace FreeCMS.Repository.System
{
    public class ContactMessageRepository:BaseRepository<ContactMessage,int>,IContactMessageRepository
    {   
        public ContactMessageRepository(FreeCMSContext uow)
            :base(uow)
            {}
    }
    
}