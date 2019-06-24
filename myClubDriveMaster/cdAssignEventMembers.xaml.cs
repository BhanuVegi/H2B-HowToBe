using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using Plugin.InputKit;

namespace myClubDriveMaster
{
    public partial class cdAssignEventMembers : ContentPage
    {
        Account myAccount = new Account();
        String pClubName = "";
        String pEventID = "";
        String pEventName = "";
        String pClubID = "";
        String pEventAddress = "";
        int maxarray = -1;
        string[,] stmap = new string[10, 10];

        async void cdHome(object sender, System.EventArgs e)
        {
            var tpage = new cdHome(myAccount);
            await Navigation.PushModalAsync(tpage);
        }

        async void ieventmem(cdEventSignups iEveSign)
        {
            cdCallAPI mycallAPI = new cdCallAPI();
            cdReadError myerror = new cdReadError();
            var jsresponse = await mycallAPI.cdcallEventsMemberPUT(iEveSign);
            if (jsresponse.ToString().Contains("ValidationException"))
            {
                System.Diagnostics.Debug.WriteLine(" Post API Call failed " + jsresponse);
                myerror = JsonConvert.DeserializeObject<cdReadError>(jsresponse.ToString());
                insertMessage.Text = "Update Failed. " + myerror.message;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Post API Call Successful");
                insertMessage.Text = "Update Successful";
            }
        }

