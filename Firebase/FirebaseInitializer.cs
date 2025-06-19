using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using System.IO;

namespace FirebaseControl.Firebase
{
    public static class FirebaseInitializer
    {
        public static FirestoreDb Initialize(string projectId, string credentialsPath)
        {
            GoogleCredential credential;
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream);
            }

            var builder = new FirestoreDbBuilder
            {
                ProjectId = projectId,
                Credential = credential
            };

            return builder.Build();
        }
    }
}
