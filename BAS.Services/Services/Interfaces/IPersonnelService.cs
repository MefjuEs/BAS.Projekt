using BAS.Database;
using System.Threading.Tasks;

namespace BAS.AppServices
{
    public interface IPersonnelService
    {
        Task<bool> DeletePersonnel(long id);
        Task<Personnel> GetPersonnel(long id);
        Task<PersonnelListWithFilters> GetPersonnelWtihFilter(PersonnelFilters personnelFilter);
        Task<bool> InsertPersonnel(PersonnelDTO personnelDTO);
        Task<bool> UpdatePersonnel(PersonnelDTO personnelDTO);
        Task<bool> IsPersonnelInDB(long id);
    }
}
