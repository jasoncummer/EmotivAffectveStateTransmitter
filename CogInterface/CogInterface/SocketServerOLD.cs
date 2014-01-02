using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Timers;
using System.Xml;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;


namespace CogInterface
{
    //class SocketServer
    //{
    //}


    public enum ActionsEnum
    {
        Foul = 0,
        MadeFoulShot = 1,
        Made2Pointer = 2,
        Made3Pointer = 3,
        Turnover = 4,
        Miss = 5
    }

    public class ScoreSocketServer
    {
        TcpListener _Listener = null;
        System.Timers.Timer _Timer = null;
        Dictionary<string, Dictionary<Guid, string>> _Teams = null;
        static ManualResetEvent _TcpClientConnected = new ManualResetEvent(false);
        List<StreamWriter> _ClientStreams = new List<StreamWriter>();
        ScoreData _ScoreData = null;


        private void InitializeData()
        {
            _Teams = new Dictionary<string, Dictionary<Guid, string>>();

            Dictionary<Guid, string> _TeamPlayers1 = new Dictionary<Guid, string>();
            _TeamPlayers1.Add(Guid.NewGuid(), "K. Johnson");
            _TeamPlayers1.Add(Guid.NewGuid(), "B. Stoudemaire");
            _TeamPlayers1.Add(Guid.NewGuid(), "O. Neal");
            _TeamPlayers1.Add(Guid.NewGuid(), "B. Thomas");
            _TeamPlayers1.Add(Guid.NewGuid(), "T. Baker");

            Dictionary<Guid, string> _TeamPlayers2 = new Dictionary<Guid, string>();
            _TeamPlayers2.Add(Guid.NewGuid(), "S. Davidson");
            _TeamPlayers2.Add(Guid.NewGuid(), "C. Jamison");
            _TeamPlayers2.Add(Guid.NewGuid(), "M. Bryant");
            _TeamPlayers2.Add(Guid.NewGuid(), "A. Nash");
            _TeamPlayers2.Add(Guid.NewGuid(), "J. Doe");

            _Teams.Add("Bug Slayers", _TeamPlayers1);
            _Teams.Add("Code Warriors", _TeamPlayers2);

            _ScoreData = new ScoreData();
            _ScoreData.TeamOnOffense = "Bug Slayers";
        }

        public void StartSocketServer()
        {
            InitializeData();
            _Timer = new System.Timers.Timer();
            _Timer.Enabled = false;
            _Timer.Interval = 2000D;
            _Timer.Elapsed += new ElapsedEventHandler(_Timer_Elapsed);
            
            try
            {
                //Allowed port range 4502-4532
                _Listener = new TcpListener(IPAddress.Any, 4530);
                _Listener.Start();
                Console.WriteLine("Server listening...");
                while (true)
                {
                    _TcpClientConnected.Reset();
                    Console.WriteLine("Waiting for client connection...");
                    //CogInterface.MainWindow. connectionLabel
                    _Listener.BeginAcceptTcpClient(new AsyncCallback(OnBeginAccept),null);
                    _TcpClientConnected.WaitOne(); //Block until client connects
                }
            }
            catch (Exception exp)
            {
                LogError(exp);
            }       
        }

        private void OnBeginAccept(IAsyncResult ar)
        {
            _TcpClientConnected.Set(); //Allow waiting thread to proceed
            TcpListener listener = _Listener;
            TcpClient client = listener.EndAcceptTcpClient(ar);
            if (client.Connected)
            {
                Console.WriteLine("Client connected...");
                StreamWriter writer = new StreamWriter(client.GetStream());
                writer.AutoFlush = true;
                _ClientStreams.Add(writer);
                Console.WriteLine("Sending initial team data...");
                writer.WriteLine(GetTeamData());
                //for (int i = 0; i < 50; i++)
                //{
                //    SendData(GetScoreData());
                //    System.Threading.Thread.Sleep(5000);
                    
                //}
                if (_Timer.Enabled == false)
                {
                    _Timer.Start();
                }
            }
        }

        public void StopSocketServer()
        {
            foreach (StreamWriter writer in _ClientStreams)
            {
                writer.Dispose();
            }
            _ClientStreams.Clear();
            _Listener.Stop();
            _Listener = null;
        }

        private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           // MainWindow mw = MainWindow.
            //CogInterface
            SendData(GetScoreData());
            //Setting new interval to simulate different data feed times
            Random r = new Random();
            _Timer.Interval = (double)r.Next(3000, 7000);
        }

