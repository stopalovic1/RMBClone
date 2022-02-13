﻿using RmbClone.Library.Models;
using RmbClone.Library.Models.Requests;
using RmbClone.Library.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public interface IBranchData
    {
        Task<List<BranchResponseModel>> GetAllBranchesAsync();
        Task InsertBranchAsync(BranchRequestModel model);
        Task<BranchDBModel> GetBranchByIdAsync(string id);
        Task DeleteBranchAsync(string id);
        Task UpdateBranchAsync(string id, BranchRequestModel model);
    }
}