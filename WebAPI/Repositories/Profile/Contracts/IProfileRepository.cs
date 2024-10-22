﻿using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface IProfileRepository
{
    Task<IEnumerable<Profile>> GetProfilesAsync();
    Task<Profile> GetProfileAsync(Guid profileId);
    Task<Profile> GetProfileByUrlAsync(string profileUrl);
    Task<bool> ProfileExistsAsync(Guid profileId);
    Task<int> UpdateProfileAsync(Profile profile);
    Task AddProfileAsync(Profile profile);
    Task DeleteProfileAsync(Guid profileId);
    Task<Profile> UpdateProfileAvatarAsync(Guid profileId, string avatarUrl);
}