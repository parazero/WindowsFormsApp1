using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Net.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static int SleepAfterWriteLineEvent = 2000;
        static int NumberOfCycle = 0;

        static private bool TestTimeout = false;
        bool EndCondition = false;
        bool IdentifiedText = false;
        bool M200 = false;
        bool BoolVib = false;
        bool BoolImu = false;
        bool ParamReserNeed = false;
        bool RcCheckBool = true;

        int sizebuff = 0;
        int sizebuffnew = 0;
        int ImuValueInt = 0;


        string G1;
        string G2;
        string G3;
        string A1;
        string A2;
        string A3;
        string M1;
        string M2;
        string M3;
        string BaroValue;
        string RollValue;
        string PitchValue;
        string PwmRCvalue;
        string PressureValue;

        private string dataIN1 = "";
        private string dataIN2 = "";
        private string dataIN3 = "";
        private string dataIN4 = "";
        private string dataIN5 = "";

        private string strTemp1 = "";
        private string strTemp2 = "";
        private string strTemp3 = "";
        private string strTemp4 = "";
        private string strTemp5 = "";

        string TempSmartAirText = "";
        private string FullTextSmartAir = "";
        string[] FullTextSmartAir5units = { "", "", "", "", "" };
        string[] MCUID1_5 = { "", "", "", "", "" };
        string[] BurnConfiguration = { "", "", "", "" };

        string CurrentClass = "";
        string DriveToSaveLogs = "C";

        int NumberOfCycleTest = 0;
        int TimeBetweenII = 280;
        int CurrentSetParams = 0;
        int CurrentCycleTest = 0;
        int CurrentCycleTestChangeOption = 0;

        string[] TempString = { "", "", "", "", "" };
        string[] filename = { "", "", "", "", "" };
        string[] SmartAirComPort = { "", "", "", "", "" };

        List<string> ParamsThatCauseReset = new List<string> { "ARM", "USD", "IMU" };
        List<string> ParamsThatCauseIgnor = new List<string> { "PSOF", "PSON", "PCV", "RCV" };
        string[] AllParams = { "ARH", "DRH", "AHS", "VIB", "NVI", "VIT", "HST", "DISE", "ARME", "VIF", "ARM", "PCV", "RCV", "LGR", "MST", "IMU", "SVPY", "SPR", "PTS", "PRV", "RCE", "EBT", "BNC", "MCV", "SPV", "XBT", "MTD", "PWM", "PSOF", "PSON", "PSTO", "RCST", "DST", "USD", "BCLS", "BCLL", "POO", "ADFD", "ADSD", "ADTD", "UFW", "PNO", "SNO", "ESR", "INV", "BET", "SMR", "INC", "TRG", "ATTR", "ATTP", "FFC", "FFL" };
        string[] M200ParamsValue = { "7", "0", "1", "0.05", "3",  "VIT", "HST", "DISE", "ARME", "100", "2",   "PCV", "RCV", "0",   "MST", "19",  "SVPY", "0.35", "15", "PRV", "0",   "0", "3", "3.5", "SPV", "1", "50", "1", "PSOF", "PSON", "PSTO", "RCST", "0", "1", "BCLS", "BCLL", "POO", "ADFD", "ADSD", "ADTD", "UFW", "0.2", "32", "100", "0", "0.3", "10", "3.5", "1", "55", "55", "300", "6.8" };
        string[] M600ParamsValue = { "7", "5", "1", "0.05", "5",  "VIT", "HST", "DISE", "ARME", "100", "2",   "PCV", "RCV", "0",   "MST", "19",  "SVPY", "0.35", "15", "PRV", "0",   "0", "3", "3.5", "SPV", "0", "50", "1", "PSOF", "PSON", "PSTO", "RCST", "0", "1", "BCLS", "BCLL", "POO", "ADFD", "ADSD", "ADTD", "UFW", "0.2", "32", "100", "0", "0.3", "10", "3.5", "1", "65", "65", "130", "7" };
        string[] Phantom3ParamsValue = { "5", "0", "AHS", "0.08", "5", "10", "0.3", "2", "3", "100",   "2",   "PCV", "RCV", "0",   "0",   "19",  "1",    "0.35", "15", "4",   "0",   "EBT", "1", "3.7", "3.5", "0", "1", "1", "1000", "1900", "1", "60", "10", "1", "20", "30", "200", "80", "5", "150", "80", "0.2", "32", "100", "0", "0.3", "10", "3.5", "1", "65", "65", "500", "7.3" };
        string[] CurrentConf = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        //int[] bits = { 0, 0, 0, 0, 0 };
        double[] CurrentConfValue = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //bool SetConfiguration = false;

        int[] IndexPorts = { -1, -1, -1, -1, -1 };

        bool CalButtonStatus = true;
        bool s1 = false;
        bool s2 = false;
        bool s3 = false;
        bool s4 = false;
        bool s5 = false;
        bool[] p = { false, false, false, false, false };
        bool[] EEFinished1_5 = { false, false, false, false, false };

        public bool BoolImu1 { get => BoolImu2; set => BoolImu2 = value; }
        public bool BoolImu2 { get => BoolImu; set => BoolImu = value; }

        public Form1()
        {
            InitializeComponent();
            getAvailablePorts();
            textBoxReader1st.ScrollBars = ScrollBars.Both;
            textBoxReader2nd.ScrollBars = ScrollBars.Both;
            textBoxReader3rd.ScrollBars = ScrollBars.Both;
            textBoxReader4th.ScrollBars = ScrollBars.Both;
            textBoxReader5th.ScrollBars = ScrollBars.Both;
            FlashTextBox.ScrollBars = ScrollBars.Both;
            ParamsList1.ScrollBars = ScrollBars.Both;
        }

        void getAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            string[] Ports = { };
            Ports = ports;
            PortName1st.Items.AddRange(ports);
            PortName2nd.Items.AddRange(ports);
            PortName3rd.Items.AddRange(ports);
            PortName4th.Items.AddRange(ports);
            PortName5th.Items.AddRange(ports);
            BurnPortsName.Items.AddRange(ports);
        }
        private void WriteToAllSmartAir(string TextToSend, int[] IndexPort)
        {
            Application.DoEvents();
            for (int i = 0; i < checkPortList.CheckedIndices.Count; i++)
            {

                switch (IndexPort[i])
                {
                    case -1:
                        break;
                    case 0:
                        serialPort1.WriteLine("\r");
                        serialPort1.WriteLine(TextToSend + "\r");
                        break;
                    case 1:
                        serialPort2.WriteLine("\r");
                        serialPort2.WriteLine(TextToSend + "\r");
                        break;
                    case 2:
                        serialPort3.WriteLine("\r");
                        serialPort3.WriteLine(TextToSend + "\r");
                        break;
                    case 3:
                        serialPort4.WriteLine("\r");
                        serialPort4.WriteLine(TextToSend + "\r");
                        break;
                    case 4:
                        serialPort5.WriteLine("\r");
                        serialPort5.WriteLine(TextToSend + "\r");
                        break;
                }
            }
            Thread.Sleep(SleepAfterWriteLineEvent);
        }
        private void WriteToSingleSmartAir(string TextToSend, int s)
        {
            Application.DoEvents();
            switch (s)
            {
                case 0:
                    serialPort1.WriteLine("\r");
                    serialPort1.WriteLine(TextToSend + "\r");
                    Thread.Sleep(SleepAfterWriteLineEvent);
                    break;
                case 1:
                    serialPort2.WriteLine("\r");
                    serialPort2.WriteLine(TextToSend + "\r");
                    Thread.Sleep(SleepAfterWriteLineEvent);
                    break;
                case 2:
                    serialPort3.WriteLine("\r");
                    serialPort3.WriteLine(TextToSend + "\r");
                    Thread.Sleep(SleepAfterWriteLineEvent);
                    break;
                case 3:
                    serialPort4.WriteLine("\r");
                    serialPort4.WriteLine(TextToSend + "\r");
                    Thread.Sleep(SleepAfterWriteLineEvent);
                    break;
                case 4:
                    serialPort5.WriteLine("\r");
                    serialPort5.WriteLine(TextToSend + "\r");
                    Thread.Sleep(SleepAfterWriteLineEvent);
                    break;
            }
            Application.DoEvents();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortName1st.Text == "" || BaudRate1st.Text == "")
                {
                    textBoxReader1st.Text = "Please select port settings";
                }
                else
                {
                    serialPort1.PortName = PortName1st.Text;
                    serialPort1.BaudRate = Convert.ToInt32(BaudRate1st.Text);
                    serialPort1.ReadBufferSize = serialPort1.BaudRate / 10;
                    serialPort1.Open();
                    progressBar1.Value = 100;
                    //serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    LedTest.Enabled = true;
                    EnableServoTest.Enabled = true;
                    BuzzerTest.Enabled = true;
                    sendButton1st.Enabled = true;
                    textBoxSender1st.Enabled = true;
                    EnableRC1.Enabled = true;
                    OpenPort1st.Enabled = false;
                    groupBox2.Enabled = true;
                    ClosePort1st.Enabled = true;
                    sendButton1st.Enabled = true;
                    InsertFinalParameters1.Enabled = true;
                    RST_1st.Enabled = true;
                    CAL_1st.Enabled = true;
                    button3.Enabled = true;
                    Arm1.Enabled = true;
                    Fire1.Enabled = true;
                    EnableIMU1.Enabled = true;
                    configuration1.Enabled = true;
                    textBox1.Text = PortName1st.Text;
                }
            }
            catch (UnauthorizedAccessException)
            {
                textBoxReader1st.Text = "Unauthorized Access";
            }
        }
        private void Open2ndPort(object sender, EventArgs e)
        {
            try
            {
                if (PortName2nd.Text == "" || BaudRate2nd.Text == "")
                {
                    textBoxReader2nd.Text = "Please select port settings";
                }
                else
                {
                    serialPort2.PortName = PortName2nd.Text;
                    serialPort2.BaudRate = Convert.ToInt32(BaudRate2nd.Text);
                    serialPort2.ReadBufferSize = serialPort2.BaudRate / 10;
                    serialPort2.Open();
                    progressBar2.Value = 100;
                    //serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    sendButton2nd.Enabled = true;
                    textBoxSender2nd.Enabled = true;
                    OpenPort2nd.Enabled = false;
                    ClosePort2nd.Enabled = true;
                    sendButton2nd.Enabled = true;
                    RST_2nd.Enabled = true;
                    CAL_2nd.Enabled = true;
                    IMUvalue2nd.Enabled = true;
                    configuration2.Enabled = true;
                    Arm2.Enabled = true;
                    button8.Enabled = true;
                    Fire2.Enabled = true;
                    textBox2.Text = PortName2nd.Text;
                }
            }
            catch (UnauthorizedAccessException)
            {
                textBoxReader2nd.Text = "Unauthorized Access";
            }
        }
        private void OpenPort3rd_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortName3rd.Text == "" || BaudRate3rd.Text == "")
                {
                    textBoxReader3rd.Text = "Please select port settings";
                }
                else
                {
                    serialPort3.PortName = PortName3rd.Text;
                    serialPort3.BaudRate = Convert.ToInt32(BaudRate3rd.Text);
                    serialPort3.ReadBufferSize = serialPort3.BaudRate / 10;
                    serialPort3.Open();
                    progressBar3.Value = 100;
                    //serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                    sendButton3rd.Enabled = true;
                    textBoxSender3rd.Enabled = true;
                    OpenPort3rd.Enabled = false;
                    ClosePort3rd.Enabled = true;
                    sendButton3rd.Enabled = true;
                    RST_3rd.Enabled = true;
                    CAL_3rd.Enabled = true;
                    IMUvalue3rd.Enabled = true;
                    configuration3.Enabled = true;
                    Arm3.Enabled = true;
                    Fire3.Enabled = true;
                    textBox3.Text = PortName3rd.Text;
                }
            }
            catch (UnauthorizedAccessException)
            {
                textBoxReader3rd.Text = "Unauthorized Access";
            }
        }
        private void OpenPort4th_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortName4th.Text == "" || BaudRate4th.Text == "")
                {
                    textBoxReader4th.Text = "Please select port settings";
                }
                else
                {
                    serialPort4.PortName = PortName4th.Text;
                    serialPort4.BaudRate = Convert.ToInt32(BaudRate4th.Text);
                    serialPort4.ReadBufferSize = serialPort4.BaudRate / 10;
                    serialPort4.Open();
                    progressBar4.Value = 100;
                    //serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    sendButton4th.Enabled = true;
                    textBoxSender4th.Enabled = true;
                    OpenPort4th.Enabled = false;
                    ClosePort4th.Enabled = true;
                    sendButton4th.Enabled = true;
                    RST_4th.Enabled = true;
                    CAL_4th.Enabled = true;
                    IMUvalue4th.Enabled = true;
                    configuration4.Enabled = true;
                    Arm4.Enabled = true;
                    Fire4.Enabled = true;
                    textBox4.Text = PortName4th.Text;
                }
            }
            catch (UnauthorizedAccessException)
            {
                textBoxReader4th.Text = "Unauthorized Access";
            }
        }
        private void OpenPort5th_Click(object sender, EventArgs e)
        {
            try
            {
                if (PortName5th.Text == "" || BaudRate5th.Text == "")
                {
                    textBoxReader5th.Text = "Please select port settings";
                }
                else
                {
                    serialPort5.PortName = PortName5th.Text;
                    serialPort5.BaudRate = Convert.ToInt32(BaudRate5th.Text);
                    serialPort5.ReadBufferSize = serialPort5.BaudRate / 10;
                    serialPort5.Open();
                    progressBar5.Value = 100;
                    //serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    sendButton5th.Enabled = true;
                    textBoxSender5th.Enabled = true;
                    OpenPort5th.Enabled = false;
                    ClosePort5th.Enabled = true;
                    sendButton5th.Enabled = true;
                    RST_5th.Enabled = true;
                    CAL_5th.Enabled = true;
                    IMUvalue5th.Enabled = true;
                    configuration5.Enabled = true;
                    Arm5.Enabled = true;
                    Fire5.Enabled = true;
                    textBox5.Text = PortName5th.Text;
                }
            }
            catch (UnauthorizedAccessException)
            {
                textBoxReader5th.Text = "Unauthorized Access";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            progressBar1.Value = 0;
            EnableServoTest.Checked = false;
            EnableServoTest.Enabled = false;
            BuzzerTest.Enabled = false;
            LedTest.Enabled = false;
            sendButton1st.Enabled = false;
            ClosePort1st.Enabled = false;
            OpenPort1st.Enabled = true;
            textBoxSender1st.Enabled = false;
            sendButton1st.Enabled = false;
            InsertFinalParameters1.Enabled = false;
            RST_1st.Enabled = false;
            groupBox2.Enabled = false;
            CAL_1st.Enabled = false;
            button3.Enabled = false;
            configuration1.Enabled = false;
            Arm1.Enabled = false;
            Fire1.Enabled = false;
            Arm1.Enabled = false;
            EnableRC1.Enabled = false;
            EnableIMU1.Enabled = false;
            Fire1.Enabled = false;
            ((TextBox)textBox1).Text = String.Empty;
            PortName1st.SelectedIndex = -1;
            BaudRate1st.SelectedIndex = -1;
        }
        private void Close2ndPort(object sender, EventArgs e)
        {
            serialPort2.Close();
            progressBar2.Value = 0;
            sendButton2nd.Enabled = false;
            ClosePort2nd.Enabled = false;
            OpenPort2nd.Enabled = true;
            textBoxSender2nd.Enabled = false;
            sendButton2nd.Enabled = false;
            RST_2nd.Enabled = false;
            CAL_2nd.Enabled = false;
            IMUvalue2nd.Enabled = false;
            Arm2.Enabled = false;
            Fire2.Enabled = false;
            configuration2.Enabled = false;
            ((TextBox)textBox2).Text = String.Empty;
            PortName2nd.SelectedIndex = -1;
            BaudRate2nd.SelectedIndex = -1;

        }
        private void ClosePort3rd_Click(object sender, EventArgs e)
        {
            serialPort3.Close();
            progressBar3.Value = 0;
            sendButton3rd.Enabled = false;
            ClosePort3rd.Enabled = false;
            OpenPort3rd.Enabled = true;
            textBoxSender3rd.Enabled = false;
            sendButton3rd.Enabled = false;
            RST_3rd.Enabled = false;
            CAL_3rd.Enabled = false;
            IMUvalue3rd.Enabled = false;
            Arm3.Enabled = false;
            Fire3.Enabled = false;
            configuration3.Enabled = false;
            ((TextBox)textBox3).Text = String.Empty;
            PortName3rd.SelectedIndex = -1;
            BaudRate3rd.SelectedIndex = -1;
        }
        private void ClosePort4th_Click(object sender, EventArgs e)
        {
            serialPort4.Close();
            progressBar4.Value = 0;
            sendButton4th.Enabled = false;
            ClosePort4th.Enabled = false;
            OpenPort4th.Enabled = true;
            textBoxSender4th.Enabled = false;
            sendButton4th.Enabled = false;
            RST_4th.Enabled = false;
            CAL_4th.Enabled = false;
            Arm4.Enabled = false;
            Fire4.Enabled = false;
            IMUvalue4th.Enabled = false;
            configuration4.Enabled = false;
            ((TextBox)textBox4).Text = String.Empty;
            PortName4th.SelectedIndex = -1;
            BaudRate4th.SelectedIndex = -1;
        }
        private void ClosePort5th_Click(object sender, EventArgs e)
        {
            serialPort5.Close();
            progressBar5.Value = 0;
            sendButton5th.Enabled = false;
            ClosePort5th.Enabled = false;
            OpenPort5th.Enabled = true;
            Arm5.Enabled = false;
            Fire5.Enabled = false;
            textBoxSender5th.Enabled = false;
            sendButton5th.Enabled = false;
            RST_5th.Enabled = false;
            CAL_5th.Enabled = false;
            IMUvalue5th.Enabled = false;
            configuration5.Enabled = false;
            ((TextBox)textBox5).Text = String.Empty;
            PortName5th.SelectedIndex = -1;
            BaudRate5th.SelectedIndex = -1;
        }
        private void sendSerialCMD(int a)
        {
            switch (a)
            {
                case 0:
                    WriteToSingleSmartAir(textBoxSender1st.Text, a);
                    if (textBoxSender1st is TextBox)
                    {
                        ((TextBox)textBoxSender1st).Text = String.Empty;
                    }
                    break;
                case 1:
                    WriteToSingleSmartAir(textBoxSender2nd.Text, a);
                    if (textBoxSender2nd is TextBox)
                    {
                        ((TextBox)textBoxSender2nd).Text = String.Empty;
                    }
                    break;
                case 2:
                    WriteToSingleSmartAir(textBoxSender3rd.Text, a);
                    if (textBoxSender3rd is TextBox)
                    {
                        ((TextBox)textBoxSender3rd).Text = String.Empty;
                    }
                    break;
                case 3:
                    WriteToSingleSmartAir(textBoxSender4th.Text, a);
                    if (textBoxSender4th is TextBox)
                    {
                        ((TextBox)textBoxSender4th).Text = String.Empty;
                    }
                    break;
                case 4:
                    WriteToSingleSmartAir(textBoxSender5th.Text, a);
                    if (textBoxSender5th is TextBox)
                    {
                        ((TextBox)textBoxSender5th).Text = String.Empty;
                    }
                    break;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            sendSerialCMD(0);
        }
        private void sendButton2nd_Click(object sender, EventArgs e)
        {
            sendSerialCMD(1);
        }
        private void sendButton3rd_Click_1(object sender, EventArgs e)
        {
            sendSerialCMD(2);
        }
        private void sendButton4th_Click(object sender, EventArgs e)
        {
            sendSerialCMD(3);
        }
        private void sendButton5th_Click(object sender, EventArgs e)
        {
            sendSerialCMD(4);
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {


        }
        public void M200_FinalConfiguration(int s)
        {
            Thread.Sleep(1000);
            Application.DoEvents();
            //ReturnToIdleSingleSMA(s);
            ParamsList1.Visible = true;
            InitBoardV(AllParams, M200ParamsValue, s);
            CloseParam1.Visible = true;
            ClearParam1.Visible = true;
            if (ParamReserNeed)
            {
                FullTextSmartAir5units[s] = "";
                WriteToSingleSmartAir("RST", s);
            }


            Thread.Sleep(1100);
            ParamReserNeed = false;
            WaitForSuccessfulInit(s);

        }

        private void InitBoardV(string[] AllParams, string[] ParamsValue, int s)
        {
            CurrentSetParams = 0;
            string CorrectParamString = "";
            string CurrentParamString = "";
            for (int j = 0; CurrentSetParams < AllParams.Length; CurrentSetParams++)
            {
                ParamBar1.Value = (100 * (CurrentSetParams + 1) / (AllParams.Length));
                bool WaitForInit = true;
                Thread.Sleep(200);
                Application.DoEvents();
                if (CurrentSetParams > AllParams.Length)
                    goto CancelAct;
                EEParam(s, CurrentSetParams);
                CorrectParamString = AllParams[CurrentSetParams] + ": " + ParamsValue[CurrentSetParams];
                CurrentParamString = AllParams[CurrentSetParams] + ": " + CurrentConfValue[CurrentSetParams];
                Application.DoEvents();

                if (CurrentParamString == CorrectParamString)
                {
                    ParamsList1.Text += (CorrectParamString + "\r\n");
                    Application.DoEvents();
                }
                else if (ParamsThatCauseIgnor.Contains(AllParams[CurrentSetParams]))
                {
                    ParamsList1.Text += (CurrentParamString + " (Unchanged)" + "\r\n");
                    Application.DoEvents();
                }
                else if (ParamsThatCauseReset.Contains(AllParams[CurrentSetParams]))
                {
                    ErasetSmartAirString("all");
                    WriteToSingleSmartAir(AllParams[CurrentSetParams] + " " + ParamsValue[CurrentSetParams], s);
                    ParamReserNeed = true;
                    Application.DoEvents();
                    Thread.Sleep(3250);
                    Stopwatch resetStopWatch = new Stopwatch();
                    resetStopWatch.Start();
                    while ((WaitForInit))
                    {
                        UseFullSmartAirString(s);
                        Application.DoEvents();
                        if (FullTextSmartAir.Contains(": Finished successfully"))
                        {
                            WaitForInit = false;
                            ParamsList1.Text += (CorrectParamString + "  (" + CurrentParamString + ")\r\n");
                            Thread.Sleep(1250);
                        }
                        TimeSpan ts = resetStopWatch.Elapsed;
                        if (((ts.TotalMilliseconds >= 35000) && (FullTextSmartAir.Contains(AllParams[CurrentSetParams] + ": " + ParamsValue[CurrentSetParams]))) && (WaitForInit))
                        {
                            ParamsList1.Text += (CorrectParamString + "  (" + CurrentParamString + ")\r\n");
                            WaitForInit = false;
                        }
                    }
                }
                else if (CorrectParamString.Contains("FFL"))
                {
                    string fflValue = "FFL: " + ((9.8 - CurrentConfValue[CurrentSetParams])).ToString();
                    if (CorrectParamString.Contains(fflValue))
                        ParamsList1.Text += (CurrentParamString + "\r\n");
                    else
                    {
                        WriteToSingleSmartAir(AllParams[CurrentSetParams] + " " + ParamsValue[CurrentSetParams], s);
                        ParamReserNeed = true;
                        ParamsList1.Text += (AllParams[CurrentSetParams] + ": " + ((9.8 - Convert.ToDouble(ParamsValue[CurrentSetParams])).ToString()) + "  (" + CurrentParamString + ")\r\n");
                        Application.DoEvents();
                        Thread.Sleep(250);
                    }
                }
                else
                {
                    WriteToSingleSmartAir(AllParams[CurrentSetParams] + " " + ParamsValue[CurrentSetParams], s);
                    ParamReserNeed = true;
                    ParamsList1.Text += (CorrectParamString + "  (" + CurrentParamString + ")\r\n");
                    Application.DoEvents();
                    Thread.Sleep(250);
                }
            CancelAct:
                Application.DoEvents();
            }
            CurrentSetParams = 0;
        }
        public void M600Beta_FinalConfiguration(int s)
        {
            int n = Convert.ToInt32(s);
            Thread.Sleep(1000);
            Application.DoEvents();
            //ReturnToIdleSingleSMA(s);
            ParamsList1.Visible = true;
            InitBoardV(AllParams, M600ParamsValue, s);
            CloseParam1.Visible = true;
            ClearParam1.Visible = true;
            if (ParamReserNeed)
            {
                FullTextSmartAir5units[n] = "";
                WriteToSingleSmartAir("RST", s);
            }

            Thread.Sleep(1100);
            ReturnToIdleSingleSMA(s);
        }
        private void UseFullSmartAirString(int s)
        {
            FullTextSmartAir = "";
            Thread.Sleep(500);
            Application.DoEvents();
            int n = Convert.ToInt32(s);
            FullTextSmartAir = FullTextSmartAir5units[n];
        }

        private void InitBoardV2(string[] Params, string[] ParamsValue, int s)
        {
            bool WaitForInit = false;
            if (!Params.Length.Equals(ParamsValue.Length))
            {
                return;
            }
            int Count = 0;
            int NumberOfParamsThatCauseReset = 0;
            foreach (string Param in Params)
            {
                ParamBar1.Value = (100 * (Count + 1) / (ParamsValue.Length));
                Application.DoEvents();
                LooKForParameterinEE(Param.ToUpperInvariant() + ": " + ParamsValue[Count], s);
                WriteToSingleSmartAir("", s);
                if (!IdentifiedText)
                {
                    if (ParamsThatCauseReset.Contains(Param))
                    {
                        WaitForInit = true;
                        ErasetSmartAirString("all");
                        Thread.Sleep(550);
                        NumberOfParamsThatCauseReset++;

                        WriteToSingleSmartAir(Param + " " + ParamsValue[Count], s);
                        Application.DoEvents();
                        Thread.Sleep(250);
                        Stopwatch resetStopWatch = new Stopwatch();
                        resetStopWatch.Start();
                        while (WaitForInit)
                        {
                            UseFullSmartAirString(s);
                            if (FullTextSmartAir.Contains(": Finished successfully")
                                || FullTextSmartAir.Contains("--Initialization--"))
                            {
                                WaitForInit = false;
                                Thread.Sleep(1250);
                            }
                            TimeSpan ts = resetStopWatch.Elapsed;
                            if ((ts.TotalMilliseconds >= 35000) && (WaitForInit))
                            {

                                WriteToSingleSmartAir("RST", s);
                                Application.DoEvents();
                                WaitForSuccessfulInit(s);
                                Thread.Sleep(1250);
                                WriteToSingleSmartAir(Param + " " + ParamsValue[Count], s);
                                Application.DoEvents();
                                resetStopWatch.Restart();
                            }
                            //}
                        }

                    }
                    else
                    {
                        //WriteToSingleSmartAir("\r\n", s);
                        WriteToSingleSmartAir(Param + " " + ParamsValue[Count], s);
                        Application.DoEvents();
                        Thread.Sleep(250);
                    }
                }
                Count++;
                IdentifiedText = false;
            }
        }
        private void LooKForParameterinEE(string str, int s)
        {

            bool EEFinished = false;
            TestTimeout = false;
            IdentifiedText = false;
            FullTextSmartAir = "";
            Thread.Sleep(250);
            WriteToSingleSmartAir("EE?\r", s);
            Thread.Sleep(450);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (!EEFinished && !TestTimeout && !IdentifiedText)
            {
                UseFullSmartAirString(s);
                TimeSpan ts = stopWatch.Elapsed;
                if (ts.TotalMilliseconds >= 2500)
                {
                    WriteToSingleSmartAir("EE?\r", s);
                    UseFullSmartAirString(s);
                }


                if (FullTextSmartAir.Contains(str + " ") || FullTextSmartAir.Contains(str + ".0 "))
                    IdentifiedText = true;
                if (FullTextSmartAir.Contains("!IMU Sensor.................: OK.") || FullTextSmartAir.Contains("!IMU Sensor.................: Version:"))
                    EEFinished = true;
            }
            EEFinished = false;
            TestTimeout = false;
        }
        private void ReturnToIdleSingleSMA(int s)
        {
            int ReturnLoop = 0;
        StartOverAgian:
            ErasetSmartAirString("all");
            WriteToSingleSmartAir("ee?", s);
            Thread.Sleep(1100);
            Application.DoEvents();
            UseFullSmartAirString(s);
            Thread.Sleep(500);
            if ((FullTextSmartAir.Contains("!Incorrect orientation......") || FullTextSmartAir.Contains("!System.....................: INITIALIZATION") || FullTextSmartAir.Contains("!System.....................: Initialization")) && (ReturnLoop < 2))
            {
                ReturnLoop++;
                if (FullTextSmartAir.Contains("RCE: 3") || FullTextSmartAir.Contains("RCE: 1") || FullTextSmartAir.Contains("RCE: 2"))
                { }
                else if (FullTextSmartAir.Contains("IMU: 0"))
                { }
                else if (ReturnLoop > 0)
                {
                    WriteToSingleSmartAir("rst", s);
                    WaitForSuccessfulInit(s);
                }
                else if (ReturnLoop == 0)
                {
                    //CalibrationSMA(s);
                    goto StartOverAgian;
                }
            }
            if (FullTextSmartAir.Contains("!System.....................: IDLE") || FullTextSmartAir.Contains("!Initialization.............: Finished successfully"))
            {
            }
            else if (FullTextSmartAir.Contains(": ARMED"))
            {
                WriteToSingleSmartAir("atg", s);
            }
            else if (FullTextSmartAir.Contains("Capacitor V................: FAIL") || (FullTextSmartAir.Contains("!System.....................: TRIGGERED")))
            {
                WriteToSingleSmartAir("RST", s);
                WaitForSuccessfulInit(s);
            }
            else if (FullTextSmartAir.Contains("!System.....................: MAINTENANCE"))
            {
                WriteToSingleSmartAir("trg 1", s);
            }
            else
            {
                //WriteToSingleSmartAir("RST", s);
                //WaitForSuccessfulInit(s);
            }

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //int n = Convert.ToInt32(s);
            SerialPort sp = (SerialPort)sender;
            string dataIN1 = sp.ReadExisting();
            if (!dataIN1.Equals(""))
            {
                strTemp1 = dataIN1;
                FullTextSmartAir5units[0] += dataIN1;
                this.Invoke(new EventHandler(ShowData1));
            }
        }
        private void ShowData1(object sender, EventArgs e)
        {
            textBoxReader1st.Text += strTemp1;
            textBoxReader1st.Select(textBoxReader1st.TextLength, 0);
            textBoxReader1st.ScrollToCaret();
            FlashTextBox.Text += strTemp1;
            FlashTextBox.Select(FlashTextBox.TextLength, 0);
            FlashTextBox.ScrollToCaret();
        }
        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string dataIN2 = sp.ReadExisting();
            if (!dataIN2.Equals(""))
            {
                strTemp2 = dataIN2;
                FullTextSmartAir5units[1] += dataIN2;
            }
            this.Invoke(new EventHandler(ShowData2));
        }

        private void ShowData2(object sender, EventArgs e)
        {
            textBoxReader2nd.Text += strTemp2;
            textBoxReader2nd.Select(textBoxReader2nd.TextLength, 0);
            textBoxReader2nd.ScrollToCaret();
        }
        private void serialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string dataIN3 = sp.ReadExisting();
            if (!dataIN3.Equals(""))
            {
                strTemp3 = dataIN3;
                FullTextSmartAir5units[2] += dataIN3;
            }
            this.Invoke(new EventHandler(ShowData3));
        }

        private void ShowData3(object sender, EventArgs e)
        {
            textBoxReader3rd.Text += strTemp3;
            textBoxReader3rd.Select(textBoxReader3rd.TextLength, 0);
            textBoxReader3rd.ScrollToCaret();
        }
        private void serialPort4_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string dataIN4 = sp.ReadExisting();
            if (!dataIN4.Equals(""))
            {
                strTemp4 = dataIN4;
                FullTextSmartAir5units[3] += dataIN4;
            }
            this.Invoke(new EventHandler(ShowData4));
        }

        private void ShowData4(object sender, EventArgs e)
        {
            textBoxReader4th.Text += strTemp4;
            textBoxReader4th.Select(textBoxReader4th.TextLength, 0);
            textBoxReader4th.ScrollToCaret();
        }
        private void serialPort5_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string dataIN5 = sp.ReadExisting();
            if (!dataIN5.Equals(""))
            {
                strTemp5 = dataIN5;
                FullTextSmartAir5units[4] += dataIN5;
            }
            this.Invoke(new EventHandler(ShowData5));
        }

        private void ShowData5(object sender, EventArgs e)
        {
            textBoxReader5th.Text += strTemp5;
            textBoxReader5th.Select(textBoxReader5th.TextLength, 0);
            textBoxReader5th.ScrollToCaret();
        }
        private void StartCalibrationSMA(int i)
        {
            CalButtonStatus = true;
            Application.DoEvents();
            switch (i)
            {
                case 0:
                    button3.Enabled = false;
                    Arm1.Enabled = false;
                    button6.Enabled = false;
                    Fire1.Enabled = false;
                    CAL_1st.Enabled = false;
                    RST_1st.Enabled = false;
                    InsertFinalParameters1.Enabled = false;
                    configuration1.Enabled = false;
                    WriteToSingleSmartAir("trg 2", i);
                    Thread.Sleep(500);
                    WriteToSingleSmartAir("atg", i);
                    break;
                case 1:
                    configuration2.Enabled = false;
                    Arm2.Enabled = false;
                    Fire2.Enabled = false;
                    IMUvalue2nd.Enabled = false;
                    CAL_2nd.Enabled = false;
                    RST_2nd.Enabled = false;
                    button8.Enabled = false;
                    WriteToSingleSmartAir("trg 2", i);
                    Thread.Sleep(500);
                    WriteToSingleSmartAir("atg", i);
                    break;
                case 2:
                    IMUvalue3rd.Enabled = false;
                    CAL_3rd.Enabled = false;
                    RST_3rd.Enabled = false;
                    WriteToSingleSmartAir("trg 2", i);
                    Thread.Sleep(500);
                    WriteToSingleSmartAir("atg", i);
                    break;
                case 3:
                    IMUvalue4th.Enabled = false;
                    CAL_4th.Enabled = false;
                    RST_4th.Enabled = false;
                    WriteToSingleSmartAir("trg 2", i);
                    Thread.Sleep(500);
                    WriteToSingleSmartAir("atg", i);
                    break;
                case 4:
                    IMUvalue5th.Enabled = false;
                    CAL_5th.Enabled = false;
                    RST_5th.Enabled = false;
                    WriteToSingleSmartAir("trg 2", i);
                    Thread.Sleep(500);
                    WriteToSingleSmartAir("atg", i);
                    Thread.Sleep(300);
                    WriteToSingleSmartAir("atg", i);
                    break;
            }
            StartCalb1.Text = "Stop";
            Thread.Sleep(250);
        }
        private void StopCalibrationSMA(int i)
        {
            CalButtonStatus = false;
            Application.DoEvents();
            switch (i)
            {
                case 0:
                    Thread.Sleep(50);
                    WriteToSingleSmartAir("atg", i);
                    Thread.Sleep(1200);
                    WriteToSingleSmartAir("rst", i);
                    configuration1.Enabled = true;
                    Arm1.Enabled = true;
                    Fire1.Enabled = true;
                    InsertFinalParameters1.Enabled = true;
                    button3.Enabled = true;
                    CAL_1st.Enabled = true;
                    RST_1st.Enabled = true;
                    button6.Enabled = true;
                    break;
                case 1:
                    Thread.Sleep(50);
                    WriteToSingleSmartAir("atg", i);
                    Thread.Sleep(1200);
                    WriteToSingleSmartAir("rst", i);
                    configuration2.Enabled = true;
                    Arm2.Enabled = true;
                    Fire2.Enabled = true;
                    button8.Enabled = true;
                    IMUvalue2nd.Enabled = true;
                    CAL_2nd.Enabled = true;
                    RST_2nd.Enabled = true;
                    break;
                case 2:
                    Thread.Sleep(50);
                    WriteToSingleSmartAir("atg", i);
                    Thread.Sleep(1200);
                    WriteToSingleSmartAir("rst", i);
                    IMUvalue3rd.Enabled = true;
                    CAL_3rd.Enabled = true;
                    RST_3rd.Enabled = true;
                    break;
                case 3:
                    Thread.Sleep(50);
                    WriteToSingleSmartAir("atg", i);
                    Thread.Sleep(1200);
                    WriteToSingleSmartAir("rst", i);
                    IMUvalue4th.Enabled = true;
                    CAL_4th.Enabled = true;
                    RST_4th.Enabled = true;
                    break;
                case 4:
                    Thread.Sleep(50);
                    WriteToSingleSmartAir("atg", i);
                    Thread.Sleep(1200);
                    WriteToSingleSmartAir("rst", i);
                    IMUvalue5th.Enabled = true;
                    CAL_5th.Enabled = true;
                    RST_5th.Enabled = true;
                    break;
            }
            WaitForSuccessfulInit(i);
            StartCalb1.Text = "Start";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void PortName1st_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PortName2nd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void PortName4th_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listPorts.Items.Clear();
            for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
            {
                IndexPorts[j] = checkPortList.CheckedIndices[j];
            }
            for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
            {
                switch (IndexPorts[j])
                {
                    case 0:
                        listPorts.Items.Add(PortName1st.Text);
                        SmartAirComPort[j] = PortName1st.Text;
                        break;
                    case 1:
                        listPorts.Items.Add(PortName2nd.Text);
                        SmartAirComPort[j] = PortName2nd.Text;
                        break;
                    case 2:
                        listPorts.Items.Add(PortName3rd.Text);
                        SmartAirComPort[j] = PortName3rd.Text;
                        break;
                    case 3:
                        listPorts.Items.Add(PortName4th.Text);
                        SmartAirComPort[j] = PortName4th.Text;
                        break;
                    case 4:
                        listPorts.Items.Add(PortName5th.Text);
                        SmartAirComPort[j] = PortName5th.Text;
                        break;
                }
            }

        }
        private void WaitForSuccessfulInit(int s)
        {
            Application.DoEvents();
            int counter = 0;
            bool WaitForInit = true;
        Startwaitforfullinit:
            Stopwatch resetStopWatch = new Stopwatch();
            resetStopWatch.Start();
            //ErasetSmartAirString(s.ToString());
            //Thread.Sleep(250);
            TimeSpan ts = resetStopWatch.Elapsed;
            while ((WaitForInit) && ts.TotalMilliseconds <= 35000)
            {
                Application.DoEvents();
                //UseFullSmartAirString(s);
                ts = resetStopWatch.Elapsed;
                if (FullTextSmartAir5units[s].Contains(": Finished successfully") || FullTextSmartAir5units[s].Contains("!System.....................: IDLE"))
                    WaitForInit = false;
                else if (FullTextSmartAir5units[s].Contains("!Incorrect orientation......"))
                {
                    WriteToSingleSmartAir("trg 2", s);
                    Thread.Sleep(500);
                    WriteToSingleSmartAir("atg", s);
                    Thread.Sleep(500);
                    WriteToSingleSmartAir("atg", s);
                    Thread.Sleep(1500);
                    WriteToSingleSmartAir("rst", s);
                    ErasetSmartAirString("all");
                }
                else if (FullTextSmartAir5units[s].Contains("!System.....................: MAINTENANCE"))
                {
                    WriteToSingleSmartAir("trg 1", s);
                    Thread.Sleep(500);
                }

            }
            if ((WaitForInit) && (counter < 1))
            {
                counter++;
                WriteToSingleSmartAir("RST", s);
                goto Startwaitforfullinit;
            }
            Thread.Sleep(1000);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            listPorts.Items.Clear();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (CAL_1st.Text == "Calibration")
            {
                ErasetSmartAirString("all");
                WriteToSingleSmartAir("ee?", 0);
                Thread.Sleep(500);
                UseFullSmartAirString(0);
                Thread.Sleep(1000);

                int VibIndex = FullTextSmartAir.IndexOf(".VIB:");
                string VibValueString = FullTextSmartAir.Substring(VibIndex + 6, 7);
                double VibValue = Convert.ToDouble(VibValueString);
                if (VibValue == 0)
                {
                    MessageBox.Show("You can not calibrate when VIB equal to 0", "Warning");
                    goto CantDoCal;
                }
                int ImuIndex = FullTextSmartAir.IndexOf(".IMU:");
                string ImuValueString = FullTextSmartAir.Substring(ImuIndex + 6, 6);
                double ImuValue = Convert.ToDouble(ImuValueString);
                if (ImuValue < 16)
                {
                    MessageBox.Show("You can not calibrate when IMU less then 16", "Warning");
                    goto CantDoCal;
                }
                StartCalb1.Visible = true;
                CAL_1st.Text = "Cancel";
            }
            else if (CAL_1st.Text == "Cancel")
            {
                StartCalb1.Visible = false;
                CAL_1st.Text = "Calibration";
            }
        CantDoCal: { }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("rst", 0);
            //WaitForSuccessfulInit(0);
            Application.DoEvents();
        }

        private void CAL_2nd_Click(object sender, EventArgs e)
        {
            //CalibrationSMA(1);
        }

        private void RST_2nd_Click(object sender, EventArgs e)
        {
            RST_2nd.Enabled = false;
            button8.Enabled = false;
            configuration2.Enabled = false;
            FullTextSmartAir5units[1] = "";
            IMUvalue2nd.Enabled = false;
            CAL_2nd.Enabled = false;
            configuration2.Enabled = false;
            Arm2.Enabled = false;
            Fire2.Enabled = false;
            sendButton2nd.Enabled = false;
            Application.DoEvents();
            WriteToSingleSmartAir("rst", 1);
            WaitForSuccessfulInit(1);
            Application.DoEvents();
            IMUvalue2nd.Enabled = true;
            CAL_2nd.Enabled = true;
            sendButton2nd.Enabled = true;
            configuration2.Enabled = true;
            Arm2.Enabled = true;
            button8.Enabled = true;
            RST_2nd.Enabled = true;
            Fire2.Enabled = true;
        }

        private void CAL_3rd_Click(object sender, EventArgs e)
        {
            //CalibrationSMA(2);
        }

        private void RST_3rd_Click(object sender, EventArgs e)
        {
            FullTextSmartAir5units[2] = "";
            IMUvalue3rd.Enabled = false;
            configuration3.Enabled = false;
            CAL_3rd.Enabled = false;
            Arm3.Enabled = false;
            Fire3.Enabled = false;
            sendButton3rd.Enabled = false;
            Application.DoEvents();
            WriteToSingleSmartAir("rst", 2);
            WaitForSuccessfulInit(2);
            Application.DoEvents();
            IMUvalue3rd.Enabled = true;
            CAL_3rd.Enabled = true;
            sendButton3rd.Enabled = true;
            configuration3.Enabled = true;
            Arm3.Enabled = true;
            Fire3.Enabled = true;
        }

        private void CAL_4th_Click(object sender, EventArgs e)
        {
            //CalibrationSMA(3);
        }

        private void RST_4th_Click(object sender, EventArgs e)
        {
            FullTextSmartAir5units[3] = "";
            IMUvalue4th.Enabled = false;
            configuration4.Enabled = false;
            Arm4.Enabled = false;
            Fire4.Enabled = false;
            CAL_4th.Enabled = false;
            sendButton4th.Enabled = false;
            Application.DoEvents();
            WriteToSingleSmartAir("rst", 3);
            WaitForSuccessfulInit(3);
            Application.DoEvents();
            IMUvalue4th.Enabled = true;
            CAL_4th.Enabled = true;
            sendButton4th.Enabled = true;
            configuration4.Enabled = true;
            Arm4.Enabled = true;
            Fire4.Enabled = true;
        }

        private void CAL_5th_Click(object sender, EventArgs e)
        {
            //CalibrationSMA(4);
        }

        private void RST_5th_Click(object sender, EventArgs e)
        {
            FullTextSmartAir5units[4] = "";
            Arm5.Enabled = false;
            Fire5.Enabled = false;
            configuration5.Enabled = false;
            IMUvalue5th.Enabled = false;
            CAL_5th.Enabled = false;
            sendButton5th.Enabled = false;
            Application.DoEvents();
            WriteToSingleSmartAir("rst", 4);
            WaitForSuccessfulInit(4);
            Application.DoEvents();
            IMUvalue5th.Enabled = true;
            CAL_5th.Enabled = true;
            sendButton5th.Enabled = true;
            configuration5.Enabled = true;
            Arm5.Enabled = true;
            Fire5.Enabled = true;
        }

        private void button3_Click_2(object sender, EventArgs e)//1st imu
        {

            IMUlist.Visible = true;
            CLR.Visible = true;
            CloseWin.Visible = true;
            imuValues(0);
            IMUlist.Items.Add("Magnometer     : " + M1 + "," + M2 + ", " + M3);
            IMUlist.Items.Add("Accelerometer  : " + A1 + "," + A2 + ", " + A3);
            IMUlist.Items.Add("Gyro                 : " + G1 + "," + G2 + ", " + G3);
            IMUlist.Items.Add("Barometer         : " + BaroValue);
            IMUlist.Items.Add("Pressure           : " + PressureValue);
            IMUlist.Items.Add("Roll Value         : " + RollValue);
            IMUlist.Items.Add("Pitch Value       : " + PitchValue);
            if (!M200)
                IMUlist.Items.Add("PWM RC Value: " + PwmRCvalue);
            Application.DoEvents();
            PwmRCvalue = "NaN";
            PitchValue = "NaN";
            RollValue = "NaN";
            BaroValue = "NaN";
            PressureValue = "NaN";
            M1 = "NaN"; M2 = "NaN"; M3 = "NaN";
            G1 = "NaN"; G2 = "NaN"; G3 = "NaN";
            A1 = "NaN"; A2 = "NaN"; A3 = "NaN";
        }

        private void imuValues(int v)
        {
            string LocalText = "";
            FullTextSmartAir5units[v] = "";
            WriteToSingleSmartAir("i?", v);
            Application.DoEvents();
            Thread.Sleep(150);
            WriteToSingleSmartAir("i?", v);
            Application.DoEvents();
            Thread.Sleep(150);
            LocalText = FullTextSmartAir5units[v];
            Thread.Sleep(200);
            Application.DoEvents();
            int MagIndex = LocalText.IndexOf("/M");
            int GyroIndex = LocalText.IndexOf("!G");
            int AccIndex = LocalText.IndexOf("/A");
            int BaroIndex = LocalText.IndexOf("/B");
            int PitchIndex = LocalText.IndexOf("/Pi");
            int RollIndex = LocalText.IndexOf("/Ro");
            int YawIndex = LocalText.IndexOf("/Ya");
            int pwmRCfireIndex = LocalText.IndexOf("RC_FIRE_pw:");
            int pwmRCarmIndex = LocalText.IndexOf("RC_ARM_pw");
            if (MagIndex == -1)
                goto EndII;
            BaroValue = LocalText.Substring(BaroIndex + 2, 6);
            PressureValue = LocalText.Substring(BaroIndex + 9, 7);
            RollValue = LocalText.Substring(RollIndex + 3, YawIndex - (RollIndex + 3));
            PitchValue = LocalText.Substring(PitchIndex + 3, RollIndex - (PitchIndex + 3));
            if ((pwmRCfireIndex == -1) || (pwmRCarmIndex == -1))
            {
                M200 = true;
                goto M200_param;
            }
            PwmRCvalue = LocalText.Substring(pwmRCfireIndex + 12, pwmRCarmIndex - (pwmRCfireIndex + 12));
        M200_param:
            M1 = LocalText.Substring(MagIndex + 2, 6);
            M2 = LocalText.Substring(MagIndex + 9, 6);
            M3 = LocalText.Substring(MagIndex + 16, 6);
            G1 = LocalText.Substring(GyroIndex + 2, 6);
            G2 = LocalText.Substring(GyroIndex + 9, 6);
            G3 = LocalText.Substring(GyroIndex + 16, 6);
            A1 = LocalText.Substring(AccIndex + 2, 6);
            A2 = LocalText.Substring(AccIndex + 9, 6);
            A3 = LocalText.Substring(AccIndex + 16, 6);
        EndII:
            { }
        }



        private void IMUlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CLR_Click(object sender, EventArgs e)
        {
            IMUlist.Items.Clear();
        }

        private void CloseWin_Click(object sender, EventArgs e)
        {
            IMUlist.Items.Clear();
            IMUlist.Visible = false;
            CLR.Visible = false;
            CloseWin.Visible = false;
        }

        private void CloseWin2nd(object sender, EventArgs e)
        {
            listIMU2nd.Items.Clear();
            listIMU2nd.Visible = false;
            CLR2nd.Visible = false;
            button4.Visible = false;
        }

        private void listIMU2nd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IMUvalue2nd_Click(object sender, EventArgs e)
        {
            listIMU2nd.Visible = true;
            CLR2nd.Visible = true;
            button4.Visible = true;
            imuValues(2);
            listIMU2nd.Items.Add("Magnometer  : " + M1 + "," + M2 + ", " + M3);
            listIMU2nd.Items.Add("Barometer   : " + BaroValue);
            listIMU2nd.Items.Add("Roll Value  : " + RollValue);
            listIMU2nd.Items.Add("Pitch Value : " + PitchValue);
            listIMU2nd.Items.Add("PWM RC Value: " + PwmRCvalue);
        }

        private void CLR2nd_Click(object sender, EventArgs e)
        {
            listIMU2nd.Items.Clear();
        }

        private void CLR3rd_Click(object sender, EventArgs e)
        {
            listIMU3rd.Items.Clear();
        }

        private void CloseWin3rd_Click(object sender, EventArgs e)
        {
            listIMU3rd.Items.Clear();
            listIMU3rd.Visible = false;
            CLR3rd.Visible = false;
            CloseWin3rd.Visible = false;
        }

        private void IMUvalue3rd_Click(object sender, EventArgs e)
        {
            listIMU3rd.Visible = true;
            CLR3rd.Visible = true;
            CloseWin3rd.Visible = true;
            imuValues(3);
            listIMU3rd.Items.Add("Magnometer  : " + M1 + "," + M2 + ", " + M3);
            listIMU3rd.Items.Add("Barometer   : " + BaroValue);
            listIMU3rd.Items.Add("Roll Value  : " + RollValue);
            listIMU3rd.Items.Add("Pitch Value : " + PitchValue);
            listIMU3rd.Items.Add("PWM RC Value: " + PwmRCvalue);
        }

        private void IMUvalue4th_Click(object sender, EventArgs e)
        {
            listIMU4th.Visible = true;
            CLR4th.Visible = true;
            CloseWin4th.Visible = true;
            imuValues(4);
            listIMU4th.Items.Add("Magnometer  : " + M1 + "," + M2 + ", " + M3);
            listIMU4th.Items.Add("Barometer   : " + BaroValue);
            listIMU4th.Items.Add("Roll Value  : " + RollValue);
            listIMU4th.Items.Add("Pitch Value : " + PitchValue);
            listIMU4th.Items.Add("PWM RC Value: " + PwmRCvalue);
        }

        private void CLR4th_Click(object sender, EventArgs e)
        {
            listIMU4th.Items.Clear();
        }

        private void CloseWin4th_Click(object sender, EventArgs e)
        {

            listIMU4th.Items.Clear();
            listIMU4th.Visible = false;
            CLR4th.Visible = false;
            CloseWin4th.Visible = false;
        }

        private void CloseWin5th_Click(object sender, EventArgs e)
        {
            listIMU5th.Items.Clear();
            listIMU5th.Visible = false;
            CLR5th.Visible = false;
            CloseWin5th.Visible = false;
        }

        private void CLR5th_Click(object sender, EventArgs e)
        {
            listIMU5th.Items.Clear();
        }

        private void IMUvalue5th_Click(object sender, EventArgs e)
        {
            listIMU5th.Visible = true;
            CLR5th.Visible = true;
            CloseWin5th.Visible = true;
            imuValues(5);
            listIMU5th.Items.Add("Magnometer  : " + M1 + "," + M2 + ", " + M3);
            listIMU5th.Items.Add("Barometer   : " + BaroValue);
            listIMU5th.Items.Add("Roll Value  : " + RollValue);
            listIMU5th.Items.Add("Pitch Value : " + PitchValue);
            listIMU5th.Items.Add("PWM RC Value: " + PwmRCvalue);
        }
        private void collectString()
        {
            for (int n = 0; n < checkPortList.CheckedIndices.Count; n++)
            {
                switch (IndexPorts[n])
                {
                    case 0:
                        TempString[IndexPorts[n]] += FullTextSmartAir5units[0] + "\r\n";
                        break;
                    case 1:
                        TempString[IndexPorts[n]] += FullTextSmartAir5units[1] + "\r\n";
                        break;
                    case 2:
                        TempString[IndexPorts[n]] += FullTextSmartAir5units[2] + "\r\n";
                        break;
                    case 3:
                        TempString[IndexPorts[n]] += FullTextSmartAir5units[3] + "\r\n";
                        break;
                    case 4:
                        TempString[IndexPorts[n]] += FullTextSmartAir5units[4] + "\r\n";
                        break;
                }
            }
        }
        private void ErasetString()
        {
            for (int n = 0; n < 5; n++)
            {
                TempString[n] = "";
            }
        }
        private void ErasetSmartAirString(string s)
        {
            FullTextSmartAir = "";
            if (s == "all")
            {
                for (int n = 0; n < 5; n++)
                {
                    FullTextSmartAir5units[n] = "";
                }
            }
            else
            {
                int n = Convert.ToInt32(s);
                FullTextSmartAir5units[n] = "";
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            groupBox12.Text = CurrentClass;
            ClearAutoDataTest.Enabled = false;
            bool[] ArmSuccess = { false, false, false, false, false };
            bool[] DisarmSuccess = { false, false, false, false, false };
            button1.Enabled = false;
            button2.Enabled = false;
            InsertNumCyc.Enabled = false;
            StartArmDisTest.Enabled = false;
            ClearCyc.Enabled = false;
            progressBar6.Maximum = 10;
            groupBox12.Visible = true;
            progressBar6.BackColor = Color.Black;
            progressBar6.Value = 2;
            Application.DoEvents();
            float[] countsuccess = { 0, 0, 0, 0, 0 };
            float[] countfailed = { 0, 0, 0, 0, 0 };
            Application.DoEvents();
            string oldName = "", newName = "", currentPath = "";
            int TestHistory = 0;
            CurrentClass = "Arm and Disarm Automated Test";
            bool[] isEmpty = { false, false, false, false, false };
            string[] path = { "", "", "", "", "" };
            int[] TestNumberIndex = { 0, 0, 0, 0, 0 };
            ReturnMCUID1_5();
            for (int k = 0; k < checkPortList.CheckedIndices.Count; k++)
            {
                path[IndexPorts[k]] = DriveToSaveLogs + ":\\GUI Tests\\" + MCUID1_5[IndexPorts[k]] + " -" +
                    SmartAirComPort[k] + "\\" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() +
                    "-" + DateTime.UtcNow.Day.ToString() + "\\";// + CurrentClass + ".txt";
                TestHistory = Directory.GetFiles(path[IndexPorts[k]]).Length;
                oldName = path[IndexPorts[k]] + "\\" + CurrentClass + " #" + (TestHistory - 1) + ".txt";
                newName = path[IndexPorts[k]] + "\\" + CurrentClass + " #" + (TestHistory) + ".txt";
                currentPath = path[IndexPorts[k]] + "\\" + CurrentClass + ".txt";
                if (TestHistory == 0)
                {
                }
                else if (Directory.GetFiles(path[IndexPorts[k]]).Length == 1)
                {
                    File.Move(path[IndexPorts[k]] + "\\" + CurrentClass + ".txt", path[IndexPorts[k]] + "\\" + CurrentClass + " #1.txt");
                }
                else
                {
                    File.Move(currentPath, newName);
                }
            }
            LogTestToFile(CurrentClass, ": " + NumberOfCycleTest + " cycles of the " + CurrentClass + " test");

            ReturnToIdle();
            LogTestToFile(CurrentClass, ": the SmartAir in idle state");
            progressBar6.Value = 6;
            string[] LocalParams = { "arm", "rce", "usd", "trg", "imu" };
            string[] LocalParamsValue = { "0", "0", "1", "1", "19" };
            InitBoardNano(LocalParams, LocalParamsValue);
            Thread.Sleep(600);
            progressBar6.Value = 10; Application.DoEvents();
            Thread.Sleep(1600);
            ClearBuffer();
            WriteToAllSmartAir("ee?", IndexPorts);
            Thread.Sleep(500); Application.DoEvents();
            LogTestToFile(CurrentClass, ": Configuartion:");

            for (int k = 0; k < checkPortList.CheckedIndices.Count; k++)
            {
                Application.DoEvents();
                LogSingleTestToFile(CurrentClass, FullTextSmartAir5units[IndexPorts[k]] + "\r", k);
                switch (k)
                {
                    case 0:
                        groupBox13.Visible = true;
                        Auto1stPort.Text = SmartAirComPort[k];
                        break;
                    case 1:
                        groupBoxAuto2nd.Visible = true;
                        Auto2ndPort.Text = SmartAirComPort[k];
                        break;
                    case 2:
                        groupBoxAuto3rd.Visible = true;
                        Auto3rdPort.Text = SmartAirComPort[k];
                        break;
                    case 3:
                        groupBoxAuto4th.Visible = true;
                        Auto4thPort.Text = SmartAirComPort[k];
                        break;
                    case 4:
                        groupBoxAuto5th.Visible = true;
                        Auto5thPort.Text = SmartAirComPort[k];
                        break;
                }
            }
            progressBar6.Value = 0;
            LogTestToFile(CurrentClass, ": ");
            LogTestToFile(CurrentClass, ": " + CurrentClass + " test - " + NumberOfCycleTest + " Cycle started");
            progressBar6.Maximum = NumberOfCycleTest + 1;
            Application.DoEvents();

            ClearAutoDataTest.Visible = true;
            ClearAutoDataTest.Enabled = true;
            CurrentCycleTest = 0;
            CurrentCycleTestChangeOption = 0;
            for (CurrentCycleTestChangeOption = 0; CurrentCycleTestChangeOption < NumberOfCycleTest; CurrentCycleTestChangeOption++)
            {

                Thread.Sleep(500);
                LogTestToFile(CurrentClass, ": test number " + (CurrentCycleTest + 1));
                label1stTNumb.Text = "test number " + (CurrentCycleTest + 1).ToString();
                label2ndTNumb.Text = "test number " + (CurrentCycleTest + 1).ToString();
                label3rdTNumb.Text = "test number " + (CurrentCycleTest + 1).ToString();
                label4thTNumb.Text = "test number " + (CurrentCycleTest + 1).ToString();
                label5thTNumb.Text = "test number " + (CurrentCycleTest + 1).ToString();
                progressBar6.Value = CurrentCycleTest + 1; Application.DoEvents();
                AutoStatusprec("1");
                Application.DoEvents();
                ClearBuffer();
                int counttest = 0;
                AutoStatusprec("15");
                LogTestToFile(CurrentClass, ": Arming");
                Thread.Sleep(150);
                WriteToAllSmartAir("atg", IndexPorts);
                AutoStatusprec("30");
                Thread.Sleep(150); Application.DoEvents();
                for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
                {
                tryagainArm:
                    Thread.Sleep(100);
                    Application.DoEvents();
                    if (FullTextSmartAir5units[IndexPorts[j]].Contains("!RC ARM/DISARM was Triggered.")
                        || FullTextSmartAir5units[IndexPorts[j]].Contains("!System.....................: ARMED"))
                    {
                        LogTestToFile(CurrentClass, ": System Armed");
                        ArmSuccess[j] = true;
                    }

                    else if (counttest <= 2)
                    {
                        LogTestToFile(CurrentClass, ": #" + (counttest + 1) + " System Armed failed");
                        counttest++;
                        goto tryagainArm;
                    }
                }
                AutoStatusprec("40");
                Application.DoEvents();
                ClearBuffer();
                counttest = 0;
                LogTestToFile(CurrentClass, ": Disrming");
                Thread.Sleep(150);
                WriteToAllSmartAir("atg", IndexPorts);
                AutoStatusprec("55");
                Thread.Sleep(150); Application.DoEvents();
                for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
                {
                tryagainDisarm:
                    Thread.Sleep(400);
                    Application.DoEvents();
                    if (FullTextSmartAir5units[IndexPorts[j]].Contains("!RC ARM / DISARM was Triggered.")
                        || FullTextSmartAir5units[IndexPorts[j]].Contains("!Discharge was operate."))
                    {
                        LogTestToFile(CurrentClass, ": System Disarmed");
                        DisarmSuccess[j] = true;
                    }
                    else if (counttest <= 2)
                    {
                        LogTestToFile(CurrentClass, ": #" + (counttest + 1) + " System Disarmed failed");
                        counttest++;
                        goto tryagainDisarm;
                    }
                }
                AutoStatusprec("70"); Application.DoEvents();
                Thread.Sleep(750);
                for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
                {
                    Thread.Sleep(250);
                    Application.DoEvents();
                    if ((ArmSuccess[j] && DisarmSuccess[j]))
                    {
                        LogTestToFile(CurrentClass, "test number " + (CurrentCycleTest + 1).ToString() + " Pass");
                        countsuccess[j]++;
                    }
                    else
                    {
                        LogTestToFile(CurrentClass, "test number " + (CurrentCycleTest + 1).ToString() + " Fail");
                        countfailed[j]++;
                    }
                    ArmSuccess[j] = false;
                    DisarmSuccess[j] = false;
                    AutoStatusprec("85");
                }
                Thread.Sleep(800);
                ClearBuffer();
                AutoStatusprec("100");
                AutoStatusSuccess(countsuccess, (CurrentCycleTest + 1));
                Thread.Sleep(1000);
                Application.DoEvents();
                TestTimeout = false;
                EndCondition = false;
                CurrentCycleTest++;
            }
            Thread.Sleep(1000);
            progressBar6.Value = NumberOfCycleTest + 1;
            Application.DoEvents();

            string TestBodyMail = "the " + CurrentClass + " test, run " + NumberOfCycleTest + " times.\r\n";
            for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
            {

                Application.DoEvents();
                TestBodyMail += "\r\n\nMCU ID :" + MCUID1_5[IndexPorts[j]] + "\r\n" + countsuccess[j] +
                    "/" + NumberOfCycleTest + " passed the test\r\n" + ((countsuccess[j] / NumberOfCycleTest) * 100) + "% success";
            }
            //SendMail("zoharb@parazero.com", CurrentClass, TestBodyMail);
            SendMailWithAttch("zoharb@parazero.com", CurrentClass + " " + DateTime.UtcNow.ToString(), TestBodyMail, path);
            ClearAutoDataTest.Text = "Clear";

        }

        private void SendMail(string SendTo, string MailSubject, string MailBody)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("parazeroauto@gmail.com");
            mail.To.Add(SendTo);
            mail.Subject = MailSubject;
            mail.Body = MailBody;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("parazeroauto", "fdfdfd3030");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
        private void SendMailWithAttch(string SendTo, string MailSubject, string MailBody, string[] dirs)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("parazeroauto@gmail.com");
            mail.To.Add(SendTo);
            mail.Subject = MailSubject;
            mail.Body = MailBody;

            for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
            {
                var attachment = new Attachment(dirs[IndexPorts[j]] + CurrentClass + ".txt");
                mail.Attachments.Add(attachment);
            }


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("parazeroauto", "fdfdfd3030");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        private void LogSingleTestToFile(string TestName, string AdditionalTextToLog, int j)
        {
            ReturnMCUID1_5();
            if (!MCUID1_5[IndexPorts[j]].Equals(""))
            {
                Directory.CreateDirectory(DriveToSaveLogs + ":\\GUI Tests\\" + MCUID1_5[IndexPorts[j]] + " -" + SmartAirComPort[j] + "\\" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-" + DateTime.UtcNow.Day.ToString() + "\\");
                string path = DriveToSaveLogs + ":\\GUI Tests\\" + MCUID1_5[IndexPorts[j]] + " -" + SmartAirComPort[j] + "\\" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-" + DateTime.UtcNow.Day.ToString() + "\\" + CurrentClass + ".txt";//Path.GetTempFileName();
                using (FileStream fs = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    if (!AdditionalTextToLog.Length.Equals(0))
                    {
                        Byte[] info2 = new UTF8Encoding(true).GetBytes(" " + DateTime.UtcNow.ToString() + " " + AdditionalTextToLog + "\r");
                        fs.Write(info2, 0, info2.Length);
                    }
                    fs.Close();
                }
                Application.DoEvents();
            }
        }
        private void LogTestToFile(string currentClass, string AdditionalTextToLog)
        {
            ReturnMCUID1_5();
            for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
            {
                if (!MCUID1_5[IndexPorts[j]].Equals(""))
                {
                    Directory.CreateDirectory(DriveToSaveLogs + ":\\GUI Tests\\" + MCUID1_5[IndexPorts[j]] + " -" + SmartAirComPort[j] + "\\" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-" + DateTime.UtcNow.Day.ToString() + "\\");
                    string path = DriveToSaveLogs + ":\\GUI Tests\\" + MCUID1_5[IndexPorts[j]] + " -" + SmartAirComPort[j] + "\\" + DateTime.UtcNow.Year.ToString() + "-" + DateTime.UtcNow.Month.ToString() + "-" + DateTime.UtcNow.Day.ToString() + "\\" + CurrentClass + ".txt";//Path.GetTempFileName();
                    using (FileStream fs = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        if (!AdditionalTextToLog.Length.Equals(0))
                        {
                            Byte[] info2 = new UTF8Encoding(true).GetBytes(" " + DateTime.UtcNow.ToString() + " " + AdditionalTextToLog + "\r");
                            fs.Write(info2, 0, info2.Length);
                        }
                        fs.Close();
                    }
                }
            }
        }

        private void AutoStatusprec(string precante)
        {
            int count = checkPortList.CheckedIndices.Count;
            progressBarAuto1st.Value = Convert.ToInt32(precante);
            label20.Text = precante + "%";
            progressBarAuto2nd.Value = Convert.ToInt32(precante);
            labelPrc2nd.Text = precante + "%";
            progressBarAuto3rd.Value = Convert.ToInt32(precante);
            labelPrc3rd.Text = precante + "%";
            progressBarAuto4th.Value = Convert.ToInt32(precante);
            labelPrc4th.Text = precante + "%";
            progressBarAuto5th.Value = Convert.ToInt32(precante);
            labelPrc5th.Text = precante + "%";
            Thread.Sleep(850);
            Application.DoEvents();
            Thread.Sleep(850);
        }
        private void AutoStatusSuccess(float[] CS, int c)//count off success , current number test , array of p\open ports
        {
            int j = 0;
            Application.DoEvents();
            float x = 0;
            int count = checkPortList.CheckedIndices.Count;
            for (int k = 0; k < 5; k++)
            {
                Application.DoEvents();
                if (p[k] == false)
                    continue;
                else
                {
                    switch (j)
                    {
                        case 0:
                            x = ((CS[j]) / c) * 100;
                            label1stpass.Text = (CS[j]).ToString() + " tests pass";
                            label1stpass1.Text = String.Format("{0:0.00}", x.ToString()) + "% success";
                            break;
                        case 1:
                            x = ((CS[j]) / c) * 100;
                            label2ndpass.Text = (CS[j]).ToString() + " tests pass";
                            label2ndpass2.Text = String.Format("{0:0.00}", x.ToString()) + "% success";
                            break;
                        case 2:
                            x = ((CS[j]) / c) * 100;
                            label3rdpass.Text = (CS[j]).ToString() + " tests pass";
                            label3rdpass3.Text = String.Format("{0:0.00}", x.ToString()) + "% success";
                            break;
                        case 3:
                            x = ((CS[j]) / c) * 100;
                            label4thpass.Text = (CS[j]).ToString() + " tests pass";
                            label4thpass4.Text = String.Format("{0:0.00}", x.ToString()) + "% success";
                            break;
                        case 4:
                            x = ((CS[j]) / c) * 100;
                            label5thpass.Text = (CS[j]).ToString() + " tests pass";
                            label5thpass5.Text = String.Format("{0:0.00}", x.ToString()) + "% success";
                            break;
                    }
                    j++;
                }

                Application.DoEvents();
            }
            Thread.Sleep(750);

        }

        private void InitBoardNano(string[] Params, string[] ParamsValue)
        {
            int count = 0;
            LogTestToFile(CurrentClass, ": pre-configuration for the test");
            foreach (string Param in Params)
            {
                WriteToAllSmartAir("", IndexPorts);
                WriteToAllSmartAir(Param + " " + ParamsValue[count], IndexPorts);
                LogTestToFile(CurrentClass, ": " + Param + ": " + ParamsValue[count]);
                count++;
            }
            ResetAllSMA();
        }

        private void ResetAllSMA()
        {
            LogTestToFile(CurrentClass, ": Reset");
            Application.DoEvents();
            bool WaitForInit = true;
        startRSTagain:
            ErasetSmartAirString("all");
            WriteToAllSmartAir("", IndexPorts);
            Thread.Sleep(500);
            WriteToAllSmartAir("rst", IndexPorts);
            Application.DoEvents();
            Thread.Sleep(1000);
            Application.DoEvents();
            for (int s = 0; s < checkPortList.CheckedIndices.Count; s++)
            {
                switch (IndexPorts[s])
                {
                    case 0:
                        p[IndexPorts[s]] = true;
                        s1 = true;
                        break;
                    case 1:
                        p[IndexPorts[s]] = true;
                        s2 = true;
                        break;
                    case 2:
                        p[IndexPorts[s]] = true;
                        s3 = true;
                        break;
                    case 3:
                        p[IndexPorts[s]] = true;
                        s4 = true;
                        break;
                    case 4:
                        s5 = true;
                        p[IndexPorts[s]] = true;
                        break;
                }
            }
            int waitforinit = 0;
            Stopwatch resetStopWatch = new Stopwatch();
            resetStopWatch.Start();
            while (WaitForInit)
            {
                Application.DoEvents();
                if (FullTextSmartAir5units[0].Contains(": Finished successfully") && (s1))
                    waitforinit++;
                if (FullTextSmartAir5units[1].Contains(": Finished successfully") && (s2))
                    waitforinit++;
                if (FullTextSmartAir5units[2].Contains(": Finished successfully") && (s3))
                    waitforinit++;
                if (FullTextSmartAir5units[3].Contains(": Finished successfully") && (s4))
                    waitforinit++;
                if (FullTextSmartAir5units[4].Contains(": Finished successfully") && (s5))
                    waitforinit++;
                if (waitforinit >= checkPortList.CheckedIndices.Count)
                {
                    WaitForInit = false;
                    LogTestToFile(CurrentClass, ": Initialization successful");
                }
                TimeSpan ts = resetStopWatch.Elapsed;
                if ((ts.TotalMilliseconds >= 30000) && (WaitForInit))
                {
                    LogTestToFile(CurrentClass, "/r: Initialization problem, retrying");
                    resetStopWatch.Restart();
                    goto startRSTagain;
                }

            }
            Application.DoEvents();
        }


        private void ReturnToIdle()
        {
            Application.DoEvents();
            bool Bcalb = false;
            ErasetSmartAirString("all");
            for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
            {
                //FullTextSmartAir = "";
                WriteToAllSmartAir("ee?", IndexPorts);
                do
                {
                    switch (IndexPorts[j])
                    {
                        case 0:
                            sizebuff = FullTextSmartAir.Length;
                            FullTextSmartAir += serialPort1.ReadExisting();
                            sizebuffnew = FullTextSmartAir.Length;
                            break;
                        case 1:
                            sizebuff = FullTextSmartAir.Length;
                            FullTextSmartAir += serialPort2.ReadExisting();
                            sizebuffnew = FullTextSmartAir.Length;
                            break;
                        case 2:
                            sizebuff = FullTextSmartAir.Length;
                            FullTextSmartAir += serialPort3.ReadExisting();
                            sizebuffnew = FullTextSmartAir.Length;
                            break;
                        case 3:
                            sizebuff = FullTextSmartAir.Length;
                            FullTextSmartAir += serialPort4.ReadExisting();
                            sizebuffnew = FullTextSmartAir.Length;
                            break;
                        case 4:
                            sizebuff = FullTextSmartAir.Length;
                            FullTextSmartAir += serialPort5.ReadExisting();
                            sizebuffnew = FullTextSmartAir.Length;
                            break;
                    }
                } while (sizebuffnew > sizebuff);
                //
                if (FullTextSmartAir.Contains("!Incorrect orientation......") || FullTextSmartAir.Contains("!System.....................: INITIALIZATION") || FullTextSmartAir.Contains("!System.....................: Initialization"))
                {
                    //CalibrationSMA(IndexPorts[j]);
                    Bcalb = true;
                }
                if (Bcalb)
                {
                    FullTextSmartAir = "";
                    WriteToAllSmartAir("ee?", IndexPorts);
                    do
                    {
                        switch (IndexPorts[j])
                        {
                            case 0:
                                sizebuff = FullTextSmartAir.Length;
                                FullTextSmartAir += FullTextSmartAir5units[IndexPorts[j]];
                                FullTextSmartAir5units[IndexPorts[j]] = "";
                                FullTextSmartAir += serialPort1.ReadExisting();
                                sizebuffnew = FullTextSmartAir.Length;
                                break;
                            case 1:
                                sizebuff = FullTextSmartAir.Length;
                                FullTextSmartAir += FullTextSmartAir5units[IndexPorts[j]];
                                FullTextSmartAir5units[IndexPorts[j]] = "";
                                FullTextSmartAir += serialPort2.ReadExisting();
                                sizebuffnew = FullTextSmartAir.Length;
                                break;
                            case 2:
                                sizebuff = FullTextSmartAir.Length;
                                FullTextSmartAir += FullTextSmartAir5units[IndexPorts[j]];
                                FullTextSmartAir5units[IndexPorts[j]] = "";
                                FullTextSmartAir += serialPort3.ReadExisting();
                                sizebuffnew = FullTextSmartAir.Length;
                                break;
                            case 3:
                                sizebuff = FullTextSmartAir.Length;
                                FullTextSmartAir += FullTextSmartAir5units[IndexPorts[j]];
                                FullTextSmartAir5units[IndexPorts[j]] = "";
                                FullTextSmartAir += serialPort4.ReadExisting();
                                sizebuffnew = FullTextSmartAir.Length;
                                break;
                            case 4:
                                sizebuff = FullTextSmartAir.Length;
                                FullTextSmartAir += FullTextSmartAir5units[IndexPorts[j]];
                                FullTextSmartAir5units[IndexPorts[j]] = "";
                                FullTextSmartAir += serialPort5.ReadExisting();
                                sizebuffnew = FullTextSmartAir.Length;
                                break;
                        }
                    } while (sizebuffnew > sizebuff);
                }
                if (FullTextSmartAir.Contains("!System.....................: IDLE"))
                {

                }
                else if (FullTextSmartAir.Contains(": ARMED"))
                {
                    WriteToSingleSmartAir("atg", IndexPorts[j]);
                }
                else if (FullTextSmartAir.Contains("Capacitor V................: FAIL") || (FullTextSmartAir.Contains("!System.....................: TRIGGERED")))
                {
                    ResetSingleSMA(IndexPorts[j]);
                }
                else if (FullTextSmartAir.Contains("!System.....................: MAINTENANCE"))
                {
                    WriteToSingleSmartAir("trg 1", IndexPorts[j] + 1);
                }
                else
                {
                    ResetSingleSMA(IndexPorts[j]);
                }

            }
        }

        private void ClearBuffer()
        {
            for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
            {
                Application.DoEvents();
                switch (IndexPorts[j])
                {
                    case -1:
                        break;
                    case 0:
                        string temp1 = serialPort1.ReadExisting();
                        break;
                    case 1:
                        string temp2 = serialPort2.ReadExisting();
                        break;
                    case 2:
                        string temp3 = serialPort3.ReadExisting();
                        break;
                    case 3:
                        string temp4 = serialPort4.ReadExisting();
                        break;
                    case 4:
                        string temp5 = serialPort5.ReadExisting();
                        break;
                }
            }
            ErasetSmartAirString("all");
            Thread.Sleep(1000); Application.DoEvents();
        }

        private void OpenPorts(int[] P)
        {
            for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
            {
                switch (P[j])
                {
                    case -1:
                        break;
                    case 0:
                        serialPort1.Open();
                        serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                        break;
                }
            }

        }

        private void CloseAllPorts()
        {
            serialPort1.Close();
            serialPort2.Close();
            serialPort3.Close();
            serialPort4.Close();
            serialPort5.Close();
        }

        private void InsertNumCyc_Click(object sender, EventArgs e)
        {
            CycleBox1.Enabled = false;
            string Tempstr = CycleBox1.Text;
            if (CycleBox1.Text == "")
                CycleBox1.Text = "Enter Number";
            else if (CycleBox1.Text.Contains("\n"))
            {
                CycleBox1.Text = "try again";
            }
            else
            {
                StartArmDisTest.Enabled = true;
                NumberOfCycleTest = Convert.ToInt32(Tempstr);
                Thread.Sleep(500);
                CycleBox1.Text = Tempstr + " cycle";
            }
            ClearCyc.Enabled = true;
            InsertNumCyc.Enabled = false;
        }
        private void ResetSingleSMA(int s)
        {
            Application.DoEvents();
            int countRST = 0;
            bool WaitForInit = true;
        startRSTagain:
            ErasetSmartAirString("all");
            Thread.Sleep(500);
            WriteToSingleSmartAir("", s);
            Thread.Sleep(500);
            WriteToSingleSmartAir("rst", s);
            Thread.Sleep(3000);
            do
            {
                switch (s)
                {
                    case 0:
                        sizebuff = FullTextSmartAir.Length;
                        FullTextSmartAir += FullTextSmartAir5units[s];
                        FullTextSmartAir5units[s] = "";
                        FullTextSmartAir += serialPort1.ReadExisting();
                        sizebuffnew = FullTextSmartAir.Length;
                        if (sizebuffnew <= sizebuff)
                        {
                            countRST++;
                            Thread.Sleep(800);
                        }
                        break;
                    case 1:
                        sizebuff = FullTextSmartAir.Length;
                        FullTextSmartAir += FullTextSmartAir5units[s];
                        FullTextSmartAir5units[s] = "";
                        FullTextSmartAir += serialPort2.ReadExisting();
                        sizebuffnew = FullTextSmartAir.Length;
                        if (sizebuffnew <= sizebuff)
                        {
                            countRST++;
                            Thread.Sleep(800);
                        }
                        break;
                    case 2:
                        sizebuff = FullTextSmartAir.Length;
                        FullTextSmartAir += FullTextSmartAir5units[s];
                        FullTextSmartAir5units[s] = "";
                        FullTextSmartAir += serialPort3.ReadExisting();
                        sizebuffnew = FullTextSmartAir.Length;
                        if (sizebuffnew <= sizebuff)
                        {
                            countRST++;
                            Thread.Sleep(800);
                        }
                        break;
                    case 3:
                        sizebuff = FullTextSmartAir.Length;
                        FullTextSmartAir += FullTextSmartAir5units[s];
                        FullTextSmartAir5units[s] = "";
                        FullTextSmartAir += serialPort4.ReadExisting();
                        sizebuffnew = FullTextSmartAir.Length;
                        if (sizebuffnew <= sizebuff)
                        {
                            countRST++;
                            Thread.Sleep(800);
                        }
                        break;
                    case 4:
                        sizebuff = FullTextSmartAir.Length;
                        FullTextSmartAir += FullTextSmartAir5units[s];
                        FullTextSmartAir5units[s] = "";
                        FullTextSmartAir += serialPort5.ReadExisting();
                        sizebuffnew = FullTextSmartAir.Length;
                        if (sizebuffnew <= sizebuff)
                        {
                            countRST++;
                            Thread.Sleep(800);
                        }
                        break;
                }
            } while ((sizebuffnew > sizebuff) && (countRST < 3));

            Stopwatch resetStopWatch = new Stopwatch();
            resetStopWatch.Start();
            while (WaitForInit)
            {
                Application.DoEvents();

                if (FullTextSmartAir.Contains(": Finished successfully"))
                    WaitForInit = false;
                TimeSpan ts = resetStopWatch.Elapsed;
                if ((ts.TotalMilliseconds >= 40000) && (WaitForInit))
                {
                    resetStopWatch.Restart();
                    goto startRSTagain;
                }

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label1stpass_Click(object sender, EventArgs e)
        {

        }

        private void label1stpass1_Click(object sender, EventArgs e)
        {

        }

        private void ClearAutoDataTest_Click(object sender, EventArgs e)
        {
            if (ClearAutoDataTest.Text == "Clear")
            {
                progressBar6.Value = 0;
                AutoStatusprec("0");
                Auto1stPort.Text = "";
                Auto2ndPort.Text = "";
                Auto3rdPort.Text = "";
                Auto4thPort.Text = "";
                Auto5thPort.Text = "";

                label1stpass.Text = "0 tests pass";
                label1stpass1.Text = String.Format("{0:0.00}", "0% success");
                label2ndpass.Text = "0 tests pass";
                label2ndpass2.Text = String.Format("{0:0.00}", "0% success");
                label3rdpass.Text = "0 tests pass";
                label3rdpass3.Text = String.Format("{0:0.00}", "0% success");
                label4thpass.Text = "0 tests pass";
                label4thpass4.Text = String.Format("{0:0.00}", "0% success");
                label5thpass.Text = "0 tests pass";
                label5thpass5.Text = String.Format("{0:0.00}", "0% success");
                Application.DoEvents();
                groupBox12.Visible = false;
                ClearAutoDataTest.Visible = false;
                button1.Enabled = true;
                button2.Enabled = true;
                ClearCyc.Enabled = true;
                ClearAutoDataTest.Text = "Abort";
                AbortArmDis_TextChanged(sender, e);
                StartArmDisTest.Enabled = true;
            }
            else if (ClearAutoDataTest.Text == "Abort")
            {
                CurrentCycleTestChangeOption = NumberOfCycleTest - 1;
                StartArmDisTest.Enabled = true;
                ClearAutoDataTest.Enabled = false;
            }
        }

        private void ClearCyc_Click(object sender, EventArgs e)
        {
            if (CycleBox1 is TextBox)
            {
                ((TextBox)CycleBox1).Text = String.Empty;
            }
            CycleBox1.Enabled = true;
            InsertNumCyc.Enabled = true;
            ClearCyc.Enabled = false;
            StartArmDisTest.Enabled = false;
        }

        private void CycleBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxReader3rd_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearScreen1st_Click(object sender, EventArgs e)
        {
            if (textBoxReader1st is TextBox)
            {
                ((TextBox)textBoxReader1st).Text = String.Empty;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (textBoxReader2nd is TextBox)
            {
                ((TextBox)textBoxReader2nd).Text = String.Empty;
            }
        }

        private void ClearScreen3rd_Click(object sender, EventArgs e)
        {
            if (textBoxReader3rd is TextBox)
            {
                ((TextBox)textBoxReader3rd).Text = String.Empty;
            }
        }

        private void ClearScreen5th_Click(object sender, EventArgs e)
        {
            if (textBoxReader5th is TextBox)
            {
                ((TextBox)textBoxReader5th).Text = String.Empty;
            }
        }

        private void ClearScreen4th_Click(object sender, EventArgs e)
        {
            if (textBoxReader4th is TextBox)
            {
                ((TextBox)textBoxReader4th).Text = String.Empty;
            }
        }

        private void textBoxReader4th_TextChanged(object sender, EventArgs e)
        {

        }
        private void Arm1_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("atg", 0);
        }

        private void Arm2_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("atg", 1);
        }

        private void Arm3_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("atg", 2);
        }

        private void Arm4_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("atg", 3);
        }

        private void Arm5_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("atg", 4);
        }

        private void Fire5_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("fire", 4);
        }

        private void Fire4_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("fire", 3);
        }

        private void Fire3_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("fire", 2);
        }

        private void Fire2_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("fire", 1);
        }

        private void Fire1_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("fire", 0);
        }

        private void textBoxSender1st_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void textBoxSender2nd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendButton2nd_Click(sender, e);
            }
        }

        private void textBoxSender3rd_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSender3rd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendButton3rd_Click_1(sender, e);
            }
        }

        private void textBoxSender4th_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendButton4th_Click(sender, e);
            }
        }

        private void textBoxSender5th_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendButton5th_Click(sender, e);
            }
        }

        private void configuration1_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            ClearParam1.Visible = true;
            ParamsList1.Visible = true;
            CloseParam1.Visible = true;
            FullTextSmartAir5units[0] = "";
            WriteToSingleSmartAir("ee?", 0);
            Thread.Sleep(1000);
            Application.DoEvents();
            for (int j = 0; j < AllParams.Length; j++)
            {
                EEParam(0, j);
                Application.DoEvents();
                if (CurrentConfValue[j] == 101010)
                {
                    ParamsList1.Text += (AllParams[j] + ": NaN\r\n");
                    goto NextParams;
                }
                ParamsList1.Text += (AllParams[j] + ": " + CurrentConfValue[j]).ToString() + "\r\n";
            NextParams:
                Application.DoEvents();
            }
        }

        private void EEParam(int v, int j)
        {
            Application.DoEvents();
            TempSmartAirText = "";
            Thread.Sleep(100);
            TempSmartAirText = FullTextSmartAir5units[v];
            int count = 0;
            if (j > AllParams.Length)
                goto nextParam;
            StartAgain:
            int TempValue = TempSmartAirText.IndexOf(AllParams[j] + ":");//[m].ARH:
            if ((TempValue == -1) && (count <= 2))
            {
                count++;
                goto StartAgain;
            }
            else if ((TempValue == -1) && (count > 2))
            {
                CurrentConfValue[j] = 101010;
                goto nextParam;
            }

            CurrentConf[j] = TempSmartAirText.Substring(TempValue + 5, 8);
            if (CurrentConf[j].Contains("["))
                CurrentConf[j] = CurrentConf[j].Substring(0, CurrentConf[j].Length - 2);
            CurrentConfValue[j] = Convert.ToDouble(CurrentConf[j]);
        nextParam:
            { }
        }

        private void configuration2_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("ee?", 1);
        }

        private void configuration3_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("ee?", 2);
        }

        private void configuration4_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("ee?", 3);
        }

        private void configuration5_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("ee?", 4);
        }



        private void RefreshPorts1_Click_1(object sender, EventArgs e)
        {
            PortName1st.Items.Clear();
            getAvailablePorts();
        }

        private void RefreshPorts2_Click(object sender, EventArgs e)
        {
            PortName2nd.Items.Clear();
            getAvailablePorts();
        }

        private void RefreshPorts3_Click(object sender, EventArgs e)
        {
            PortName3rd.Items.Clear();
            getAvailablePorts();
        }

        private void RefreshPorts4_Click(object sender, EventArgs e)
        {
            PortName4th.Items.Clear();
            getAvailablePorts();
        }

        private void RefreshPorts5_Click(object sender, EventArgs e)
        {
            PortName5th.Items.Clear();
            getAvailablePorts();
        }

        private void InsertFinalParameters1_Click(object sender, EventArgs e)
        {
            Cancel1.Visible = true;
            Phantom3.Visible = true;
            M200Param1.Visible = true;
            M600Param1.Visible = true;
            InsertFinalParameters1.Enabled = false;
        }

        private void M200Param1_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            CloseParam1.Enabled = false;
            ClearParam1.Enabled = false;
            Cancel1.Enabled = false;
            textBoxSender1st.Enabled = false;
            ClosePort1st.Enabled = false;
            InsertFinalParameters1.Enabled = false;
            M600Param1.Visible = false;
            M200Param1.Visible = false;
            Phantom3.Visible = false;
            button6.Enabled = false;//FMT button
            sendButton1st.Enabled = false;
            InsertFinalParameters1.Enabled = false;
            configuration1.Enabled = false;
            button3.Enabled = false;
            CAL_1st.Enabled = false;
            RST_1st.Enabled = false;
            Arm1.Enabled = false;
            Fire1.Enabled = false;
            ParamBar1.Visible = true;
            FullTextSmartAir5units[0] = "";
            WriteToSingleSmartAir("ee?", 0);
            Thread.Sleep(1250);
            Cancel1.Enabled = true;
            Application.DoEvents();
            M200_FinalConfiguration(0);
            Application.DoEvents();
            Arm1.Enabled = true;
            Fire1.Enabled = true;
            CAL_1st.Enabled = true;
            RST_1st.Enabled = true;
            button3.Enabled = true;
            button6.Enabled = true;//FMT button
            configuration1.Enabled = true;
            sendButton1st.Enabled = true;
            //InsertFinalParameters1.Enabled = true;
            textBoxSender1st.Enabled = true;
            ClosePort1st.Enabled = true;
            double[] CurrentConfValue = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] CurrentConf = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            CloseParam1.Enabled = true;
            ClearParam1.Enabled = true;
            ParamBar1.Visible = false;
            ParamBar1.Value = 0;
            Cancel1.Visible = false;
        }

        private void M600Param1_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            CloseParam1.Enabled = false;
            ClearParam1.Enabled = false;
            Cancel1.Enabled = false;
            textBoxSender1st.Enabled = false;
            ClosePort1st.Enabled = false;
            InsertFinalParameters1.Enabled = false;
            M600Param1.Visible = false;
            M200Param1.Visible = false;
            Phantom3.Visible = false;
            button6.Enabled = false;//FMT button
            sendButton1st.Enabled = false;
            InsertFinalParameters1.Enabled = false;
            configuration1.Enabled = false;
            button3.Enabled = false;
            CAL_1st.Enabled = false;
            RST_1st.Enabled = false;
            Arm1.Enabled = false;
            Fire1.Enabled = false;
            ParamBar1.Visible = true;
            FullTextSmartAir5units[0] = "";
            WriteToSingleSmartAir("ee?", 0);
            Thread.Sleep(1250);
            Cancel1.Enabled = true;
            Application.DoEvents();
            M600Beta_FinalConfiguration(0);
            Application.DoEvents();
            Arm1.Enabled = true;
            Fire1.Enabled = true;
            CAL_1st.Enabled = true;
            RST_1st.Enabled = true;
            button3.Enabled = true;
            configuration1.Enabled = true;
            button6.Enabled = true;//FMT button
            sendButton1st.Enabled = true;
            //InsertFinalParameters1.Enabled = true;
            textBoxSender1st.Enabled = true;
            ClosePort1st.Enabled = true;
            double[] CurrentConfValue = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] CurrentConf = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            CloseParam1.Enabled = true;
            ClearParam1.Enabled = true;
            ParamBar1.Visible = false;
            ParamBar1.Value = 0;
            Cancel1.Visible = false;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            M200Param2.Visible = true;
            M600Param2.Visible = true;
            button8.Enabled = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void InsertFinalParameters3_Click(object sender, EventArgs e)
        {
            InsertFinalParameters3.Enabled = false;
            M200Param3.Visible = true;
            M600Param3.Visible = true;
        }

        private void InsertFinalParameters4_Click(object sender, EventArgs e)
        {
            InsertFinalParameters4.Enabled = false;
            M200Param4.Visible = true;
            M600Param4.Visible = true;
        }

        private void InsertFinalParameters5_Click(object sender, EventArgs e)
        {
            InsertFinalParameters5.Enabled = false;
            button15.Visible = true;
            M600Param5.Visible = true;
        }

        private void M200Param2_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            textBoxSender2nd.Enabled = false;
            ClosePort2nd.Enabled = false;
            button8.Enabled = false;
            M600Param2.Visible = false;
            M200Param2.Visible = false;
            sendButton2nd.Enabled = false;
            configuration2.Enabled = false;
            listIMU2nd.Enabled = false;
            CAL_2nd.Enabled = false;
            RST_2nd.Enabled = false;
            Arm2.Enabled = false;
            Fire2.Enabled = false;
            M200_FinalConfiguration(1);
            Application.DoEvents();
            textBoxSender2nd.Enabled = true;
            ClosePort2nd.Enabled = true;
            button8.Enabled = true;
            M600Param2.Visible = true;
            M200Param2.Visible = true;
            sendButton2nd.Enabled = true;
            configuration2.Enabled = true;
            listIMU2nd.Enabled = true;
            CAL_2nd.Enabled = true;
            RST_2nd.Enabled = true;
            Arm2.Enabled = true;
            Fire2.Enabled = true;
        }

        private void M600Param2_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            textBoxSender2nd.Enabled = false;
            ClosePort2nd.Enabled = false;
            button8.Enabled = false;
            M600Param2.Visible = false;
            M200Param2.Visible = false;
            sendButton2nd.Enabled = false;
            configuration2.Enabled = false;
            listIMU2nd.Enabled = false;
            CAL_2nd.Enabled = false;
            RST_2nd.Enabled = false;
            Arm2.Enabled = false;
            Fire2.Enabled = false;
            M600Beta_FinalConfiguration(1);
            Application.DoEvents();
            textBoxSender2nd.Enabled = true;
            ClosePort2nd.Enabled = true;
            button8.Enabled = true;
            M600Param2.Visible = true;
            M200Param2.Visible = true;
            sendButton2nd.Enabled = true;
            configuration2.Enabled = true;
            listIMU2nd.Enabled = true;
            CAL_2nd.Enabled = true;
            RST_2nd.Enabled = true;
            Arm2.Enabled = true;
            Fire2.Enabled = true;
        }

        private void ParamBar1_Click(object sender, EventArgs e)
        {

        }

        private void ClearParam1_Click(object sender, EventArgs e)
        {
            InsertFinalParameters1.Enabled = true;
            if (ParamsList1 is TextBox)
            {
                ((TextBox)ParamsList1).Text = String.Empty;
            }
            double[] CurrentConfValue = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] CurrentConf = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            ParamsList1.Text = "";
        }

        private void CloseParam1_Click(object sender, EventArgs e)
        {
            InsertFinalParameters1.Enabled = true;
            if (ParamsList1 is TextBox)
            {
                ((TextBox)ParamsList1).Text = String.Empty;
            }
            double[] CurrentConfValue = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] CurrentConf = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            ParamsList1.Text = "";
            ClearParam1.Visible = false;
            CloseParam1.Visible = false;
            ParamsList1.Visible = false;
        }

        private void ParamsList1_TextChanged(object sender, EventArgs e)
        {

        }

        private void StartCalb1_Click(object sender, EventArgs e)
        {
            if (StartCalb1.Text == "Start")
            {
                CAL_1st.Text = "Calibration";
                StartCalibrationSMA(0);
                StartCalb1.Text = "Stop";
                goto EndButton;
            }
            if (StartCalb1.Text == "Stop")
            {
                StopCalibrationSMA(0);
                StartCalb1.Text = "Start";
                goto EndButton;
            }
        EndButton:
            if (StartCalb1.Text == "Start")
            {
                StartCalb1.Visible = false;
                configuration1.Enabled = true;
            }

        }

        private void Cancel1_Click(object sender, EventArgs e)
        {
            InsertFinalParameters1.Enabled = true;
            M200Param1.Visible = false;
            M600Param1.Visible = false;
            Phantom3.Visible = false;
            Cancel1.Visible = false;
            Abort1_TextChanged(sender, e);
        }

        private void Abort1_Click(object sender, EventArgs e)
        {

        }

        private void Abort1_TextChanged(object sender, EventArgs e)
        {
            CurrentSetParams = CurrentConfValue.Length - 1;
        }

        private void Abort1_KeyDown(object sender, KeyEventArgs e)
        {
            CurrentSetParams = CurrentConfValue.Length - 1;
        }

        private void RcButton1_Click(object sender, EventArgs e)
        {
            timer_2.Interval = 30000; // here time in milliseconds
            timer_2.Tick += timer2_Tick;
            timer_2.Start();
            Application.DoEvents();
            int RceValueChange = 0;
            if (RcButton1.Text == "RC Status")
            {
                groupBox2.Enabled = false;
                ImuButton1.Enabled = false;
                CancelRc1.Visible = true;
                FullTextSmartAir5units[0] = "";
                WriteToSingleSmartAir("ee?", 0);
                Thread.Sleep(800);
                Application.DoEvents();
                CheckStatusRc(0);
                if (RcCheckBool)
                {
                    ArmCheckBox1.Visible = true;
                    FireCheckBox1.Visible = true;
                    CancelRc1.Enabled = true;
                    RcButton1.Text = "Apply";
                }
                else
                {
                    Application.DoEvents();
                    ArmCheckBox1.Visible = false;
                    FireCheckBox1.Visible = false;
                    RcCheckBool = true;
                    CancelRc1.Enabled = false;
                    groupBox2.Enabled = true;
                    ImuButton1.Enabled = true;
                    CancelRc1.Visible = false;
                    CancelRc1.Enabled = false;

                }

            }
            else if (RcButton1.Text == "Apply")
            {
                if (ArmCheckBox1.Checked)
                    RceValueChange = RceValueChange + 2;
                if (FireCheckBox1.Checked)
                    RceValueChange = RceValueChange + 1;
                Thread.Sleep(1000);
                Application.DoEvents();
                WriteToSingleSmartAir("rce " + RceValueChange, 0);
                Thread.Sleep(500);
                Application.DoEvents();
                ArmCheckBox1.Visible = false;
                FireCheckBox1.Visible = false;
                RcButton1.Text = "RC Status";
                groupBox2.Enabled = true;
                ImuButton1.Enabled = true;
                CancelRc1.Visible = false;
                CancelRc1.Enabled = false;

            }
            Application.DoEvents();
        }

        private void CheckStatusRc(int s)
        {
            double RceValueInt = 5;
            UseFullSmartAirString(s);
            Thread.Sleep(300);
            int RceIndex = FullTextSmartAir.IndexOf("RCE:");
            if (RceIndex == -1)
                goto errorTry;
            string RceValueStr = FullTextSmartAir.Substring(RceIndex + 5, 8);
            if (RceValueStr.Contains("["))
                RceValueStr = RceValueStr.Substring(0, RceValueStr.Length - 2);
            RceValueInt = Convert.ToDouble(RceValueStr);
        errorTry:
            switch (RceValueInt)
            {
                case 0:
                    this.ArmCheckBox1.Checked = false;
                    this.FireCheckBox1.Checked = false;
                    break;
                case 1:
                    this.ArmCheckBox1.Checked = false;
                    this.FireCheckBox1.Checked = true;
                    break;
                case 2:
                    this.ArmCheckBox1.Checked = true;
                    this.FireCheckBox1.Checked = false;
                    break;
                case 3:
                    this.ArmCheckBox1.Checked = true;
                    this.FireCheckBox1.Checked = true;
                    break;
                default:

                    RcCheckBool = false;
                    MessageBox.Show("Error! , Please try again", "Warning");
                    break;
            }
        }
        private void CheckStatusImu(int s)
        {
            this.Yaw1.Checked = false; this.Height1.Checked = false; this.Roll_Pitch1.Checked = false; this.Enable1.Checked = false; this.FreeFall1.Checked = false;
            UseFullSmartAirString(s);
            Thread.Sleep(300);
            int ImuIndex = FullTextSmartAir.IndexOf("IMU:");
            string ImuValueStr = FullTextSmartAir.Substring(ImuIndex + 5, 8);
            if (ImuValueStr.Contains("["))
                ImuValueStr = ImuValueStr.Substring(0, ImuValueStr.Length - 2);
            ImuValueInt = Convert.ToInt32(ImuValueStr);

            string ImuBinary = Convert.ToString(ImuValueInt, 2);
            int[] bits = ImuBinary.PadRight(5, '0') // Add 0's from left
                         .Select(c => int.Parse(c.ToString())) // convert each char to int
                         .ToArray();
            Array.Reverse(bits);
            for (int j = 0; j < bits.Length; j++)
            {

                switch (j)
                {
                    case 0:
                        if (bits[j] == 1)
                            this.Roll_Pitch1.Checked = true;
                        break;
                    case 1:
                        if (bits[j] == 1)
                            this.FreeFall1.Checked = true;
                        break;
                    case 2:
                        if (bits[j] == 1)
                            this.Yaw1.Checked = true;
                        break;
                    case 4:
                        if (bits[j] == 1)
                            this.Enable1.Checked = true;
                        break;
                    case 5:
                        if (bits[j] == 1)
                            this.Height1.Checked = true;
                        break;
                }
            }
        }
        private void ArmCheckBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ImuButton1_Click(object sender, EventArgs e)
        {
            timer_3.Interval = 30000; // here time in milliseconds
            timer_3.Tick += timer3_Tick;
            timer_3.Start();
            int imuVal = 0;
            if (ImuButton1.Text == "IMU Status")
            {
                groupBox2.Enabled = false;
                RcButton1.Enabled = false;
                FullTextSmartAir5units[0] = "";
                WriteToSingleSmartAir("ee?", 0);
                Thread.Sleep(800);
                Application.DoEvents();
                CheckStatusImu(0);
                Enable1.Visible = true;
                Roll_Pitch1.Visible = true;
                FreeFall1.Visible = true;
                Yaw1.Visible = true;
                Height1.Visible = true;
                CancelImu1.Visible = true;
                CancelImu1.Enabled = true;
                ImuButton1.Text = "Apply";
                Application.DoEvents();
            }
            else if (ImuButton1.Text == "Apply")
            {
                if (Roll_Pitch1.Checked)
                    imuVal = imuVal + 1;
                if (FreeFall1.Checked)
                    imuVal = imuVal + 2;
                if (Yaw1.Checked)
                    imuVal = imuVal + 4;
                if (Enable1.Checked)
                    imuVal = imuVal + 16;
                if (Height1.Checked)
                    imuVal = imuVal + 32;
                Thread.Sleep(1000);
                WriteToSingleSmartAir("imu " + imuVal, 0);
                this.Yaw1.Checked = false; this.Height1.Checked = false; this.Roll_Pitch1.Checked = false; this.Enable1.Checked = false; this.FreeFall1.Checked = false;
                Application.DoEvents();
                Thread.Sleep(500);
                Enable1.Visible = false;
                Roll_Pitch1.Visible = false;
                FreeFall1.Visible = false;
                Yaw1.Visible = false;
                Height1.Visible = false;
                ImuButton1.Text = "IMU Status";
                groupBox2.Enabled = true;
                RcButton1.Enabled = true;
                CancelImu1.Visible = false;
            }
            Application.DoEvents();
        }

        private void CancelRc1_Click(object sender, EventArgs e)
        {
            RcButton1.Text = "RC Status";
            ArmCheckBox1.Visible = false;
            FireCheckBox1.Visible = false;
            RcCheckBool = false;
            groupBox2.Enabled = true;
            ImuButton1.Enabled = true;
            CancelRc1.Visible = false;
            Application.DoEvents();
        }

        private void CancelImu1_Click(object sender, EventArgs e)
        {
            ImuButton1.Text = "IMU Status";
            Enable1.Visible = false;
            Roll_Pitch1.Visible = false;
            FreeFall1.Visible = false;
            Yaw1.Visible = false;
            Height1.Visible = false;
            groupBox2.Enabled = true;
            RcButton1.Enabled = true;
            CancelImu1.Visible = false;
            Application.DoEvents();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WriteToSingleSmartAir("fmt", 0);
            Thread.Sleep(800);
            WriteToSingleSmartAir("rst", 0);
        }

        private void ReturnMCUID1_5()//zohar port
        {
            FullTextSmartAir = "";
            WriteToAllSmartAir("ee?", IndexPorts);
            for (int j = 0; j < checkPortList.CheckedIndices.Count; j++)
            {
                if (MCUID1_5[IndexPorts[j]].Equals(""))
                {
                    EEFinished1_5[IndexPorts[j]] = false;

                    Thread.Sleep(SleepAfterWriteLineEvent);
                    while (!EEFinished1_5[IndexPorts[j]])
                    {
                        if (FullTextSmartAir5units[IndexPorts[j]].Contains("!MCU ID.....................:"))
                        {//!MCU ID.....................: 00250023 30385114 38343334
                            MCUID1_5[IndexPorts[j]] = FullTextSmartAir5units[IndexPorts[j]].Substring(FullTextSmartAir5units[IndexPorts[j]].IndexOf("!MCU ID.....................:") + 30, 26);
                            MCUID1_5[IndexPorts[j]] = MCUID1_5[IndexPorts[j]].Replace(" ", "_");
                            EEFinished1_5[IndexPorts[j]] = true;
                        }
                        else
                        {
                            WriteToAllSmartAir("ee?", IndexPorts);
                            Thread.Sleep(1000);
                        }
                    }
                    EEFinished1_5[IndexPorts[j]] = false;
                }

            }
        }

        private void AbortArmDis_TextChanged(object sender, EventArgs e)
        {

            CurrentCycleTest = NumberOfCycleTest - 1;
        }

        private void Phantom3_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            CloseParam1.Enabled = false;
            ClearParam1.Enabled = false;
            Cancel1.Enabled = false;
            textBoxSender1st.Enabled = false;
            ClosePort1st.Enabled = false;
            InsertFinalParameters1.Enabled = false;
            M600Param1.Visible = false;
            M200Param1.Visible = false;
            Phantom3.Visible = false;
            button6.Enabled = false;//FMT button
            sendButton1st.Enabled = false;
            InsertFinalParameters1.Enabled = false;
            configuration1.Enabled = false;
            button3.Enabled = false;
            CAL_1st.Enabled = false;
            RST_1st.Enabled = false;
            Arm1.Enabled = false;
            Fire1.Enabled = false;
            ParamBar1.Visible = true;
            FullTextSmartAir5units[0] = "";
            WriteToSingleSmartAir("ee?", 0);
            Thread.Sleep(1250);
            Cancel1.Enabled = true;
            Application.DoEvents();
            Phantom3_FinalConfiguration(0);
            Application.DoEvents();
            Arm1.Enabled = true;
            Fire1.Enabled = true;
            CAL_1st.Enabled = true;
            RST_1st.Enabled = true;
            button3.Enabled = true;
            button6.Enabled = true;//FMT button
            configuration1.Enabled = true;
            sendButton1st.Enabled = true;
            //InsertFinalParameters1.Enabled = true;
            textBoxSender1st.Enabled = true;
            ClosePort1st.Enabled = true;
            double[] CurrentConfValue = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] CurrentConf = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            CloseParam1.Enabled = true;
            ClearParam1.Enabled = true;
            ParamBar1.Visible = false;
            ParamBar1.Value = 0;
            Cancel1.Visible = false;
        }

        private void Phantom3_FinalConfiguration(int s)
        {
            Thread.Sleep(1000);
            Application.DoEvents();
            //ReturnToIdleSingleSMA(s);
            ParamsList1.Visible = true;
            InitBoard_Nano(AllParams, Phantom3ParamsValue, s);
            CloseParam1.Visible = true;
            ClearParam1.Visible = true;
            if (ParamReserNeed)
            {
                FullTextSmartAir5units[s] = "";
                WriteToSingleSmartAir("RST", s);
            }


            Thread.Sleep(1100);
            ParamReserNeed = false;
            WaitForSuccessfulInit(s);
        }

        private void InitBoard_Nano(string[] allParams, string[] ParamsValue, int s)
        {
            CurrentSetParams = 0;
            string CorrectParamString = "";
            string CurrentParamString = "";
            for (int j = 0; CurrentSetParams < AllParams.Length; CurrentSetParams++)
            {
                ParamBar1.Value = (100 * (CurrentSetParams + 1) / (AllParams.Length));
                bool WaitForInit = true;
                Thread.Sleep(600);
                Application.DoEvents();
                if (CurrentSetParams > AllParams.Length)
                    goto CancelAct;
                EEParam(s, CurrentSetParams);
                CorrectParamString = AllParams[CurrentSetParams] + ": " + ParamsValue[CurrentSetParams];
                CurrentParamString = AllParams[CurrentSetParams] + ": " + CurrentConfValue[CurrentSetParams];
                Application.DoEvents();

                if (CurrentParamString == CorrectParamString)
                {
                    ParamsList1.Text += (CorrectParamString + "\r\n");
                    Application.DoEvents();
                }
                else if (ParamsThatCauseIgnor.Contains(AllParams[CurrentSetParams]))
                {
                    ParamsList1.Text += (CurrentParamString + " (Unchanged)" + "\r\n");
                    Application.DoEvents();
                }
                else if (CorrectParamString.Contains("FFL"))
                {
                    string fflValue = "FFL: " + ((9.8 - CurrentConfValue[CurrentSetParams])).ToString();
                    if (CorrectParamString.Contains(fflValue))
                        ParamsList1.Text += (CurrentParamString + "\r\n");
                    else
                    {
                        WriteToSingleSmartAir(AllParams[CurrentSetParams] + " " + ParamsValue[CurrentSetParams], s);
                        ParamReserNeed = true;
                        ParamsList1.Text += (AllParams[CurrentSetParams] + ": " + ((9.8 - Convert.ToDouble(ParamsValue[CurrentSetParams])).ToString()) + "  (" + CurrentParamString + ")\r\n");
                        Application.DoEvents();
                        Thread.Sleep(250);
                    }
                }
                else if (AllParams[CurrentSetParams] == ParamsValue[CurrentSetParams])
                {
                    ParamsList1.Text += (AllParams[CurrentSetParams] + ": NaN\r\n");
                    Application.DoEvents();
                }
                else
                {
                    WriteToSingleSmartAir(AllParams[CurrentSetParams] + " " + ParamsValue[CurrentSetParams], s);
                    ParamReserNeed = true;
                    ParamsList1.Text += (CorrectParamString + "  (" + CurrentParamString + ")\r\n");
                    Application.DoEvents();
                    Thread.Sleep(250);
                }
            CancelAct:
                Application.DoEvents();
            }
            CurrentSetParams = 0;
        }

        private void LedTest_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            WriteToSingleSmartAir("ledt", 0);
            Thread.Sleep(500);
        }

        private void button11_Click(object sender, EventArgs e)//servo test
        {
            EnableServoTest.Checked = false;
            ServoTest.Enabled = false;
            Application.DoEvents();
            WriteToSingleSmartAir("srvt", 0);
            Thread.Sleep(500);

        }

        private void BuzzerTest_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            WriteToSingleSmartAir("bzrt", 0);
            Thread.Sleep(500);
        }

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();//define timer for sevo test button
        System.Windows.Forms.Timer timer_2 = new System.Windows.Forms.Timer();//define timer for sevo test button
        System.Windows.Forms.Timer timer_3 = new System.Windows.Forms.Timer();//define timer for sevo test button
        private void EnableServoTest_CheckedChanged(object sender, EventArgs e)
        {
            ServoTest.Enabled = EnableServoTest.Checked;
            timer.Interval = 5000; // here time in milliseconds
            timer.Tick += timer1_Tick;
            timer.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            EnableServoTest.Checked = false;
            ServoTest.Enabled = EnableServoTest.Checked;
            timer.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer_2.Stop();
            Application.DoEvents();
            ArmCheckBox1.Visible = false;
            FireCheckBox1.Visible = false;
            RcButton1.Text = "RC Status";
            groupBox2.Enabled = true;
            ImuButton1.Enabled = true;
            CancelRc1.Visible = false;
            CancelRc1.Enabled = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer_3.Stop();
            Enable1.Visible = false;
            Roll_Pitch1.Visible = false;
            FreeFall1.Visible = false;
            Yaw1.Visible = false;
            Height1.Visible = false;
            ImuButton1.Text = "IMU Status";
            groupBox2.Enabled = true;
            RcButton1.Enabled = true;
            CancelImu1.Visible = false;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OpenBurnPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (BurnPortsName.Text == "" || BurnBaudRate.Text == "" || BurnSystem.Text == "" || BurnVersion.Text == "")
                {
                    MessageBox.Show("Please select port settings");
                }
                else
                {
                    serialPort1.PortName = BurnPortsName.Text;
                    serialPort1.BaudRate = Convert.ToInt32(BurnBaudRate.Text);
                    serialPort1.ReadBufferSize = serialPort1.BaudRate / 10;
                    serialPort1.Open();
                    BurnProgressBar7.Value = 100;
                    OpenBurnPort.Enabled = false;
                    CloseBurnPort.Enabled = true;
                    button9.Enabled = true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                FlashTextBox.Text = "Unauthorized Access";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BurnPortsName.Items.Clear();
            getAvailablePorts();
        }

        private void CloseBurnPort_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            BurnProgressBar7.Value = 0;
            OpenBurnPort.Enabled = true;
            CloseBurnPort.Enabled = false;
        }

        private void SMAbutton_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Thread.Sleep(1000);

        }

        private void ClearFlashScreen_Click(object sender, EventArgs e)
        {
            if (FlashTextBox is TextBox)
            {
                ((TextBox)FlashTextBox).Text = String.Empty;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FullTextSmartAir5units[0] = "";
            button9.Enabled = false;
            WriteToSingleSmartAir("rst", 0);
            Thread.Sleep(6000);
            int countTry = 0,countTry2=0;
            Application.DoEvents();
            Thread.Sleep(500);
            StartFlashBar.Value = 0;
         tryAgian:
            Thread.Sleep(1000);
            if (FullTextSmartAir5units[0].Contains("Bootloader ver"))
            {
                StartFlashBar.Value = 1;
                Application.DoEvents();
                SMAbutton.Enabled = true;
                if (BurnSystem.Text=="V2")
                {
                    FlashSCUbutton.Visible = true;
                    CCUFlashbutton.Visible = true;
                    SenFlashbutton.Visible = true;
                }
                Application.DoEvents();
                goto endButton;
            }
            else if (countTry < 3)
            {
                Application.DoEvents();
                countTry++;
                goto tryAgian;
            }
            countTry = 0;
            if (FullTextSmartAir5units[0].Contains("Application................: Start"))
            {
                Thread.Sleep(6000);
                Application.DoEvents();
                if (FullTextSmartAir5units[0].Contains("!System.....................:"))
                {
                    Thread.Sleep(1500);
                    WriteToSingleSmartAir("trg 2", 0);
                    Thread.Sleep(4000);
                    WriteToSingleSmartAir("fwu", 0);
                }
                if (countTry2 < 3)
                {
                    countTry2++;
                    goto tryAgian;
                }
            }
        endButton:;
        }
    }
}
