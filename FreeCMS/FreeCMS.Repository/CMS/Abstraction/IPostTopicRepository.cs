using FreeCMS.DomainModels.Cms;

namespace FreeCMS.Repository.CMS.Abstraction
{
	public interface IPostTopicRepository
	{
		PostTopic FindById(int postId, int topicId);
		PostTopic Add(int postId, int topicId);
		PostTopic Add(PostTopic postTopic);
		PostTopic Delete(int postId, int topicId);
		PostTopic Delete(PostTopic postTopic);
		bool Exists(int postId, int topicId);
		bool Exists(PostTopic postTopic);
		void RemovePostFromTopics(int postId);
		IList<PostTopic> GetPostTopics(int postId);
	}
}
