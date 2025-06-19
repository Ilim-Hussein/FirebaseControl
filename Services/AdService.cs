using System;
using System.Threading.Tasks;
using FirebaseControl.Interfaces;
using FirebaseControl.Models;

namespace FirebaseControl.Services
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _adRepository;

        public AdService(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public Task AddCustomAdAsync(CustomAdModel ad)
        {
            ad.Type = "custom";
            ad.Timestamp = DateTime.UtcNow;
            return _adRepository.SaveCustomAdAsync(ad);
        }

        public Task AddAdmobAdAsync(AdmobAdModel ad)
        {
            ad.Type = "admob";
            ad.Timestamp = DateTime.UtcNow;
            return _adRepository.SaveAdmobAdAsync(ad);
        }
    }
}
