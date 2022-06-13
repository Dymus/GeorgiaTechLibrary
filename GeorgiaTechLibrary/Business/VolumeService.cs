using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;

namespace GeorgiaTechLibrary.Business
{
    public class VolumeService
    {
        private readonly IVolumeRepository _volumeRepository;
        public VolumeService(IVolumeRepository volumeRepository)
        {
            _volumeRepository = volumeRepository;
        }
        public async Task<IEnumerable<Volume>> CreateVolumes(List<VolumeDTO> volumes)
        {
            List<Volume> insertedVolumes = new List<Volume>();
            foreach(VolumeDTO volume in volumes)
            {
                insertedVolumes.Add(await _volumeRepository.CreateVolume(volume));
            }
            return insertedVolumes;
        }

        public Task<IEnumerable<Volume>> GetAllVolumes(string ISBN) => _volumeRepository.GetAllVolumes(ISBN);
        public Task<Volume> GetVolume(string volumeId) => _volumeRepository.GetVolume(volumeId);   
        public Task<IEnumerable<Volume>> GetAvailableVolumes(string ISBN) => _volumeRepository.GetAvailableVolumes(ISBN);
        public Task<Volume> GetAvailableVolume(string ISBN) => _volumeRepository.GetAvailableVolume(ISBN);

    }
}
