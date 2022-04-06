using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace EggAdmin
{
    class Program
    {
        static string dev = "0";

        #region AuthShit
        //MAIN TOOL SHIT
        static string API_key = "API_KEY"; //MAIN TOOL API KEY

        //PANEL SHIT
        static string AppName = "APP_NAME";
        static string AID = "AID";
        static string Secret = "SECRET";
        static string Version = "1.0";
        static string AdminAPI_Key = "API_KEY"; //ADMIN PANEL API KEY
        #endregion

        #region Main
        static void Main(string[] args)
        {
            if (dev == "0")
            {
                Login();
            }
            else if (dev == "1")
            {
                GenLogo();
            }
        }

        static void DrawLogo()
        {
            // https://fsymbols.com/generators/tarty/ <--- WEBSITE TO MAKE LOGO SPED
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("███████╗██████╗░██╗███████╗██████╗░███████╗░██████╗░░██████╗░░██████╗");
            Console.WriteLine("██╔════╝██╔══██╗██║██╔════╝██╔══██╗██╔════╝██╔════╝░██╔════╝░██╔════╝");
            Console.WriteLine("█████╗░░██████╔╝██║█████╗░░██║░░██║█████╗░░██║░░██╗░██║░░██╗░╚█████╗░");
            Console.WriteLine("██╔══╝░░██╔══██╗██║██╔══╝░░██║░░██║██╔══╝░░██║░░╚██╗██║░░╚██╗░╚═══██╗");
            Console.WriteLine("██║░░░░░██║░░██║██║███████╗██████╔╝███████╗╚██████╔╝╚██████╔╝██████╔╝");
            Console.WriteLine("╚═╝░░░░░╚═╝░░╚═╝╚═╝╚══════╝╚═════╝░╚══════╝░╚═════╝░░╚═════╝░╚═════╝░");
            Console.WriteLine("");
            Console.WriteLine("██████╗░░█████╗░███╗░░██╗███████╗██╗░░░░░");
            Console.WriteLine("██╔══██╗██╔══██╗████╗░██║██╔════╝██║░░░░░");
            Console.WriteLine("██████╔╝███████║██╔██╗██║█████╗░░██║░░░░░");
            Console.WriteLine("██╔═══╝░██╔══██║██║╚████║██╔══╝░░██║░░░░░");
            Console.WriteLine("██║░░░░░██║░░██║██║░╚███║███████╗███████╗");
            Console.WriteLine("╚═╝░░░░░╚═╝░░╚═╝╚═╝░░╚══╝╚══════╝╚══════╝");
            Console.WriteLine("");
        }

        static void GenLogo()
        {
            DrawLogo();
            Console.WriteLine("USE THE NUMBERS 0-2 TO SELECT A SECTION");
            Console.WriteLine("");
            Console.WriteLine("[0] - Token Stuff");
            Console.WriteLine("[1] - User Stuff");
            Console.WriteLine("[2] - Panel Stuff");
            Console.WriteLine("");
            Console.WriteLine("Command:");
            string cmd = Console.ReadLine().ToString();
            if (cmd == "0")
            {
                if (User.Rank == "2" || User.Rank == "3")
                {
                    TokenTab();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Unauthorized");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("[N] - New Session");
                    Console.Beep();
                    string saving = Console.ReadLine();
                    if (saving == "N" || saving == "n")
                    {
                        Console.Clear();
                        GenLogo();
                    }
                }
            }
            if (cmd == "1")
            {
                UserTab();
            }
            if (cmd == "2")
            {
                if (User.Rank == "3")
                {
                    PanelControl();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Unauthorized");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("[N] - New Session");
                    Console.Beep();
                    string saving = Console.ReadLine();
                    if (saving == "N" || saving == "n")
                    {
                        Console.Clear();
                        GenLogo();
                    }
                }
            }
        }

        static void TokenTab()
        {
            DrawLogo();
            Console.WriteLine("USE THE NUMBERS 0-2 TO SELECT A COMMAND");
            Console.WriteLine("");
            Console.WriteLine("[0] - Generate Tokens");
            Console.WriteLine("[1] - Delete All Tokens");
            Console.WriteLine("");
            Console.WriteLine("[2] - Back To Main Page");
            Console.WriteLine("");
            Console.WriteLine("Command:");
            string cmd = Console.ReadLine().ToString();
            if (cmd == "0")
            {
                GenerateTokens();
            }
            if (cmd == "1")
            {
                if (User.Rank == "3")
                {
                    DeleteAllTokens();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Unauthorized");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("[N] - New Session");
                    Console.Beep();
                    string saving = Console.ReadLine();
                    if (saving == "N" || saving == "n")
                    {
                        Console.Clear();
                        GenLogo();
                    }
                }
            }
            if (cmd == "2")
            {
                GenLogo();
            }
        }

        static void UserTab()
        {
            DrawLogo();
            Console.WriteLine("USE THE NUMBERS 0-3 TO SELECT A COMMAND");
            Console.WriteLine("");
            Console.WriteLine("[0] - Reset User HWID");
            Console.WriteLine("[1] - Change Password");
            Console.WriteLine("[2] - User Information");
            Console.WriteLine("[3] - Delete User");
            Console.WriteLine("[4] - Change User Rank");
            Console.WriteLine("");
            Console.WriteLine("[5] - Back To Main Page");
            Console.WriteLine("");
            Console.WriteLine("Command:");
            string cmd = Console.ReadLine().ToString();
            if (cmd == "0")
            {
                ResetHWID();
            }
            if (cmd == "1")
            {
                if (User.Rank == "2" || User.Rank == "3")
                {
                    ChangePass();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Unauthorized");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("[N] - New Session");
                    Console.Beep();
                    string saving = Console.ReadLine();
                    if (saving == "N" || saving == "n")
                    {
                        Console.Clear();
                        GenLogo();
                    }
                }
            }
            if (cmd == "2")
            {
                UserInfo();
            }
            if (cmd == "3")
            {
                if (User.Rank == "3")
                {
                    DeleteUser();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Unauthorized");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("[N] - New Session");
                    Console.Beep();
                    string saving = Console.ReadLine();
                    if (saving == "N" || saving == "n")
                    {
                        Console.Clear();
                        GenLogo();
                    }
                }
            }
            if (cmd == "4")
            {
                if (User.Rank == "3")
                {
                    ChangeRank();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Unauthorized");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("[N] - New Session");
                    Console.Beep();
                    string saving = Console.ReadLine();
                    if (saving == "N" || saving == "n")
                    {
                        Console.Clear();
                        GenLogo();
                    }
                }

            }
            if (cmd == "5")
            {
                GenLogo();
            }
        }

        static void PanelControl()
        {
            DrawLogo();
            Console.WriteLine("USE THE NUMBERS 0-3 TO SELECT A COMMAND");
            Console.WriteLine("");
            Console.WriteLine("[0] - Make Panel Account");
            Console.WriteLine("[1] - Delete Panel Account");
            Console.WriteLine("[2] - List Panel Users");
            Console.WriteLine("");
            Console.WriteLine("[3] - Back To Main Page");
            Console.WriteLine("");
            Console.WriteLine("Command:");
            string cmd = Console.ReadLine().ToString();
            if (cmd == "0")
            {
                AddPanelUser();
            }
            if (cmd == "1")
            {
                DeletePanelUser();
            }
            if (cmd == "2")
            {
                ListPanelUsers();
            }
            if (cmd == "3")
            {
                GenLogo();
            }
        }
        #endregion

        #region Functions

        static void Login()
        {
            Console.WriteLine("");
            Console.WriteLine("███████╗██████╗░██╗███████╗██████╗░███████╗░██████╗░░██████╗░░██████╗");
            Console.WriteLine("██╔════╝██╔══██╗██║██╔════╝██╔══██╗██╔════╝██╔════╝░██╔════╝░██╔════╝");
            Console.WriteLine("█████╗░░██████╔╝██║█████╗░░██║░░██║█████╗░░██║░░██╗░██║░░██╗░╚█████╗░");
            Console.WriteLine("██╔══╝░░██╔══██╗██║██╔══╝░░██║░░██║██╔══╝░░██║░░╚██╗██║░░╚██╗░╚═══██╗");
            Console.WriteLine("██║░░░░░██║░░██║██║███████╗██████╔╝███████╗╚██████╔╝╚██████╔╝██████╔╝");
            Console.WriteLine("╚═╝░░░░░╚═╝░░╚═╝╚═╝╚══════╝╚═════╝░╚══════╝░╚═════╝░░╚═════╝░╚═════╝░");
            Console.WriteLine("");
            Console.WriteLine("██████╗░░█████╗░███╗░░██╗███████╗██╗░░░░░  ██╗░░░░░░█████╗░░██████╗░██╗███╗░░██╗");
            Console.WriteLine("██╔══██╗██╔══██╗████╗░██║██╔════╝██║░░░░░  ██║░░░░░██╔══██╗██╔════╝░██║████╗░██║");
            Console.WriteLine("██████╔╝███████║██╔██╗██║█████╗░░██║░░░░░  ██║░░░░░██║░░██║██║░░██╗░██║██╔██╗██║");
            Console.WriteLine("██╔═══╝░██╔══██║██║╚████║██╔══╝░░██║░░░░░  ██║░░░░░██║░░██║██║░░╚██╗██║██║╚████║");
            Console.WriteLine("██║░░░░░██║░░██║██║░╚███║███████╗███████╗  ███████╗╚█████╔╝╚██████╔╝██║██║░╚███║");
            Console.WriteLine("╚═╝░░░░░╚═╝░░╚═╝╚═╝░░╚══╝╚══════╝╚══════╝  ╚══════╝░╚════╝░░╚═════╝░╚═╝╚═╝░░╚══╝");
            Console.WriteLine("");
            Console.WriteLine("");
            OnProgramStart.Initialize(AppName, AID, Secret, Version);
            Console.Title = "FriedEggs Admin Login";
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            StringBuilder passwordBuilder = new StringBuilder();
            bool continueReading = true;
            char newLineChar = '\r';
            while (continueReading)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                char passwordChar = consoleKeyInfo.KeyChar;

                if (passwordChar == newLineChar)
                {
                    continueReading = false;
                }
                else
                {
                    passwordBuilder.Append(passwordChar.ToString());
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nAuthenticating...");
            Console.ForegroundColor = ConsoleColor.White;
            if (API.Login(username, passwordBuilder.ToString()))
            {
                Console.Clear();
                #region Json Shit
                WebClient eggclient = new WebClient();
                string userout = eggclient.DownloadString("https://developers.auth.gg/USERS/?type=count&authorization=" + API_key);
                dynamic usershit = JsonConvert.DeserializeObject<dynamic>(userout);

                string tokenout = eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=count&authorization=" + API_key);
                dynamic tokenshit = JsonConvert.DeserializeObject<dynamic>(tokenout);

                string status_u = usershit["status"].ToString();
                string status_t = tokenshit["status"].ToString();

                if (status_u == "failed")
                {
                    Console.WriteLine("\nInternal Error");
                    Console.ReadLine();
                }
                else if (status_u == "success")
                {
                    if (status_t == "failed")
                    {
                        Console.WriteLine("\nInternal Error");
                        Console.ReadLine();
                    }
                    else if (status_t == "success")
                    {
                        string value_u = usershit["value"].ToString();
                        string value_t = tokenshit["value"].ToString();
                        Console.Title = "FriedEggs Admin | Welcome, " + User.Username + " | Total Tokens: " + value_t + " | User Count: " + value_u;
                        Console.Beep();
                        GenLogo();
                    }
                }
                #endregion
            }
        }

        static void ChangePass()
        {
            WebClient eggclient = new WebClient();
            Console.WriteLine("");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("New Password: ");
            string newpass = Console.ReadLine();
            Console.Write("Confirm Password: ");
            string confirmpass = Console.ReadLine();
            if (newpass == confirmpass)
            {
                eggclient.DownloadString("https://developers.auth.gg/USERS/?type=changepw&authorization=" + API_key + "&user=" + username + "&password=" + confirmpass);
                Console.Beep();
                Console.Beep();
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    GenLogo();
                }
            }
            else
            {
                Console.WriteLine("Passwords Not The Same");
                Thread.Sleep(500);
                Console.Clear();
                //EggCMDS();
                GenLogo();
            }
        }

        static void GenerateTokens()
        {
            var TokenList = new List<string>();
            var Logo = new List<string>() { "███████╗░██████╗░░██████╗░  ████████╗░█████╗░██╗░░██╗███████╗███╗░░██╗  ██╗░░░░░██╗░██████╗████████╗", "██╔════╝██╔════╝░██╔════╝░  ╚══██╔══╝██╔══██╗██║░██╔╝██╔════╝████╗░██║  ██║░░░░░██║██╔════╝╚══██╔══╝", "█████╗░░██║░░██╗░██║░░██╗░  ░░░██║░░░██║░░██║█████═╝░█████╗░░██╔██╗██║  ██║░░░░░██║╚█████╗░░░░██║░░░", "██╔══╝░░██║░░╚██╗██║░░╚██╗  ░░░██║░░░██║░░██║██╔═██╗░██╔══╝░░██║╚████║  ██║░░░░░██║░╚═══██╗░░░██║░░░", "███████╗╚██████╔╝╚██████╔╝  ░░░██║░░░╚█████╔╝██║░╚██╗███████╗██║░╚███║  ███████╗██║██████╔╝░░░██║░░░", "╚══════╝░╚═════╝░░╚═════╝░  ░░░╚═╝░░░░╚════╝░╚═╝░░╚═╝╚══════╝╚═╝░░╚══╝  ╚══════╝╚═╝╚═════╝░░░░╚═╝░░░" };
            Console.WriteLine("");
            Console.WriteLine("Length of token(s) in days:");
            string days = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Amount of tokens:");
            int amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");
            Console.WriteLine("Level:");
            string level = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Who is this for:");
            string person = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Generating Tokens Please Wait...");
            #region Generate
            WebClient eggclient = new WebClient();
            string tokenout = eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=generate&days=" + days + "&amount=" + amount + "&level=" + level + "&length=8&authorization=" + API_key);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Tokens          | Length");
            Console.WriteLine("===============================");

            for (int index = 0; index < amount; index++)
            {
                dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(tokenout);
                string token = goodshit[index.ToString()].ToString();
                TokenList.Add(token);
                Console.WriteLine(token + "        " + days + " Days");
                if (index == amount - 1)
                {
                    Console.Beep();
                    Console.Beep();
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("[T] - Save Tokens To Txt File");
                    Console.WriteLine("[N] - New Session");

                    #region Json Shit
                    string userout = eggclient.DownloadString("https://developers.auth.gg/USERS/?type=count&authorization=" + API_key);
                    dynamic usershit = JsonConvert.DeserializeObject<dynamic>(userout);

                    string outtoken = eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=count&authorization=" + API_key);
                    dynamic tokenshit = JsonConvert.DeserializeObject<dynamic>(outtoken);

                    string status_u = usershit["status"].ToString();
                    string status_t = tokenshit["status"].ToString();

                    if (status_u == "failed")
                    {
                        Console.WriteLine("\nInternal Error");
                        Console.ReadLine();
                    }
                    else if (status_u == "success")
                    {
                        if (status_t == "failed")
                        {
                            Console.WriteLine("\nInternal Error");
                            Console.ReadLine();
                        }
                        else if (status_t == "success")
                        {
                            string value_u = usershit["value"].ToString();
                            string value_t = tokenshit["value"].ToString();
                            Console.Title = "FriedEggs Admin | Welcome, " + User.Username + " | Total Tokens: " + value_t + " | User Count: " + value_u;
                        }
                    }
                    #endregion

                    string saving = Console.ReadLine();
                    if (saving == "T" || saving == "t")
                    {
                        string tokenOutPath = @"C:\Users\" + Environment.UserName + "\\Desktop\\Tokens\\";

                        if (!Directory.Exists(tokenOutPath))
                        {
                            Directory.CreateDirectory(tokenOutPath);
                        }

                        foreach (object savelist in TokenList)
                        {
                            File.WriteAllLines(tokenOutPath + person + "_" + amount + "_" + days + "days.txt", Logo.Cast<string>().ToArray());
                            File.AppendAllText(tokenOutPath + person + "_" + amount + "_" + days + "days.txt", "========================================================================================" + "\n");
                            File.AppendAllText(tokenOutPath + person + "_" + amount + "_" + days + "days.txt", "Token(s) For: " + person + "\n");
                            File.AppendAllText(tokenOutPath + person + "_" + amount + "_" + days + "days.txt", "========================================================================================" + "\n");
                            File.AppendAllLines(tokenOutPath + person + "_" + amount + "_" + days + "days.txt", TokenList.Cast<string>().ToArray());
                        }
                        Process.Start(@"C:\Users\Death\Desktop\Tokens\");
                        Console.Clear();
                        GenLogo();
                    }
                    if (saving == "N" || saving == "n")
                    {
                        Console.Clear();
                        GenLogo();
                    }
                }
            }
            #endregion
        }

        static void ResetHWID()
        {
            Console.WriteLine("");
            Console.WriteLine("Username:");
            string person = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("");

            WebClient eggclient = new WebClient();
            string hwidout = eggclient.DownloadString("https://developers.auth.gg/HWID/?type=reset&authorization=" + API_key + "&user=" + person);
            dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(hwidout);
            string status = goodshit["status"].ToString();

            if (status == "success")
            {
                Console.WriteLine(person + "'s HWID Has Been Reset");
                Console.Beep();
                Console.Beep();
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    GenLogo();
                }
            }
        }

        static void UserInfo()
        {
            Console.WriteLine("");
            Console.WriteLine("Username:");
            string person = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("");

            WebClient eggclient = new WebClient();
            string hwidout = eggclient.DownloadString("https://developers.auth.gg/USERS/?type=fetch&authorization=" + API_key + "&user=" + person);
            dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(hwidout);
            string status = goodshit["status"].ToString();

            if (status == "success")
            {
                string username = goodshit["username"].ToString();
                string email = goodshit["email"].ToString();
                string hwid = goodshit["hwid"].ToString();
                string lastlogin = goodshit["lastlogin"].ToString();
                string lastip = goodshit["lastip"].ToString();
                string expiry = goodshit["expiry"].ToString();

                Console.WriteLine("Users Info");
                Console.WriteLine("============================================");
                Console.WriteLine("Username: " + username);
                Console.WriteLine("Email: " + email);
                Console.WriteLine("HWID: " + hwid);
                Console.WriteLine("LastLogin: " + lastlogin);
                Console.WriteLine("LastIP: " + lastip);
                Console.WriteLine("Expiry: " + expiry);
                Console.WriteLine("============================================");
                Console.Beep();
                Console.Beep();
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    GenLogo();
                }
            }
            else if (status == "failed")
            {
                Console.WriteLine("Unable To Get User Information");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                Console.Beep();
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    //EggCMDS();
                    GenLogo();
                }
            }
        }

        static void AddPanelUser()
        {
            string delToken = "";
            Console.WriteLine("");
            Console.WriteLine("Username:");
            string person = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("[S] - Support");
            Console.WriteLine("[R] - Reseller");
            Console.WriteLine("[A] - Admin");
            Console.WriteLine("");
            Console.WriteLine("Account Type:");
            string type = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("");

            if (type == "S" || type == "s")
            {
                WebClient eggclient = new WebClient();
                string tokenout = eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=generate&days=9998&amount=1&level=1&length=8&authorization=" + AdminAPI_Key);
                dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(tokenout);
                string token = goodshit["0"].ToString();
                delToken = token;
                string pass = RandomString(10);
                API.Register(person, pass, person + "@friedeggs.com", token);
                Console.WriteLine("New Panel User Info:");
                Console.WriteLine("============================================");
                Console.WriteLine("Username: " + person);
                Console.WriteLine("Password: " + pass);
                Console.WriteLine("Account Type: Support");
                Console.WriteLine("============================================");
            }
            if (type == "R" || type == "r")
            {
                WebClient eggclient = new WebClient();
                string tokenout = eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=generate&days=9998&amount=1&level=2&length=8&authorization=" + AdminAPI_Key);
                dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(tokenout);
                string token = goodshit["0"].ToString();
                delToken = token;
                string pass = RandomString(10);
                API.Register(person, pass, person + "@friedeggs.com", token);
                Console.WriteLine("New Panel User Info:");
                Console.WriteLine("============================================");
                Console.WriteLine("Username: " + person);
                Console.WriteLine("Password: " + pass);
                Console.WriteLine("Account Type: Reseller");
                Console.WriteLine("============================================");
            }
            else if (type == "A" || type == "a")
            {
                WebClient eggclient = new WebClient();
                string tokenout = eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=generate&days=9998&amount=1&level=3&length=8&authorization=" + AdminAPI_Key);
                dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(tokenout);
                string token = goodshit["0"].ToString();
                delToken = token;
                string pass = RandomString(10);
                API.Register(person, pass, person + "@friedeggs.com", token);
                Console.WriteLine("New Panel User Info:");
                Console.WriteLine("============================================");
                Console.WriteLine("Username: " + person);
                Console.WriteLine("Password: " + pass);
                Console.WriteLine("Account Type: Admin");
                Console.WriteLine("============================================");

            }

            Console.Beep();
            Console.Beep();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("[N] - New Session");
            WebClient eggclientt = new WebClient();
            eggclientt.DownloadString("https://developers.auth.gg/LICENSES/?type=delete&license=" + delToken + "&authorization=" + AdminAPI_Key);
            string saving = Console.ReadLine();
            if (saving == "N" || saving == "n")
            {
                Console.Clear();
                GenLogo();
            }

        }

        static void DeleteAllTokens()
        {
            #region Json Shit
            var TokenList = new List<string>();
            WebClient eggclient = new WebClient();
            string tokencount = eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=count&authorization=" + API_key);
            string tokenout = eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=fetchall&authorization=" + API_key);
            dynamic tokenshit = JsonConvert.DeserializeObject<dynamic>(tokencount);
            string status_t = tokenshit["status"].ToString();
            int amount = tokenshit["value"];
            #endregion

            if (status_t == "failed")
            {
                Console.WriteLine("\nInternal Error");
                Console.ReadLine();
            }
            else if (status_t == "success")
            {
                Console.WriteLine("");
                Console.WriteLine("Deleting All Tokens, Please Wait This May Take A Moment Depending On How Many Tokens You Have");
                for (int index = 0; index < amount; index++)
                {
                    #region Title Shit
                    dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(tokenout);
                    string token = goodshit[index.ToString()]["token"].ToString();
                    TokenList.Add(token);
                    eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=delete&license=" + token + "&authorization=" + API_key);

                    string t_out = eggclient.DownloadString("https://developers.auth.gg/LICENSES/?type=count&authorization=" + API_key);
                    dynamic t_title = JsonConvert.DeserializeObject<dynamic>(t_out);

                    string userout = eggclient.DownloadString("https://developers.auth.gg/USERS/?type=count&authorization=" + API_key);
                    dynamic usershit = JsonConvert.DeserializeObject<dynamic>(userout);

                    string value_u = usershit["value"].ToString();
                    string value_t = t_title["value"].ToString();

                    Console.Title = "FriedEggs Admin | Welcome, " + User.Username + " | Total Tokens: " + value_t + " | User Count: " + value_u;
                    #endregion
                    if (index == amount - 1)
                    {
                        Console.Beep();
                        Console.Beep();
                        Console.WriteLine("\nDeleted " + amount + " Tokens");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("[N] - New Session");



                        string saving = Console.ReadLine();
                        if (saving == "N" || saving == "n")
                        {
                            Console.Clear();
                            GenLogo();
                        }
                    }

                }
            }

        }

        static void DeletePanelUser()
        {
            Console.WriteLine("");
            Console.WriteLine("Username:");
            string person = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("");

            WebClient eggclient = new WebClient();
            string delusr = eggclient.DownloadString("https://developers.auth.gg/USERS/?type=fetch&authorization=" + AdminAPI_Key + "&user=" + person);
            dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(delusr);
            string status = goodshit["status"].ToString();

            if (status == "success")
            {
                eggclient.DownloadString("https://developers.auth.gg/USERS/?type=delete&authorization=" + AdminAPI_Key + "&user=" + person);
                Console.Beep();
                Console.Beep();
                Console.WriteLine("User '" + person + "' Has Been Deleted");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    GenLogo();
                }
            }
            else if (status == "failed")
            {
                Console.WriteLine("Unable To Delete User");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    GenLogo();
                }
            }
        }

        static void ListPanelUsers()
        {
            WebClient eggclient = new WebClient();
            string usercount = eggclient.DownloadString("https://developers.auth.gg/USERS/?type=count&authorization=" + AdminAPI_Key);
            string allusers = eggclient.DownloadString("https://developers.auth.gg/USERS/?type=fetchall&authorization=" + AdminAPI_Key);
            dynamic userc_out = JsonConvert.DeserializeObject<dynamic>(usercount);
            int amount = userc_out["value"];
            string status_t = userc_out["status"].ToString();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("All Panel Users");
            Console.WriteLine("===============================");

            if (status_t == "failed")
            {
                Console.WriteLine("\nInternal Error");
                Console.ReadLine();
            }
            else if (status_t == "success")
            {
                for (int index = 0; index < amount; index++)
                {
                    dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(allusers);
                    string user = goodshit[index.ToString()]["username"].ToString();
                    Console.WriteLine(user);

                    if (index == amount - 1)
                    {
                        Console.Beep();
                        Console.Beep();
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("[N] - New Session");

                        string saving = Console.ReadLine();
                        if (saving == "N" || saving == "n")
                        {
                            Console.Clear();
                            GenLogo();
                        }
                    }
                }
            }
        }

        static void DeleteUser()
        {
            Console.WriteLine("");
            Console.WriteLine("Username:");
            string person = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("");

            WebClient eggclient = new WebClient();
            string delusr = eggclient.DownloadString("https://developers.auth.gg/USERS/?type=fetch&authorization=" + API_key + "&user=" + person);
            dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(delusr);
            string status = goodshit["status"].ToString();

            if (status == "success")
            {
                eggclient.DownloadString("https://developers.auth.gg/USERS/?type=delete&authorization=" + API_key + "&user=" + person);
                Console.Beep();
                Console.Beep();
                Console.WriteLine("User '" + person + "' Has Been Deleted");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    GenLogo();
                }
            }
            else if (status == "failed")
            {
                Console.WriteLine("Unable To Delete User");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                Console.Beep();
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    GenLogo();
                }
            }
        }

        static void ChangeRank()
        {
            Console.WriteLine("");
            Console.WriteLine("Username:");
            string person = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Rank (in numbers):");
            int rank = Convert.ToInt32(Console.ReadLine());

            WebClient eggclient = new WebClient();
            string rankk = eggclient.DownloadString("https://developers.auth.gg/USERS/?type=editrank&authorization=" + API_key + "&user=" + person + "&rank=" + rank);
            dynamic goodshit = JsonConvert.DeserializeObject<dynamic>(rankk);
            string status = goodshit["status"].ToString();

            if (status == "success")
            {
                Console.WriteLine(person + "'s Rank Has Been Reset");
                Console.Beep();
                Console.Beep();
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    GenLogo();
                }
            }
            else if (status == "failed")
            {
                Console.WriteLine("Unable To Change Users Rank");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("[N] - New Session");
                Console.Beep();
                string saving = Console.ReadLine();
                if (saving == "N" || saving == "n")
                {
                    Console.Clear();
                    //EggCMDS();
                    GenLogo();
                }
            }
        }


        #region Random String Shit
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #endregion

        #region AuthAPI
        internal class App
        {
            public static string GrabVariable(string name)
            {
                try
                {
                    if (User.ID != null || User.HWID != null || User.IP != null || !Constants.Breached)
                    {
                        return Variables[name];
                    }
                    else
                    {
                        Constants.Breached = true;
                        return "User is not logged in, possible breach detected!";
                    }
                }
                catch
                {
                    return "N/A";
                }
            }
            public static string Error = null;
            public static Dictionary<string, string> Variables = new Dictionary<string, string>();
        }
        internal class Constants
        {
            public static string Token { get; set; }

            public static string Date { get; set; }

            public static string APIENCRYPTKEY { get; set; }

            public static string APIENCRYPTSALT { get; set; }

            public static bool Breached = false;

            public static bool Started = false;

            public static string IV = null;

            public static string Key = null;

            public static string ApiUrl = "https://api.auth.gg/csharp/";

            public static bool Initialized = false;

            public static Random random = new Random();

            public static string RandomString(int length)
            {
                return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length).Select(s => s[random.Next(s.Length)]).ToArray());
            }

            public static string HWID()
            {
                return WindowsIdentity.GetCurrent().User.Value;
            }
        }
        internal class User
        {
            public static string ID { get; set; }

            public static string Username { get; set; }

            public static string Password { get; set; }

            public static string Email { get; set; }

            public static string HWID { get; set; }

            public static string IP { get; set; }

            public static string UserVariable { get; set; }

            public static string Rank { get; set; }

            public static string Expiry { get; set; }

            public static string LastLogin { get; set; }

            public static string RegisterDate { get; set; }
        }
        internal class ApplicationSettings
        {
            public static bool Status { get; set; }

            public static bool DeveloperMode { get; set; }

            public static string Hash { get; set; }

            public static string Version { get; set; }

            public static string Update_Link { get; set; }

            public static bool Freemode { get; set; }

            public static bool Login { get; set; }

            public static string Name { get; set; }

            public static bool Register { get; set; }
        }

        internal class OnProgramStart
        {
            public static string AID = null;

            public static string Secret = null;

            public static string Version = null;

            public static string Name = null;

            public static string Salt = null;

            public static void Initialize(string name, string aid, string secret, string version)
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(aid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version))
                {
                    Console.WriteLine("\nInvalid application information!");
                    Console.ReadLine();
                    Process.GetCurrentProcess().Kill();
                }
                AID = aid;
                Secret = secret;
                Version = version;
                Name = name;
                string[] response = new string[] { };
                using (WebClient wc = new WebClient())
                {

                    try
                    {
                        wc.Proxy = null;
                        Security.Start();
                        response = (Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
                        {
                            ["token"] = Encryption.EncryptService(Constants.Token),
                            ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString()),
                            ["aid"] = Encryption.APIService(AID),
                            ["session_id"] = Constants.IV,
                            ["api_id"] = Constants.APIENCRYPTSALT,
                            ["api_key"] = Constants.APIENCRYPTKEY,
                            ["session_key"] = Constants.Key,
                            ["secret"] = Encryption.APIService(Secret),
                            ["type"] = Encryption.APIService("start")

                        }))).Split("|".ToCharArray()));
                        if (Security.MaliciousCheck(response[1]))
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        if (Constants.Breached)
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        if (response[0] != Constants.Token)
                        {
                            Console.WriteLine("\nSecurity error has been triggered!");
                            Console.ReadLine();
                        }
                        switch (response[2])
                        {
                            case "success":
                                Constants.Initialized = true;
                                if (response[3] == "Enabled")
                                    ApplicationSettings.Status = true;
                                if (response[4] == "Enabled")
                                    ApplicationSettings.DeveloperMode = true;
                                ApplicationSettings.Hash = response[5];
                                ApplicationSettings.Version = response[6];
                                ApplicationSettings.Update_Link = response[7];
                                if (response[8] == "Enabled")
                                    ApplicationSettings.Freemode = true;
                                if (response[9] == "Enabled")
                                    ApplicationSettings.Login = true;
                                ApplicationSettings.Name = response[10];
                                if (response[11] == "Enabled")
                                    ApplicationSettings.Register = true;
                                if (ApplicationSettings.DeveloperMode)
                                {
                                    Console.WriteLine("\nApplication is in Developer Mode, bypassing integrity and update check!");
                                    File.Create(Environment.CurrentDirectory + "/integrity.log").Close();
                                    string hash = Security.Integrity(Process.GetCurrentProcess().MainModule.FileName);
                                    File.WriteAllText(Environment.CurrentDirectory + "/integrity.log", hash);
                                    Console.WriteLine("\nYour applications hash has been saved to integrity.txt, please refer to this when your application is ready for release!");
                                }
                                else
                                {
                                    if (response[12] == "Enabled")
                                    {
                                        if (ApplicationSettings.Hash != Security.Integrity(Process.GetCurrentProcess().MainModule.FileName))
                                        {
                                            Console.WriteLine("\nFile has been tampered with, couldn't verify integrity!");
                                            Console.ReadLine();
                                        }
                                    }
                                    if (ApplicationSettings.Version != Version)
                                    {
                                        Console.WriteLine("\nUpdate available press ENTER to download");
                                        Console.ReadLine();
                                        Process.Start(ApplicationSettings.Update_Link);
                                    }

                                }
                                if (ApplicationSettings.Status == false)
                                {
                                    Console.WriteLine("\nShits disabled retard");
                                    Console.ReadLine();
                                }
                                break;
                            case "binderror":
                                Console.WriteLine("\n" + Encryption.Decode("RmFpbGVkIHRvIGJpbmQgdG8gc2VydmVyLCBjaGVjayB5b3VyIEFJRCAmIFNlY3JldCBpbiB5b3VyIGNvZGUh"));
                                Console.ReadLine();
                                return;
                            case "banned":
                                Console.WriteLine("\nThis application has been banned for violating the TOS" + Environment.NewLine + "Contact us at support@auth.gg");
                                Console.ReadLine();
                                return;
                        }
                        Security.End();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                        Console.ReadLine();
                    }
                }
            }
        }

        internal class API
        {
            public static void Log(string username, string action)
            {
                if (!Constants.Initialized)
                {
                    Console.WriteLine("\nPlease initialize your application first!");
                    Console.ReadLine();
                }
                if (string.IsNullOrWhiteSpace(action))
                {
                    Console.WriteLine("\nMissing log information!");
                    Console.ReadLine();
                }
                string[] response = new string[] { };
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        Security.Start();
                        wc.Proxy = null;
                        response = (Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
                        {
                            ["token"] = Encryption.EncryptService(Constants.Token),
                            ["aid"] = Encryption.APIService(OnProgramStart.AID),
                            ["username"] = Encryption.APIService(username),
                            ["pcuser"] = Encryption.APIService(Environment.UserName),
                            ["session_id"] = Constants.IV,
                            ["api_id"] = Constants.APIENCRYPTSALT,
                            ["api_key"] = Constants.APIENCRYPTKEY,
                            ["data"] = Encryption.APIService(action),
                            ["session_key"] = Constants.Key,
                            ["secret"] = Encryption.APIService(OnProgramStart.Secret),
                            ["type"] = Encryption.APIService("log")
                        }))).Split("|".ToCharArray()));
                        Security.End();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                        Console.ReadLine();
                    }
                }
            }
            public static bool AIO(string AIO)
            {
                if (AIOLogin(AIO))
                {
                    return true;
                }
                else
                {
                    if (AIORegister(AIO))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            public static bool AIOLogin(string AIO)
            {
                if (!Constants.Initialized)
                {
                    Console.WriteLine("\nPlease initialize your application first!");
                    Console.ReadLine();
                }
                if (string.IsNullOrWhiteSpace(AIO))
                {
                    Console.WriteLine("\nMissing user login information!");
                    Console.ReadLine();
                }
                string[] response = new string[] { };
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        Security.Start();
                        wc.Proxy = null;
                        response = (Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
                        {
                            ["token"] = Encryption.EncryptService(Constants.Token),
                            ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString()),
                            ["aid"] = Encryption.APIService(OnProgramStart.AID),
                            ["session_id"] = Constants.IV,
                            ["api_id"] = Constants.APIENCRYPTSALT,
                            ["api_key"] = Constants.APIENCRYPTKEY,
                            ["username"] = Encryption.APIService(AIO),
                            ["password"] = Encryption.APIService(AIO),
                            ["hwid"] = Encryption.APIService(Constants.HWID()),
                            ["session_key"] = Constants.Key,
                            ["secret"] = Encryption.APIService(OnProgramStart.Secret),
                            ["type"] = Encryption.APIService("login")

                        }))).Split("|".ToCharArray()));
                        if (response[0] != Constants.Token)
                        {
                            Console.WriteLine("\nSecurity error has been triggered!");
                            Console.ReadLine();
                        }
                        if (Security.MaliciousCheck(response[1]))
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        if (Constants.Breached)
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        switch (response[2])
                        {
                            case "success":
                                Security.End();
                                User.ID = response[3];
                                User.Username = response[4];
                                User.Password = response[5];
                                User.Email = response[6];
                                User.HWID = response[7];
                                User.UserVariable = response[8];
                                User.Rank = response[9];
                                User.IP = response[10];
                                User.Expiry = response[11];
                                User.LastLogin = response[12];
                                User.RegisterDate = response[13];
                                string Variables = response[14];
                                foreach (string var in Variables.Split('~'))
                                {
                                    string[] items = var.Split('^');
                                    try
                                    {
                                        App.Variables.Add(items[0], items[1]);
                                    }
                                    catch
                                    {
                                        //If some are null or not loaded, just ignore.
                                        //Error will be shown when loading the variable anyways
                                    }
                                }
                                return true;
                            case "invalid_details":
                                Security.End();
                                return false;
                            case "time_expired":
                                Console.WriteLine("\nYour time on FriedEggs has expired!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                            case "hwid_updated":
                                Console.WriteLine("\nNew machine has been binded, re-open the application!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                            case "invalid_hwid":
                                Console.WriteLine("\nInvalid HWID!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                        Console.ReadLine();
                        Security.End();
                    }
                    return false;

                }
            }
            public static bool AIORegister(string AIO)
            {
                if (!Constants.Initialized)
                {
                    Console.WriteLine("\nPlease initialize your application first!");
                    Console.ReadLine();
                    Security.End();
                }
                if (string.IsNullOrWhiteSpace(AIO))
                {
                    Console.WriteLine("\nInvalid registrar information!");
                    Console.ReadLine();
                }
                string[] response = new string[] { };
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        Security.Start();
                        wc.Proxy = null;

                        response = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
                        {
                            ["token"] = Encryption.EncryptService(Constants.Token),
                            ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString()),
                            ["aid"] = Encryption.APIService(OnProgramStart.AID),
                            ["session_id"] = Constants.IV,
                            ["api_id"] = Constants.APIENCRYPTSALT,
                            ["api_key"] = Constants.APIENCRYPTKEY,
                            ["session_key"] = Constants.Key,
                            ["secret"] = Encryption.APIService(OnProgramStart.Secret),
                            ["type"] = Encryption.APIService("register"),
                            ["username"] = Encryption.APIService(AIO),
                            ["password"] = Encryption.APIService(AIO),
                            ["email"] = Encryption.APIService(AIO),
                            ["license"] = Encryption.APIService(AIO),
                            ["hwid"] = Encryption.APIService(Constants.HWID()),

                        }))).Split("|".ToCharArray());
                        if (response[0] != Constants.Token)
                        {
                            Console.WriteLine("\nSecurity error has been triggered!");
                            Console.ReadLine();
                            Security.End();
                        }
                        if (Security.MaliciousCheck(response[1]))
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        if (Constants.Breached)
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        Security.End();
                        switch (response[2])
                        {
                            case "success":
                                return true;
                            case "error":
                                return false;

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                        Console.ReadLine();
                    }
                    return false;
                }
            }
            public static bool Login(string username, string password)
            {
                if (!Constants.Initialized)
                {
                    Console.WriteLine("\nPlease initialize your application first!");
                    Console.ReadLine();
                }
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("\nMissing user login information!");
                    Console.ReadLine();
                }
                string[] response = new string[] { };
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        Security.Start();
                        wc.Proxy = null;
                        response = (Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
                        {
                            ["token"] = Encryption.EncryptService(Constants.Token),
                            ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString()),
                            ["aid"] = Encryption.APIService(OnProgramStart.AID),
                            ["session_id"] = Constants.IV,
                            ["api_id"] = Constants.APIENCRYPTSALT,
                            ["api_key"] = Constants.APIENCRYPTKEY,
                            ["username"] = Encryption.APIService(username),
                            ["password"] = Encryption.APIService(password),
                            ["hwid"] = Encryption.APIService(Constants.HWID()),
                            ["session_key"] = Constants.Key,
                            ["secret"] = Encryption.APIService(OnProgramStart.Secret),
                            ["type"] = Encryption.APIService("login")

                        }))).Split("|".ToCharArray()));
                        if (response[0] != Constants.Token)
                        {
                            Console.WriteLine("\nSecurity error has been triggered!");
                            Console.ReadLine();
                        }
                        if (Security.MaliciousCheck(response[1]))
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        if (Constants.Breached)
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        switch (response[2])
                        {
                            case "success":
                                User.ID = response[3];
                                User.Username = response[4];
                                User.Password = response[5];
                                User.Email = response[6];
                                User.HWID = response[7];
                                User.UserVariable = response[8];
                                User.Rank = response[9];
                                User.IP = response[10];
                                User.Expiry = response[11];
                                User.LastLogin = response[12];
                                User.RegisterDate = response[13];
                                string Variables = response[14];
                                foreach (string var in Variables.Split('~'))
                                {
                                    string[] items = var.Split('^');
                                    try
                                    {
                                        App.Variables.Add(items[0], items[1]);
                                    }
                                    catch
                                    {
                                        //If some are null or not loaded, just ignore.
                                        //Error will be shown when loading the variable anyways
                                    }
                                }
                                Security.End();
                                return true;
                            case "invalid_details":
                                Console.WriteLine("\nSorry, your username/password does not match!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                            case "time_expired":
                                Console.WriteLine("\nYour subscription has expired!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                            case "hwid_updated":
                                Console.WriteLine("\nNew machine has been binded, re-open the application!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                            case "invalid_hwid":
                                Console.WriteLine("\nInvalid HWID!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                        Console.ReadLine();
                        Security.End();
                    }
                    return false;

                }
            }
            public static bool Register(string username, string password, string email, string license)
            {
                if (!Constants.Initialized)
                {
                    Console.WriteLine("\nPlease initialize your application first!");
                    Console.ReadLine();
                    Security.End();
                }
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(license))
                {
                    Console.WriteLine("\nInvalid registrar information!");
                    Console.ReadLine();
                }
                string[] response = new string[] { };
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        Security.Start();
                        wc.Proxy = null;

                        response = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
                        {
                            ["token"] = Encryption.EncryptService(Constants.Token),
                            ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString()),
                            ["aid"] = Encryption.APIService(OnProgramStart.AID),
                            ["session_id"] = Constants.IV,
                            ["api_id"] = Constants.APIENCRYPTSALT,
                            ["api_key"] = Constants.APIENCRYPTKEY,
                            ["session_key"] = Constants.Key,
                            ["secret"] = Encryption.APIService(OnProgramStart.Secret),
                            ["type"] = Encryption.APIService("register"),
                            ["username"] = Encryption.APIService(username),
                            ["password"] = Encryption.APIService(password),
                            ["email"] = Encryption.APIService(email),
                            ["license"] = Encryption.APIService(license),
                            ["hwid"] = Encryption.APIService(Constants.HWID()),

                        }))).Split("|".ToCharArray());
                        if (response[0] != Constants.Token)
                        {
                            Console.WriteLine("\nSecurity error has been triggered!");
                            Console.ReadLine();
                            Security.End();
                        }
                        if (Security.MaliciousCheck(response[1]))
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        if (Constants.Breached)
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        switch (response[2])
                        {
                            case "success":
                                Security.End();
                                return true;
                            case "invalid_license":
                                Console.WriteLine("\nLicense does not exist!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                            case "email_used":
                                Console.WriteLine("\nEmail has already been used!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                            case "invalid_username":
                                Console.WriteLine("\nYou entered an invalid/used username!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                        Console.ReadLine();
                        Process.GetCurrentProcess().Kill();
                    }
                    return false;
                }
            }
            public static bool ExtendSubscription(string username, string password, string license)
            {
                if (!Constants.Initialized)
                {
                    Console.WriteLine("\nPlease initialize your application first!");
                    Console.ReadLine();
                    Security.End();
                }
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(license))
                {
                    Console.WriteLine("\nInvalid registrar information!");
                    Console.ReadLine();
                }
                string[] response = new string[] { };
                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        Security.Start();
                        wc.Proxy = null;
                        response = Encryption.DecryptService(Encoding.Default.GetString(wc.UploadValues(Constants.ApiUrl, new NameValueCollection
                        {
                            ["token"] = Encryption.EncryptService(Constants.Token),
                            ["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString()),
                            ["aid"] = Encryption.APIService(OnProgramStart.AID),
                            ["session_id"] = Constants.IV,
                            ["api_id"] = Constants.APIENCRYPTSALT,
                            ["api_key"] = Constants.APIENCRYPTKEY,
                            ["session_key"] = Constants.Key,
                            ["secret"] = Encryption.APIService(OnProgramStart.Secret),
                            ["type"] = Encryption.APIService("extend"),
                            ["username"] = Encryption.APIService(username),
                            ["password"] = Encryption.APIService(password),
                            ["license"] = Encryption.APIService(license),

                        }))).Split("|".ToCharArray());
                        if (response[0] != Constants.Token)
                        {
                            Console.WriteLine("\nSecurity error has been triggered!");
                            Console.ReadLine();
                            Security.End();
                        }
                        if (Security.MaliciousCheck(response[1]))
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        if (Constants.Breached)
                        {
                            Console.WriteLine("\nPossible malicious activity detected!");
                            Console.ReadLine();
                        }
                        switch (response[2])
                        {
                            case "success":
                                Security.End();
                                return true;
                            case "invalid_token":
                                Console.WriteLine("\nToken does not exist!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                            case "invalid_details":
                                Console.WriteLine("\nYour user details are invalid!");
                                Console.ReadLine();
                                Security.End();
                                return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                        Console.ReadLine();
                    }
                    return false;
                }
            }
        }
        internal class Security
        {
            public static string Signature(string value)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] input = Encoding.UTF8.GetBytes(value);
                    byte[] hash = md5.ComputeHash(input);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
            private static string Session(int length)
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
                return new string(Enumerable.Repeat(chars, length)
                 .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            public static string Obfuscate(int length)
            {
                Random random = new Random();
                const string chars = "gd8JQ57nxXzLLMPrLylVhxoGnWGCFjO4knKTfRE6mVvdjug2NF/4aptAsZcdIGbAPmcx0O+ftU/KvMIjcfUnH3j+IMdhAW5OpoX3MrjQdf5AAP97tTB5g1wdDSAqKpq9gw06t3VaqMWZHKtPSuAXy0kkZRsc+DicpcY8E9+vWMHXa3jMdbPx4YES0p66GzhqLd/heA2zMvX8iWv4wK7S3QKIW/a9dD4ALZJpmcr9OOE=";
                return new string(Enumerable.Repeat(chars, length)
                 .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            public static void Start()
            {
                string drive = Path.GetPathRoot(Environment.SystemDirectory);
                if (Constants.Started)
                {
                    Console.WriteLine("\nA session has already been started, please end the previous one!");
                    Console.ReadLine();
                }
                else
                {
                    using (StreamReader sr = new StreamReader($@"{drive}Windows\System32\drivers\etc\hosts"))
                    {
                        string contents = sr.ReadToEnd();
                        if (contents.Contains("api.auth.gg"))
                        {
                            Constants.Breached = true;
                            Console.WriteLine("\nDNS redirecting has been detected!");
                            Console.ReadLine();
                        }
                    }
                    InfoManager infoManager = new InfoManager();
                    infoManager.StartListener();
                    Constants.Token = Guid.NewGuid().ToString();
                    ServicePointManager.ServerCertificateValidationCallback += PinPublicKey;
                    Constants.APIENCRYPTKEY = Convert.ToBase64String(Encoding.Default.GetBytes(Session(32)));
                    Constants.APIENCRYPTSALT = Convert.ToBase64String(Encoding.Default.GetBytes(Session(16)));
                    Constants.IV = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(16)));
                    Constants.Key = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(32)));
                    Constants.Started = true;
                }
            }
            public static void End()
            {
                if (!Constants.Started)
                {
                    Console.WriteLine("\nNo session has been started, closing for security reasons!");
                    Console.ReadLine();
                }
                else
                {
                    Constants.Token = null;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    Constants.APIENCRYPTKEY = null;
                    Constants.APIENCRYPTSALT = null;
                    Constants.IV = null;
                    Constants.Key = null;
                    Constants.Started = false;
                }
            }
            private static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return certificate != null && certificate.GetPublicKeyString() == _key;
            }
            private const string _key = "3082010A0282010100D0A2FCAC2861DF72F05EE166613656F27D3C037B985FECFCB5D943BC28B40DD9C035FFE44E16C57772312A9457E54973E15D40DF91660E2914ACE0AC3705562F32F023EBF32BC218423AE9DA1C752FD843EC0176307E1EE97EFCA50510DBBC88C4A253A9A06C7646BFB30CE86B773708D4240AB72919898387C60FB2F0B1B4E579BB5BC9DA286C348DD81A1205C1C43BF522032C0CA4226E08C2108E847670363B292E8E90D8B541C03CB11B03A13A88BCCC209D899994F8EADDF43AE8BBE63214EC4817922EC9496855D47E00CA21B533950C5401C6E31A727BC1A14F025D7F94B3DB2D4EE749B05C83A68A3EB17A4E375CD5CE16904F0CB1F8B7B8E75A86D30203010001";
            public static string Integrity(string filename)
            {
                string result;
                using (MD5 md = MD5.Create())
                {
                    using (FileStream fileStream = File.OpenRead(filename))
                    {
                        byte[] value = md.ComputeHash(fileStream);
                        result = BitConverter.ToString(value).Replace("-", "").ToLowerInvariant();
                    }
                }
                return result;
            }
            public static bool MaliciousCheck(string date)
            {
                DateTime dt1 = DateTime.Parse(date); //time sent
                DateTime dt2 = DateTime.Now; //time received
                TimeSpan d3 = dt1 - dt2;
                if (Convert.ToInt32(d3.Seconds.ToString().Replace("-", "")) >= 5 || Convert.ToInt32(d3.Minutes.ToString().Replace("-", "")) >= 1)
                {
                    Constants.Breached = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        internal class Encryption
        {
            public static string APIService(string value)
            {
                string message = value;
                string password = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
                SHA256 mySHA256 = SHA256Managed.Create();
                byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
                byte[] iv = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
                string encrypted = EncryptString(message, key, iv);
                return encrypted;
            }
            public static string EncryptService(string value)
            {
                string message = value;
                string password = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
                SHA256 mySHA256 = SHA256Managed.Create();
                byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
                byte[] iv = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
                string encrypted = EncryptString(message, key, iv);
                int property = Int32.Parse((OnProgramStart.AID.Substring(0, 2)));
                string final = encrypted + Security.Obfuscate(property);
                return final;
            }
            public static string DecryptService(string value)
            {
                string message = value;
                string password = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
                SHA256 mySHA256 = SHA256Managed.Create();
                byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
                byte[] iv = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
                string decrypted = DecryptString(message, key, iv);
                return decrypted;
            }
            public static string EncryptString(string plainText, byte[] key, byte[] iv)
            {
                Aes encryptor = Aes.Create();
                encryptor.Mode = CipherMode.CBC;
                encryptor.Key = key;
                encryptor.IV = iv;
                MemoryStream memoryStream = new MemoryStream();
                ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);
                byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
                return cipherText;
            }

            public static string DecryptString(string cipherText, byte[] key, byte[] iv)
            {
                Aes encryptor = Aes.Create();
                encryptor.Mode = CipherMode.CBC;
                encryptor.Key = key;
                encryptor.IV = iv;
                MemoryStream memoryStream = new MemoryStream();
                ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
                string plainText = String.Empty;
                try
                {
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] plainBytes = memoryStream.ToArray();
                    plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
                }
                finally
                {
                    memoryStream.Close();
                    cryptoStream.Close();
                }
                return plainText;
            }
            public static string Decode(string text)
            {
                text = text.Replace('_', '/').Replace('-', '+');
                switch (text.Length % 4)
                {
                    case 2:
                        text += "==";
                        break;
                    case 3:
                        text += "=";
                        break;
                }
                return Encoding.UTF8.GetString(Convert.FromBase64String(text));
            }

        }
        class InfoManager
        {
            private System.Threading.Timer timer;
            private string lastGateway;

            public InfoManager()
            {
                lastGateway = GetGatewayMAC();
            }

            public void StartListener()
            {
                timer = new System.Threading.Timer(_ => OnCallBack(), null, 5000, Timeout.Infinite);
            }

            private void OnCallBack()
            {
                timer.Dispose();
                if (!(GetGatewayMAC() == lastGateway))
                {
                    //Constants.Breached = true;
                    //MessageBox.Show("ARP Cache poisoning has been detected!", OnProgramStart.Name);
                    //Process.GetCurrentProcess().Kill();
                }
                else
                {
                    lastGateway = GetGatewayMAC();
                }
                timer = new System.Threading.Timer(_ => OnCallBack(), null, 5000, Timeout.Infinite);
            }

            public static IPAddress GetDefaultGateway()
            {
                return NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(n => n.OperationalStatus == OperationalStatus.Up)
                    .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                    .Select(g => g?.Address)
                    .Where(a => a != null)
                    .FirstOrDefault();
            }

            private string GetArpTable()
            {
                string drive = Path.GetPathRoot(Environment.SystemDirectory);
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{drive}Windows\System32\arp.exe";
                start.Arguments = "-a";
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.CreateNoWindow = true;

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            private string GetGatewayMAC()
            {
                string routerIP = GetDefaultGateway().ToString();
                string regx = String.Format(@"({0} [\W]*) ([a-z0-9-]*)", routerIP);
                Regex regex = new Regex(@regx);
                Match matches = regex.Match(GetArpTable());
                return matches.Groups[2].ToString();
            }
        }
        #endregion
    }
}
