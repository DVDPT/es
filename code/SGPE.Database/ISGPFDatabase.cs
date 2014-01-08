﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGPF.Data;

namespace SGPF.Database
{
    public interface ISGPFDatabase
    {
        IAsyncEntityMapper<int, Project> Projects { get; }
        IAsyncEntityMapper<string, Promoter> Promoters { get; }
        IAsyncEntityMapper<string, Person> Representatives { get; }
        IAsyncEntityMapper<string, FinantialTechnician> Technicians { get; }
        IAsyncEntityMapper<string, FinantialCommitteeMember> FinantialCommitteeMembers { get; }


    }
}
