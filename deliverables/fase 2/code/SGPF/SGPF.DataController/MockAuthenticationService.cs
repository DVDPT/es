using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGPF.Data;
using SGPF.Database;
using SGPF.DataController.Exceptions;

namespace SGPF.DataController
{
    public class MockAuthenticationService : IAuthenticationService
    {
        private readonly ISGPFDatabase _database;

        public MockAuthenticationService(ISGPFDatabase database)
        {
            _database = database;
        }

        public async Task<UserSession> StartSession(string id)
        {
            var person  = await _database.Persons.Get(id);

            if(person == null)
                throw new PersonNotFoundException(id);

            return new UserSession {UserDetails = person};
        }

        public async Task EndSession(UserSession session)
        {

        }
    }
}
