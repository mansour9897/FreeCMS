using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;

namespace FreeCMS.Service.CMS.Abstraction
{
	public interface IPostService : IService<Post, int>
	{
		PostTopic AddPostToTopic(int postId, int topicId);
		PostTopic RemovePostFromTopic(int postId, int topicId);
		void AddPostToTopics(int postId, int[] topics);
		void RemovePostFromAllTopics(int postId);
		List<Post> GetPublishedPosts();
		List<Post> GetPostsByTopicId(int topicId, bool isPublished);
		List<Post> GetMostViewedPosts(int count);
		List<Post> GetPostsByAuthorId(int authorId, bool isPublished);
		List<Post> Search(string query);
		List<Post> RelatedPosts(int postId, int count);
	}
}
