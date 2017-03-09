using GroceriesStore.Domain.Entities;
using GroceriesStore.Domain.Repositories;
using GroceriesStore.Infra.Mappings;
using GroceriesStore.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace GroceriesStore.Infra.XmlRepository
{
    public class GroceriesRepository : IGroceriesRepository
    {
        private List<GroceriesMap> groceriesList;

        public GroceriesRepository()
        {
            FileName = Runtime.GroceriesPath;
            if (File.Exists(FileName))
                ReadXMLFile();
            else
            {
                WriteXMLFile();
                ReadXMLFile();
            }
        }

        private void ReadXMLFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<GroceriesMap>), new XmlRootAttribute("Groceries"));
            using (StreamReader reader = new StreamReader(FileName))
            {
                groceriesList = (List<GroceriesMap>)serializer.Deserialize(reader);
                reader.Close();
            }
        }

        internal string FileName { get; private set; }

        public List<Groceries> GetAll()
        {
            int totalItems = groceriesList.Count;
            var groceries = groceriesList.Select(x => new Groceries(x.Id,
                x.Name, x.Price, x.Unity, x.Category)).ToList();
            return groceries;
        }

        public Groceries GetById(Guid id)
        {
            return (from c in groceriesList where c.Id.Equals(id) select c)
                .Select(x => new Groceries(x.Id, x.Name, x.Price, x.Unity, x.Category)).FirstOrDefault();
        }

        public void Insert(Groceries entity)
        {
            groceriesList.Add(new GroceriesMap(entity.Id, entity.Name, entity.Price, entity.Unity, entity.Category));
            Save();
        }

        public void Update(Groceries entity)
        {
            var groceries =
                (from g in groceriesList
                where g.Id == entity.Id
                select g).FirstOrDefault();
            int index = groceriesList.IndexOf(groceries);
            groceriesList[index] = entity;
            Save();
        }

        public void Update(List<Groceries> groceriesList)
        {
            this.groceriesList = groceriesList.ConvertAll(x => new GroceriesMap(x.Id, x.Name, x.Price, x.Unity, x.Category)).ToList();
            Save();
        }

        public void Delete(Guid id)
        {
            var groceries = groceriesList.Where(x => x.Id == id).FirstOrDefault();
            groceriesList.Remove(groceries);
            Save();
        }

        private void Save()
        {
            WriteXMLFile();
        }

        private void WriteXMLFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<GroceriesMap>), new XmlRootAttribute("Groceries"));
            using (StreamWriter myWriter = new StreamWriter(FileName))
            {
                serializer.Serialize(myWriter, groceriesList);
                myWriter.Close();
            }
        }
    }
}
