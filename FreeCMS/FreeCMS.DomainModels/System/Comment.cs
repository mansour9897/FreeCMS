using FreeCMS.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCMS.DomainModels.System
{
	public enum CommentType
	{
		Guest,
		User
	}
	
	public class Comment
	{
		public int Id { get; set; }
		public CommentType Type { get; set; }
		public DateTime CreateDate { get; set; }
		public string SenderName { get; set; }
		public string SenderEmail { get; set; }
		public string Text { get; set; }
		public bool Reported { get; set; }
		public bool IsVerified { get; set; }

		public int SystemObjectId { get; set; }
		public string SystemObjectRecordId { get; set; }
		public int? UserId { get; set; }
		public virtual ApplicationUser User { get; set; }
		public virtual SystemObject SystemObject { get; set; }
	}
}
