using System;
using System.Collections.Generic;

namespace myClubDriveMaster
{

        public class cdQueryAttr
        {
            public string IndexName { get; set; }
            public string ColIndex { get; set; }
            public string ColName { get; set; }
            public string ColValue { get; set; }
        }

        public class Account
        {
            public string AccountID { get; set; }
            public string AccountStatus { get; set; }
            public string EmailAddress { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string AddressLine3 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string County { get; set; }
            public string Destination { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string ParentID { get; set; }
            public string Phone { get; set; }
            public string Role { get; set; }
            public string School { get; set; }
            public string SchoolID { get; set; }
            public string Teacher { get; set; }
            public string UserName { get; set; }
            public string Attr1 { get; set; }
            public string Attr2 { get; set; }
            public string Attr3 { get; set; }
            public string Attr4 { get; set; }
            public string Attr5 { get; set; }
            public string Attr6 { get; set; }
            public string Attr7 { get; set; }
            public string Attr8 { get; set; }
            public string Attr9 { get; set; }
            public string Attr10 { get; set; }
        }

        public class getAccounts
        {
            public List<Account> Account { get; set; }
        }

}
