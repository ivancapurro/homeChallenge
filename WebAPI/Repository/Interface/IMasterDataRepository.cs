using WebAPI.Models;

namespace WebAPI.Repository.Interface
{
    public interface IMasterDataRepository
    {
        IsAdminMasterData GetRandomMasterData();
    }
}
