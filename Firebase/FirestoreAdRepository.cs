using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseControl.Interfaces;
using FirebaseControl.Models;
using Google.Cloud.Firestore;

namespace FirebaseControl.Firebase
{
    public class FirestoreAdRepository : IAdRepository
    {
        private readonly FirestoreDb _db;

        public FirestoreAdRepository(FirestoreDb db)
        {
            _db = db;
        }

        public async Task SaveCustomAdAsync(CustomAdModel ad)
        {
            DocumentReference docRef = _db.Collection("ads").Document(ad.Id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                Console.WriteLine($"⚠ Документ с ID '{ad.Id}' уже существует. Добавление отменено.");
                return;
            }

            var dict = new Dictionary<string, object>
            {
                { "id", ad.Id },
                { "type", ad.Type },
                { "title", ad.Title },
                { "message", ad.Message },
                { "imageUrl", ad.ImageUrl },
                { "buttonUrl", ad.ButtonUrl },
                { "buttonText", ad.ButtonText },
                { "showButton", ad.ShowButton },
                { "day", ad.Day },
                { "priority", ad.Priority },
                { "timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") }
            };

            await docRef.SetAsync(dict);
        }


        public async Task SaveAdmobAdAsync(AdmobAdModel ad)
        {
            DocumentReference docRef = _db.Collection("ads").Document(ad.Id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                Console.WriteLine($"⚠ Документ с ID '{ad.Id}' уже существует. Добавление отменено.");
                return;
            }

            var dict = new Dictionary<string, object>
            {
                { "id", ad.Id },
                { "type", ad.Type },
                { "adFormat", ad.AdFormat },
                { "adUnitId", ad.AdUnitId },
                { "show", ad.Show },
                { "day", ad.Day },
                { "priority", ad.Priority }
            };

            await docRef.SetAsync(dict);
        }

    }
}
