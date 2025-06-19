using FirebaseControl.Firebase;
using FirebaseControl.Interfaces;
using FirebaseControl.Models;
using FirebaseControl.Services;
using Google.Cloud.Firestore;
using System.Threading.Tasks;
using System;

namespace FirebaseControl
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Загружаем конфигурацию из JSON
            var config = ConfigLoader.LoadConfig();

            //Инициализируем Firestore по конфигу
            FirestoreDb db = FirebaseInitializer.Initialize(config.ProjectId, config.CredentialsPath);

            //Зависимости
            IAdRepository adRepo = new FirestoreAdRepository(db);
            ILogRepository logRepo = new FirestoreLogRepository(db);
            IUserMapper userMapper = UserMapping.LoadFromFile();

            IAdService adService = new AdService(adRepo);
            ILogService logService = new LogService(logRepo, userMapper);

            //Меню
            while (true)
            {
                Console.WriteLine("\n===== Меню =====");
                Console.WriteLine("1. Добавить Custom рекламу");
                Console.WriteLine("2. Добавить AdMob рекламу");
                Console.WriteLine("3. Анализ логов");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        await AddCustomAdAsync(adService);
                        break;
                    case "2":
                        await AddAdmobAdAsync(adService);
                        break;
                    case "3":
                        await logService.AnalyzeLogsAsync();
                        break;
                    case "0":
                        Console.WriteLine("Выход...");
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }

        static async Task AddCustomAdAsync(IAdService adService)
        {
            var ad = new CustomAdModel();

            Console.Write("ID (например, custom_005): ");
            ad.Id = Console.ReadLine();

            Console.Write("Заголовок: ");
            ad.Title = Console.ReadLine();

            Console.Write("Сообщение: ");
            ad.Message = Console.ReadLine();

            Console.Write("Ссылка на изображение: ");
            ad.ImageUrl = Console.ReadLine();

            Console.Write("Ссылка кнопки: ");
            ad.ButtonUrl = Console.ReadLine();

            Console.Write("Текст кнопки: ");
            ad.ButtonText = Console.ReadLine();

            Console.Write("Показывать кнопку? (true/false): ");
            ad.ShowButton = bool.TryParse(Console.ReadLine(), out var sb) && sb;

            Console.Write("Дата (yyyy-MM-dd): ");
            ad.Day = Console.ReadLine();

            Console.Write("Приоритет (целое число): ");
            ad.Priority = int.TryParse(Console.ReadLine(), out var p) ? p : 0;

            await adService.AddCustomAdAsync(ad);
            Console.WriteLine("✅ Custom реклама добавлена.");
        }

        static async Task AddAdmobAdAsync(IAdService adService)
        {
            var ad = new AdmobAdModel();

            Console.Write("ID (например, admob_005): ");
            ad.Id = Console.ReadLine();

            Console.Write("Ad Unit ID: ");
            ad.AdUnitId = Console.ReadLine();

            Console.Write("Формат (например, native): ");
            ad.AdFormat = Console.ReadLine();

            Console.Write("Показывать? (true/false): ");
            ad.Show = bool.TryParse(Console.ReadLine(), out var sh) && sh;

            Console.Write("Дата (yyyy-MM-dd): ");
            ad.Day = Console.ReadLine();

            Console.Write("Приоритет (целое число): ");
            ad.Priority = int.TryParse(Console.ReadLine(), out var p) ? p : 0;

            await adService.AddAdmobAdAsync(ad);
            Console.WriteLine("✅ AdMob реклама добавлена.");
        }
    }
}
