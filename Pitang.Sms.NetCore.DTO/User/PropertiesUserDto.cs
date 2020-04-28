using System;
using System.Collections.Generic;

namespace Pitang.Sms.NetCore.DTO.User
{
    public class PropertiesUserDto
    {
        public int ObjectId { get; set; }

        public IList<PropertyInput> PropertiesToUpdate { get; set; }


        public PropertiesUserDto()
        {
        }
    }

    public class PropertyInput
    {

        public string PropertyName { get; set; }

        public string PropertyValue { get; set; }
    }
}