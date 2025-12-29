namespace BusinessLogic.Services.AttachmentService
{
    public interface IAttachmentService
    {
        string? Upload(string folderName, IFormFile file);

        bool Delete(string folderName, string fileName);
    }
}
