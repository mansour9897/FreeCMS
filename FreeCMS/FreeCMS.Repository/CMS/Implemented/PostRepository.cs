using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;

namespace FreeCMS.Repository.CMS.Implemented
{
	public class PostRepository : BaseRepository<Post, int>, IPostRepository
	{
		public PostRepository(FreeCMSContext context) : base(context) { }
	}
}
