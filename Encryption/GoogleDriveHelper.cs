using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Encryption
{
    class GoogleDriveHelper
    {
        static string[] Scopes = { DriveService.Scope.DriveFile };
        static string ApplicationName = "TextEncryptor";
        private UserCredential userCredential;
        private DriveService driveService;
        private Google.Apis.Drive.v3.Data.File driveFile;
        public String fileData;
        public bool LoginToGoogle()
        {

            using (var stream =new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-textencryptor.json");

                try
                {
                    userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                      GoogleClientSecrets.Load(stream).Secrets,
                      Scopes,
                      "user",
                      CancellationToken.None,
                      new FileDataStore(credPath, true)).Result;
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool CreateService()
        {
            try
            {
                driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = userCredential,
                    ApplicationName = ApplicationName,
                });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool SearchFile(String fileName)
        {
            FilesResource.ListRequest listRequest = driveService.Files.List();
            listRequest.PageSize = 10;
            listRequest.Q = "name = '" + fileName + "' and trashed = false and mimeType = 'text/plain'";
            listRequest.Fields = "files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            if (files != null && files.Count > 0)
            {
                driveFile = files[0];
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CreateFile(String fileName, String data)
        {
            try
            {
                driveService.Files.Create(new Google.Apis.Drive.v3.Data.File
                {
                    Name = fileName,
                    MimeType = "text/plain"
                }, GenerateStreamFromString(data), "text/plain").Upload();
                return true;
            }catch (Exception e)
            {
                return false;
            }
        }

        public bool ReadFile(String fileName)
        {
            try
            {
                if (SearchFile(fileName))
                {
                    MemoryStream stream = new MemoryStream();
                    FilesResource.GetRequest getRequest = driveService.Files.Get(driveFile.Id);
                    getRequest.Download(stream);
                    stream.Position = 0;
                    StreamReader reader = new StreamReader(stream);
                    fileData = reader.ReadToEnd();
                }
                else
                {
                    return false;
                }
                return true;
            }catch (Exception e)
            {
                return false;
            }
        }

        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
