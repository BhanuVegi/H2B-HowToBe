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
                regAccount.AccountStatus = "Approved";
                if (regAccount.AddressLine1 is null)
                {
                    regAccount.AddressLine1 = "None";
                }
                if (regAccount.AddressLine2 is null)
                {
                    regAccount.AddressLine2 = "None";
                }
                if (regAccount.AddressLine1 is null)
                {
                    regAccount.AddressLine2 = "None";
                }
                if (regAccount.City is null)
                {
                    regAccount.City = "None";
                }
                if (regAccount.cdState is null)
                {
                    regAccount.cdState = "None";
                }
                if (regAccount.PostalCode is null)
                {
                    regAccount.PostalCode = "None";
                }
                if (regAccount.MiddleName is null || regAccount.MiddleName == " " || regAccount.MiddleName == "")
                {
                    System.Diagnostics.Debug.WriteLine(" Setting middle name to none ");
                    regAccount.MiddleName = "None";
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(" middle name is populated ");
                }
                regAccount.AddressLine3 = "None";
                regAccount.County = "None";
                regAccount.Destination = "None";
                if (regAccount.ParentID is null)
                {
                    regAccount.ParentID = "None";
                }
                if (regAccount.Phone is null)
                {
                    regAccount.Phone = "None";
                }
                if (regAccount.School == null)
                {
                    regAccount.School = "None";
                }
                if (regAccount.SchoolID is null)
                {
                    regAccount.SchoolID = "None";
                }
                if (regAccount.Teacher is null)
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
                regAccount.Attr1 = "None";
                regAccount.Attr2 = "None";
                regAccount.Attr3 = "None";
                regAccount.Attr4 = "None";
                regAccount.Attr5 = "None";
                regAccount.Attr6 = "None";
                regAccount.Attr7 = "None";
                regAccount.Attr8 = "None";
                regAccount.Attr9 = "None";
                regAccount.Attr10 = "None";
            }

            if ( performSubmit == 1 )
            { 

                var jsresponse = await mycallAPI.cdcallAccountsPUT(regAccount);

                if (jsresponse.ToString().Contains("ValidationException"))
                {
                    System.Diagnostics.Debug.WriteLine(" Account creation call failed " + jsresponse);
                    var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                    cdStatus.Text = "Account Creation Failed because " + myerror.message;
                    await DisplayAlert("Account creation failed", cdStatus.Text, "OK");
                }
                else
                {
                    signupAccount mysignupAccount = new signupAccount();
                    mysignupAccount.email = regAccount.EmailAddress;
                    mysignupAccount.username = regAccount.UserName;
                    mysignupAccount.password = regPassword;

                    var signupRespose = await mycallAPI.cdCreateSignup(mysignupAccount);

                    if (signupRespose.ToString().Contains("ValidationException"))
                    {
                        System.Diagnostics.Debug.WriteLine(" Account signup failed " + jsresponse);
                        var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                        cdStatus.Text = "Login creation failed. " + myerror.message;
                        await DisplayAlert("Login creation failed", cdStatus.Text, "OK");
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
                                regClub.AddressLine2 = "None";
                                System.Diagnostics.Debug.WriteLine(" AL2 is null ");
                            }
                            else
                            {
                                regClub.AddressLine2 = cdcAddress2.Text;
                            }
                            regClub.City = cdcCity.Text;
                            regClub.cdState = cdcState.Text;
                            regClub.PostalCode = cdcPostalCode.Text;
                            regClub.ClubID = cdClubName.Text.Substring(0, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
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
                            regClub.Attr9 = "Approved";
                            regClub.Attr10 = "NA";

                            jsresponse = await mycallAPI.cdcallClubsPUT(regClub);

                            if (jsresponse.ToString().Contains("ValidationException"))
                            {
                                System.Diagnostics.Debug.WriteLine(" Club creation call failed " + jsresponse);
                                var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                                cdStatus.Text = "Club Creation Failed. " + myerror.message;
                                await DisplayAlert("Club creation failed", cdStatus.Text, "OK");
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
                                myclubmembership.Attr2 = regAccount.EmailAddress;
                                myclubmembership.Attr3 = "NA";
                                myclubmembership.Attr4 = "NA";
                                myclubmembership.Attr5 = "NA";
                                myclubmembership.Attr6 = "NA";
                                myclubmembership.Attr7 = "NA";
                                myclubmembership.Attr8 = "NA";
                                myclubmembership.Attr9 = "NotApproved";
                                myclubmembership.Attr10 = "NA";

                                jsresponse = await mycallAPI.cdcallClubMemberPUT(myclubmembership);
                                if (jsresponse.ToString().Contains("ValidationException"))
                                {
                                    System.Diagnostics.Debug.WriteLine(" Club Membership creation call failed " + jsresponse);
                                    var myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                                    cdStatus.Text = "Club Member Creation Failed. " + myerror.message;
                                    await DisplayAlert("Club Member creation failed", cdStatus.Text, "OK");
                                }
                                else
                                {
                                    await DisplayAlert("Action", "Registration Successful", "Ok");
                                    var bpage = new MainPage();
                                    await Navigation.PushModalAsync(bpage);
                                }
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
