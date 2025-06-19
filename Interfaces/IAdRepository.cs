using System.Threading.Tasks;
using FirebaseControl.Models;

namespace FirebaseControl.Interfaces
{
    public interface IAdRepository
    {
        Task SaveCustomAdAsync(CustomAdModel ad);
        Task SaveAdmobAdAsync(AdmobAdModel ad);
    }
}
