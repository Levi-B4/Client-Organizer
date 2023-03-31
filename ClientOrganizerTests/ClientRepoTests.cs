using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientOrganizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace ClientOrganizer.Tests
{
    [TestClass()]
    public class ClientRepoTests
    {
        [TestMethod()]
        public void SearchForExistingClientTestWithNameNotInRepo()
        {
            //arrange
            ClientRepo clientRepo = new ClientRepo();
            String longClientName = "0123456788901234567890123456789012345";

            String expected = "exit search";

            //act
            string clientName = clientRepo.SearchForExistingClient(longClientName).ClientName;
            //assert
            Assert.AreEqual(expected, clientName);
        }

        [TestMethod()]
        public void VerifyIfClientExistsTestWithNameNotInRepo()
        {
            //arrange
            ClientRepo clientRepo = new ClientRepo();
            String longClientName = "0123456788901234567890123456789012345";

            bool expected = false;

            //act and assert
            Assert.AreEqual(expected, clientRepo.VerifyIfClientExists(longClientName));
        }

        [TestMethod()]
        public void AddClientToTempMemoryTest()
        {
            //arrange
            ClientRepo clientRepo = new ClientRepo();
            Client clientToAdd = new Client("Test Client");
            int listLengthBefore = clientRepo.Clients.Count;

            bool expected = true;

            //act
            clientRepo.AddClientToTempMemory(clientToAdd);
            int listLengthAfter = clientRepo.Clients.Count;

            //assert
            Assert.AreEqual(expected, listLengthBefore < listLengthAfter);
        }
    }
}