        private void SendData(string data)
        {
            if (_ClientStreams != null)
            {
                foreach (StreamWriter writer in _ClientStreams)
                {
                    if (writer != null)
                    {
                        writer.Write(data);
                    }
                }
            }
        }

        private string GetTeamData()
        {
            StringWriter sw = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(sw))
            {
                writer.WriteStartElement("Teams");
                foreach (string key in _Teams.Keys)
                {
                    writer.WriteStartElement("Team");
                    writer.WriteAttributeString("Name", key);
                    Dictionary<Guid, string> players = _Teams[key];
                    foreach (Guid playerKey in players.Keys)
                    {
                        writer.WriteStartElement("Player");
                        writer.WriteAttributeString("ID", playerKey.ToString());
                        writer.WriteAttributeString("Name", players[playerKey]);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            return sw.ToString();
        }

        private string GetScoreData()
        {
            UpdateScoreData();

            Console.WriteLine("Sending score data...");

            StringWriter sw = new StringWriter();
            XmlSerializer xm = new XmlSerializer(typeof(ScoreData));
            xm.Serialize(sw, _ScoreData);
            return sw.ToString();
        }

        private void UpdateScoreData()
        {
            Random r = new Random();
            //Get last action
            ActionsEnum action = (ActionsEnum)r.Next(0, Enum.GetNames(typeof(ActionsEnum)).Length);
            //Get player that performed action
            KeyValuePair<Guid,string> player = GetActionPlayer(action);
            string defensiveTeam = _Teams.Keys.Where(key => key != _ScoreData.TeamOnOffense).First();
            string message = _ScoreData.TeamOnOffense + ": " + player.Value;
            switch (action)
            {
                case ActionsEnum.Foul:
                    message = defensiveTeam + ": " + player.Value + " committed a foul.";
                    break;
                case ActionsEnum.Made2Pointer:
                    message += " scored 2 points.";
                    break;
                case ActionsEnum.Made3Pointer:
                    message += " scored 3 points.";
                    break;
                case ActionsEnum.MadeFoulShot:
                    message += " made a foul shot after foul.";
                    break;
                case ActionsEnum.Miss:
                    message += " missed.";
                    break;
                case ActionsEnum.Turnover:
                    message += " turned it over";
                    break;
            }


            int teamPos = 1;
            int points = ((int)action > 3)?0:(int)action;
            foreach (string name in _Teams.Keys)
            {
                if (teamPos == 1 && name == _ScoreData.TeamOnOffense)
                {
                    _ScoreData.Team1Score += points;
                }
                if (teamPos == 2 && name == _ScoreData.TeamOnOffense)
                {
                    _ScoreData.Team2Score += points;
                }
                teamPos++;
            }

            _ScoreData.Action = action;
            _ScoreData.LastActionPlayerID = player.Key;
            _ScoreData.LastAction = message;
            _ScoreData.LastActionPoints = points;

            //Change teams if a foul wasn't committed
            if (action != ActionsEnum.Foul)
            {
                _ScoreData.TeamOnOffense = defensiveTeam;
            }
        }

        private KeyValuePair<Guid, string> GetActionPlayer(ActionsEnum action)
        {
            Dictionary<Guid, string> players = null;
            if (action == ActionsEnum.Foul)
            {
                //Get defensive team players
                players = _Teams[_Teams.Keys.Where(name => name != _ScoreData.TeamOnOffense).First()];
            }
            else
            {
                players = _Teams[_Teams.Keys.Where(name => name == _ScoreData.TeamOnOffense).First()];
            }

            Random r = new Random();
            return players.ElementAt(r.Next(0, players.Count));
        }

        private void LogError(Exception exp)
        {
            string appFullPath = Assembly.GetCallingAssembly().Location;
            string logPath = appFullPath.Substring(0, appFullPath.LastIndexOf("\\")) + ".log";
            StreamWriter writer = new StreamWriter(logPath, true);
            try
            {
                writer.WriteLine(logPath,
                    String.Format("Error in ScoreSocketServer: {0} \r\n StackTrace: {1}", exp.Message, exp.StackTrace));
            }
            catch { }
            finally
            {
                writer.Close();
            }
        }
    }

    public class ScoreData
    {
        public ActionsEnum Action { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public string LastAction { get; set; }
        public Guid LastActionPlayerID { get; set; }
        public int LastActionPoints { get; set; }
        public string TeamOnOffense { get; set; }
    }
}

