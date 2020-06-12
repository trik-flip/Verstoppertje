using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Verstoppertje.Properties;

namespace Verstoppertje.Connection
{
    /// <summary>
    /// Back-end side of the 'Hide and Seek' Game
    /// Handels rooms, pictures, text, webrequests, input, output
    /// mostly threaded so there isn't any lag feeling to it
    /// </summary>
    class Game
    {
        #region Webrequest
        // these values are used for the web request
        private const string URL = "http://localhost:8080/json.htm?username=cm9vdA==&password=cm9vdA==&";
        private const string SINGELDEVICE = "type=devices&rid=";
        private const string ALLDEVICES = "type=devices&filter=all&used=true&order=Name";
        private const string TOGGLESWITCH = "type=command&param=switchlight&idx={0}&switchcmd={1}";
        #endregion

        #region game configurations
        private const int STARTID = 38;
        private const int ENDID = 45;
        private const int CAMERATIMEOUT = 10;
        private const int POWERUPTIMEOUT = 8;
        private const int POWERUPTIME = 20 + POWERUPTIMEOUT;
#if DEBUG
        private const int REFRESHRATE = 2;
#else
        private const int REFRESHRATE = 10;
#endif
        #endregion

        #region Game variables
        private int BuutId;
        private readonly string[] Rooms = { "Kitchen", "Entrance", "Entrance 2", "Pantry", "Laundry", "Family", "Living", "Bathroom" };
        private Dictionary<string, Image> ROOMPICTURES_HGHLIGHT = new Dictionary<string, Image>();
        private Dictionary<string, Image> ROOMPICTURES = new Dictionary<string, Image>();
        // is not used in Debug Mode, because there is no powerup in debug mode
        private bool powerUpActive = false;
        private int timeStampPowerUp;
        private int timeStampCamera;
        private Dictionary<string, int> RoomIds = new Dictionary<string, int>();
        private List<Thread> openThreads = new List<Thread>();
        private WebClient client = new WebClient();
        private List<Switch> switchesList = new List<Switch>();
        private Zoeker_App zoekerApp;
        private bool playing = true;
        #endregion
        private string room;
        #region Setup-up fase
        /// <summary>
        /// Creates a Game, it handels everything, from picture replacement, to web requests. to powerup/ camera usage.
        /// it is the core of the program.
        /// </summary>
        /// <param name="zoekerApp"></param>
        public Game(Zoeker_App zoekerApp)
        {
            timeStampPowerUp = 0;
            timeStampCamera = 0;
            this.zoekerApp = zoekerApp;
            Console.WriteLine("Check 1");
            CreatHLDictionary();
            Console.WriteLine("Check 2");
            CreatDictionary();
            Console.WriteLine("Check 3");
            Setup();
            foreach(Switch @switch in switchesList)
            {
                RoomIds.Add(@switch.Name, @switch.Idx);
                Console.WriteLine("Added:" + @switch.Name + ": id" + @switch.Idx);
            }
        }
        /// <summary>
        /// Creates a link from the roomname to the room picture on the floorplan
        /// is used for highlighted rooms
        /// </summary>
        private void CreatHLDictionary()
        {
            ROOMPICTURES_HGHLIGHT.Add("Kitchen", Resources.Floorplan_kitchen_HighLight);
            ROOMPICTURES_HGHLIGHT.Add("Entrance", Resources.Floorplan_Entrance_HighLight);
            ROOMPICTURES_HGHLIGHT.Add("Entrance 2", Resources.Floorplan_Entrance2_HighLight);
            ROOMPICTURES_HGHLIGHT.Add("Pantry", Resources.Floorplan_Pantry_HighLight);
            ROOMPICTURES_HGHLIGHT.Add("Laundry", Resources.Floorplan_Laundry_HighLight);
            ROOMPICTURES_HGHLIGHT.Add("Living", Resources.Floorplan_Living_HighLight);
            ROOMPICTURES_HGHLIGHT.Add("Family", Resources.Floorplan_Family_HighLight);
            ROOMPICTURES_HGHLIGHT.Add("Bathroom", Resources.Floorplan_Bathroom_HighLight);
        }
        /// <summary>
        /// Creates a link from the roomname to the room picture on the floorplan
        /// is used for Non highlighted rooms. 
        /// </summary>
        private void CreatDictionary()
        {
            ROOMPICTURES.Add("Kitchen", Resources.Floorplan_kitchen);
            ROOMPICTURES.Add("Entrance", Resources.Floorplan_Entrance);
            ROOMPICTURES.Add("Entrance 2", Resources.Floorplan_Entrance2);
            ROOMPICTURES.Add("Pantry", Resources.Floorplan_Pantry);
            ROOMPICTURES.Add("Laundry", Resources.Floorplan_Laundry);
            ROOMPICTURES.Add("Living", Resources.Floorplan_Living);
            ROOMPICTURES.Add("Family", Resources.Floorplan_Family);
            ROOMPICTURES.Add("Bathroom", Resources.Floorplan_Bathroom);
        }
        /// <summary>
        /// Start a thread for the gameloop nad
        /// </summary>
        public void Start()
        {
            Thread thread = new Thread(GameLoop);
            //openThreads.Add(thread);
            thread.Start();
        }

