using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;

namespace FreeCMS.Service.CMS.Abstraction
{
	public interface ITopicService : IService<Topic, int>
	{
		IList<Topic> GetTopicsExpectChildren(int id);
		IList<Topic> GetRootTopics();
		bool TopicExists(string name);
		bool TopicExists(string name, int id);
		Topic FindByName(string name);
	}
}
