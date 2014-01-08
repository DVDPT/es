using System;
using System.Collections.Generic;
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
        private readonly InMemoryAsyncEntityMapper<string, Person> _representatives;
        private readonly InMemoryAsyncEntityMapper<string, FinantialTechnician> _technicians;
        private readonly InMemoryAsyncEntityMapper<string, FinantialCommitteeMember> _finantialCommitteeMembers;

        public IAsyncEntityMapper<int, Project> Projects { get { return _projects; } }
        public IAsyncEntityMapper<string, Promoter> Promoters { get { return _promoters; } }
        public IAsyncEntityMapper<string, Person> Representatives { get { return _representatives; } }
        public IAsyncEntityMapper<string, FinantialTechnician> Technicians { get { return _technicians; } }
        public IAsyncEntityMapper<string, FinantialCommitteeMember> FinantialCommitteeMembers { get { return _finantialCommitteeMembers; } }

        public InMemorySGPFDatabase()
        {
            _projects = new InMemoryAsyncEntityMapper<int, Project>(p => p.Id);
            _promoters = new InMemoryAsyncEntityMapper<string, Promoter>(p => p.Nif);
            _representatives = new InMemoryAsyncEntityMapper<string, Person>(p => p.Id);
            _technicians = new InMemoryAsyncEntityMapper<string, FinantialTechnician>(f => f.Id);
            _finantialCommitteeMembers = new InMemoryAsyncEntityMapper<string, FinantialCommitteeMember>(f => f.Id);

            FillWithData();
        }

        private void FillWithData()
        {
            
        }
    }
}
