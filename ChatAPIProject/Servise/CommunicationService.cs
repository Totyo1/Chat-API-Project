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

        public List<CommunicationServiceModel> All(int userId)
        {
            var allCommunications = this.communicationCode.All(userId)
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
        
        public void DeleteUsersCommunications(int id)
        {
            this.communicationCode.DeleteserCommunications(id);
        }

        public Communication GetCommunicationById(int communicationId)
        {
            var communication = this.communicationCode.GetCommunicationById(communicationId);

            return communication;
        }

        public Communication GetCommunicationByUsers(int firstUserId, int secondUserId)
        {
            Communication communication = this.communicationCode.GetCommunicationByUsers(firstUserId, secondUserId);

            return communication;
        }
    }
}