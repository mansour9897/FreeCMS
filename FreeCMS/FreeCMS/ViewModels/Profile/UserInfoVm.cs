using FreeCMS.DomainModels.Identity;
namespace FreeCMS.ViewModels.Profile
{
    
    public class UserInfoVm
    {
        public UserInfoVm(){}
        public UserInfoVm(ApplicationUser user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.UserName = user.UserName;
            this.Active = user.Active;
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[] Image { get; set; }
        public bool Active { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
    
}