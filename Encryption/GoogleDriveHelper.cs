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

            //using (var stream =new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            //{
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-textencryptor.json");

                try
                {
                    String clientId = "11399004738-s5gbs77m93l1p8k5emj489b0ou1acg4q.apps.googleusercontent.com";
                    String clientSecret = "Hp7F0PLIaDVu54AeO_GlhWZt";
                    userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                      new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret },
                      Scopes,
                      "user",
                      CancellationToken.None,
                      new FileDataStore(credPath, true)).Result;
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error logging in to Google Drive : " + e.Message);
                    return false;
                }
            //}
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
                Console.WriteLine("Error creating Google Drive Service : " + e.Message);
                return false;
            }
        }
        public bool SearchFile(String fileName)
        {
            FilesResource.ListRequest listRequest = driveService.Files.List();
            listRequest.Q = "name = '" + fileName + "' and trashed = false and mimeType = 'text/plain'";
            listRequest.Fields = "files(id, name, trashed, mimeType)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            if (files != null && files.Count > 0)
            {
                driveFile = files[0];
                return true;
            }
            else
            {
                Console.WriteLine("No files found in Google Drive matching the filter : " + listRequest.Q.ToString());
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
                Console.WriteLine("Error creating file : " + e.Message);
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
                    fileData = removeNewLineCharacters(fileData);
                }
                else
                {
                    Console.WriteLine("Error searching for file");
                    return false;
                }
                return true;
            }catch (Exception e)
            {
                Console.WriteLine("Error ... " + e.Message);
                return false;
            }
        }

        public bool WriteFile(String fileName, String data)
        {
            try
            {
                if (CreateService())
                {
                    
                    if (SearchFile(fileName))
                    {
                        Google.Apis.Upload.IUploadProgress uploadProgress = driveService.Files.Update(new Google.Apis.Drive.v3.Data.File
                            {
                                Name = fileName,
                                MimeType = "text/plain"
                            }, driveFile.Id, GenerateStreamFromString(data), "text/plain").Upload();
                        Google.Apis.Upload.UploadStatus status = uploadProgress.Status;
                        
                        if (status == Google.Apis.Upload.UploadStatus.Completed)
                        {
                            return true;
                        }
                        else
                        {
                            switch (status)
                            {
                                case Google.Apis.Upload.UploadStatus.Completed:
                                    return true;
                                case Google.Apis.Upload.UploadStatus.Failed:
                                    Console.WriteLine("Error uploading content : "+ uploadProgress.Exception.Message);
                                    return false;
                                default:
                                    Console.WriteLine(status);
                                    return false;

                            }
                        }
                        
                    }
                    else
                    {
                        return CreateFile(fileName,data);
                    }
                }
                else
                {
                    Console.WriteLine("Error creating Google Drive Service");
                    return false;
                }
            }catch (Exception e)
            {
                Console.WriteLine("Error in writing file : " + e.Message);
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
        private String removeNewLineCharacters(String sourceData)
        {
            sourceData = sourceData.Replace("\r\n", "");
            sourceData = sourceData.Replace("\r", "");
            sourceData = sourceData.Replace("\n", "");
            return sourceData;
        }

        private String updateNewLineCharacters(String sourceData)
        {
            if (sourceData != null)
            {
                sourceData = sourceData.Replace("\r\n", "\n");
                sourceData = sourceData.Replace("\n", "\r\n");
            }
            return sourceData;
        }
    }
}
