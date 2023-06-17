using WebAPI.Models;
using WebAPI.Repository.Data;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository.Implementation
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly ApiDbContext _dbContext;
        public MasterDataRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IsAdminMasterData GetRandomMasterData()
        {
            if (!_dbContext.IsAdminMasterData.Any())
            {
                throw new InvalidOperationException("IsAdminMasterData table is empty");
            }

            var random = new Random();
            var totalCount = _dbContext.IsAdminMasterData.Count();
            var skipCount = random.Next(0, totalCount);

            var masterData = _dbContext.IsAdminMasterData
                .OrderBy(product => Guid.NewGuid())
                .Skip(skipCount)
                .FirstOrDefault();

            return masterData;
        }
    }
}
