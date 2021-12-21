using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using RmbClone.Library.Models.Requests;
using RmbClone.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class BranchData : IBranchData
    {
        private readonly ISqlDataAccess _sql;
        private readonly ILocationData _locationData;
        private readonly ICityData _cityData;

        public BranchData(ISqlDataAccess sql, ILocationData locationData, ICityData cityData)
        {
            _sql = sql;
            _locationData = locationData;
            _cityData = cityData;
        }

        public async Task<List<BranchResponseModel>> GetAllBranchesAsync()
        {
            var branches = await _sql.LoadDataAsync<BranchDBModel, dynamic>("dbo.spBranch_Lookup", new { }, "RmbCloneDb");
            var result = new List<BranchResponseModel>();
            foreach (var branch in branches)
            {
                var location = await _locationData.FindAsync(branch.LocationId);
                var city = await _cityData.FindAsync(branch.CityId);
                var workingHours = await _sql.LoadDataAsync<WorkingHoursDBModel, dynamic>("dbo.spWorkingHours_LookupById", new { BranchId = branch.Id }, "RmbCloneDb");

                var branchResponse = new BranchResponseModel
                {
                    Id = branch.Id,
                    Location = location,
                    Name = branch.Name,
                    City = city,
                    Contact = branch.Contact,
                    WorkingHours = workingHours,
                    BranchType = new BranchTypeDBModel(), //dobaviti prave vrijednosti
                    BranchServiceType = new BranchServiceTypeDBModel(),//isto
                    ATMType = branch.ATMType,
                    ATMFilter = branch.ATMFilter
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
                LocationId = model.LocationId,
                Name = model.Name,
                CityId = model.CityId,
                Contact = model.Contact,
                BranchTypeId = model.BranchTypeId,
                BranchServiceTypeId = model.BranchServiceTypeId,
                ATMType = model.ATMType,
                ATMFilter = model.ATMFilter
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

            foreach (var item in workingHours)
            {
                await _sql.SaveDataAsync("dbo.spWorkingHours_Insert", item, "RmbCloneDb");
            }
        }

    }
}
