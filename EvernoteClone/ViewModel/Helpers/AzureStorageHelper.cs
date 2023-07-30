using Azure.Storage.Blobs;
using System.Threading.Tasks;

namespace EvernoteClone.ViewModel.Helpers
{
    public static class AzureStorageHelper
    {
        public static async Task<string> UpdateFile(string rtfFilePath, string fileName)
        {
            var connectionString = AppSecretsHelper.Read("StorageConnectionString");
            var containerName = "notes";
            var container = new BlobContainerClient(connectionString, containerName);

            var blob = container.GetBlobClient(fileName);
            await blob.UploadAsync(rtfFilePath);

            return $"https://evernotecloneapp.blob.core.windows.net/notes/{fileName}";
        }

        public static async Task<bool> DeleteFile(string fileName)
        {
            var connectionString = AppSecretsHelper.Read("StorageConnectionString");
            var containerName = "notes";
            var container = new BlobContainerClient(connectionString, containerName);

            var blob = container.GetBlobClient(fileName);
            var response = await blob.DeleteAsync();

            if (response.Status == 200)
                return true;
            else
                return false;
        }
    }
}
