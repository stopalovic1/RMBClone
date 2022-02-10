﻿using RmbClone.Library.Internal.DataAccess;
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

        public BranchData(ISqlDataAccess sql, ILocationData locationData, ICityData cityData,IBranchServiceTypeData branchServiceTypeData,IBranchTypeData branchTypeData,IATMFilterData atmFilterData)
        {
            _sql = sql;
            _locationData = locationData;
            _cityData = cityData;
            _branchServiceTypeData = branchServiceTypeData;
            _branchTypeData = branchTypeData;
            _atmFilterData = atmFilterData;
        }

        public async Task<List<BranchResponseModel>> GetAllBranchesAsync()
        {
            var branches = await _sql.LoadDataAsync<BranchDBModel, dynamic>("dbo.spBranch_Lookup", new { }, "RmbCloneDb");
            var result = new List<BranchResponseModel>();
            foreach (var branch in branches)
            {
                var location = await _locationData.FindByBranchIdAsync(branch.Id);
                var city = await _cityData.FindAsync(branch.CityId);
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
            }
            return result;
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

    }
}
