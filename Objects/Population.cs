using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AcademyBot.Objects
{
    public class Population
    {
        public List<Person> People { get; set; }
        private string Path { get; set; }
        private JObject JsonPeople { get; set; }

        public Population(string path = @"../../../json/people.json")
        {
            Path = path;
            People = new List<Person>();
            UpdateJobject();
        }

        public bool LoadPeople()
        {
            // Loads people saved in json path and initializes Person objects
            UpdateJobject();
            foreach (var person in JsonPeople)
            {
                var personObject = new Person(
                    ulong.Parse(person.Key),
                    "",
                    admin: bool.Parse(person.Value["Admin"].ToString())
                    );
                personObject.AddRolesFromString(person.Value["Roles"].ToString());
                People.Add(personObject);
            }
            return true; // Add logic for error handling
        }

        public bool SavePeople()
        {
            foreach (var person in People)
            {
                SavePerson(person);
            }
            WriteJson();
            return true;
        }

        public void MakePerson(ulong id, string roles, bool admin = false)
        {
            Person person;
            foreach (var p in People) // Check if the id exists in People
            {
                if (p.Id == id) return; // Break out and return nothing
            }
            person = new Person(id);
            person.AddRolesFromString(roles.ToString());
            person = new Person(id, roles, admin);
            People.Add(person);
        }

        public void MakePerson(ulong id, List<ulong> roles, bool admin = false)
        {
            Person person;
            foreach (var p in People) // Check if the id exists in People
            {
                if (p.Id == id) return; // Break out and return nothing
            }
            person = new Person(id, roles, admin);
            People.Add(person);
        }



        public bool WriteJson()
        {
            File.WriteAllText(Path, JsonPeople.ToString());
            return true;
        }

        public bool SavePerson(Person person)
        {
            // Write the given person object to JObject. If the person exists, overwrite with current values
            var obj = new JObject(
                new JProperty("Admin", person.Admin),
                new JProperty("Roles", person.Roles));
            try
            {
                JsonPeople.Add(person.Id.ToString(), obj);
            }
            catch (System.ArgumentException)
            {
                JsonPeople[person.Id.ToString()]["Admin"] = person.Admin.ToString();
                JsonPeople[person.Id.ToString()]["Roles"] = JArray.Parse(person.RoleString());
            }
            return true;
        }

        public void UpdateJobject()
        {
            var peopleJson = File.ReadAllText(Path);
            JsonPeople = JObject.Parse(peopleJson);
        }

        public Person LoadPerson(ulong id)
        {
            // Loads the person by the given id, if the person does not exist in people.json, return an error.
            UpdateJobject();
            try
            {
                var person = JsonPeople[id.ToString()]; // id
                var roles = new List<ulong>();
                var jRoles = person["Roles"].Children();
                foreach (var role in jRoles)
                {
                    Console.WriteLine(role.ToString());
                    roles.Add(ulong.Parse(role.ToString()));
                }
                return new Person(id, roles, bool.Parse(person["Admin"].ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return null;
        }

        public Person GetPerson(ulong id)
        {
            // Returns a person object from loaded memory
            return People.Find(x => x.Id == id);
        }
    }
}
