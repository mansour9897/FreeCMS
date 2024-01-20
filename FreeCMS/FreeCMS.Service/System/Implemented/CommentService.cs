using FreeCMS.DomainModels.System;
using FreeCMS.Service.System.Abstraction;
using System.Linq.Expressions;

namespace FreeCMS.Service.System.Implemented
{
    public class CommentService:ICommentService
    {
        #region variables ...
        private readonly ICommentRepository _commentRepo;
        #endregion

        #region  constructors ...
        public CommentService(ICommentRepository commentRepo)
        {
            this._commentRepo = commentRepo;
        }
        #endregion
        #region properties ...
        public bool AutoVerify{ get; set;}
        #endregion
        #region methods ...
        public List<Comment> GetAll()
        {
            return _commentRepo.List() as List<Comment>;
        }
        public List<Comment> GetAll(Expression<Func<Comment,bool>> expression)
        {
            return _commentRepo.List(expression) as List<Comment>;
        }
        public List<Comment> GetAllBySystemObjectId(int sysObjId)
        {
            return this.GetAll(c => c.SystemObjectId == sysObjId);
        }
        public List<Comment> GetAllByRecordId(int sysObjId,string recordId)
        {
            return this.GetAll(c => c.SystemObjectId == sysObjId && c.SystemObjectRecordId == recordId);
        }
        public List<Comment> GetAllByRecordId(int sysObjId,string recordId,bool isVerified = true)
        {
            return this.GetAll(c => c.SystemObjectId == sysObjId && c.SystemObjectRecordId
                == recordId && c.IsVerified == isVerified);
        }
        public Comment FindById(int id)
        {
            return _commentRepo.Get(id);
        }
        public Comment Add(Comment comment)
        {
            comment.CreateDate = DateTime.Now;
            comment.Reported = false;
            comment.IsVerified = false;
            if(this.AutoVerify)
            {
                comment.IsVerified = true;
            }
            return _commentRepo.Insert(comment);
        }
        public bool Delete(Comment comment)
        {
            Comment temp = _commentRepo.Delete(comment);
            return temp==null ? false:true;
        }
        public bool Delete(int id)
        {
            Comment temp = _commentRepo.Get(id);
            if(temp != null)
            {
                return this.Delete(temp);
            }
            return false;
        }
        public Comment Update(Comment comment)
        {
            return _commentRepo.Update(comment);
        }
        public Comment VerifyById(int id)
        {
            Comment temp = _commentRepo.Get(id);
            if(temp != null)
            {
                temp.IsVerified = true;
                return _commentRepo.Update(temp);
            }
            return null;
        }
        public Comment RejectById(int id)
        {
            Comment temp = _commentRepo.Get(id);
            if(temp != null)
            {
                temp.IsVerified = false;
                return _commentRepo.Update(temp);
            }
            return null;
        }
        public Comment ReportById(int id)
        {
            Comment temp = _commentRepo.Get(id);
            if(temp != null)
            {
                temp.Reported = true;
                return _commentRepo.Update(temp);
            }
            return null;
        }
        public Comment DisreportById(int id)
        {
            Comment temp = _commentRepo.Get(id);
            if(temp != null)
            {
                temp.Reported = false;
                return _commentRepo.Update(temp);
            }
            return null;
        }
        #endregion
    }
}