using ChatAPIProject.Models.ServiceModels.Communication;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Service
{
    public class CommunicationService : ICommunicationService
    {
        public IEnumerable<CommunicationServiceModel> All()
        {
            //take all comunication from communication table 
            throw new NotImplementedException();
        }
    }
}