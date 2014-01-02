using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using Emotiv;
using System.Threading;


namespace CogInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Process cmd = new Process();

        static EmoEngine engine;
        static private bool emoStarted = false;
        private bool bConnectionType;
        static private bool processEngineEvents = false;

        Thread processEventsThread;
        private uint userId;

        

        AsynchronousSocketListener asl ;
        

        public MainWindow()
        {
            InitializeComponent();

            //ScoreSocketServer server = new ScoreSocketServer();
            //server.StartSocketServer();

            asl = new AsynchronousSocketListener();
            asl.StartListening();

            this.Closed +=new EventHandler(MainWindow_Closed);
            //call cearabean quest
            launch();
        }

        /// <summary>
        /// 
        /// </summary>
        private void launch()
        {

         // do i need a server and client
        // or would some raw camands be better

            // going to try sending the keys for 0-9 this is shitty but should work


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_Closed(object sender, EventArgs e) {
            

            // kills the thread 
            processEventsThread.Abort();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        { 
            //"C:\Program Files (x86)\Microsoft Silverlight\sllauncher.exe" 63053788.fireball.cs.uvic.ca
            Process cq = new Process();

            //"C:\Program Files (x86)\Microsoft Silverlight\sllauncher.exe" 2683824653.localhost 
            // so the argument that is passed in is different... not great
            //do i have to change this every build i install (my guess: yes)(actual result: ...)
            cq.StartInfo.FileName = "C:\\Program Files (x86)\\Microsoft Silverlight\\sllauncher.exe";// 63053788.fireball.cs.uvic.ca;
            cq.StartInfo.Arguments = "63053788.fireball.cs.uvic.ca";

            cq.Start();

            // <TODO> try catch for  launching this and enabing the emo buttons if successful

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // <todo> disable button if caribbean quest is not on 
            // though they could be seperate ...
 
            Process notePad = new Process();

            notePad.StartInfo.FileName = "notepad.exe";
            notePad.StartInfo.Arguments = "ProcessStart.cs";

            notePad.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (!emoStarted)
            {
                EmoLabel.Content = "EmoStatus: Starting the Emotiv Engine";
                System.Diagnostics.Debug.WriteLine("Starting the Emotiv Engine");
                try
                {
                    emo();
                    EmoButton.Content = "Deactiveate Emotiv";
                    emoStarted = true;
                }
                catch(Exception error)
                {
                    EmoLabel.Content = "EmoStatus: Error" + error;
                }

            }
            else
            {
                EmoLabel.Content = "EmoStatus: Disconnecting the Emotiv Engine";
                System.Diagnostics.Debug.WriteLine("Disconnecting the Emotiv Engine");
                engine.Disconnect();
                EmoButton.Content = "Emotiv";

                processEngineEvents = false;
                System.Diagnostics.Debug.WriteLine("processEngineEvents : false");
                //change the button content
                collectButton.Content = "Collect Data";
                //change the label info
                collectLabel.Content = "Collect: false";

                emoStarted = false;


            }
        }

        private void button3_Click_1(object sender, RoutedEventArgs e)
        {
            if (!processEngineEvents)
            {

                processEngineEvents = true;
                System.Diagnostics.Debug.WriteLine("processEngineEvents: true");

                //change the button content
                collectButton.Content = "Collecting";
                //change the label info
                collectLabel.Content = "Collect: true";
            }
            else
            {
                processEngineEvents = false;
                System.Diagnostics.Debug.WriteLine("processEngineEvents : false");

                //change the button content
                collectButton.Content = "Collect Data";
                //change the label info
                collectLabel.Content = "Collect: false";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void emo() 
        {
            engine = EmoEngine.Instance; 
            EmoLabel.Content = "EmoStatus: EmoEngine Instance Created"; System.Diagnostics.Debug.WriteLine("EmoStatus: EmoEngine Instance Created");

            // connection and state
            engine.EmoEngineConnected +=new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected); 
            engine.InternalStateChanged +=new EmoEngine.InternalStateChangedEventHandler(engine_InternalStateChanged);
            engine.EmoEngineEmoStateUpdated +=new EmoEngine.EmoEngineEmoStateUpdatedEventHandler(engine_EmoEngineEmoStateUpdated);
            engine.UserAdded +=new EmoEngine.UserAddedEventHandler(engine_UserAdded);

            bConnectionType = true;
            #region connect Engine
            if (bConnectionType)
            {
                //When your trying to connect directly 
                System.Diagnostics.Debug.WriteLine("Connect");
                EmoLabel.Content = "EmoStatus: Connect";
                engine.Connect();
            }
            else
            {
                // if you are using SDKlite or wish to connect your application to EmoComposer or Emotiv Control Panel.
                System.Diagnostics.Debug.WriteLine("remote connect");
                EmoLabel.Content = "EmoStatus: remote connect";
                engine.RemoteConnect("127.0.0.1",1726);
            }
            #endregion

            processEventsThread = new Thread(new ThreadStart(staticProcessEvents));
            processEventsThread.Name = "processEventsThread";
            processEventsThread.Start();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e) 
        {
            userId = e.userId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void engine_InternalStateChanged(object sender, EmoEngineEventArgs e)
        {
            Trace.WriteLine(e.ToString());
            System.Diagnostics.Debug.WriteLine(e.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void engine_EmoEngineEmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("engine_EmoStateUpdating");
            EmoState es = e.emoState;

            //System.Diagnostics.Debug.WriteLine("AffectivGetExcitementShortTermScore: " + es.AffectivGetExcitementShortTermScore());

            System.Diagnostics.Debug.WriteLine("AffectivGetEngagementBoredomScore:" + es.AffectivGetEngagementBoredomScore());
            //concentrationLabel.Content = "" + es.AffectivGetEngagementBoredomScore();

            //System.Diagnostics.Debug.WriteLine("AffectivGetFrustrationScore: " + es.AffectivGetFrustrationScore());

            //System.Diagnostics.Debug.WriteLine("AffectivGetMeditationScore: " + es.AffectivGetMeditationScore());

            //System.Diagnostics.Debug.WriteLine("" + Environment.NewLine);

            // send the information via winAPI key_press
            //if (!((bool)checkBox1.IsChecked))
            //{
                sendConcentrationLevel(es);
            //}
            // send the information via sockets
            //else if((bool)checkBox1.IsChecked)
            //{
                //asl.sendConcentrationLevelSockets(es);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void engine_UserAdded(object sender, EmoEngineEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("User added event has occured");

            // record the user
            userId = e.userId;
            System.Diagnostics.Debug.WriteLine("user ID added");
        }

        /// <summary>
        /// 
        /// </summary>
        static void staticProcessEvents()
        {
            System.Diagnostics.Debug.WriteLine("Process Events ");
            while (emoStarted)
            {
                while (processEngineEvents)// seems to jump past with it set as false default going to have to put a while around it
                {
                    engine.ProcessEvents(1000);
                    System.Diagnostics.Debug.WriteLine("events Processed");
                    Thread.Sleep(100); // so that it can catch up to the second for async prosessisn should be a second no?
                }
            }
            
        }

        

        void sendConcentrationLevel(EmoState es)
        {
            int attention = (int)Math.Round( es.AffectivGetEngagementBoredomScore() * 10);
            System.Diagnostics.Debug.WriteLine("attention: " + attention);

            switch (attention)
            {
                case 0:
                    virtual_keys.sendZero();
                    break;
                case 1:
                    virtual_keys.sendOne();
                    break;
                case 2:
                    virtual_keys.sendTwo();
                    break;
                case 3:
                    virtual_keys.sendThree();
                    break;
                case 4:
                    virtual_keys.sendFour();
                    break;
                case 5:
                    virtual_keys.sendFive();
                    break;
                case 6:
                    virtual_keys.sendSix();
                    break;
                case 7:
                    virtual_keys.sendSeven();
                    break;
                case 8:
                    virtual_keys.sendEight();
                    break;
                case 9:
                    virtual_keys.sendNine();
                    break;
                default:
                    break;
            }

            // might need this:  http://social.msdn.microsoft.com/Forums/en/csharpgeneral/thread/2d38d219-b6ea-42d8-92fe-5af344ea820e
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="es"></param>
        public void sendConcentrationLevelSockets(EmoState es)
        //public void sendConcentrationLevelSockets(Socket handler, EmoState es)
        {

            //// Get the socket that handles the client request.
            //Socket listener = (Socket)ar.AsyncState;
            //Socket handler = listener.EndAccept(ar);

            //int attention = (int)Math.Round(es.AffectivGetEngagementBoredomScore() * 10);
            //System.Diagnostics.Debug.WriteLine("attention: " + attention);

            //switch (attention)
            //{
            //    case 0:
            //        Send(handler, "Conc, 0 <EOF>");
            //        break;
            //    case 1:
            //        Send(handler, "Conc, 1 <EOF>");
            //        break;
            //    case 2:
            //        Send(handler, "Conc, 2 <EOF>");
            //        break;
            //    case 3:
            //        Send(handler, "Conc, 3 <EOF>");
            //        break;
            //    case 4:
            //        Send(handler, "Conc, 4 <EOF>");
            //        break;
            //    case 5:
            //        Send(handler, "Conc, 5 <EOF>");
            //        break;
            //    case 6:
            //        Send(handler, "Conc, 6 <EOF>");
            //        break;
            //    case 7:
            //        Send(handler, "Conc, 7 <EOF>");
            //        break;
            //    case 8:
            //        Send(handler, "Conc, 8 <EOF>");
            //        break;
            //    case 9:
            //        Send(handler, "Conc, 9 <EOF>");
            //        break;
            //    default:
            //        break;
            //}
        }


    }
}



/*
 * when trying to run it on the command line i got this error when no aruguments
 * 
 * navigated to here 
 *     C:\Program Files (x86)\Microsoft Silverlight\
 *run:
 *    sllauncher.exe" 
 * 
 * Usage: SLLauncher.exe [app_id][debug][/instal:<file path to XAP>]
 * [/emulate:<file path to xap>][/overwrite][/original app uri][/unistall]
 * [/shortcut:<desktop|startmenu|desktop+startmenu|none>][/pid]
 * 
 * ----------------------------------------------------------------------------------------
 * got it to run with this:
 * 
 * navigated to here 
 *     C:\Program Files (x86)\Microsoft Silverlight\
 *run:
 *    sllauncher.exe" 
 * 
 * with this argument:
 * 63053788.fireball.cs.uvic.ca
 * 
*/


   ////Process cmd = new Process();

   //         String strCmdLine = "c:\\";//"C:\Program Files (x86)\Microsoft Silverlight\sllauncher.exe" 63053788.fireball.cs.uvic.ca "

   //             //ProcessStartInfo info = new ProcessStartInfo();
   //             //info.FileName = "cmd.exe";
   //             //info.RedirectStandardInput = true;
   //             //info.UseShellExecute = false;

   //             //cmd.StartInfo = info;

   //             cmd.StartInfo.FileName = "CMD.exe"; // 63053788.fireball.cs.uvic.ca;
   //             //cmd.StartInfo.RedirectStandardInput = true;
   //             //cmd.StartInfo.UseShellExecute = false;
   //             cmd.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\Microsoft Silverlight\\";//strWorkingDirectory;
   //             cmd.StartInfo.Arguments = " " + strCmdLine;
   //             cmd.Start();

   //             //StreamWriter sw = cmd.StandardInput;
   //             //using (StreamWriter sw = cmd.StandardInput)
   //             //{
   //             //if (sw.BaseStream.CanWrite)
   //             //{
   //             //sw.WriteLine(" cd c:\\");
   //             //sw.WriteLine("C:\\Program Files (x86)\\Microsoft Silverlight\\sllauncher.exe 63053788.fireball.cs.uvic.ca");
   //             //sw.WriteLine("cmd.exe");
   //             //}
   //             //}
