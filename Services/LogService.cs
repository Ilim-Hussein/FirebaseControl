using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FirebaseControl.Interfaces;

namespace FirebaseControl.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IUserMapper _userMapper;

        public LogService(ILogRepository logRepository, IUserMapper userMapper)
        {
            _logRepository = logRepository;
            _userMapper = userMapper;
        }

        public async Task AnalyzeLogsAsync()
        {
            var logs = await _logRepository.GetAllLogsAsync();
            int totalViews = 0;
            int totalClicks = 0;


            if (logs == null || logs.Count == 0)
            {
                Console.WriteLine("Логи отсутствуют.");
                return;
            }

            var grouped = logs
                .GroupBy(log => new { log.UserId, log.AdId })
                .OrderBy(g => g.Key.UserId);

            foreach (var group in grouped)
            {
                string readableId = _userMapper.GetReadableId(group.Key.UserId);
                bool wasViewed = group.Any(l => l.Event == "view");
                bool wasClicked = group.Any(l => l.Event == "click");
                string result = " | ";

                if (wasViewed && wasClicked)
                { 
                    result += "просмотрел и кликнул"; 
                    totalClicks++; 
                }

                else
                    result += "действий нет";
                    totalViews++;

                Console.WriteLine($"{readableId} просмотрел рекламу {group.Key.AdId} — {group.Count()} раз" + result);
            }

            Console.WriteLine("\n📊 Сводка:");
            Console.WriteLine($"👁️ Всего просмотров: {totalViews}");
            Console.WriteLine($"🖱️ Всего кликов: {totalClicks}");
            _userMapper.Save();
        }
    }
}