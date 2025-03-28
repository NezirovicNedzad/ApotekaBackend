﻿using CloudinaryDotNet.Actions;

namespace ApotekaBackend.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletPhotoAsync(string publicId);

    }
}
