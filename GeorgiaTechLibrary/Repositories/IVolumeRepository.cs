using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IVolumeRepository
    {
        //Task<IEnumerable<Volume>> GetVolumes();
        Task<IEnumerable<Volume>> GetAllVolumes(string ISBN);
        Task<IEnumerable<Volume>> GetAvailableVolumes(string ISBN);
        Task<Volume> GetAvailableVolume(string ISBN);   
        Task<Volume> GetVolume(string volumeId);
        Task<Volume> CreateVolume(Volume volume);

        
    }
}
