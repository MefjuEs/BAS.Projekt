using BAS.AppServices.DTOs;
using BAS.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices.Services.Interfaces
{
    public interface IPersonnelService
    {
        Task<bool> DeletePersonnel(long id);
        Task<Personnel> GetPersonnel(long id);
        Task<PersonnelListWithFilters> GetPersonnelWtihFilter(PersonnelFilters personnelFilter);
        Task<bool> InsertPersonnel (Personnel personnel);
        Task<bool> UpdatePersonnel(PersonnelDTO personnelDTO);
        Task<bool> IsPersonnelInDB(long id);
    }
}
