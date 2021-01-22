using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace AcademyBot.Objects
{
    class Population
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
                    admin: bool.Parse(person.Value["Admin"].ToString())
                    );
                //Console.WriteLine(person.Value["Roles"].ToString());

                personObject.AddRolesFromString(person.Value["Roles"].ToString());
                People.Add(personObject);
            }
            return true; // Add logic for error handling
        }

        public bool SavePeople()
        {
            // Loads all people into memory as Person objects, then checks for duplicates and appends the remaining to file. 
            return true;
        }

        public bool WriteJson()
        {
            File.WriteAllText(Path, JsonPeople.ToString());
            return true;
        }

        public bool SavePerson(Person person)
        {
            // Write the given person object to JObject. If the person exists, overwrite with current values
            // UpdateJobject();
            // new JProperty("Admin", person.Admin) new JProperty("Roles", person.RoleString())
            var obj = new JObject(
                new JProperty("Admin", person.Admin),
                new JProperty("Roles", person.Roles));
            JsonPeople.Add(person.Id.ToString(), obj);
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
                    roles.Add(ulong.Parse(role.ToString()));
                }
                return new Person(id, roles, bool.Parse(person["Admin"].ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return null;

            //if ()
        }

        public Person GetPerson(ulong id)
        {
            // Returns a person object from loaded memory
            return People.Find(x => x.Id == id);
        }
    }
}
