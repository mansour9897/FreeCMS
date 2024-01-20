using FreeCMS.DomainModels.System;

namespace FreeCMS.Service.System.Abstraction
{
    public interface ISocialNetworkService
    {
        List<SocialNetwork> List();
        SocialNetwork FindById(int id);
        SocialNetwork Add(SocialNetwork socialNetwork);
        SocialNetwork Update(SocialNetwork socialNetwork);
        void Delete(int id);
        void Delete(SocialNetwork socialNetwork);
    }
}