using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IVolumeRepository
    {
        //Task<IEnumerable<Volume>> GetVolumes();
        Task<IEnumerable<Volume>> GetVolumes(string ISBN);
        Task<Volume> GetVolume(string volumeId);
        Task<Volume> CreateVolume(Volume volume);
    }
}
