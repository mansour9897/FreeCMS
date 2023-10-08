using FreeCMS.DAL;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCMS.Repository.CMS.Implemented
{
	public class PostTopicRepository : IPostTopicRepository
	{
		#region variables
		private readonly FreeCMSContext _context;
		#endregion

		#region constructors
		public PostTopicRepository(FreeCMSContext context)
		{
			this._context = context;
		}
		#endregion

		#region methods
		public PostTopic FindById(int postId, int topicId)
		{
			return _context.Set<PostTopic>().ToList().FirstOrDefault(pt => pt.PostId == postId &&
				 pt.TopicId == topicId);
		}
		public PostTopic Add(int postId, int topicId)
		{
			return this.Add(new PostTopic { PostId = postId, TopicId = topicId });
		}
		public PostTopic Add(PostTopic postTopic)
		{
			_context.Set<PostTopic>().Add(postTopic);
			_context.SaveChanges();
			return postTopic;
		}
		public PostTopic Delete(int postId, int topicId)
		{
			return this.Delete(new PostTopic { PostId = postId, TopicId = topicId });
		}
		public PostTopic Delete(PostTopic postTopic)
		{
			_context.Set<PostTopic>().Remove(postTopic);
			_context.SaveChanges();
			return postTopic;
		}
		public bool Exists(int postId, int topicId)
		{
			if (FindById(postId, topicId) != null)
				return true;
			return false;
		}
		public bool Exists(PostTopic postTopic)
		{
			if (FindById(postTopic.PostId, postTopic.TopicId) != null)
				return true;
			return false;
		}
		public void RemovePostFromTopics(int postId)
		{
			var postTopics = _context.Set<PostTopic>().ToList().Where(pt => pt.PostId == postId).ToList();
			foreach (var postTopic in postTopics)
			{
				Delete(postTopic);
			}
		}
		#endregion
	}
}
