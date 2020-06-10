using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Verstoppertje.Properties;

namespace Verstoppertje.Connection
{
    class Listener
    {
        private const string URL = "http://localhost:8080/json.htm?username=cm9vdA==&password=cm9vdA==&";
        private const string SINGELDEVICE = "type=devices&rid=";
        private const string ALLDEVICES = "type=devices&filter=all&used=true&order=Name";
        private const string TOGGLESWITCH = "type=command&param=switchlight&idx={0}&switchcmd={1}";
        private const int STARTID = 38;
        private const int ENDID = 45;
        private string[] Rooms = { "Kitchen", "Entrance", "Entrance 2", "Pantry", "Laundry", "Family", "Living", "Bathroom" };
        private Dictionary<string, Image> ROOMPICTURES_HGHLIGHT = new Dictionary<string, Image>();
        private Dictionary<string, Image> ROOMPICTURES = new Dictionary<string, Image>();

        private WebClient client = new WebClient();
        private List<Switch> switchesList = new List<Switch>();
        private Zoeker_App zoekerApp;
        public Listener(Zoeker_App zoekerApp)
        {
            this.zoekerApp = zoekerApp;
            creatHLDictionary();
            creatDictionary();
        }

        private void creatHLDictionary()
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

        private void creatDictionary()
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
        public void start()
        {
            Setup();
            Thread thread = new Thread(gameLoop);
            thread.Start();
        }

        /// <summary>
        /// Keeps updating all the data in the game
        /// it updates all the "Lamp"'s every 2 seconds
        /// and outputs the data to the richtextbox
        /// </summary>
        private void gameLoop()
        {
            List<Thread> threadList = new List<Thread>();
            for(int i = STARTID; i <= ENDID; i++)
            {
                Thread thread = new Thread(() => getLampStatus(i));
                threadList.Add(thread);
                thread.Start();
                Thread.Sleep(100);
            }
            while(true)
            {
                Thread.Sleep(2000);
                zoekerApp.SetRichTextBox("");
                foreach(Switch singleSwitch in switchesList)
                {
                    if(singleSwitch.Idx >= STARTID && singleSwitch.Idx <= ENDID)
                    {
#if DEBUG
                        zoekerApp.addToRichTextBox(singleSwitch.ToString() + "\n");
#endif
                        foreach(string room in Rooms)
                        {

                            if(singleSwitch.Name.Equals("Lampje - " + room) && singleSwitch.Status.Equals("On"))
                            {
                                zoekerApp.SetPicture(ROOMPICTURES_HGHLIGHT[room], room);
                            }
                            else if(singleSwitch.Name.Equals("Lampje - " + room) && singleSwitch.Status.Equals("Off"))
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
        }

        /// <summary>
        /// get the status of a single switch("Lamp")
        /// </summary>
        /// <param name="id"></param>
        private void getLampStatus(int id)
        {
            while(true)
            {
                Thread.Sleep(2000);
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
        public void ToggleLamp(int id)
        {
            string s = String.Format(TOGGLESWITCH, id, "Toggle");
            var response = client.DownloadString(URL + s);
        }
    }
}
