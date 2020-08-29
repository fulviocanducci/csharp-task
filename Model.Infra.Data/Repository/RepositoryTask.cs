using Model.Infra.Data.Context;
using Model.Infra.Data.Repository.Base;

namespace Model.Infra.Data.Repository
{
    public class RepositoryTask : RepositoryBase<Domain.Entities.Task>, IRepositoryTask
    {
        public RepositoryTask(DatabaseContext context) : base(context)
        {
        }
    }
}
