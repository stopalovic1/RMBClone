using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using RmbClone.Library.Models.Requests;
using RmbClone.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class BranchData : IBranchData
    {
        private readonly ISqlDataAccess _sql;
        private readonly ILocationData _locationData;
        private readonly ICityData _cityData;
        private readonly IBranchServiceTypeData _branchServiceTypeData;
        private readonly IBranchTypeData _branchTypeData;
        private readonly IATMFilterData _atmFilterData;

        public BranchData(ISqlDataAccess sql, ILocationData locationData, ICityData cityData, IBranchServiceTypeData branchServiceTypeData, IBranchTypeData branchTypeData, IATMFilterData atmFilterData)
        {
            _sql = sql;
            _locationData = locationData;
            _cityData = cityData;
            _branchServiceTypeData = branchServiceTypeData;
            _branchTypeData = branchTypeData;
            _atmFilterData = atmFilterData;
        }

        public async Task<List<BranchResponseModel>> GetAllBranchesAsync(List<BranchDBModel> brs = null)
        {
            var branches = brs ?? await _sql.LoadDataAsync<BranchDBModel, dynamic>("dbo.spBranch_Lookup", new { }, "RmbCloneDb");
            var result = new List<BranchResponseModel>();

            var tasks = branches.Select(async branch =>
            {
                var location = await _locationData.FindByBranchIdAsync(branch.Id);
                var city = await _cityData.FindCityByIdAsync(branch.CityId);
                var workingHours = await _sql.LoadDataAsync<WorkingHoursDBModel, dynamic>("dbo.spWorkingHours_LookupById", new { BranchId = branch.Id }, "RmbCloneDb");
                var branchType = await _branchTypeData.GetBranchTypeByIdAsync(branch.BranchTypeId);
                var branchServiceType = await _branchServiceTypeData.GetBranchServiceTypeByIdAsync(branch.BranchServiceTypeId);
                var atmFilter = await _atmFilterData.GetAtmFilterByIdAsync(branch.ATMFilterId);
                var branchResponse = new BranchResponseModel
                {
                    Id = branch.Id,
                    Location = location,
                    Name = branch.Name,
                    City = city,
                    Contact = branch.Contact,
                    WorkingHours = workingHours,
                    BranchType = branchType,
                    BranchServiceType = branchServiceType,
                    ATMType = branch.ATMType,
                    ATMFilter = atmFilter
                };
                result.Add(branchResponse);
            });
            await Task.WhenAll(tasks);


            //foreach (var branch in branches)
            //{
            //    var location = await _locationData.FindByBranchIdAsync(branch.Id);
            //    var city = await _cityData.FindCityByIdAsync(branch.CityId);
            //    var workingHours = await _sql.LoadDataAsync<WorkingHoursDBModel, dynamic>("dbo.spWorkingHours_LookupById", new { BranchId = branch.Id }, "RmbCloneDb");
            //    var branchType = await _branchTypeData.GetBranchTypeByIdAsync(branch.BranchTypeId);
            //    var branchServiceType = await _branchServiceTypeData.GetBranchServiceTypeByIdAsync(branch.BranchServiceTypeId);
            //    var atmFilter = await _atmFilterData.GetAtmFilterByIdAsync(branch.ATMFilterId);
            //    var branchResponse = new BranchResponseModel
            //    {
            //        Id = branch.Id,
            //        Location = location,
            //        Name = branch.Name,
            //        City = city,
            //        Contact = branch.Contact,
            //        WorkingHours = workingHours,
            //        BranchType = branchType,
            //        BranchServiceType = branchServiceType,
            //        ATMType = branch.ATMType,
            //        ATMFilter = atmFilter
            //    };
            //    result.Add(branchResponse);
            //}

            return result;
        }

        public async Task<BranchDBModel> GetBranchByIdAsync(string id)
        {
            var result = await _sql.LoadDataAsync<BranchDBModel, dynamic>("dbo.spBranch_LookupById", new { Id = id }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

        public async Task InsertBranchAsync(BranchRequestModel model)
        {

            var workingHours = new List<WorkingHoursDBModel>();

            var branch = new BranchDBModel
            {
                Name = model.Name,
                CityId = model.CityId,
                Contact = model.Contact,
                BranchTypeId = model.BranchTypeId,
                BranchServiceTypeId = model.BranchServiceTypeId,
                ATMType = model.ATMType,
                ATMFilterId = model.ATMFilterId
            };

            var location = new LocationDBModel
            {
                BranchId = branch.Id,
                Address = model.Location.Address,
                Latitude = model.Location.Latitude,
                Longitude = model.Location.Longitude
            };

            foreach (var item in model.WorkingHours)
            {
                var workingHour = new WorkingHoursDBModel
                {
                    BranchId = branch.Id,
                    Day = item.Day,
                    Hours = item.Hours
                };
                workingHours.Add(workingHour);
            }

            await _sql.SaveDataAsync("dbo.spBranch_Insert", branch, "RmbCloneDb");
            await _sql.SaveDataAsync("dbo.spLocation_insert", location, "RmbCloneDb");

            foreach (var item in workingHours)
            {
                await _sql.SaveDataAsync("dbo.spWorkingHours_Insert", item, "RmbCloneDb");
            }
        }

        public async Task DeleteBranchAsync(string id)
        {
            await _sql.SaveDataAsync("dbo.spWorkingHours_Delete", new { BranchId = id }, "RmbCloneDb");
            await _locationData.DeleteLocationByBranchIdAsync(id);
            await _sql.SaveDataAsync("dbo.spBranch_Delete", new { Id = id }, "RmbCloneDb");
        }

        public async Task UpdateBranchAsync(string id, BranchRequestModel model)
        {
            var branch = (await _sql.LoadDataAsync<BranchDBModel, dynamic>("dbo.spBranch_LookupById", new { Id = id }, "RmbCloneDb")).FirstOrDefault();
            var location = await _locationData.FindByBranchIdAsync(id);
            var workingHours = await _sql.LoadDataAsync<WorkingHoursDBModel, dynamic>("dbo.spWorkingHours_LookupById", new { BranchId = branch.Id }, "RmbCloneDb");


            location.Address = model.Location.Address;
            location.Latitude = model.Location.Latitude;
            location.Longitude = model.Location.Longitude;

            for (int i = 0; i < workingHours.Count; i++)
            {
                var whFromModel = model.WorkingHours.FirstOrDefault(x => x.Day == workingHours[i].Day);
                if (whFromModel == null) continue;
                workingHours[i].Day = whFromModel.Day;
                workingHours[i].Hours = whFromModel.Hours;
            }

            branch.Name = model.Name;
            branch.CityId = model.CityId;
            branch.Contact = model.Contact;
            branch.BranchTypeId = model.BranchTypeId;
            branch.BranchServiceTypeId = model.BranchServiceTypeId;
            branch.ATMType = model.ATMType;
            branch.ATMFilterId = model.ATMFilterId;

            await _locationData.UpdateLocationAsync(location);

            foreach (var item in workingHours)
            {
                await _sql.SaveDataAsync("dbo.spWorkingHours_Update", item, "RmbCloneDb");
            }

            await _sql.SaveDataAsync("dbo.spBranch_Update", branch, "RmbCloneDb");
        }

        public async Task<List<BranchResponseModel>> GetFilteredBranchesAsync(string cityId = null, string branchTypeId = null, string branchServiceTypeId = null, string atmType = null, double? radius = null, double? latitude = null, double? longitude = null)
        {
            var parameters = new
            {
                CityId = cityId,
                BranchTypeId = branchTypeId,
                BranchServiceTypeId = branchServiceTypeId,
                ATMType = atmType,
                Radius = radius,
                Latitude = latitude,
                Longitude = longitude
            };

            var result = await _sql.LoadDataAsync<BranchDBModel, dynamic>("dbo.spBranch_Filter", parameters, "RmbCLoneDb");
            var mappedResults = await GetAllBranchesAsync(result);

            return mappedResults;
        }


    }
}
