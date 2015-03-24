﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOEN341_nobean.Class;
using System.Data;
namespace SOEN341_nobean
{

    /// <summary>
    /// after logging in and before going to the next page
    /// you have to save an object of the user in class Global + his preferences + record
    /// AND you have to create Course Directory object and save it also in Global 
    /// so everyone can have access to these while logging in and not let him wait at every page
    /// 
    /// second you have to have a button on the login page 'Sign Up' -> registration page.. this page makes you add data and sign up 
    /// you have to check that netname is not repeated in the database and email is a valid email and not repeated in the database
    /// after registration successfully you go back to login page
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection tempConnection = new SqlConnection();
            try
            {
                tempConnection.ConnectionString = "Data Source=buax9l2psh.database.windows.net,1433;Initial Catalog=masterscheduler100_db;Persist Security Info=True;User ID=nobean;Password=Abc_12345";
                Global.myConnection = tempConnection;
                
                if(Global.myConnection != null && Global.myConnection.State == ConnectionState.Closed)
                       Global.myConnection.Open();

                TextBox3.Text = TextBox3.Text + "Test User to login \nNetName: test123\nPass: 123test\n";
            }
            catch (Exception exp)
            {
                TextBox3.Text = TextBox3.Text + exp.ToString() + "\n";
                Console.WriteLine(exp.ToString());
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                //this will be in DBHandler class so you'll only use getUser(& netName&) and it will return and object of User 
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(
                    "SELECT Password FROM [dbo].[User] WHERE netName = @netName;", Global.myConnection);
                SqlParameter myParam = new SqlParameter("@netName", SqlDbType.VarChar, 11);
                myParam.Value = TextBox1.Text;
                myCommand.Parameters.Add(myParam);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    string tempPass = myReader["Password"].ToString();

                    if (TextBox2.Text == tempPass)
                    {
                       
                        Server.Transfer("Home.aspx");
                    }
                    else
                        TextBox3.Text = TextBox3.Text + "\nPasswords doesn't match!! \n";
                }
            }
            catch (Exception exp)
            {
                TextBox3.Text = TextBox3.Text + exp.ToString() + "\n";
                Console.WriteLine(exp.ToString());
            }

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}