using AutoRepairMainCore.DTO.Models;
using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service.Implementations
{
    public class MediaService : IMediaService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private List<string> imageExtensions;
        private List<string> videoExtensions;
        private string baseDirectory;

        private void InitiateListsOfExtentions()
        {
            baseDirectory = "../AutoServices";

            imageExtensions = new List<string>
            {
                ".jpg", ".jpeg", ".png", ".bmp", ".webp", ".svg"
            };

            videoExtensions = new List<string>
            {
                ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".flv", ".webm", ".m4v"
            };
        }

        public MediaService(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;

            InitiateListsOfExtentions();
        }

        public async Task<string> SaveAutoServiceLogo(AutoService autoService, IFormFile logoFile)
        {
            IsFileEmpty(logoFile);
            string fileExtension = GetFileExtension(logoFile);
            CheckFileExtension(fileExtension, MediaType.image);

            string baseFileName = $"{autoService.Name}'s_Logo";
            string relativePath = $"{autoService.Name}/Logo";
            string directoryPath = CreateDirectory(relativePath);
            string filePath = GetUniqueFilePath(directoryPath, baseFileName, fileExtension);

            SaveFile(logoFile, filePath);
            _userService.UpdateAutoServiceLogoPath(autoService, filePath);

            return filePath;
        }
        private bool IsFileEmpty(IFormFile file)
        {
            if (file == null)
            {
                throw new InvalidOperationException("No file");
            }

            return true;
        }

        private string GetFileExtension(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(fileExtension))
            {
                throw new InvalidOperationException("Invalid file format: file must have an extension.");
            }
            return fileExtension;
        }

        private bool CheckFileExtension(string fileExtension, MediaType expectedType)
        {
            if (expectedType == MediaType.image)
            {
                if (!imageExtensions.Contains(fileExtension))
                {
                    throw new InvalidOperationException($"Invalid file extension. " +
                        $"Allowed image extensions are: {string.Join(", ", imageExtensions)}");
                }
            }

            else if (expectedType == MediaType.video)
            {
                if (!videoExtensions.Contains(fileExtension))
                {
                    throw new InvalidOperationException($"Invalid file extension. " +
                        $"Allowed video extensions are: {string.Join(", ", videoExtensions)}");
                }
            }

            return true;
        }

        private string CreateDirectory(string relativePath)
        {
            string fullPath = Path.Combine(baseDirectory, relativePath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            return fullPath;
        }

        private string GetUniqueFilePath(string directoryPath, string baseFileName, string fileExtension)
        {
            string uniqueFileName = $"{baseFileName}_{Guid.NewGuid()}{fileExtension}";
            return Path.Combine(directoryPath, uniqueFileName);
        }

        private void SaveFile(IFormFile file, string filePath)
        {
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    file.CopyTo(stream);
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to save the file to {filePath}.", ex);
            }
        }

    }
}