        /// <summary>
        /// The Gameloop keeps updating all the data in the game
        /// it updates all the "Lamp"'s every 0.REFRESHRATE seconds
        /// and outputs the data to the richtextbox (in DEBUG mode)
        /// </summary>
        private void GameLoop()
        {

            for(int i = STARTID; i <= ENDID; i++)
            {
                Thread thread = new Thread(() => GetLampStatus(i));
                openThreads.Add(thread);
                thread.Start();
                Thread.Sleep(200);
            }
            Thread buutThread = new Thread(GetBuutStatus);
            //openThreads.Add(buutThread);
            buutThread.Start();
#if DEBUG
            string text = "";
#endif
            while(playing)
            {
                Thread.Sleep(REFRESHRATE * 100);
#if DEBUG
                zoekerApp.SetRichTextBox(text);
                text = "";
#endif
                foreach(Switch singleSwitch in switchesList)
                {
                    if(singleSwitch.Idx >= STARTID && singleSwitch.Idx <= ENDID)
                    {
#if DEBUG
                        text += singleSwitch.ToString() + "\n";
#endif
                        foreach(string roome in Rooms)
                        {
                            this.room = roome;
#if DEBUG
                            if(singleSwitch.Name.Equals("Lampje - " + room) && singleSwitch.Status.Equals("On"))
#else
                            if(singleSwitch.Name.Equals("Lampje - " + room) && singleSwitch.Status.Equals("On") && powerUpActive)
#endif
                            {
                                zoekerApp.SetPicture(ROOMPICTURES_HGHLIGHT[room], room);
                            }
                            //else if(singleSwitch.Name.Equals("Lampje - " + room) && singleSwitch.Status.Equals("Off"))
                            else if(singleSwitch.Name.Equals("Lampje - " + room))
                            {
                                zoekerApp.SetPicture(ROOMPICTURES[room], room);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the data for all "Lamp"'s
        /// and saves it in the corrosponding switch
        /// is runned once at start-up
        /// </summary>
        public void Setup()
        {
            var response = client.DownloadString(URL + ALLDEVICES);
            Dictionary<string, object> json = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ToString());
            foreach(string j in json["result"].ToString().Substring(1, json["result"].ToString().Length - 4).Split('{'))
            {
                if(5 < j.Length)
                {
                    string i = "{" + j.Substring(0, j.Length - 5);
                    if(switchesList.Count == 26)
                    {
                        i += '}';
                    }
                    Dictionary<string, object> switche = JsonConvert.DeserializeObject<Dictionary<string, object>>(i);
                    Switch newSwitch = new Switch(switche);
                    switchesList.Add(newSwitch);
                }
            }
            foreach(string room in Rooms)
            {
                zoekerApp.SetPicture(ROOMPICTURES[room], room);
            }
            List<int> knoppenList = new List<int>();
            foreach(Switch @switch in switchesList)
            {
                if(@switch.Name.Contains("Knopje"))
                {
                    knoppenList.Add(@switch.Idx);
                }
            }
#if DEBUG
            // dit is de woonkamer AKA "Living"
            BuutId = 10;
#else
            Random random = new Random();
            BuutId = knoppenList[random.Next(knoppenList.Count)];
            Console.WriteLine(switchesList.Find(x => x.Idx == BuutId).Name);
#endif
        }
        #endregion

        #region Gameloop fase
        /// <summary>
        /// is used in a single Thread
        /// get's the status of a single switch("Lamp")
        /// </summary>
        /// <param name="id"></param>
        private void GetLampStatus(int id)
        {
            while(true)
            {
                Thread.Sleep(REFRESHRATE * 100);
                Dictionary<string, object> json;
                lock(client)
                {
                    var response = client.DownloadString(URL + SINGELDEVICE + id);
                    client.Dispose();
                    json = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ToString());
                }
                string jsonString = "{" + json["result"].ToString().Substring(1, json["result"].ToString().Length - 4).Split('{')[1];
                json = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                string switchStatus = json["Status"].ToString();
                Switch oldSwitch = switchesList.Find(x => x.Idx == id);
                if(oldSwitch.Status != switchStatus)
                {
                    lock(switchesList)
                    {
                        switchesList[switchesList.IndexOf(oldSwitch)].Status = switchStatus;
                    }
                }
            }
        }
        /// <summary>
        /// Gets the status of the buut, this methode is run from a thread so it won't lag the front-end
        /// </summary>
        private void GetBuutStatus()
        {
            while(true)
            {
                Thread.Sleep(REFRESHRATE * 100);
                Dictionary<string, object> json;
                lock(client)
                {
                    var response = client.DownloadString(URL + SINGELDEVICE + BuutId);
                    client.Dispose();
                    json = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ToString());
                }
                string jsonString = "{" + json["result"].ToString().Substring(1, json["result"].ToString().Length - 4).Split('{')[1];
                json = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                string switchStatus = json["Status"].ToString();
                Switch oldSwitch = switchesList.Find(x => x.Idx == BuutId);
                if(oldSwitch.Status != switchStatus)
                {
                    lock(switchesList)
                    {
                        switchesList[switchesList.IndexOf(oldSwitch)].Status = switchStatus;
                    }
                }
                if(switchStatus == "On")
                {
                    Lose();
                }
            }
        }
        // Limiteren tot 1 lamp aan per keer, of 1 lamp aanzetten per 5 seconden ofzo
        /// <summary>
        /// turns a lamp on when it's off, and off when it's on, used by the seeker to find the hider
        /// </summary>
        /// <param name="roomName"></param>
        public void ToggleLamp(string roomName)
        {
            int id = RoomIds[roomName];
            lock(client)
            {
                string s = String.Format(TOGGLESWITCH, id, "Toggle");
                var response = client.DownloadString(URL + s);
#if DEBUG
                Console.WriteLine(response.ToString());
#endif
            }
        }
        /// <summary>
        /// used in a Thread, activates the powerup which is motion detection
        /// and auto turns off after 5 seconds
        /// does only works every POWERUPTIMEOUT seconds
        /// </summary>
        public void ActivatePowerUp()
        {
            if((DateTime.Now.Second - timeStampPowerUp) > POWERUPTIMEOUT)
            {

                timeStampPowerUp = DateTime.Now.Second;
                powerUpActive = true;
#if DEBUG
                Console.WriteLine("PowerUp Actief");
#else
                zoekerApp.SetRichTextBox("Power-Up Actief");
#endif
                Thread.Sleep(POWERUPTIME * 1000);
                powerUpActive = false;
#if DEBUG
                Console.WriteLine("PowerUp niet Actief");
#else
                zoekerApp.SetRichTextBox("Power-Up niet Actief");
#endif
            }
            else
            {
#if DEBUG
                Console.WriteLine("Nog" + (POWERUPTIMEOUT - (DateTime.Now.Second - timeStampPowerUp)) + "Seconde");
#else
                zoekerApp.SetRichTextBox("Nog " + (POWERUPTIMEOUT - (DateTime.Now.Second - timeStampPowerUp)) + " Seconde voordat Power-up actief is");
#endif
            }
        }
        /// <summary>
        /// Checks if the camera is already used, and how long ago
        /// is limited to every CAMERATIMEOUT seconds
        /// </summary>
        /// <returns></returns>
        public bool CameraCheck()
        {
            if((DateTime.Now.Second - timeStampCamera) > CAMERATIMEOUT)
            {
                timeStampCamera = DateTime.Now.Second;
                return true;
            }
            else
            {
#if DEBUG
                Console.WriteLine("Nog" + (CAMERATIMEOUT - (DateTime.Now.Second - timeStampCamera)) + "Seconde");
#else
                zoekerApp.SetRichTextBox("Nog " + (CAMERATIMEOUT - (DateTime.Now.Second - timeStampCamera)) + " Seconde voordat camera actief is");
#endif
                return false;
            }
        }
        /// <summary>
        /// Checks if the knop is pressed in the same room
        /// indicating the hider is in that room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool checkKnop(string room)
        {
            foreach(Switch @switch in switchesList)
            {
                if(@switch.Name == "Knopje - " + room)
                {
                    int id = @switch.Idx;
                    Dictionary<string, object> json;
                    lock(client)
                    {
                        var response = client.DownloadString(URL + SINGELDEVICE + id);
                        client.Dispose();
                        json = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.ToString());
                    }
                    string jsonString = "{" + json["result"].ToString().Substring(1, json["result"].ToString().Length - 4).Split('{')[1];
                    json = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                    string switchStatus = json["Status"].ToString();
                    Switch oldSwitch = switchesList.Find(x => x.Idx == id);
                    Console.WriteLine(oldSwitch.Idx);
                    if(oldSwitch.Status != switchStatus)
                    {
                        lock(switchesList)
                        {
                            switchesList[switchesList.IndexOf(oldSwitch)].Status = switchStatus;
                        }
                    }
                    return (json["Status"].ToString() == "On");
                }
            }
            return false;
        }
        public void Win()
        {
            playing = false;
            foreach(Thread thread in openThreads)
            {
                thread.Abort();
            }
            zoekerApp.SetRichTextBox("Je hebt Gewonnen");
        }
        public void Lose()
        {
            playing = false;
            Console.WriteLine("||||||||||||||||||||||");
            foreach(Thread thread in openThreads)
            {
                thread.Abort();
            }
            Console.WriteLine("-------------------------------");
            zoekerApp.SetRichTextBox("Je hebt Verloren");
        }
        #endregion

        #region TODO's
        // Done: Check of zoeker in dezelfde kamer is als de verstopper
        // Done: Check if Buut is pressed
        // Done: Random generate location of buut
        // Done: Schakel alles uit als gewonnen of verloren
        // -> TODO: Check if Motion Sensor is active, otherwise show black camera Feed
        // TODO: Power-up invisible vertopper
        // TODO: Time voor spel duur
        // TODO: Score op basis van tijd, aantal gokken, aantal power-ups, aantal camera uses
        // TODO: lamp niet meer aan en uit zetten bij het klikken
        // TODO: Weergeven in welke kamer de buut is (in de console) 
        #endregion
    }
}
