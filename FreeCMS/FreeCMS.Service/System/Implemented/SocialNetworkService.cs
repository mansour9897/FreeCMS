using FreeCMS.DomainModels.System;
using FreeCMS.Repository.System;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System.Implemented
{
    public class SocialNetworkService:ISocialNetworkService
    {
        #region variables
        private readonly ISocialNetworkRepository _socialNetworkRepository;
        #endregion

        #region constructors 
        public  SocialNetworkService(ISocialNetworkRepository socialNetworkRepository)
        {
            this._socialNetworkRepository = socialNetworkRepository;
        }
        #endregion

        #region methods
        public List<SocialNetwork> List()
        {
            return this._socialNetworkRepository.List() as List<SocialNetwork>;
        }   
        public SocialNetwork FindById(int id)
        {
            return this._socialNetworkRepository.Get(id);
        }
        public SocialNetwork Add(SocialNetwork socialNetwork)
        {
            socialNetwork.CreationDate = DateTime.Now;
            return this._socialNetworkRepository.Insert(socialNetwork);
        }
        public SocialNetwork Update(SocialNetwork socialNetwork)
        {
            return this._socialNetworkRepository.Update(socialNetwork);
        }
        public void Delete(int id)
        {
            SocialNetwork targetLink = this._socialNetworkRepository.Get(id);
            if(targetLink != null)
            {
                this.Delete(targetLink);
            }
        }
        public void Delete(SocialNetwork socialNetwork)
        {
            this._socialNetworkRepository.Delete(socialNetwork);
        }
        #endregion
    }
}