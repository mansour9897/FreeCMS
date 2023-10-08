using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;
using FreeCMS.Service.CMS.Abstraction;

namespace FreeCMS.Service.CMS.Implemented
{
	public class PostService : BaseService<Post, int>, IPostService
	{
		private readonly IPostTopicRepository _postTopicRepo;
		public PostService(IPostRepository repository, IPostTopicRepository postTopicRepo)
			: base(repository)
		{
			this._postTopicRepo = postTopicRepo;
		}
		public override Post Add(Post post)
		{
			post.CreationDate = DateTime.Now;
			post.LastModified = DateTime.Now;
			return base.Add(post);
		}
		public override bool Update(Post post)
		{
			post.LastModified = DateTime.Now;
			return base.Update(post);
		}
		public PostTopic AddPostToTopic(int postId, int topicId)
		{
			return _postTopicRepo.Add(postId, topicId);
		}
		public PostTopic RemovePostFromTopic(int postId, int topicId)
		{
			return _postTopicRepo.Delete(postId, topicId);
		}
		public void AddPostToTopics(int postId, int[] topics)
		{
			foreach (int topicId in topics)
			{
				AddPostToTopic(postId, topicId);
			}
		}
		public void RemovePostFromAllTopics(int postId)
		{
			_postTopicRepo.RemovePostFromTopics(postId);
		}
		public List<Post> GetPublishedPosts()
		{
			return this.List(p => p.IsPublished == true).ToList();
		}
		public List<Post> GetPostsByTopicId(int topicId, bool isPublished)
		{
			return this.List(p => p.IsPublished == isPublished &&
				p.PostTopics.Select(pt => pt.TopicId).Contains(topicId)).ToList();
		}
		public List<Post> GetMostViewedPosts(int count)
		{
			return this.List(p => p.IsPublished == true).OrderByDescending(p => p.CountView).Take(count).ToList();
		}
		public List<Post> GetPostsByAuthorId(int authorId, bool isPublished)
		{
			return this.List(p => p.IsPublished == isPublished &&
				p.AuthorId == authorId).ToList();
		}
		public List<Post> Search(string query)
		{
			return this.List(p => p.IsPublished == true && (p.Title.Contains(query) || p.Body.Contains(query)))
				.OrderByDescending(p => p.CreationDate).ToList();
		}
		public List<Post> RelatedPosts(int postId, int count)
		{
			Post targetPost = this.FindById(postId);
			if (targetPost == null)
				return null;
			string[] targetPostKeywords = targetPost.MetaKeywords.Split(',');
			List<Post> result = new List<Post>();

			//find realted posts based on keywords
			foreach (string keyword in targetPostKeywords)
			{
				List<Post> foundedPosts = this.List(p => (p.Id != postId && p.MetaKeywords.Contains(keyword)))
					.ToList();
				//add posts that there is not in result
				//avoid repeated posts
				result.AddRange(foundedPosts.Except(result));
			}
			return result.Take(count).ToList();
		}
	}
}
