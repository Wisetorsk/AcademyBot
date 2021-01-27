using System;
using System.Collections.Generic;

namespace AcademyBot.Objects
{
    public class Person
    {
        public ulong Id { get; private set; }
        public List<ulong> Roles { get; set; } = new List<ulong>();
        public bool Admin { get; private set; } = false;
        public Person(ulong id, List<ulong> roles = null, bool admin = false)
        {
            Id = id;
            if (roles is null)
            {
                Roles = new List<ulong>(); //!!!!!!!!
            }
            else
            {
                Roles = roles;
            }
            Admin = admin;
        }

        public Person(ulong id)
        {
            Id = id;
        }

        public Person(ulong id, string roles = null, bool admin = false)
        {
            Id = id;
            if (roles is null || roles.Length < 1)
            {
                Roles = new List<ulong>();
            }
            else
            {
                AddRolesFromString(roles);
            }
            Admin = admin;
        }

        public void AddRole(ulong roleId)
        {
            Roles.Add(roleId);
        }

        public string RoleString()
        {
            if (Roles is null || Roles.Count == 0) return "[]";
            var outString = "[";
            for (int i = 0; i < Roles.Count; i++)
            {
                outString += (i == Roles.Count - 1) ? $"{Roles[i]}]" : $"{Roles[i]},";
            }
            return outString;
        }

        public bool AddRolesFromString(string roleInputString)
        {
            if (roleInputString.Length < 5)
            {
                return false;
            };
            var stringRoles = roleInputString.Replace("[", "").Replace("]", "").Split(',');
            foreach (var role in stringRoles)
            {
                var result = ulong.TryParse(role, out ulong roleUlong);
                if (result) Roles.Add(roleUlong);
            }
            return true;
        }

        public bool RemoveRole(ulong roleId)
        {
            if (Roles.Contains(roleId))
            {
                Roles.Remove(roleId);
                return true;
            }
            else
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
