using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBot.Objects
{
    class Person
    {
        public ulong Id { get; private set; }
        public List<ulong> Roles { get; set; }
        public bool Admin { get; set; }
        public Person(ulong id, List<ulong> roles = null, bool admin = false)
        {
            Id = id;
            if (roles is null)
            {
                Roles = new List<ulong>();
            } else
            {
                Roles = roles;
            }
            Admin = admin;
        }

        public void AddRole(ulong roleId)
        {
            Roles.Add(roleId);
        }

        public string RoleString()
        {
            if (Roles.Count == 0) return "";
            var outString = "[";
            for (int i = 0; i < Roles.Count; i++)
            {
                outString += (i == Roles.Count - 1) ? $"{Roles[i]}]" : $"{Roles[i]},";
            }
            return outString;
        }

        public bool AddRolesFromString(string v)
        {
                        
            
            if (v.Length < 10) { 
                //Console.WriteLine("no Roles Found");
                return false;
            };
            var snippet = v.Substring(1, v.Length - 2);
            var stringRoles = snippet.Split(',');
            foreach (var role in stringRoles)
            {
                var result = ulong.TryParse(role, out ulong roleUlong);
                if (result) AddRole(roleUlong);
            }
            return true;
        }

        public bool RemoveRole(ulong roleId)
        {
            if (Roles.Contains(roleId))
            {
                Roles.Remove(roleId);
                return true;
            } else
            {
                return false;
            }
        }

        public List<ulong> GetRoles() // Redundant
        {
            return Roles;
        }

        public override string ToString()
        {
            return $"id: {Id}\tAdmin: {Admin}\tRoles: {RoleString()}";
        }
    }
}
