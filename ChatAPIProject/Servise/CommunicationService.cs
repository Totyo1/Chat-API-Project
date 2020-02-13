using ChatAPIProject.Data;
using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.ServiceModels.Communication;

using Service.Contracts;
using System.Collections.Generic;
using System.Linq;

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
            List<CommunicationServiceModel> allCommunications = this.communicationCode.All(userId)
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

        public int DeleteFriend(int myId, int friendId)
        {
            return this.communicationCode.DeleteFriend(myId, friendId);
        }

        public void DeleteUsersCommunications(int id)
        {
            this.communicationCode.DeletUserCommunications(id);
        }

        public Communication GetCommunicationById(int communicationId)
        {
            Communication communication = this.communicationCode.GetCommunicationById(communicationId);

            return communication;
        }

        public Communication GetCommunicationByUsers(int firstUserId, int secondUserId)
        {
            Communication communication = this.communicationCode.GetCommunicationByUsers(firstUserId, secondUserId);

            return communication;
        }
    }
}