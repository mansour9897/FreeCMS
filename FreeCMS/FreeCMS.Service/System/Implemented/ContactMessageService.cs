using FreeCMS.DomainModels.System;
using FreeCMS.Repository.System;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System.Implemented
{
    public class ContactMessageService:IContactMessageService
    {
        #region variables ...
        private readonly IContactMessageRepository _contactRepo;
        #endregion

        #region constructors ...
        public ContactMessageService(IContactMessageRepository contactRepo)
        {
            this._contactRepo = contactRepo;
        }
        #endregion

        #region methods ...
        public List<ContactMessage> GetAll()
        {
            return _contactRepo.List() as List<ContactMessage>;
        }
        public ContactMessage FindById(int id)
        {
            return _contactRepo.Get(id);
        }
        public ContactMessage Add(ContactMessage message)
        {
            message.CreateDate = DateTime.Now;
            return _contactRepo.Insert(message);
        }
        public bool Remove(ContactMessage message)
        {
            if(_contactRepo.Delete(message) != null)
                return true;
            return false;
        }
        public bool Remove(int id)
        {
            ContactMessage message = _contactRepo.Get(id);
            if(message != null)
            {
                return this.Remove(message);
            }
            return false;
        }
        #endregion
    }
    
}