using FreeCMS.DomainModels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FreeCMS.Service.System.Abstraction
{
	public interface ICommentService
	{
		bool AutoVerify { get; set; }
		List<Comment> GetAll();
		List<Comment> GetAll(Expression<Func<Comment, bool>> expression);
		List<Comment> GetAllBySystemObjectId(int sysObjId);
		List<Comment> GetAllByRecordId(int sysObjId, string recordId);
		List<Comment> GetAllByRecordId(int sysObjId, string recordId, bool isVerified = true);
		Comment FindById(int id);
		Comment Add(Comment comment);
		bool Delete(Comment comment);
		bool Delete(int id);
		Comment Update(Comment comment);
		Comment VerifyById(int id);
		Comment RejectById(int id);
		Comment ReportById(int id);
		Comment DisreportById(int id);
	}

}
