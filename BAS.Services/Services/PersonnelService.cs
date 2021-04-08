using BAS.AppCommon;
using BAS.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BAS.AppServices
{
    public class PersonnelService : IPersonnelService
    {
        private readonly MovieDbContext db;

        public PersonnelService(MovieDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> DeletePersonnel(long id)
        {
            var personnel = db.Actors.Find(id);

            if (personnel != null)
            {
                db.Actors.Remove(personnel);
                db.SaveChanges();
            }

            return true;
        }

        public async Task<Personnel> GetPersonnel(long id)
        {
            return db.Actors.Find(id);
        }

        public async Task<PersonnelListWithFilters> GetPersonnelWtihFilter(PersonnelFilters personnelFilter)
        {
            if (string.IsNullOrWhiteSpace(personnelFilter.FullName))
            {
                personnelFilter.FullName = "";
            }

            if (string.IsNullOrWhiteSpace(personnelFilter.Nationality))
            {
                personnelFilter.Nationality = "";
            }


            var pageSize = personnelFilter.PageSize.HasValue ? personnelFilter.PageSize.Value : int.MaxValue;

            var result = new PersonnelListWithFilters()
            {
                CurrentPage = personnelFilter.Page,
                PageSize = pageSize,
                AllPages = (int)Math.Ceiling(db.Actors.Count(p => (p.Name.ToLower() + p.Surname.ToLower()).Contains(personnelFilter.FullName.ToLower()) &&
                    p.Nationality.ToLower().Contains(personnelFilter.Nationality.ToLower()) &&
                    (!personnelFilter.BirthDateFrom.HasValue || p.DateOfBirth >= personnelFilter.BirthDateFrom.Value) &&
                    (!personnelFilter.BirthDateTo.HasValue || p.DateOfBirth <= personnelFilter.BirthDateTo.Value))
                                * 1.0 / pageSize)
            };

            var personnel = db.Actors.Where(p => (p.Name.ToLower() + p.Surname.ToLower()).Contains(personnelFilter.FullName.ToLower()) &&
                    p.Nationality.ToLower().Contains(personnelFilter.Nationality.ToLower()) &&
                    (!personnelFilter.BirthDateFrom.HasValue || p.DateOfBirth >= personnelFilter.BirthDateFrom.Value) &&
                    (!personnelFilter.BirthDateTo.HasValue || p.DateOfBirth <= personnelFilter.BirthDateTo.Value));


            switch (personnelFilter.OrderBy.ToLower())
            {
                case "surname":
                    if (personnelFilter.IsDescending)
                        personnel = personnel.OrderByDescending(g => g.Surname);
                    else
                        personnel = personnel.OrderBy(g => g.Surname);
                    break;
                case "nationality":
                    if (personnelFilter.IsDescending)
                        personnel = personnel.OrderByDescending(g => g.Nationality);
                    else
                        personnel = personnel.OrderBy(g => g.Nationality);
                    break;
                case "dateofbirth":
                    if (personnelFilter.IsDescending)
                        personnel = personnel.OrderByDescending(g => g.DateOfBirth);
                    else
                        personnel = personnel.OrderBy(g => g.DateOfBirth);
                    break;
                case "name":
                    if (personnelFilter.IsDescending)
                        personnel = personnel.OrderByDescending(g => g.Name);
                    else
                        personnel = personnel.OrderBy(g => g.Name);
                    break;
                default:
                    break;
            }

            personnel = personnel.Skip((personnelFilter.Page - 1) * pageSize).Take(pageSize);

            result.PersonnelList = personnel.ToList();

            return result;
        }

        public async Task<bool> InsertPersonnel(PersonnelDTO personnelDTO)
        {
            if (personnelDTO.DateOfBirth >= DateTime.Now)
                return false;

            if (personnelDTO.Name.Length == 0 ||
                personnelDTO.Name.Length >= StaticValues.PersonnelNameMaxLength)
                return false;

            if (personnelDTO.Surname.Length == 0 ||
                personnelDTO.Name.Length >= StaticValues.PersonnelSurnameMaxLength)
                return false;

            if (personnelDTO.Nationality.Length == 0 ||
                personnelDTO.Name.Length >= StaticValues.PersonnelNationalityMaxLength)
                return false;

            var personnel = new Personnel()
            {
                Name = personnelDTO.Name,
                Surname = personnelDTO.Surname,
                Nationality = personnelDTO.Nationality,
                DateOfBirth = personnelDTO.DateOfBirth
            };

            db.Actors.Add(personnel);
            db.SaveChanges();

            return true;
        }

        public async Task<bool> UpdatePersonnel(PersonnelDTO personnelDTO)
        {
            if (personnelDTO.DateOfBirth >= DateTime.Now)
                return false;

            if (personnelDTO.Name.Length == 0 ||
                personnelDTO.Name.Length >= StaticValues.PersonnelNameMaxLength)
                return false;

            if (personnelDTO.Surname.Length == 0 ||
                personnelDTO.Name.Length >= StaticValues.PersonnelSurnameMaxLength)
                return false;

            if (personnelDTO.Nationality.Length == 0 ||
                personnelDTO.Name.Length >= StaticValues.PersonnelNationalityMaxLength)
                return false;

            var personnel = db.Actors.Find(personnelDTO.Id);

            if (personnel == null)
                return false;

            personnel.Name = personnelDTO.Name;
            personnel.Nationality = personnelDTO.Nationality;
            personnel.Surname = personnelDTO.Surname;

            db.Actors.Update(personnel);
            db.SaveChanges();

            return true;
        }

        public async Task<bool> IsPersonnelInDB(long id)
        {
            return await db.Actors.AnyAsync(p => p.Id == id);
        }
    }
}
