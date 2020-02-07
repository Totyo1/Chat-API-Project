using ChatAPIProject.Data;
using ChatAPIProject.Models.DataModels;
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
        private CommunicationCode communicationCode; 

        public CommunicationService()
        {
            this.communicationCode = new CommunicationCode();
        }

        public List<CommunicationServiceModel> All()
        {
            var allCommunications = this.communicationCode.All()
                .Select(x => new CommunicationServiceModel
                {
                    Id = x.Id,
                    FirstUserId = x.FirstUserId,
                    SecondUserId = x.SecondUserId
                })
                .ToList();

            return allCommunications;
        }

        public void Create(int firstUserId, int secondUserId)
        {
            this.communicationCode.CreateCommunication(firstUserId, secondUserId);
        }

        public Communication GetCommunicationByUsers(int firstUserId, int secondUserId)
        {
            var communication = this.communicationCode.GetCommunicationByUsers(firstUserId, secondUserId);

            return communication;
        }
    }
}