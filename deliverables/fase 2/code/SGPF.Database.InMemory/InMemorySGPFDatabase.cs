using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGPF.Data;

namespace SGPF.Database.InMemory
{
    public class InMemorySGPFDatabase : ISGPFDatabase
    {
        private readonly InMemoryAsyncEntityMapper<int, Project> _projects;
        private readonly InMemoryAsyncEntityMapper<string, Promoter> _promoters;
        private readonly InMemoryAsyncEntityMapper<string, BasePerson> _persons;

        public IAsyncEntityMapper<int, Project> Projects { get { return _projects; } }
        public IAsyncEntityMapper<string, Promoter> Promoters { get { return _promoters; } }
        public IAsyncEntityMapper<string, BasePerson> Persons { get { return _persons; } }

        public InMemorySGPFDatabase()
        {
            _projects = new InMemoryAsyncEntityMapper<int, Project>(p => p.Id);
            _promoters = new InMemoryAsyncEntityMapper<string, Promoter>(p => p.Nif);
            _persons = new InMemoryAsyncEntityMapper<string, BasePerson>(p => p.Id);

            FillWithData();
        }

        private void FillWithData()
        {
            _persons.Add(new Person
            {
                Designation = "Person",
                Email = "sr@sr.sr",
                Id = "123",
                Name = "Person",
                Phone = "123"
            });

            _persons.Add(new Technician()
            {
                Designation = "Technician",
                Email = "str@sr.srt",
                Id = "12",
                Name = "Technician",
                Phone = "12"
            });

            _persons.Add(new FinancialManager
            {
                Designation = "FinancialManager",
                Email = "str@sr.srt",
                Id = "23",
                Name = "FinancialManager",
                Phone = "12"
            });

            _persons.Add(new FinantialCommitteeMember()
            {
                Designation = "FinantialCommitteeMember",
                Email = "str@sr.srt",
                Id = "34",
                Name = "FinantialCommitteeMember",
                Phone = "12"
            });


            _promoters.Add(new Promoter
            {
                Address = "Rua",
                Nationality = "Italian",
                Nif = "nif1"

            });

            _promoters.Add(new Promoter
            {
                Address = "Rua",
                Nationality = "Russian",
                Nif = "nif13"

            });

            _promoters.Add(new Promoter
            {
                Address = "Rua",
                Nationality = "Pt",
                Nif = "nif12"

            });

            _projects.Add(new Project { 
                
                Promoter = _promoters.First(),
                Id = GenerateProjectId(),
                Type = ProjectType.Incentive,
                CreatedTime = new DateTime(2012, 12, 20),
                Description = "This is a description",
                History = new ObservableCollection<ProjectHistory>(),
                Payments = new ObservableCollection<ProjectPayment>(),
                State = ProjectState.AwaitingDispatch
            });

            _projects.Add(new Project
            {
                Promoter = _promoters.Last(),
                Id = GenerateProjectId(),
                Type = ProjectType.Loan,
                LoanRate = 0.6,
                Manager = _persons.OfType<FinancialManager>().FirstOrDefault(),
                CreatedTime = new DateTime(2010, 5, 3),
                Description = "My project description",
                History = new ObservableCollection<ProjectHistory>(),
                Payments = new ObservableCollection<ProjectPayment>(),
                State = ProjectState.Archived
            });

            _projects.Add(new Project
            {
                Promoter = _promoters.Last(),
                Id = GenerateProjectId(),
                Type = ProjectType.Incentive,
                CreatedTime = new DateTime(2010, 5, 3),
                Description = "My project description",
                History = new ObservableCollection<ProjectHistory>(),
                Payments = new ObservableCollection<ProjectPayment>(),
                State = ProjectState.Open
            });
        }

        public int GenerateProjectId()
        {
            return _projects.Count;
        }
    }
}
