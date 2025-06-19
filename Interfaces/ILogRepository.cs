using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseControl.Models;

namespace FirebaseControl.Interfaces
{
    public interface ILogRepository
    {
        Task<List<LogEntry>> GetAllLogsAsync();
    }
}
