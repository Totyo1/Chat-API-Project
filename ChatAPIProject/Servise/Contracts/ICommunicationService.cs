using ChatAPIProject.Models.ServiceModels;
using ChatAPIProject.Models.ServiceModels.Communication;
using ChatAPIProject.Models.ServiceModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servise.Contracts
{
    public interface ICommunicationService
    {
        IEnumerable<CommunicationServiceModel> All();

        
    }
}
