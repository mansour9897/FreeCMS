using FreeCMS.Common.Repository;
using FreeCMS.DAL;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;

namespace FreeCMS.Repository.CMS.Implemented
{
	public class TopicRepository : BaseRepository<Topic, int>, ITopicRepository
	{
		public TopicRepository(FreeCMSContext context)
			: base(context) { }
	}
}
