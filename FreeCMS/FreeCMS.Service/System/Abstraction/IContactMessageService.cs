using FreeCMS.DomainModels.System;

namespace FreeCMS.Service.System.Abstraction
{
    public interface IContactMessageService
    {
        List<ContactMessage> GetAll();
        ContactMessage FindById(int id);
        ContactMessage Add(ContactMessage message);
        bool Remove(ContactMessage message);
        bool Remove(int id);
    }
    
}