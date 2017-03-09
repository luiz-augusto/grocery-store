using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using GroceriesStore.Infra.Mappings;
using GroceriesStore.Domain.Commands.Handlers;
using Rhino.Mocks;
using GroceriesStore.Domain.Repositories;
using GroceriesStore.Domain.Commands.Inputs;
using GroceriesStore.Domain.Entities;
using GroceriesStore.Domain.Enums;
using GroceriesStore.Domain.Commands.Results;
using System.Linq;

namespace GroceriesStore.Domain.Tests
{
    [TestClass]
    public class GroceriesTests
    {
        [TestMethod]
        [TestCategory("XML")]
        public void GenerateXML()
        {
            var groceriesList = new List<GroceriesMap>() {
                new GroceriesMap() { Id = Guid.NewGuid(), Name = "A", Price = 10, Unity = GroceriesStore.Domain.Enums.Unity.Grams, Category = GroceriesStore.Domain.Enums.Category.Bakery},
                new GroceriesMap() { Id = Guid.NewGuid(), Name = "B", Price = 20, Unity = GroceriesStore.Domain.Enums.Unity.Grams, Category = GroceriesStore.Domain.Enums.Category.Bakery},
                new GroceriesMap() { Id = Guid.NewGuid(), Name = "C", Price = 30, Unity = GroceriesStore.Domain.Enums.Unity.Grams, Category = GroceriesStore.Domain.Enums.Category.Canned }
            };
            XmlSerializer serializer = new XmlSerializer(typeof(List<GroceriesMap>), new XmlRootAttribute("Groceries"));
            using (StreamWriter myWriter = new StreamWriter("Groceries.xml"))
            {
                serializer.Serialize(myWriter, groceriesList);
                myWriter.Close();
            }
        }

