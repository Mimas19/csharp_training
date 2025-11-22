using System.Text.RegularExpressions;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace addressbook_web_tests
{
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        [Column(Name = "group_id")]
        public string GroupId { get; }
        
        [Column(Name = "id")]
        public string ContactId { get; }
          
    }
}