using FreeCMS.Common.Repository;
using FreeCMS.DomainModels.System;

namespace FreeCMS.DomainModels.System
{
    public interface ICommentRepository:IRepository<Comment,int>
    {
    }
}