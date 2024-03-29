﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGPF.DataController.Exceptions
{
    static class ErrorConstants
    {
        public const string 
            PersonNotFoundExceptionFormat = "The person with id \"{0}\" coudn't be found",
            InvalidResumeOperationFormat = "This project have to be resumed by {0}({1})",
            SuspendedProjectExceptionFormat = "Project {0} it's suspended",
            UpdateProjectException = "A project can only be updated when it's on the open or awaiting dispatch states.",
            RejectedProjectExceptionFormat = "Project with id {0} is already rejected",
            CompletedProjectFormat = "Go home, project {0} is already completed.",
            InvalidStateException = "This action cannot be done while project state is {0}."
            ;

    }
}
