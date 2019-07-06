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

    public class cdUpdateAccount
    {
        public string AccountID { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public string ColumnName1 { get; set; }
        public string ColumnValue1 { get; set; }
        public string ColumnName2 { get; set; }
        public string ColumnValue2 { get; set; }
        public string ColumnName3 { get; set; }
        public string ColumnValue3 { get; set; }
        public string ColumnName4 { get; set; }
        public string ColumnValue4 { get; set; }
    }
    public class cdUpdateClub
    {
        public string ClubID { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public string ColumnName1 { get; set; }
        public string ColumnValue1 { get; set; }
        public string ColumnName2 { get; set; }
        public string ColumnValue2 { get; set; }
        public string ColumnName3 { get; set; }
        public string ColumnValue3 { get; set; }
        public string ColumnName4 { get; set; }
        public string ColumnValue4 { get; set; }
    }
    public class cdUpdateEvent
    {
        public string EventID { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public string ColumnName1 { get; set; }
        public string ColumnValue1 { get; set; }
        public string ColumnName2 { get; set; }
        public string ColumnValue2 { get; set; }
        public string ColumnName3 { get; set; }
        public string ColumnValue3 { get; set; }
        public string ColumnName4 { get; set; }
        public string ColumnValue4 { get; set; }
    }
    public class cdLocation
    {
        public string TripID { get; set; }
        public int seqNumber { get; set; }
        public string driverID { get; set; }
        public string cddatetime { get; set; }
        public string cdLatitude { get; set; }
        public string cdLongitude { get; set; }
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

    public class Account
    {
        public string AccountID { get; set; }
        public string AccountStatus { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string cdState { get; set; }
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

    public class loginObject
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class loginResponse
    {
        public string status { get; set; }
        public string id_token { get; set; }
        public string user_id { get; set; }
        public string is_new { get; set; }
    }

    public class DriverAlloc
    {
        public string AllocationID { get; set; }
        public string StudentID { get; set; }
        public string DriverID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
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

    public class getDriver
    {
        public List<DriverAlloc> DriverAlloc { get; set; }
    }

    public class Trip
    {
        public string TripID { get; set; }
        public string seqNumber { get; set; }
        public string driverID { get; set; }
        public string cddatetime { get; set; }
        public string cdLatitude { get; set; }
        public string cdLongitude { get; set; }
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

    public class getTrips
    {
        public List<Trip> Trips { get; set; }
    }

    public class cdReadError
    {
        public String __type { get; set; }
        public String message { get; set; }
    }

    public class Club
    {
        public string ClubID { get; set; }
        public string ClubName { get; set; }
        public string ClubReg { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string cdState { get; set; }
        public string PostalCode { get; set; }
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

    public class getClubs
    {
        public List<Club> Club { get; set; }
    }

    public class ClubMembers
    {
        public string ClubMemberID { get; set; }
        public string ClubID { get; set; }
        public string MemberAccountID { get; set; }
        public string ClubName { get; set; }
        public string MemberName { get; set; }
        public string MemberRole { get; set; }
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


    public class getClubMembers
    {
        public List<ClubMembers> ClubMember { get; set; }
    }

    public class cdEvents
    {
        public string EventID { get; set; }
        public string EventName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string cdState { get; set; }
        public string PostalCode { get; set; }
        public string ClubName { get; set; }
        public string ClubID { get; set; }
        public string ClubAdmin { get; set; }
        public string Notes { get; set; }
        public string EventDate { get; set; }
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

    public class getEvents
    {
        public List<cdEvents> cdEvents { get; set; }
    }

    public class cdEventSignups
    {
        public string EventMemberID { get; set; }
        public string EventID { get; set; }
        public string ClubID { get; set; }
        public string MemberAccountID { get; set; }
        public string MemberRole { get; set; }
        public string EventName { get; set; }
        public string ClubName { get; set; }
        public string MemberName { get; set; }
        public string PickupLocation { get; set; }
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

    public class getEventMembers
    {
        public List<cdEventSignups> EventMember { get; set; }
    }

    public class signupAccount
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }

    public class Parameter
    {
        public string ParameterName { get; set; }
        public string EndPoint { get; set; }
        public string AccessKey { get; set; }
        public string Instance { get; set; }
    }

    public class getParameters
    {
        public List<Parameter> Parameters { get; set; }

    }

}
