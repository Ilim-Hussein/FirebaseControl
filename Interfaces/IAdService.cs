using System.Threading.Tasks;
using FirebaseControl.Models;

namespace FirebaseControl.Interfaces
{
    public interface IAdService
    {
        Task AddCustomAdAsync(CustomAdModel ad);
        Task AddAdmobAdAsync(AdmobAdModel ad);
    }
}
