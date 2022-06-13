using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IVolumeRepository
    {
        //Task<IEnumerable<Volume>> GetVolumes();
        Task<IEnumerable<Volume>> GetAllVolumes(string ISBN);
        Task<IEnumerable<Volume>> GetAvailableVolumes(string ISBN);
        Task<Volume> GetAvailableVolume(string ISBN);   
        Task<Volume> GetVolume(string volume_id);
        Task<Volume> CreateVolume(VolumeDTO volume);
        Task<bool> SetVolumeToUnavailable(string volume_id);
        Task<bool> IsVolumeAvailable(string volume_id);


    }
}
