using CloudinaryDotNet.Actions;

namespace GoDate.API.Services.PhotoService
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync (IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}


// <ImageUploadResult> type from cloudinarydotnet
// IFormFile is a file sent with the http request, typically used in file uploads