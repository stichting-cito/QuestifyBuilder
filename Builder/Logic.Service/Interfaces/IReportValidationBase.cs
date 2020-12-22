using System;
using System.Collections.Generic;
using Cito.Tester.Common;
using ResourceDto = Questify.Builder.Logic.Service.Model.Entities.ResourceDto;

namespace Questify.Builder.Logic.Service.Interfaces
{

    public interface IReportValidationBase
    {



        event EventHandler<StartEventArgs> StartProgress;

        event EventHandler<ProgressEventArgs> Progress;


        string Name { get; }

        string Description { get; }


        IList<ResourceDto> Collection { get; set; }

        int BankId { get; set; }





        bool IsDatasourceSupported();






    }
}
