﻿using System;
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
        }

        public int GenerateProjectId()
        {
            return _projects.Count;
        }
    }
}
