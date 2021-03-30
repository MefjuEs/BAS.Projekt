using BAS.Repository.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices.Services
{
    public class PersonnelService
    {
        private readonly IUnitOfWork unitOfWork;

        public PersonnelService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task InsertPersonnel()
        {

        }

        public async Task UpdatePersonnel()
        {

        }

        public async Task DeletePersonnel()
        {

        }

        public async Task GetPersonnel()
        {

        }

        public async Task GetPersonnelsByFilter()
        {

        }
    }
}
