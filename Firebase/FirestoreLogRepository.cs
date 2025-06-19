using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseControl.Interfaces;
using FirebaseControl.Models;
using Google.Cloud.Firestore;

namespace FirebaseControl.Firebase
{
    public class FirestoreLogRepository : ILogRepository
    {
        private readonly FirestoreDb _db;

        public FirestoreLogRepository(FirestoreDb db)
        {
            _db = db;
        }

        public async Task<List<LogEntry>> GetAllLogsAsync()
        {
            var logs = new List<LogEntry>();

            QuerySnapshot snapshot = await _db.Collection("ad_logs").GetSnapshotAsync();
            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    var dict = doc.ToDictionary();

                    string adId = dict.TryGetValue("adId", out var adIdObj) ? adIdObj?.ToString() : null;
                    string userId = dict.TryGetValue("userId", out var userIdObj) ? userIdObj?.ToString() : null;
                    string type = dict.TryGetValue("type", out var typeObj) ? typeObj?.ToString() : null;
                    string evt = dict.TryGetValue("event", out var eventObj) ? eventObj?.ToString() : null;
                    object tsObj = dict.TryGetValue("timestamp", out var tsVal) ? tsVal : null;

                    var log = new LogEntry
                    {
                        AdId = adId,
                        UserId = userId,
                        Type = type,
                        Event = evt,
                        Timestamp = ParseTimestamp(tsObj)
                    };

                    logs.Add(log);
                }
            }

            return logs;
        }

        private DateTime ParseTimestamp(object ts)
        {
            if (ts is Timestamp t)
                return t.ToDateTime();
            if (DateTime.TryParse(ts?.ToString(), out var parsed))
                return parsed;

            return DateTime.MinValue;
        }
    }
}
