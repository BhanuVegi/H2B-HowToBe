using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace myClubDriveMaster
{
    public partial class cdAccounts2 : ContentPage
    {
        Account regAccount = new Account();
        Club regClub = new Club();
        String regPassword = "";

        async void cdMain(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Account Reg button clicked");
            var bpage = new MainPage();
            await Navigation.PushModalAsync(bpage);
        }

        async void cdAccounts(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" Account Reg button clicked");
            regClub.ClubName = cdClubName.Text;
            regClub.AddressLine1 = cdcAddress1.Text;
            regClub.AddressLine2 = cdcAddress2.Text;
            regClub.City = cdcCity.Text;
            regClub.cdState = cdcState.Text;
            regClub.PostalCode = cdcPostalCode.Text;
            var cpage = new cdAccounts(regAccount,regClub,regPassword);
            await Navigation.PushModalAsync(cpage);
        }

        async void cdSubmit(object sender, System.EventArgs e)
        {
            int performSubmit = 0;
            System.Diagnostics.Debug.WriteLine(" Submit button clicked");
            cdCallAPI mycallAPI = new cdCallAPI();

            //Populate regiatration data
            if (regAccount.UserName == null || 
                regAccount.FirstName == null || 
                regAccount.AddressLine1 == null || 
                regAccount.cdState == null ||
                regAccount.City == null ||
                regAccount.PostalCode == null ||
                regAccount.EmailAddress == null ||
                regAccount.LastName == null)
            {
                await DisplayAlert("Action", "Key attributes cannot be null. Please go to personal information and enter the same", "Ok");
                performSubmit = 0;
            }
            else
            {
                performSubmit = 1;
                regAccount.AccountID = regAccount.UserName;
                if (cdNewClub.IsChecked == true)
                { 
                    regAccount.AccountStatus = "Approved";
                }
                else 
                {
                    regAccount.AccountStatus = "NotApproved";
                }
                if (regAccount.AddressLine2 ==null)
                {
                    regAccount.AddressLine2 = "None";
                }
                regAccount.AddressLine3 = "None";
                regAccount.County = "NA";
                regAccount.Destination = "NA";
                if (regAccount.MiddleName == null )
                {
                    regAccount.MiddleName = "None";
                }
                if (regAccount.ParentID == null)
                {
                    regAccount.ParentID = "NA";
                }
                if (regAccount.Phone == null)
                {
                    regAccount.Phone = "None";
                }
                if (regAccount.School == null)
                {
                    regAccount.School = "None";
                }
                if (regAccount.SchoolID == null)
                {
                    regAccount.SchoolID = "None";
                }
                if (regAccount.Teacher == null)
                {
                    regAccount.Teacher = "None";
                }
                if(cdNewClub.IsChecked == true || cdCheckAdmin.IsChecked == true)
                {
                    regAccount.Role = "A";
                }
                if (cdCheckRider.IsChecked == true)
                {
                    regAccount.Role = regAccount.Role + "R";

                }
                if (cdCheckDriver.IsChecked == true)
                {
                    regAccount.Role = regAccount.Role + "D";
                }
                if (cdCheckParent.IsChecked == true)
                {
                    regAccount.Role = regAccount.Role + "P";

                }
                regAccount.Attr1 = "NA";
                regAccount.Attr2 = "NA";
                regAccount.Attr3 = "NA";
                regAccount.Attr4 = "NA";
                regAccount.Attr5 = "NA";
                regAccount.Attr6 = "NA";
                regAccount.Attr7 = "NA";
                regAccount.Attr8 = "NA";
                regAccount.Attr9 = "NA";
                regAccount.Attr10 = "NA";
            }

            if ( performSubmit == 1 )
            { 

                var jsresponse = await mycallAPI.cdcallAccountsPUT(regAccount);

                if (jsresponse.ToString().Contains("ValidationException"))
                {
                    System.Diagnostics.Debug.WriteLine(" Account creation call failed " + jsresponse);
                    var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                    cdStatus.Text = "Account Creation Failed. " + myerror.message;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(" Put API Call Successful");
                    if (cdNewClub.IsChecked == true)
                    {
                        regClub.ClubName = cdClubName.Text;
                        regClub.AddressLine1 = cdcAddress1.Text;
                        if (cdcAddress2.Text == null)
                        {
                            System.Diagnostics.Debug.WriteLine(" AL2 is null ");
                        }
                        else
                        {
                            regClub.AddressLine2 = cdcAddress2.Text;
                        }
                        regClub.City = cdcCity.Text;
                        regClub.cdState = cdcState.Text;
                        regClub.PostalCode = cdcPostalCode.Text;
                        regClub.ClubID = cdClubName.Text.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                        regClub.ClubReg = "NA";
                        regClub.AddressLine3 = "NA";
                        regClub.Attr1 = "NA";
                        regClub.Attr2 = "NA";
                        regClub.Attr3 = "NA";
                        regClub.Attr4 = "NA";
                        regClub.Attr5 = "NA";
                        regClub.Attr6 = "NA";
                        regClub.Attr7 = "NA";
                        regClub.Attr8 = "NA";
                        regClub.Attr9 = "NA";
                        regClub.Attr10 = "NA";

                        jsresponse = await mycallAPI.cdcallClubsPUT(regClub);

                        if (jsresponse.ToString().Contains("ValidationException"))
                        {
                            System.Diagnostics.Debug.WriteLine(" Club creation call failed " + jsresponse);
                            var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                            cdStatus.Text = "Club Creation Failed. " + myerror.message;
                        }
                        else
                        {
                            ClubMembers myclubmembership = new ClubMembers();
                            myclubmembership.ClubMemberID = regAccount.UserName + regClub.ClubID;
                            myclubmembership.ClubID = regClub.ClubID;
                            myclubmembership.MemberAccountID = regAccount.UserName;
                            myclubmembership.ClubName = regClub.ClubName;
                            myclubmembership.MemberName = regAccount.FirstName + " " + regAccount.LastName;
                            myclubmembership.MemberRole = regAccount.Role;
                            myclubmembership.Attr1 = "NA";
                            myclubmembership.Attr2 = "NA";
                            myclubmembership.Attr3 = "NA";
                            myclubmembership.Attr4 = "NA";
                            myclubmembership.Attr5 = "NA";
                            myclubmembership.Attr6 = "NA";
                            myclubmembership.Attr7 = "NA";
                            myclubmembership.Attr8 = "NA";
                            myclubmembership.Attr9 = "NA";
                            myclubmembership.Attr10 = "NA";

                            jsresponse = await mycallAPI.cdcallClubMemberPUT(myclubmembership);
                            if (jsresponse.ToString().Contains("ValidationException"))
                            {
                                System.Diagnostics.Debug.WriteLine(" Club Membership creation call failed " + jsresponse);
                                var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                                cdStatus.Text = "Club Member Creation Failed. " + myerror.message;
                            }
                            else
                            {
                                cdStatus.Text = "Registration Successful ";
                            }
                        }

                    }
                }
            }

        }

        public cdAccounts2(Account pAccount, Club pClub, String pPassword)
        {
            InitializeComponent();
            regAccount = pAccount;
            regClub = pClub ;
            regPassword = pPassword;
            cdClubName.Text = pClub.ClubName;
            cdcAddress1.Text = pClub.AddressLine1;
            cdcAddress2.Text = pClub.AddressLine2;
            cdcCity.Text = pClub.City;
            cdcState.Text = pClub.cdState;
            cdcPostalCode.Text = pClub.PostalCode;
        }
    }
}