        void cdSubmit(object sender, System.EventArgs e)
        {
        
            cdEventSignups insertEventMembers = new cdEventSignups();
            insertEventMembers.EventID = pEventID;
            insertEventMembers.EventName = pEventName;
            insertEventMembers.ClubName = pClubName;
            insertEventMembers.ClubID = pClubID;
            insertEventMembers.PickupLocation = pEventAddress;
            insertEventMembers.Attr1 = "None";
            insertEventMembers.Attr2 = "None";
            insertEventMembers.Attr3 = "None";
            insertEventMembers.Attr4 = "None";
            insertEventMembers.Attr5 = "None";
            insertEventMembers.Attr6 = "None";
            insertEventMembers.Attr7 = "None";
            insertEventMembers.Attr8 = "None";
            insertEventMembers.Attr9 = "None";
            insertEventMembers.Attr10 = "None";


            for (int li=0; li <= maxarray; li++)
            { 
                if ( li == 0  & cdCheckStd1.IsChecked == true) 
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd1.Key,1]+ pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd1.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd1.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
                if (li == 1 & cdCheckStd2.IsChecked == true)
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd2.Key, 1] + pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd2.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd2.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
                if (li == 2 & cdCheckStd3.IsChecked == true)
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd3.Key, 1] + pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd3.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd3.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
                if (li == 3 & cdCheckStd4.IsChecked == true)
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd4.Key, 1] + pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd4.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd4.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
                if (li == 4 & cdCheckStd5.IsChecked == true)
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd5.Key, 1] + pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd5.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd5.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
                if (li == 5 & cdCheckStd6.IsChecked == true)
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd6.Key, 1] + pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd6.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd6.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
                if (li == 6 & cdCheckStd7.IsChecked == true)
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd7.Key, 1] + pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd7.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd7.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
                if (li == 7 & cdCheckStd8.IsChecked == true)
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd8.Key, 1] + pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd8.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd8.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
                if (li == 8 & cdCheckStd9.IsChecked == true)
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd9.Key, 1] + pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd9.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd9.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
                if (li == 9 & cdCheckStd10.IsChecked == true)
                {
                    insertEventMembers.EventMemberID = stmap[cdCheckStd10.Key, 1] + pClubName.Substring(1, 3) + (Math.Abs(DateTime.Now.ToBinary()).ToString());
                    insertEventMembers.MemberName = stmap[cdCheckStd10.Key, 0];
                    insertEventMembers.MemberAccountID = stmap[cdCheckStd10.Key, 1];
                    insertEventMembers.MemberRole = "R";
                    ieventmem(insertEventMembers);
                }
            }
        }

        async void getStudentInfo(Account logAccount)
        {
            cdQueryAttr qryAcct = new cdQueryAttr();
            qryAcct.ColIndex = "IndexName";
            qryAcct.IndexName = "ParentIDindex";
            qryAcct.ColName = "ParentID";
            qryAcct.ColValue = logAccount.UserName;

            getAccounts myStudentArray = new getAccounts();
            cdCallAPI mycallAPI = new cdCallAPI();

            var jsreponse = await mycallAPI.cdcallAccountsGET(qryAcct);
            myStudentArray = JsonConvert.DeserializeObject<getAccounts>((string)jsreponse);

            try
            {
                foreach (var stacc in myStudentArray.Account)
                {
                    maxarray = maxarray + 1;
                    if (maxarray == 0 )
                    {
                        cdCheckStd1.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[0,0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[0, 1] = stacc.UserName;
                        cdCheckStd1.Key = 0;
                    }
                    else if ( maxarray == 1)
                    {
                        cdCheckStd2.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[1, 0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[1, 1] = stacc.UserName;
                        cdCheckStd1.Key = 1;
                    }
                    else if (maxarray == 2)
                    {
                        cdCheckStd3.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[2, 0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[2, 1] = stacc.UserName;
                        cdCheckStd1.Key = 2;
                    }
                    else if (maxarray == 3)
                    {
                        cdCheckStd4.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[3, 0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[3, 1] = stacc.UserName;
                        cdCheckStd1.Key = 3;
                    }
                    else if (maxarray == 4)
                    {
                        cdCheckStd5.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[4, 0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[4, 1] = stacc.UserName;
                        cdCheckStd1.Key = 4;
                    }
                    else if (maxarray == 5)
                    {
                        cdCheckStd6.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[5, 0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[5, 1] = stacc.UserName;
                        cdCheckStd1.Key = 5;
                    }
                    else if (maxarray == 6)
                    {
                        cdCheckStd7.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[6, 0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[6, 1] = stacc.UserName;
                        cdCheckStd1.Key = 6;
                    }
                    else if (maxarray == 7)
                    {
                        cdCheckStd8.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[7, 0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[7, 1] = stacc.UserName;
                        cdCheckStd1.Key = 7;
                    }
                    else if (maxarray == 8)
                    {
                        cdCheckStd9.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[8, 0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[8, 1] = stacc.UserName;
                        cdCheckStd1.Key = 8;
                    }
                    else if (maxarray == 9)
                    {
                        cdCheckStd10.Text = stacc.FirstName + " " + stacc.LastName;
                        stmap[9, 0] = stacc.FirstName + " " + stacc.LastName;
                        stmap[9, 1] = stacc.UserName;
                        cdCheckStd1.Key = 9;
                    }
                }
                
                for (int tic=10; tic > maxarray; tic--)
                {
                    if(tic == 10)
                    {
                        cdCheckStd10.IsVisible = false;
                    }
                    if (tic == 9)
                    {
                        cdCheckStd9.IsVisible = false;
                    }
                    if (tic == 8)
                    {
                        cdCheckStd8.IsVisible = false;
                    }
                    if (tic == 7)
                    {
                        cdCheckStd7.IsVisible = false;
                    }
                    if (tic == 6)
                    {
                        cdCheckStd6.IsVisible = false;
                    }
                    if (tic == 5)
                    {
                        cdCheckStd5.IsVisible = false;
                    }
                    if (tic == 4)
                    {
                        cdCheckStd4.IsVisible = false;
                    }
                    if (tic == 3)
                    {
                        cdCheckStd3.IsVisible = false;
                    }
                    if (tic == 2)
                    {
                        cdCheckStd2.IsVisible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("End of Array " + ex);
            }

            System.Diagnostics.Debug.WriteLine(" Max Array is " + maxarray);

        }

        public cdAssignEventMembers(Account loginAccount, String ClubName, String EventID, String EventName)
        {
            InitializeComponent();
            myAccount = loginAccount;
            pClubName = ClubName;
            pEventID = EventID;
            pEventName = EventName;


            if ( loginAccount.Role.Contains("P") == true )
            {
                getStudentInfo(loginAccount);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(" Just use the student key ");
                cdCheckStd1.Text = loginAccount.FirstName + " " + loginAccount.LastName;
                cdCheckStd2.IsVisible = false;
                cdCheckStd3.IsVisible = false;
                cdCheckStd4.IsVisible = false;
                cdCheckStd5.IsVisible = false;
                cdCheckStd6.IsVisible = false;
                cdCheckStd7.IsVisible = false;
                cdCheckStd8.IsVisible = false;
                cdCheckStd9.IsVisible = false;
                cdCheckStd10.IsVisible = false;
            }

        }
    }
}
