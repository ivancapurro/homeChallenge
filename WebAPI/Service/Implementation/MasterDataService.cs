using WebAPI.Models;
using WebAPI.Repository.Interface;
using WebAPI.Service.Interface;

namespace WebAPI.Service.Implementation
{
    public class MasterDataService : IMasterDataService

    {

        private readonly IMasterDataRepository _MasterDataRepository;
        public MasterDataService(IMasterDataRepository masterDataRepository) {
            _MasterDataRepository = masterDataRepository;
        }

        public IsAdminMasterData GetRandomMasterData()
        {
           return _MasterDataRepository.GetRandomMasterData();
        }
    }

}
