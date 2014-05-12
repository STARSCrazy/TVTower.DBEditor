using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVTower.Entities.Helper
{
    public static class EntityHelper
    {
        public static List<TVTPerson> GetPersonsByNameOrCreate(this ITVTDatabase database, string names, TVTDataStatus defaultStatus, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown)
        {
            var result = new List<TVTPerson>();
            if (!string.IsNullOrEmpty(names))
            {
                var array = names.Split(',');
                foreach (var aValue in array)
                {
                    var personName = aValue.Trim();

                    var person = database.GetPersonByNameOrCreate(personName, defaultStatus, functionForNew);
                    result.Add(person);
                }
            }
            return result;
        }

        public static TVTPerson GetPersonByNameOrCreate(this ITVTDatabase database, string name, TVTDataStatus defaultStatus, TVTPersonFunction functionForNew = TVTPersonFunction.Unknown)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var tempName = name.Split(',')[0];
                var person = database.GetPersonByName(tempName);

                if (person == null)
                {
                    person = new TVTPerson();
                    person.GenerateGuid();
                    person.DataStatus = defaultStatus;
                    if (defaultStatus == TVTDataStatus.Fake || defaultStatus == TVTDataStatus.FakeWithRefId)
                        person.ConvertFakeFullname(tempName);
                    else
                        person.ConvertFullname(tempName);
                    person.Functions.Add(functionForNew);
                    database.AddPerson(person);
                }
                return person;
            }
            else
                return null;
        }
    }
}