        [TestMethod]
        [TestCategory("XML")]
        public void ReadXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<GroceriesMap>), new XmlRootAttribute("Groceries"));
            using (StreamReader reader = new StreamReader("Groceries.xml"))
            {
                var groceriesList = (List<GroceriesMap>)serializer.Deserialize(reader);
                reader.Close();
            }
        }

        [TestMethod]
        [TestCategory("GetGroceriesCommandHandler")]
        public void GetGroceriesCommandHandlerShouldntReturnNotifications()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var command = new GetGroceriesCommand(Guid.NewGuid());
            _mock.Expect(x => x.GetById(command.Id)).Return(new Groceries(command.Id, string.Empty, 0, Enums.Unity.Grams, Enums.Category.Bakery));

            GetGroceriesCommandHandler handler = new GetGroceriesCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreEqual(handler.Notifications.Count, 0);
        }

        [TestMethod]
        [TestCategory("GetGroceriesCommandHandler")]
        public void GetGroceriesCommandHandlerShouldReturnNotification()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var guid = Guid.NewGuid();
            _mock.Expect(x => x.GetById(guid)).Return(new Groceries(guid, string.Empty, 0, Enums.Unity.Grams, Enums.Category.Bakery));

            GetGroceriesCommandHandler handler = new GetGroceriesCommandHandler(_mock);
            var command = new GetGroceriesCommand(Guid.NewGuid());
            var g = handler.Handle(command);
            Assert.AreNotEqual(handler.Notifications.Count, 0);
        }

        [TestMethod]
        [TestCategory("RegisterGroceriesCommandHandler")]
        public void RegisterGroceriesCommandHandlerShouldntReturnNotifications()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var command = new RegisterGroceriesCommand()
            {
                Name = "TestGrocery",
                Price = 10,
                Unity = Enums.Unity.Liters,
                Category = Enums.Category.BakingGoods
            };

            RegisterGroceriesCommandHandler handler = new RegisterGroceriesCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreEqual(handler.Notifications.Count, 0);
        }

        [TestMethod]
        [TestCategory("RegisterGroceriesCommandHandler")]
        public void RegisterGroceriesCommandHandlerShouldReturnNotifications()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var command = new RegisterGroceriesCommand()
            {
                Name = "Te",//Invalid name
                Price = 10,
                Unity = Enums.Unity.Liters,
                Category = Enums.Category.BakingGoods
            };

            RegisterGroceriesCommandHandler handler = new RegisterGroceriesCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreNotEqual(handler.Notifications.Count, 0);
        }

        [TestMethod]
        [TestCategory("UpdateGroceriesCommandHandler")]
        public void UpdateGroceriesCommandHandlerShouldntReturnNotifications()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            string originalName = "TestGrocery";
            var command = new UpdateGroceriesCommand()
            {
                Id = Guid.NewGuid(),
                Name = "TestGroceryUpdated",
                Price = 10,
                Unity = Enums.Unity.Liters,
                Category = Enums.Category.BakingGoods
            };

            _mock.Expect(x => x.GetById(command.Id)).Return(new Groceries(command.Id, originalName, command.Price,
                command.Unity, command.Category));

            UpdateGroceriesCommandHandler handler = new UpdateGroceriesCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreEqual(handler.Notifications.Count, 0);
        }

        [TestMethod]
        [TestCategory("UpdateGroceriesCommandHandler")]
        public void UpdateGroceriesCommandHandlerShouldReturnNotifications()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            string originalName = "TestGrocery";
            var command = new UpdateGroceriesCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Te", //Invalid name
                Price = 10,
                Unity = Enums.Unity.Liters,
                Category = Enums.Category.BakingGoods
            };

            _mock.Expect(x => x.GetById(command.Id)).Return(new Groceries(command.Id, originalName, command.Price,
                command.Unity, command.Category));

            UpdateGroceriesCommandHandler handler = new UpdateGroceriesCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreNotEqual(handler.Notifications.Count, 0);
        }


        [TestMethod]
        [TestCategory("UpdateGroceriesPositionCommandHandler")]
        public void UpdateGroceriesPositionCommandHandlerShouldntReturnNotifications()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var command = new UpdateGroceriesPositionCommand(Guid.NewGuid(), 2);

            _mock.Expect(x => x.GetAll()).Return(new List<Groceries>() {
                new Groceries(command.Id, "Test1", 10, Unity.Grams, Category.Bakery),//Selected element to move.
                new Groceries(Guid.NewGuid(), "Test2", 20, Unity.Liters, Category.Bakery),
                new Groceries(Guid.NewGuid(), "Test3", 30, Unity.Unity, Category.Beverages)
            });

            UpdateGroceriesPositionCommandHandler handler = new UpdateGroceriesPositionCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreEqual(handler.Notifications.Count, 0);
            //Verify the new position of the element in the array.
            SearchGroceriesCommand s = new SearchGroceriesCommand();
            SearchGroceriesCommandHandler handlerSearch = new SearchGroceriesCommandHandler(_mock);
            SearchGroceriesCommandResult groceries = (SearchGroceriesCommandResult)handlerSearch.Handle(s);
            var grocery = groceries.GroceriesList.FirstOrDefault(x => x.Id == command.Id);
            var index = groceries.GroceriesList.ToList().LastIndexOf(grocery);

            Assert.AreEqual(index, (command.Position - 1)); //The element was moved from 1 to 2. How the element was in the first position, the index changed from 2 to 1;
        }

        [TestMethod]
        [TestCategory("UpdateGroceriesPositionCommandHandler")]
        public void UpdateGroceriesPositionElementShouldMoveToCorrectPosition()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var command = new UpdateGroceriesPositionCommand(Guid.NewGuid(), 0);

            _mock.Expect(x => x.GetAll()).Return(new List<Groceries>() {
                new Groceries(Guid.NewGuid(), "Test1", 10, Unity.Grams, Category.Bakery),
                new Groceries(Guid.NewGuid(), "Test2", 20, Unity.Liters, Category.Bakery),
                new Groceries(command.Id, "Test3", 30, Unity.Unity, Category.Beverages) //Selected element to move.
            });

            UpdateGroceriesPositionCommandHandler handler = new UpdateGroceriesPositionCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreEqual(handler.Notifications.Count, 0);
            //Verify the new position of the element in the array.
            SearchGroceriesCommand s = new SearchGroceriesCommand();
            SearchGroceriesCommandHandler handlerSearch = new SearchGroceriesCommandHandler(_mock);
            SearchGroceriesCommandResult groceries = (SearchGroceriesCommandResult)handlerSearch.Handle(s);
            var grocery = groceries.GroceriesList.FirstOrDefault(x => x.Id == command.Id);
            var index = groceries.GroceriesList.ToList().LastIndexOf(grocery);

            Assert.AreEqual(index, command.Position);
        }


        [TestMethod]
        [TestCategory("DeleteGroceriesCommandHandler")]
        public void DeleteGroceriesCommandHandlerShouldntReturnNotification()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var command = new DeleteGroceriesCommand(Guid.NewGuid());

            _mock.Expect(x => x.GetById(command.Id)).Return(new Groceries(command.Id, "Test3", 30, Unity.Unity, Category.Beverages));

            DeleteGroceriesCommandHandler handler = new DeleteGroceriesCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreEqual(handler.Notifications.Count, 0);
        }

        [TestMethod]
        [TestCategory("DeleteGroceriesCommandHandler")]
        public void DeleteGroceriesCommandHandlerShoulReturnNotification()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var command = new DeleteGroceriesCommand(Guid.NewGuid());

            _mock.Expect(x => x.GetById(command.Id)).Return(null);

            DeleteGroceriesCommandHandler handler = new DeleteGroceriesCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreNotEqual(handler.Notifications.Count, 0);
        }

        [TestMethod]
        [TestCategory("SearchGroceriesCommandHandler")]
        public void SearchGroceriesCommandHandlerShouldntReturnNotification()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var command = new SearchGroceriesCommand();

            _mock.Expect(x => x.GetAll()).Return(new List<Groceries>() {
                new Groceries(Guid.NewGuid(), "Test1", 10, Unity.Grams, Category.Bakery),
                new Groceries(Guid.NewGuid(), "Test2", 20, Unity.Liters, Category.Bakery),
                new Groceries(Guid.NewGuid(), "Test3", 30, Unity.Unity, Category.Beverages)
            });

            SearchGroceriesCommandHandler handler = new SearchGroceriesCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreEqual(handler.Notifications.Count, 0);
        }

        [TestMethod]
        [TestCategory("SearchGroceriesCommandHandler")]
        public void SearchGroceriesCommandHandlerShouldReturnNotification()
        {
            var _mock = MockRepository.GenerateMock<IGroceriesRepository>();
            var command = new SearchGroceriesCommand();

            _mock.Expect(x => x.GetAll()).Return(null);

            SearchGroceriesCommandHandler handler = new SearchGroceriesCommandHandler(_mock);
            var g = handler.Handle(command);
            Assert.AreNotEqual(handler.Notifications.Count, 0);
        }
    }
}
