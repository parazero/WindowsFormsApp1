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
using System.Net.Sockets;
using System.Net;
using Curit.Module.RTX.Com;
using DisplayGraphs;

//using Curit.Module.RTX.Com;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static int SleepAfterWriteLineEvent = 2000;
        static int NumberOfCycle = 0;
        static int NumOfPackets;

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
        
        private string path= Directory.GetCurrentDirectory();
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
        string[] AllParams = {"RAW", "ARH", "DRH", "AHS", "VIB", "NVI", "VIT", "HST", "DISE", "ARME", "VIF", "ARM", "PCV", "RCV", "LGR", "MST", "IMU",  "SVPY",     "SPR", "PTS", "PRV", "RCE",   "EBT", "BNC", "MCV", "SPV", "XBT", "MTD", "PWM", "PSOF", "PSON", "PSTO", "RCST", "DST", "USD", "BCLS", "BCLL", "POO", "ADFD", "ADSD", "ADTD", "UFW", "PNO", "SNO", "ESR", "INV", "BET", "SMR", "INC", "TRG", "ATTP", "ATTR", "ATTS", "ATC", "FFC", "FFL", "HTC",   "DHT", "ATRR", "ATRP", "ATRL", "YRL" };
        string[] M200ParamsValue = { "RAW", "7", "0", "1", "0.05", "3",  "VIT", "HST", "DISE", "ARME", "100", "2",   "PCV", "RCV", "0",   "MST", "19",  "SVPY",    "0.35",  "15", "PRV",   "0",     "0", "3",   "3.5", "SPV",   "1",  "50",   "1", "PSOF", "PSON", "PSTO", "RCST",   "0",   "1", "BCLS", "BCLL", "POO", "ADFD", "ADSD", "ADTD", "UFW", "0.2",  "32", "100", "0", "0.3",    "10", "3.5",   "1",   "55",   "55", "ATTS", "ATC", "300", "6.8", "HTC",   "DHT", "ATRR", "ATRP", "ATRL", "YRL" };
        string[] M600ParamsValue = { "RAW", "7", "5", "1", "0.05", "5",  "VIT", "HST", "DISE", "ARME", "100", "2",   "PCV", "RCV", "0",   "MST", "19",  "SVPY",    "0.35",  "15", "PRV",   "0",     "0", "3",   "3.5", "SPV",   "0",  "50",   "1", "PSOF", "PSON", "PSTO", "RCST",   "0",   "1", "BCLS", "BCLL", "POO", "ADFD", "ADSD", "ADTD", "UFW", "0.2",  "32", "100", "0", "0.3",    "10", "3.5",   "1",   "65",   "65", "ATTS", "ATC", "130", "7",   "HTC",   "DHT", "ATRR", "ATRP", "ATRL", "YRL" };
        string[] Phantom4ParamsValue = { "1","5", "0", "AHS", "0.08", "5", "10", "0.3", "2", "3", "100",   "2",   "PCV", "RCV", "0",   "0",      "23",     "1",    "0.35",  "15",  "4",    "0",   "EBT", "1",   "3.7", "3.5",   "0",   "1",   "1", "1000", "1900",    "1",   "60",  "10",   "1",   "20",   "30", "200",   "80",    "5",  "150",  "80", "0.2",  "32", "100", "0", "0.3",    "10", "3.5",   "1",   "65",   "65", "85",     "5", "300", "5.8",   "8", "-0.17",   "20",   "20",  "250", "300" };
        string[] CurrentConf = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        //int[] bits = { 0, 0, 0, 0, 0 };
        double[] CurrentConfValue = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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
            BurnSystem.Text="Nano";
            BurnSystem.Enabled = false;
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
                    ParamsStatus.Enabled = true;
                    RefreshStatusParams.Enabled = true;
                    updateParamsStatus();
                }
            }
            catch (UnauthorizedAccessException)
            {
                textBoxReader1st.Text = "Unauthorized Access";
            }
        }
        private double checkValueParam(string param)
        {
            double ValueParam;
            param = param + ":";
            int indexParam = FullTextSmartAir5units[0].IndexOf(param);
            if (indexParam == -1)
                ValueParam = -111.111;
            else
            {
                string TempTxt = FullTextSmartAir5units[0].Substring(indexParam);
                int indexEndStr = TempTxt.IndexOf("[");
                TempTxt = TempTxt.Substring(0, indexEndStr);
                string[] splitStr = TempTxt.Split(':');
                try
                {
                    ValueParam = Convert.ToDouble(splitStr[1]);
                }
                catch
                {
                    int endValParam = splitStr[1].IndexOf("\r\n");
                    ValueParam = Convert.ToDouble(splitStr[1].Substring(0, endValParam));
                }
            }
            return ValueParam;
        }
        private void updateParamsStatus()
        {
            int i = 0;
            StatusParamsBar.Visible = true;
            List<string> EnableParams = new List<string>() { "RAW", "XBT", "PWM", "USD", "EBT" };
            List<string> ModeParams = new List<string>() { "ARM", "TRG", "DISE", "ARME", "SVPY" };
            List<string> ValueParams = new List<string>() { "ARH", "DRH", "VIB", "NVI", "VIT","MTD"};
            List<string> MoreValuesParams = new List<string>() { "AHS", "PTS", "VIF", "PSTO", "RCST","DST" };

            FullTextSmartAir5units[0] = "";
            StatusParamsBar.Value = 2;Application.DoEvents();
            WriteToSingleSmartAir("ee?", 0);
            StatusParamsBar.Value = 5; Application.DoEvents();
            string checkboxName;
            if (FullTextSmartAir5units[0].Length > 5000)
            {
                if (textBoxReader1st.Text.Length < 7800)
                    textBoxReader1st.Clear();
                i = 0;
                foreach (string param in EnableParams)
                {
                    checkboxName = param;
                    if (FullTextSmartAir5units[0].Contains(param))
                    {
                        i++;
                        if (param == "USD")
                            checkboxName = "Use SD Card";
                        if (param == "XBT")
                            checkboxName = "External battery";
                        if (param == "RAW")
                            checkboxName = "Raw data to log";
                        if (param == "PWM")
                            checkboxName = "PWM signal";
                        if (!CheckBoxEnableList.Items.Contains(checkboxName))
                            CheckBoxEnableList.Items.Add(checkboxName);
                        if (checkValueParam(param) == 1)
                            CheckBoxEnableList.SetItemChecked(i - 1, true);

                        Thread.Sleep(5); Application.DoEvents();
                    }
                }
                StatusParamsBar.Value = 15; Application.DoEvents();
                int ValueParam;
                string ImuBinary;
                foreach (string param in ModeParams)
                {
                    if (FullTextSmartAir5units[0].Contains(param))
                    {
                        Thread.Sleep(5); Application.DoEvents();
                        if (FullTextSmartAir5units[0].Contains(param))
                        {
                            ValueParam = Convert.ToInt32(checkValueParam(param));
                            switch (param)
                            {
                                case "ARM":
                                    i++;
                                    if (!CheckBoxEnableList.Items.Contains("Auto Arm"))
                                        CheckBoxEnableList.Items.Add("Auto Arm");
                                    if (!CheckBoxEnableList.Items.Contains("Auto Disarm"))
                                        CheckBoxEnableList.Items.Add("Auto Disarm");
                                    ImuBinary = Convert.ToString(ValueParam, 2);
                                    if (ImuBinary == "1")
                                        ImuBinary = "01";
                                    int[] bitsARM = ImuBinary.PadRight(2, '0') // Add 0's from left
                                                            .Select(c => int.Parse(c.ToString())) // convert each char to int
                                                            .ToArray();
                                    Array.Reverse(bitsARM);
                                    if (bitsARM[1] == 1)
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, true);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, true);
                                        Application.DoEvents();
                                    }
                                    else if (bitsARM[0] == 1)
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, true);
                                        Application.DoEvents();
                                    }
                                    else
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        Application.DoEvents();
                                    }
                                    break;
                                case "TRG":
                                    i++;
                                    if (!CheckBoxEnableList.Items.Contains("Auto Trigger"))
                                        CheckBoxEnableList.Items.Add("Auto Trigger");
                                    if (!CheckBoxEnableList.Items.Contains("Maintenance mode"))
                                        CheckBoxEnableList.Items.Add("Maintenance mode");
                                    ImuBinary = Convert.ToString(ValueParam, 2);
                                    if (ImuBinary == "1")
                                        ImuBinary = "01";
                                    int[] bitsTRG = ImuBinary.PadRight(2, '0') // Add 0's from left
                                                            .Select(c => int.Parse(c.ToString())) // convert each char to int
                                                            .ToArray();
                                    Array.Reverse(bitsTRG);
                                    if (bitsTRG[0] == 1)
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, true);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        Application.DoEvents();
                                    }
                                    else if (bitsTRG[1] == 1)
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, true);
                                        Application.DoEvents();
                                    }
                                    else
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        Application.DoEvents();
                                    }
                                    break;
                                case "ARME"://+1 - height changed, +2 - vibrations
                                    if (!CheckBoxArmModeList.Items.Contains("from height change"))
                                        CheckBoxArmModeList.Items.Add("from height change");
                                    if (!CheckBoxArmModeList.Items.Contains("from vibration"))
                                        CheckBoxArmModeList.Items.Add("from vibration");
                                    ArmModeGroupBox.Visible = true;
                                    Thread.Sleep(5); Application.DoEvents();
                                    if (ValueParam == 1)
                                    {
                                        CheckBoxArmModeList.SetItemChecked(0, true);
                                        CheckBoxArmModeList.SetItemChecked(1, false);
                                        Application.DoEvents();
                                    }
                                    else if (ValueParam == 2)
                                    {
                                        CheckBoxArmModeList.SetItemChecked(0, false);
                                        CheckBoxArmModeList.SetItemChecked(1, true);
                                        Application.DoEvents();
                                    }
                                    else if (ValueParam == 3)
                                    {
                                        CheckBoxArmModeList.SetItemChecked(0, true);
                                        CheckBoxArmModeList.SetItemChecked(1, true);
                                        Application.DoEvents();
                                    }

                                    break;
                                case "SVPY":
                                    i++;
                                    if (!CheckBoxEnableList.Items.Contains("Use Servo"))
                                        CheckBoxEnableList.Items.Add("Use Servo");
                                    if (!CheckBoxEnableList.Items.Contains("Use Pyro"))
                                        CheckBoxEnableList.Items.Add("Use Pyro");
                                    if (ValueParam == 1)
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, true);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        Application.DoEvents();
                                    }
                                    else if (ValueParam == 2)
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, true);
                                        Application.DoEvents();
                                    }
                                    else if (ValueParam == 3)
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, true);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, true);
                                        Application.DoEvents();
                                    }
                                    else
                                    {
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        i++;
                                        CheckBoxEnableList.SetItemChecked(i - 1, false);
                                        Application.DoEvents();
                                    }
                                    break;
                                case "DISE":
                                    DisarmModeGroupBox.Visible = true;
                                    if (ValueParam == 1)
                                    {
                                        DISETrackBar.Value = 1;
                                        DISEValue.Text = "Disarm from low height";
                                        Application.DoEvents();
                                    }
                                    else if (ValueParam == 2)
                                    {
                                        DISETrackBar.Value = 2;
                                        DISEValue.Text = "Disarm from no vibrations";
                                        Application.DoEvents();
                                    }
                                    else if (ValueParam == 4)
                                    {
                                        DISETrackBar.Value = 3;
                                        DISEValue.Text = "Disarm from no vibrations\nand no height change";
                                        Application.DoEvents();
                                    }
                                    break;
                            }
                        }
                    }
                }
                StatusParamsBar.Value = 38; Application.DoEvents();
                double ImuVal = checkValueParam("IMU");
                {
                    ATC1.Visible = true;
                    ATC1Label.Visible = true;
                    ATTSLabel.Visible = true;
                    ATTS.Visible = true;
                    HTCLabel.Visible = true;
                    HTC.Visible = true;
                    DHTLabel.Visible = true;
                    DHT.Visible = true;
                    YRLLabel.Visible = true;
                    YRL.Visible = true;
                    ATC2Label.Visible = true;
                    ATC2.Visible = true;
                    ATRLLabel.Visible = true;
                    ATRL.Visible = true;
                    ATRRLabel.Visible = true;
                    ATRR.Visible = true;
                    ATRPLabel.Visible = true;
                    ATRP.Visible = true;
                }
                string ImuValBinary = Convert.ToString(Convert.ToInt32(ImuVal), 2);
                int[] bits = ImuValBinary.PadLeft(6, '0') // Add 0's from left
                             .Select(c => int.Parse(c.ToString())) // convert each char to int
                             .ToArray();
                Array.Reverse(bits);

                if (bits[0] == 1)
                {
                    AngularGroupBox1.Visible = true;
                    double ATTPVal = checkValueParam("ATTP");
                    ATTP.Text = ATTPVal.ToString();
                    double ATTRVal = checkValueParam("ATTR");
                    ATTR.Text = ATTRVal.ToString();
                    double ATTSVal = checkValueParam("ATTS");
                    if (ATTSVal == -111.111)
                    {
                        ATTSLabel.Visible = false;
                        ATTS.Visible = false;
                    }
                    else
                        ATTS.Text = ATTSVal.ToString();
                    double ATCVal = checkValueParam("ATC");
                    if (ATCVal == -111.111)
                    {
                        ATC1.Visible = false;
                        ATC1Label.Visible = false;
                    }
                    else
                        ATC1.Text = ATCVal.ToString();
                    Application.DoEvents(); Thread.Sleep(10);
                }
                else
                    AngularGroupBox1.Visible = false;
                StatusParamsBar.Value = 46; Application.DoEvents();
                if (bits[1] == 1)
                {
                    FFLGroupBox.Visible = true;
                    double FFCVal = checkValueParam("FFC");
                    FFC.Text = FFCVal.ToString();
                    double FFLVal = checkValueParam("FFL");
                    FFL.Text = FFLVal.ToString();
                    double HTCVal = checkValueParam("HTC");
                    if (HTCVal == -111.111)
                    {
                        HTCLabel.Visible = false;
                        HTC.Visible = false;
                    }
                    else
                        HTC.Text = HTCVal.ToString();
                    double DHTVal = checkValueParam("DHT");
                    if (DHTVal == -111.111)
                    {
                        DHTLabel.Visible = false;
                        DHT.Visible = false;
                    }
                    else
                        DHT.Text = DHTVal.ToString();
                    Application.DoEvents(); Thread.Sleep(10);
                }
                else
                    FFLGroupBox.Visible = false;
                StatusParamsBar.Value = 58; Application.DoEvents();
                if (bits[2] == 1)
                {
                    AngularGroupBox2.Visible = true;
                    YawGroupBox.Visible = true;
                    double ATRPVal = checkValueParam("ATRP");
                    if (ATRPVal == -111.111)
                    {
                        ATRPLabel.Visible = false;
                        ATRP.Visible = false;
                    }
                    else
                        ATRP.Text = ATRPVal.ToString();
                    double ATRRVal = checkValueParam("ATRR");
                    if (ATRRVal == -111.111)
                    {
                        ATRRLabel.Visible = false;
                        ATRR.Visible = false;
                    }
                    else
                        ATRR.Text = ATRRVal.ToString();
                    double ATRLVal = checkValueParam("ATRL");
                    if (ATRLVal == -111.111)
                    {
                        ATRLLabel.Visible = false;
                        ATRL.Visible = false;
                    }
                    else
                        ATRL.Text = ATRLVal.ToString();
                    double ATCVal = checkValueParam("ATC");
                    if (ATCVal == -111.111)
                    {
                        ATC2Label.Visible = false;
                        ATC2.Visible = false;
                    }
                    else
                        ATC2.Text = ATCVal.ToString();
                    double YRLVal = checkValueParam("YRL");
                    if (YRLVal == -111.111)
                    {
                        YRLLabel.Visible = false;
                        YRL.Visible = false;
                    }
                    else
                        YRL.Text = YRLVal.ToString();
                    Application.DoEvents(); Thread.Sleep(10);
                }
                else
                {
                    AngularGroupBox2.Visible = false;
                    YawGroupBox.Visible = false;
                }
                StatusParamsBar.Value = 70; Application.DoEvents();
                if (bits[5] == 1)
                {
                    HeightGroupBox.Visible = true;
                    double HGTVal = checkValueParam("HGT");
                    HGT.Text = HGTVal.ToString();
                    Application.DoEvents(); Thread.Sleep(10);
                }
                else
                    HeightGroupBox.Visible = false;
                StatusParamsBar.Value = 81; Application.DoEvents();
                StatusParamsBar.Value = StatusParamsBar.Maximum; Application.DoEvents();
                Thread.Sleep(500);StatusParamsBar.Visible = false;Application.DoEvents();
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
            ParamsStatus.Enabled = false;
            RefreshStatusParams.Enabled = false;
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
        bool ReadSP = true;
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //int n = Convert.ToInt32(s);
            if (ReadSP)
            {
                SerialPort sp = (SerialPort)sender;
                string dataIN1 = sp.ReadExisting();
                if (!dataIN1.Equals(""))
                {
                    strTemp1 = dataIN1;
                    FullTextSmartAir5units[0] += dataIN1;
                    this.Invoke(new EventHandler(ShowData1));
                }
            }
        }
        private void ShowData1(object sender, EventArgs e)
        {
            if (ReadSP)
            {
                textBoxReader1st.Text += strTemp1;
                textBoxReader1st.Select(textBoxReader1st.TextLength, 0);
                textBoxReader1st.ScrollToCaret();
                FlashTextBox.Text += strTemp1;
                FlashTextBox.Select(FlashTextBox.TextLength, 0);
                FlashTextBox.ScrollToCaret();
            }
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
            FullTextSmartAir5units[i] = "";
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
            MessageBox.Show("Wait until system boot is complete");
            WaitForSuccessfulInit(i);
            Thread.Sleep(1500); Application.DoEvents();
            MessageBox.Show("Press OK to reset the PCV and RCV values");
            WriteToSingleSmartAir("rcv 0", i);
            Thread.Sleep(500);Application.DoEvents();
            WriteToSingleSmartAir("pcv 0", i);
            Thread.Sleep(500); Application.DoEvents();
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
            updateParamsStatus();
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
            updateParamsStatus();

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
            //DialogResult dr = MessageBox.Show("Manual", "Automated", MessageBoxButtons.YesNo);
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
                updateParamsStatus();
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
            Phantom4_FinalConfiguration(0);
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
            updateParamsStatus();
        }

        private void Phantom4_FinalConfiguration(int s)
        {
            Thread.Sleep(1000);
            Application.DoEvents();
            //ReturnToIdleSingleSMA(s);
            ParamsList1.Visible = true;
            InitBoard_Nano(AllParams, Phantom4ParamsValue, s);
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
                    SMAbutton.Visible = true;
                    if (BurnSystem.Text=="V2")
                    {
                        FlashSCUbutton.Visible = true;
                        CCUFlashbutton.Visible = true;
                        SenFlashbutton.Visible = true;
                    }
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
            button9.Enabled = false;
            FlashSCUbutton.Enabled = false;
            CCUFlashbutton.Enabled = false;
            SenFlashbutton.Enabled = false;
            SMAbutton.Visible = false;
            FlashSCUbutton.Visible = false;
            CCUFlashbutton.Visible = false;
            SenFlashbutton.Visible = false;
        }

        private void SMAbutton_Click(object sender, EventArgs e)
        {
            bool again = true;
            EndCondition = false;
            string LocalPathFM="";
            SMAbutton.Enabled = false;
            FullTextSmartAir5units[0] = "";
            SendToSMAChars("SMA");
            //WriteToSingleSmartAir("sma", 0);
            Application.DoEvents();
            Thread.Sleep(1000);
            switch (BurnSystem.Text)
            {
                case "Nano":
                    if (BurnVersion.Text == "Firmware Default")
                    {
                        Application.DoEvents();
                        Thread.Sleep(1000);
                        MemoryStream Temp = new MemoryStream();
                        Temp.Write(Resource1.Nano, 0, Resource1.Nano.Length);
                        Temp.Position = 0;
                        YmodemUploadFile_DefaultFile(Temp); Thread.Sleep(100); Application.DoEvents();
                        //string SmaPath = path + "\\Firmware\\V2_FW\\SmartAir_STM32.bin";
                        //YmodemUploadFile(SmaPath);
                    }
                    else if (BurnVersion.Text == "Manual selection firmware")
                    {
                        var folderBrowserDialog1 = new OpenFileDialog();
                        DialogResult result = folderBrowserDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string filename = new DirectoryInfo(folderBrowserDialog1.FileName).Name;
                            FlashTextBox.Text += "\r\n\r\nVersion " + filename + " is selected \r\n"; Application.DoEvents();
                            LocalPathFM = folderBrowserDialog1.FileName;
                            Application.DoEvents();
                            Thread.Sleep(1000);
                            //FlashTextBox.ScrollBars = ScrollBars.Both;
                            YmodemUploadFile(LocalPathFM); Thread.Sleep(100); Application.DoEvents();
                        }
                        else
                        {
                            MessageBox.Show("Please try again");
                        }
                    }
                    Again:
                    Application.DoEvents();
                    Thread.Sleep(2500);
                    Application.DoEvents();
                    if (FullTextSmartAir5units[0].Contains("SmartAir FW programming completed Successfully."))
                    {
                        Thread.Sleep(1500);Application.DoEvents();
                        WriteToSingleSmartAir("end", 0);
                        Thread.Sleep(500); Application.DoEvents();
                        Stopwatch resetStopWatch = new Stopwatch();
                        resetStopWatch.Start();
                        TimeSpan ts = resetStopWatch.Elapsed;
                        FullTextSmartAir5units[0] = "";
                        while ((ts.TotalSeconds < 40) && (!EndCondition))
                        {
                            ts = resetStopWatch.Elapsed;
                            Application.DoEvents();
                            if (FullTextSmartAir5units[0].Contains(".: Finished successfully."))
                            {
                                EndCondition = true;
                            }
                            Thread.Sleep(250);
                            if ((ts.TotalSeconds > 10) && (FullTextSmartAir5units[0] == ""))
                            {
                                WriteToSingleSmartAir("end", 0);
                                Thread.Sleep(500); Application.DoEvents();
                            }

                        }
                    }
                    else if(again)
                    {
                        again = false;
                        goto Again;
                    }
                    again = true;
                    if (EndCondition)
                    {
                        WriteToSingleSmartAir("trg 1", 0);
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                        SMAbutton.Enabled = true;
                    }
                    button9.Enabled = true;    
                    break;

                case "V2":
                    if (BurnVersion.Text == "Firmware Default")
                    {
                        Thread.Sleep(1000);
                        Application.DoEvents();
                        MemoryStream Temp = new MemoryStream();
                        Temp.Write(Resource1.SmartAir_STM32, 0, Resource1.SmartAir_STM32.Length);
                        Temp.Position = 0;
                        //YmodemUploadFile_DefaultFile(Temp);
                        string SmaPath = path + "\\Firmware\\V2_FW\\SmartAir_STM32.bin";
                        YmodemUploadFile(SmaPath);
                        Thread.Sleep(500);
                        Application.DoEvents();
                    }
                    else if (BurnVersion.Text == "Manual selection firmware")
                    {
                        var folderBrowserDialog1 = new OpenFileDialog();
                        DialogResult result = folderBrowserDialog1.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            LocalPathFM = folderBrowserDialog1.FileName;
                            Application.DoEvents();
                            Thread.Sleep(1000);
                            YmodemUploadFile(LocalPathFM);
                        }
                        else
                        {
                            MessageBox.Show("Please try again");
                        }
                    }
                    Thread.Sleep(500);
                    Application.DoEvents();
                    Thread.Sleep(500);
                    Application.DoEvents();
                    if (FullTextSmartAir5units[0].Contains("SmartAir FW programming completed Successfully."))
                    {
                        FlashSCUbutton.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                        SendToSMAChars("a");
                        Thread.Sleep(500);Application.DoEvents();
                        SMAbutton.Enabled = true;
                    }
                    break;
            }
            Thread.Sleep(500);
            Application.DoEvents();
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
            int TryCount = 0;
            FullTextSmartAir5units[0] = "";
        ManReboot:
            button9.Enabled = false;
            Application.DoEvents();
            if (BurnSystem.Text == "Nano")
            {
                WriteToSingleSmartAir("ee?", 0);
                Thread.Sleep(1200);
                Application.DoEvents();
                if (FullTextSmartAir5units[0].Contains("!System.....................:"))
                {
                    FullTextSmartAir5units[0] = "";
                    Thread.Sleep(1500);
                    WriteToSingleSmartAir("trg 2", 0);
                    Thread.Sleep(1500);
                    WriteToSingleSmartAir("fwu", 0);
                    Thread.Sleep(300);
                    Stopwatch resetStopWatch = new Stopwatch();
                    resetStopWatch.Start();
                    TimeSpan ts = resetStopWatch.Elapsed;
                    while ((!FullTextSmartAir5units[0].Contains("Bootloader ver")) && (TryCount < 3))
                    {
                        Application.DoEvents();
                        ts = resetStopWatch.Elapsed;
                        if (ts.TotalSeconds > 10)
                        {
                            TryCount++;
                            WriteToSingleSmartAir("trg 2", 0);
                            Thread.Sleep(4000);
                            WriteToSingleSmartAir("fwu", 0);
                            Thread.Sleep(300);
                            resetStopWatch.Restart();
                        }
                        Thread.Sleep(250);
                    }
                }
                else
                {
                    WriteToSingleSmartAir("rst", 0);
                    TryCount = 4;
                    Application.DoEvents();
                }
                /*if ((TryCount < 3)/* && FullTextSmartAir5units[0].Contains("Bootloader ver"))
                    WriteToSingleSmartAir("rst", 0);*/
                Thread.Sleep(500);Application.DoEvents();
                if (FullTextSmartAir5units[0].Contains("Bootloader ver")) 
                {
                    SMAbutton.Enabled = true;
                    //pictureBox1.ErrorImage =  ;
                }
                else
                {
                    MessageBox.Show("Please try again");
                    button9.Enabled = true;
                }
            }
            else if (BurnSystem.Text == "V2")
            {
                bool v2Again = true;
                Thread.Sleep(1000);
                Application.DoEvents();
                AGAIN:
                Thread.Sleep(500);
                Application.DoEvents();
                if (FullTextSmartAir5units[0].Contains("Boot loader ver") || FullTextSmartAir5units[0].Contains("Enter command"))
                {
                    SMAbutton.Enabled = true;
                    
                }
                else if (v2Again)
                {
                    SendToSMAChars("?");
                    v2Again = false;
                    goto AGAIN;
                }
                else
                {
                    MessageBox.Show("Manual reset required");
                    MessageBox.Show("After manual reset, click OK to continue");
                    Thread.Sleep(5500);
                    Application.DoEvents();
                    goto ManReboot;
                }
            }
        }
        private void BurnSystem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        int PacketNumber = 0;
        int packetNumber = 0;
        int NumTotPackage = 0;
        public bool YmodemUploadFile(string path)//string path
        {
            //Application.DoEvents();
            /* control signals */
            const byte STX = 2;  // Start of TeXt 
            const byte EOT = 4;  // End Of Transmission
            const byte ACK = 6;  // Positive ACknowledgement
            const byte C = 67;   // capital letter C

            /* sizes */
            const int dataSize = 1024;
            const int crcSize = 2;

            /* THE PACKET: 1029 bytes */
            /* header: 3 bytes */
            // STX
            /*int PacketNumber = 0;
            int packetNumber = 0;*/
            int invertedPacketNumber = 255;
            /* data: 1024 bytes */
            byte[] data = new byte[dataSize];
            /* footer: 2 bytes */
            byte[] CRC = new byte[crcSize];
            /* get the file */
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            double numTotPackage = (fileStream.Length / (dataSize - 1));
            var NumTotPackageVAR = Math.Floor(numTotPackage);
            NumTotPackage = Convert.ToInt32(NumTotPackageVAR);
            try
            {
                //Application.DoEvents();
                /* send the initial packet with filename and filesize */
                if (serialPort1.ReadByte() != C)
                {
                    Console.WriteLine("Can't begin the transfer.");
                    //Application.DoEvents();
                    return false;
                }

                sendYmodemInitialPacket(STX, packetNumber, invertedPacketNumber, data, dataSize, path, fileStream, CRC, crcSize);
                if (serialPort1.ReadByte() != ACK)
                 {
                    Console.WriteLine("Can't send the initial packet.");
                    return false;
                }

                if (serialPort1.ReadByte() != C)
                    return false;
                //Application.DoEvents();
                /* send packets with a cycle until we send the last byte */
                bool ACKstatus = false;
                int fileReadCount;
                int Cerr = 0;
                do
                {
                    ACKstatus = false;
                    /* if this is the last packet fill the remaining bytes with 0 */
                    fileReadCount = fileStream.Read(data, 0, dataSize);
                    if (fileReadCount == 0) break;
                    if (fileReadCount != dataSize)
                        for (int i = fileReadCount; i < dataSize; i++)
                            data[i] = 0;

                    /* calculate packetNumber */
                    packetNumber++;
                    PacketNumber++;
                    if (packetNumber > 255)
                    {
                        packetNumber -= 256;
                    }
                    
                    /*ReadSP = false;
                    
                    ReadSP = true;*/

                    Console.WriteLine(packetNumber);

                    /* calculate invertedPacketNumber */
                    invertedPacketNumber = 255 - packetNumber;

                    /* calculate CRC */
                    Crc16Ccitt crc16Ccitt = new Crc16Ccitt(InitialCrcValue.Zeros);
                    CRC = crc16Ccitt.ComputeChecksumBytes(data);

                    /* send the packet */

                    sendYmodemPacket(STX, packetNumber, invertedPacketNumber, data, dataSize, CRC, crcSize);
                    Thread.Sleep(40);
                    Stopwatch resetStopWatch1 = new Stopwatch();
                    resetStopWatch1.Start();
                    TimeSpan ts1 = resetStopWatch1.Elapsed;
                    /* wait for ACK */
                    while ((ts1.TotalSeconds <= 25) && !ACKstatus)
                    {
                        ts1 = resetStopWatch1.Elapsed;
                        if (serialPort1.ReadByte() == ACK)
                        {
                            ACKstatus = true;
                            /*ShowFlashData("\r\npackage number " + PacketNumber + "/" + NumTotPackage + " was sent ");
                            Application.DoEvents();*/
                        }
                        else if (ts1.Seconds > 22)
                        {
                            Cerr++;
                            ts1 = resetStopWatch1.Elapsed;
                        }
                        if (Cerr == 3)
                        {
                            //PacketNumber = PacketNumber + packetNumber;
                            return false;
                        }
                            
                    }
                    //Application.DoEvents();Thread.Sleep(2000);FlashTextBox.ScrollToCaret();
                } while (dataSize == fileReadCount);
                //Application.DoEvents();
                /* send EOT (tell the downloader we are finished) */
                Thread.Sleep(500);
                serialPort1.Write(new byte[] { EOT }, 0, 1);
                /* send closing packet */
                //PacketNumber = PacketNumber+packetNumber;
                packetNumber = 0;
                invertedPacketNumber = 255;
                data = new byte[dataSize];
                CRC = new byte[crcSize];
                Thread.Sleep(500);
                sendYmodemClosingPacket(STX, packetNumber, invertedPacketNumber, data, dataSize, CRC, crcSize);
                /* get ACK (downloader acknowledge the EOT) */
                if (serialPort1.ReadByte() != ACK)
                {
                    Console.WriteLine("Can't complete the transfer.");
                    return false;
                }
            }
            catch (TimeoutException)
            {
                throw new Exception("Eductor does not answering");
            }
            finally
            {
                Thread.Sleep(500); Application.DoEvents();
                //FlashTextBox.Text += "\n\r";
                FlashTextBox.Text += "\r\n" + PacketNumber.ToString() + "/" + NumTotPackage.ToString() + " packets sent\r\n";
                Application.DoEvents();
                Thread.Sleep(1000);
                PacketNumber = 0;
                Application.DoEvents();
                fileStream.Close();
            }

            Console.WriteLine("File transfer is succesful");
            return true;
        }

        private void ShowFlashData(string text)
        {
            //FlashTextBox.Text += "\r\npackage number " + PacketNumber + "/" + NumTotPackage + " was sent ";
            FlashTextBox.Text += text;
            FlashTextBox.Select(FlashTextBox.TextLength, 0);
            FlashTextBox.ScrollToCaret();
        }

        private void sendYmodemInitialPacket(byte STX, int packetNumber, int invertedPacketNumber, byte[] data, int dataSize, string path, FileStream fileStream, byte[] CRC, int crcSize)
        {
            //Application.DoEvents();
            string fileName = System.IO.Path.GetFileName(path);
            string fileSize = fileStream.Length.ToString();

            /* add filename to data */
            int i;
            for (i = 0; i < fileName.Length && (fileName.ToCharArray()[i] != 0); i++)
            {
                data[i] = (byte)fileName.ToCharArray()[i];
            }
            data[i] = 0;
            //Application.DoEvents();
            /* add filesize to data */
            int j;
            for (j = 0; j < fileSize.Length && (fileSize.ToCharArray()[j] != 0); j++)
            {
                data[(i + 1) + j] = (byte)fileSize.ToCharArray()[j];
            }
            data[(i + 1) + j] = 0;

            /* fill the remaining data bytes with 0 */
            for (int k = ((i + 1) + j) + 1; k < dataSize; k++)
            {
                data[k] = 0;
            }
            //Application.DoEvents();
            /* calculate CRC */
            Crc16Ccitt crc16Ccitt = new Crc16Ccitt(InitialCrcValue.Zeros);
            CRC = crc16Ccitt.ComputeChecksumBytes(data);
            //Application.DoEvents();
            /* send the packet */
            sendYmodemPacket(STX, packetNumber, invertedPacketNumber, data, dataSize, CRC, crcSize);
        }
        public bool YmodemUploadFile_DefaultFile(MemoryStream fileStream)//string path
        {
            //Application.DoEvents();
            /* control signals */
            const byte STX = 2;  // Start of TeXt 
            const byte EOT = 4;  // End Of Transmission
            const byte ACK = 6;  // Positive ACknowledgement
            const byte C = 67;   // capital letter C

            /* sizes */
            const int dataSize = 1024;
            const int crcSize = 2;

            /* THE PACKET: 1029 bytes */
            /* header: 3 bytes */
            // STX
            int PacketNumber = 0;
            int packetNumber = 0;
            int invertedPacketNumber = 255;
            /* data: 1024 bytes */
            byte[] data = new byte[dataSize];
            /* footer: 2 bytes */
            byte[] CRC = new byte[crcSize];

            /* get the file */
            //FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            //MemoryStream fileStream2 = new MemoryStream();
            //Application.DoEvents();
            try
            {
                
                /* send the initial packet with filename and filesize */
                if (serialPort1.ReadByte() != C)
                {
                    Console.WriteLine("Can't begin the transfer.");
                    //Application.DoEvents();
                    return false;
                }
                //Application.DoEvents();
                sendYmodemInitialPacket_DefaultFile(STX, packetNumber, invertedPacketNumber, data, dataSize, path, fileStream, CRC, crcSize); //Application.DoEvents();
                if (serialPort1.ReadByte() != ACK)
                {
                    Console.WriteLine("Can't send the initial packet.");
                    return false;
                }

                if (serialPort1.ReadByte() != C)
                    return false;
                //Application.DoEvents();
                /* send packets with a cycle until we send the last byte */
                bool ACKstatus = false;
                int fileReadCount;
                int Cerr = 0;
                do
                {
                    ACKstatus = false;
                    //Application.DoEvents();
                    /* if this is the last packet fill the remaining bytes with 0 */
                    fileReadCount = fileStream.Read(data, 0, dataSize);
                    if (fileReadCount == 0) break;
                    if (fileReadCount != dataSize)
                        for (int i = fileReadCount; i < dataSize; i++)
                            data[i] = 0;

                    /* calculate packetNumber */
                    packetNumber++;
                    //FlashTextBox.Text += packetNumber.ToString()+ "\n" ; Application.DoEvents();
                    if (packetNumber > 255)
                        packetNumber -= 256;
                    Console.WriteLine(packetNumber);

                    /* calculate invertedPacketNumber */
                    invertedPacketNumber = 255 - packetNumber;

                    /* calculate CRC */
                    Crc16Ccitt crc16Ccitt = new Crc16Ccitt(InitialCrcValue.Zeros);
                    CRC = crc16Ccitt.ComputeChecksumBytes(data);

                    /* send the packet */
                    sendYmodemPacket(STX, packetNumber, invertedPacketNumber, data, dataSize, CRC, crcSize);Thread.Sleep(20);
                    //Application.DoEvents();
                    /* wait for ACK */
                    Stopwatch resetStopWatch1 = new Stopwatch();
                    resetStopWatch1.Start();
                    TimeSpan ts1 = resetStopWatch1.Elapsed;
                    
                    while ((ts1.TotalSeconds <= 25)&&!ACKstatus)
                    {
                        ts1 = resetStopWatch1.Elapsed;
                        if (serialPort1.ReadByte() == ACK)
                        {
                            ACKstatus = true;
                        }
                        else if (ts1.Seconds > 23)
                        {
                            Cerr++;
                            ts1 = resetStopWatch1.Elapsed;
                        }
                        if (Cerr == 3)
                            return false;
                    }
                } while (dataSize == fileReadCount);
                /* send EOT (tell the downloader we are finished) */
                Thread.Sleep(500);
                serialPort1.Write(new byte[] { EOT }, 0, 1);
                /* send closing packet */
                PacketNumber = packetNumber;
                packetNumber = 0;
                invertedPacketNumber = 255;
                data = new byte[dataSize];
                CRC = new byte[crcSize];
                Thread.Sleep(500);
                sendYmodemClosingPacket(STX, packetNumber, invertedPacketNumber, data, dataSize, CRC, crcSize);
                /* get ACK (downloader acknowledge the EOT) */
                if (serialPort1.ReadByte() != ACK)
                {
                    Console.WriteLine("Can't complete the transfer.");
                    return false;
                }
            }
            catch (TimeoutException)
            {
                throw new Exception("Eductor does not answering");
            }
            finally
            {
                Thread.Sleep(500);
                Application.DoEvents();
                double x = Math.Floor(Convert.ToDouble(fileStream.Length) / 1023)+1;
                FlashTextBox.Text += "\n\r";
                FlashTextBox.Text += "\r\n" + PacketNumber.ToString() + "/" + x.ToString() + " packets sent\n\r";
                Thread.Sleep(3000);
                Application.DoEvents();
                fileStream.Close();
            }
            Console.WriteLine("File transfer is succesful");
            return true;
        }
        private void sendYmodemInitialPacket_DefaultFile(byte STX, int packetNumber, int invertedPacketNumber, byte[] data, int dataSize, string path, MemoryStream fileStream, byte[] CRC, int crcSize)
        {
            //Application.DoEvents();
            string fileName = System.IO.Path.GetFileName(path);
            string fileSize = fileStream.Length.ToString();

            /* add filename to data */
            int i;
            for (i = 0; i < fileName.Length && (fileName.ToCharArray()[i] != 0); i++)
            {
                data[i] = (byte)fileName.ToCharArray()[i];
            }
            data[i] = 0;
            //Application.DoEvents();
            /* add filesize to data */
            int j;
            for (j = 0; j < fileSize.Length && (fileSize.ToCharArray()[j] != 0); j++)
            {
                data[(i + 1) + j] = (byte)fileSize.ToCharArray()[j];
            }
            data[(i + 1) + j] = 0;

            /* fill the remaining data bytes with 0 */
            for (int k = ((i + 1) + j) + 1; k < dataSize; k++)
            {
                data[k] = 0;
            }
            //Application.DoEvents();
            /* calculate CRC */
            Crc16Ccitt crc16Ccitt = new Crc16Ccitt(InitialCrcValue.Zeros);
            CRC = crc16Ccitt.ComputeChecksumBytes(data);
            //Application.DoEvents();
            /* send the packet */
            sendYmodemPacket(STX, packetNumber, invertedPacketNumber, data, dataSize, CRC, crcSize);
        }
        private void sendYmodemClosingPacket(byte STX, int packetNumber, int invertedPacketNumber, byte[] data, int dataSize, byte[] CRC, int crcSize)
        {
            /* calculate CRC */
            Crc16Ccitt crc16Ccitt = new Crc16Ccitt(InitialCrcValue.Zeros);
            CRC = crc16Ccitt.ComputeChecksumBytes(data);

            /* send the packet */
            sendYmodemPacket(STX, packetNumber, invertedPacketNumber, data, dataSize, CRC, crcSize);
        }
        private void sendYmodemPacket(byte STX, int packetNumber, int invertedPacketNumber, byte[] data, int dataSize, byte[] CRC, int crcSize)
        {
            //Application.DoEvents();
            serialPort1.Write(new byte[] { STX }, 0, 1);
            serialPort1.Write(new byte[] { (byte)packetNumber }, 0, 1);
            serialPort1.Write(new byte[] { (byte)invertedPacketNumber }, 0, 1);
            serialPort1.Write(data, 0, dataSize);
            serialPort1.Write(CRC, 0, crcSize);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void progressBar7_Click(object sender, EventArgs e)
        {

        }
        private void FlashSCUbutton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Application.DoEvents();
            EndCondition = false;
            string LocalPathFM = "";
            FlashSCUbutton.Enabled = false;
            FullTextSmartAir5units[0] = "";
            SendToSMAChars("SCU");
            //WriteToSingleSmartAir("sma", 0);
            Thread.Sleep(3000);
            Application.DoEvents();
            //Thread.Sleep(1000);
            switch (BurnVersion.Text)
            {
                case "Firmware Default":
                    Thread.Sleep(1000);
                    Application.DoEvents();
                    /*MemoryStream Temp = new MemoryStream();
                    Temp.Write(Resource1.ACU, 0, Resource1.ACU.Length);
                    Temp.Position = 0;*/
                    string SmaPath = path + "\\Firmware\\V2_FW\\ACU.bin";
                    YmodemUploadFile(SmaPath);
                    Thread.Sleep(500);
                    Application.DoEvents();
                    break;
                case "Manual selection firmware":
                    var folderBrowserDialog1 = new OpenFileDialog();
                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        LocalPathFM = folderBrowserDialog1.FileName;
                        Application.DoEvents();
                        Thread.Sleep(1000);
                        YmodemUploadFile(LocalPathFM);
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                    break;
            }
            Stopwatch resetStopWatch1 = new Stopwatch();
            resetStopWatch1.Start();
            TimeSpan ts1 = resetStopWatch1.Elapsed;
            FullTextSmartAir5units[0] = "";
            while ((ts1.Minutes<2)&&!EndCondition)
            {
                Thread.Sleep(50);
                Application.DoEvents();
                if (FullTextSmartAir5units[0].Contains("SenseAir Modem ACU sertificate loaded"))
                    EndCondition = true;
                else if (FullTextSmartAir5units[0].Contains("CCC"))
                    break;
                ts1 = resetStopWatch1.Elapsed;
            }
            if (EndCondition)
                CCUFlashbutton.Enabled = true;
            else
            {
                FlashSCUbutton.Enabled = true;
                MessageBox.Show("Please try again");
                SendToSMAChars("a");
            }
            Thread.Sleep(500);
            Application.DoEvents();

        }
        private void SendToSMAChars(string StringToSend)
        {
            serialPort1.Write(new byte[] { (byte)0x0A }, 0, 1);
            foreach (char C in StringToSend)
            {
                byte hex = Convert.ToByte(C);
                serialPort1.Write(new byte[] { hex }, 0, 1);
            }
            serialPort1.Write(new byte[] { (byte)0x0D }, 0, 1);
        }
        private void CCUFlashbutton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Application.DoEvents();
            EndCondition = false;
            string LocalPathFM = "";
            //FlashSCUbutton.Enabled = false;
            CCUFlashbutton.Enabled = false;
            FullTextSmartAir5units[0] = "";
            SendToSMAChars("CCU");
            Thread.Sleep(3000);
            Application.DoEvents();
            switch (BurnVersion.Text)
            {
                case "Firmware Default":
                    Thread.Sleep(1000);
                    Application.DoEvents();
                    /*MemoryStream Temp = new MemoryStream();
                    Temp.Write(Resource1.CCU, 0, Resource1.CCU.Length);
                    Temp.Position = 0;
                    YmodemUploadFile_DefaultFile(Temp);
                    */
                    string SmaPath = path + "\\Firmware\\V2_FW\\CCU.bin";
                    YmodemUploadFile(SmaPath);
                    Thread.Sleep(500);
                    Application.DoEvents();
                    break;
                case "Manual selection firmware":
                    var folderBrowserDialog1 = new OpenFileDialog();
                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        LocalPathFM = folderBrowserDialog1.FileName;
                        Application.DoEvents();
                        Thread.Sleep(1000);
                        YmodemUploadFile(LocalPathFM);
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                    break;
            }
            Stopwatch resetStopWatch1 = new Stopwatch();
            resetStopWatch1.Start();
            TimeSpan ts1 = resetStopWatch1.Elapsed;
            FullTextSmartAir5units[0] = "";
            while ((ts1.Minutes < 2) && !EndCondition)
            {
                Thread.Sleep(50);
                Application.DoEvents();
                if (FullTextSmartAir5units[0].Contains("SenseAir Modem CCU sertificate loaded"))
                    EndCondition = true;
                else if (FullTextSmartAir5units[0].Contains("CCC"))
                    break;
                ts1 = resetStopWatch1.Elapsed;
            }
            if (EndCondition)
                SenFlashbutton.Enabled = true;
            else
            {
                CCUFlashbutton.Enabled = true;
                MessageBox.Show("Please try again\nManual reset required");
                MessageBox.Show("After manual reset, click OK to continue");
                Thread.Sleep(5500);
                Application.DoEvents();
                //SendToSMAChars("a");
            }
            Thread.Sleep(500);
            Application.DoEvents();

        }
        private void SenFlashbutton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Application.DoEvents();
            EndCondition = false;
            string LocalPathFM = "";
            SenFlashbutton.Enabled = false;
            FullTextSmartAir5units[0] = "";
            SendToSMAChars("SEN");
            Thread.Sleep(3000);
            Application.DoEvents();
            switch (BurnVersion.Text)
            {
                case "Firmware Default":
                    Thread.Sleep(1000);
                    Application.DoEvents();
                    /*MemoryStream Temp = new MemoryStream();
                    Temp.Write(Resource1.SenseAirNew, 0, Resource1.SenseAirNew.Length);
                    Temp.Position = 0;
                    YmodemUploadFile_DefaultFile(Temp);*/

                    string SmaPath = path + "\\Firmware\\V2_FW\\SenseAirNew.bin";
                    YmodemUploadFile(SmaPath);
                    Thread.Sleep(500);
                    Application.DoEvents();
                    break;
                case "Manual selection firmware":
                    var folderBrowserDialog1 = new OpenFileDialog();
                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        LocalPathFM = folderBrowserDialog1.FileName;
                        Application.DoEvents();
                        Thread.Sleep(1000);
                        YmodemUploadFile(LocalPathFM);
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                    break;
            }
            Stopwatch resetStopWatch1 = new Stopwatch();
            resetStopWatch1.Start();
            TimeSpan ts1 = resetStopWatch1.Elapsed;
            int SMAstrLength = 0, TH = 0;
            FullTextSmartAir5units[0] = "";
            while ((ts1.Minutes < 3.5) && !EndCondition)
            {
                ts1 = resetStopWatch1.Elapsed;
                Thread.Sleep(50);
                Application.DoEvents();
                if (FullTextSmartAir5units[0].Contains("SenseAir SW 2.34 Modem SW 02.27.01 Started"))
                    EndCondition = true;
                if (Convert.ToInt32(ts1.TotalSeconds)%5==0)
                {
                    if ((SMAstrLength == FullTextSmartAir5units[0].Length)||(FullTextSmartAir5units[0].Contains("C")))
                    {
                        TH = TH + Convert.ToInt32(ts1.TotalSeconds);
                    }
                    else
                    {
                        SMAstrLength = FullTextSmartAir5units[0].Length;
                        TH = 0;
                    }
                    if (TH > 40)
                        break;
                }
            }
            if (EndCondition)
                EndButton.Enabled = true;
            else
            {
                SenFlashbutton.Enabled = true;
                MessageBox.Show("Please try again\nManual reset required");
                MessageBox.Show("After manual reset, click OK to continue");
                Thread.Sleep(5500);
                Application.DoEvents();
            }
            Thread.Sleep(500);
            Application.DoEvents();

        }
        private void EndButton_Click(object sender, EventArgs e)
        {
            int count = 0;
            bool TimeOut = false, EndCondition = false;
            Application.DoEvents();
            FullTextSmartAir5units[0] = "";
            SendToSMAChars("end");
            Stopwatch resetStopWatch1 = new Stopwatch();
            resetStopWatch1.Start();
            TimeSpan ts1 = resetStopWatch1.Elapsed;
            while ((!EndCondition)&&(!TimeOut))
            {
                Application.DoEvents();
                Thread.Sleep(500);
                ts1 = resetStopWatch1.Elapsed;
                if (FullTextSmartAir5units[0].Contains("GO."))
                {
                    EndCondition = true;
                    FullTextSmartAir5units[0] = "";
                    MessageBox.Show("Manual reset required");
                }
                else if (count<3)
                {
                    SendToSMAChars("end");
                    count++;
                    Application.DoEvents();
                }
                else if (ts1.Seconds >= 30)
                    TimeOut = true;
            }
            if (EndCondition)
            {
                if(FullTextSmartAir5units[0].Contains("!Application................: Start"))
                    MessageBox.Show("The firmware was successfully burned\nGo to the \"Manual Test panel\" for further testing");
                EndButton.Enabled = false;
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show(path);
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void RefreshStatusParams_Click(object sender, EventArgs e)
        {
            updateParamsStatus();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void DISETrackBar_Scroll(object sender, EventArgs e)
        {
            if (DISETrackBar.Value == 1)
                DISEValue.Text = "Disarm from low height";
            if (DISETrackBar.Value == 2)
                DISEValue.Text = "Disarm from no vibrations";
            if (DISETrackBar.Value == 3)
                DISEValue.Text = "Disarm from no vibrations\nand no height change";
        }

        private void ApplyChangesButton_Click(object sender, EventArgs e)
        {
                       
        }

        private void CheckBoxEnableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ValueParamMarked = 1, ValueParamUnmarked = 0;
            string param="";
            if (CheckBoxEnableList.Text == "Raw data to log")
            {
                ValueParamMarked = 1;
                ValueParamUnmarked = 0;
                param = "RAW";
            }
            if (CheckBoxEnableList.Text == "External battery")
            {
                ValueParamMarked = 1;
                ValueParamUnmarked = 0;
                param = "XBT";
            }
            if (CheckBoxEnableList.Text == "PWM signal")
            {
                ValueParamMarked = 1;
                ValueParamUnmarked = 0;
                param = "PWM";
            }
            if (CheckBoxEnableList.Text == "Use SD Card")
            {
                ValueParamMarked = 1;
                ValueParamUnmarked = 0;
                param = "USD";
            }
            if (CheckBoxEnableList.Text == "Auto Arm")
            {
                ValueParamMarked = 2;
                int AutoDisIndex = CheckBoxEnableList.Items.IndexOf(CheckBoxEnableList.Text)+1;
                if (CheckBoxEnableList.GetItemCheckState(AutoDisIndex) == CheckState.Checked)
                    ValueParamUnmarked = 1;
                else
                    ValueParamUnmarked = 0;
                if (CheckBoxEnableList.GetItemCheckState(AutoDisIndex-1) == CheckState.Checked)
                    CheckBoxEnableList.SetItemCheckState(AutoDisIndex, CheckState.Checked);
                param = "ARM";
            }
            if (CheckBoxEnableList.Text == "Auto Disarm")
            {
                ValueParamMarked = 1;
                int AutoArmIndex = CheckBoxEnableList.Items.IndexOf(CheckBoxEnableList.Text) - 1;
                if ((CheckBoxEnableList.GetItemCheckState(AutoArmIndex+1) == CheckState.Unchecked))
                    CheckBoxEnableList.SetItemCheckState(AutoArmIndex, CheckState.Unchecked);
                ValueParamUnmarked = 0;
                param = "ARM";
            }
            if (CheckBoxEnableList.Text == "Auto Trigger")
            {
                ValueParamMarked = 1;
                int MainenanceIndex = CheckBoxEnableList.Items.IndexOf(CheckBoxEnableList.Text)+1;
                if ((CheckBoxEnableList.GetItemCheckState(MainenanceIndex-1) == CheckState.Checked))
                    CheckBoxEnableList.SetItemCheckState(MainenanceIndex, CheckState.Unchecked);
                ValueParamUnmarked = 0;
                param = "TRG";
            }
            if (CheckBoxEnableList.Text == "Maintenance mode")
            {
                ValueParamMarked = 2;
                int TriggerIndex = CheckBoxEnableList.Items.IndexOf(CheckBoxEnableList.Text) -1;
                if ((CheckBoxEnableList.GetItemCheckState(TriggerIndex+1) == CheckState.Checked))
                    CheckBoxEnableList.SetItemCheckState(TriggerIndex, CheckState.Unchecked);
                else
                    CheckBoxEnableList.SetItemCheckState(TriggerIndex, CheckState.Checked);
                ValueParamUnmarked = 1;
                param = "TRG";
            }
            if (CheckBoxEnableList.Text == "Use Servo")
            {
                int UseServoIndex = CheckBoxEnableList.Items.IndexOf(CheckBoxEnableList.Text);
                int UsePyroIndex = UseServoIndex + 1;
                if ((CheckBoxEnableList.GetItemCheckState(UsePyroIndex) == CheckState.Checked))
                {
                    ValueParamMarked = 3;
                    ValueParamUnmarked = 2;
                }
                else
                {
                    ValueParamMarked = 1;
                    ValueParamUnmarked = 0;
                }
                param = "SVPY";
            }
            if (CheckBoxEnableList.Text == "Use Pyro")
            {
                int UsePyroIndex = CheckBoxEnableList.Items.IndexOf(CheckBoxEnableList.Text);
                int UseServoIndex = UsePyroIndex - 1;
                if ((CheckBoxEnableList.GetItemCheckState(UseServoIndex) == CheckState.Checked))
                {
                    ValueParamMarked = 3;
                    ValueParamUnmarked = 1;
                }
                else
                {
                    ValueParamMarked = 2;
                    ValueParamUnmarked = 0;
                }
                    

                param = "SVPY";
            }


            int IndexParam = CheckBoxEnableList.Items.IndexOf(CheckBoxEnableList.Text);
            if (CheckBoxEnableList.GetItemCheckState(IndexParam) == CheckState.Checked)
            {
                WriteToSingleSmartAir(param + " " + ValueParamMarked, 0);
            }
            else
            {
                WriteToSingleSmartAir(param + " " + ValueParamUnmarked, 0);
                
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (DISETrackBar.Value == 1)
                WriteToSingleSmartAir("DISE 1",0);
            if (DISETrackBar.Value == 2)
                WriteToSingleSmartAir("DISE 2",0);
            if (DISETrackBar.Value == 3)
                WriteToSingleSmartAir("DISE 4",0);
        }

        private void CheckBoxArmModeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IndexHeight = 0;
            int IndexVibration = 1;
            if ((CheckBoxArmModeList.GetItemCheckState(IndexHeight) == CheckState.Checked) &&
                (CheckBoxArmModeList.GetItemCheckState(IndexVibration) == CheckState.Checked))
                WriteToSingleSmartAir("ARME 3", 0);
            else if ((CheckBoxArmModeList.GetItemCheckState(IndexHeight) == CheckState.Unchecked) &&
                (CheckBoxArmModeList.GetItemCheckState(IndexVibration) == CheckState.Checked))
                WriteToSingleSmartAir("ARME 2", 0);
            else if ((CheckBoxArmModeList.GetItemCheckState(IndexHeight) == CheckState.Checked) &&
                    (CheckBoxArmModeList.GetItemCheckState(IndexVibration) == CheckState.Unchecked))
                WriteToSingleSmartAir("ARME 1", 0);
            else
                WriteToSingleSmartAir("ARME 0", 0);
            
        }

        private void ATTP_TextChanged(object sender, EventArgs e)
        {

        }

        private void ATTP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(ATTP.Text);
                    WriteToSingleSmartAir("ATTP " + ATTP.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    ATTP.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    ATTP.Clear();
                }
            }
        }

        private void ATTR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(ATTR.Text);
                    WriteToSingleSmartAir("ATTR " + ATTR.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    ATTR.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    ATTR.Clear();
                }
            }
        }

        private void ATTS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(ATTS.Text);
                    WriteToSingleSmartAir("ATTS " + ATTS.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    ATTS.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    ATTS.Clear();
                }
            }
        }

        private void ATC1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(ATC1.Text);
                    WriteToSingleSmartAir("ATC " + ATC1.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    ATC1.Text = Temp; ATC2.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    ATC1.Clear();
                }
            }
        }

        private void ATC2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(ATC2.Text);
                    WriteToSingleSmartAir("ATC " + ATC2.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    ATC1.Text = Temp; ATC2.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    ATC2.Clear();
                }
            }
        }

        private void ATRL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(ATRL.Text);
                    WriteToSingleSmartAir("ATRL " + ATRL.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    ATRL.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    ATRL.Clear();
                }
            }
        }

        private void ATRR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(ATRR.Text);
                    WriteToSingleSmartAir("ATRR " + ATRR.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    ATRR.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    ATRR.Clear();
                }
            }
        }

        private void ATRP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(ATRP.Text);
                    WriteToSingleSmartAir("ATRP " + ATRP.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    ATRP.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    ATRP.Clear();
                }
            }
        }

        private void FFC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(FFC.Text);
                    WriteToSingleSmartAir("FFC " + FFC.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    FFC.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    FFC.Clear();
                }
            }
        }

        private void FFL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(FFL.Text);
                    value = 9.8 - value;
                    WriteToSingleSmartAir("FFL " + value, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    FFL.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    FFL.Clear();
                }
            }
        }

        private void HTC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(HTC.Text);
                    WriteToSingleSmartAir("HTC " + HTC.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    HTC.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    HTC.Clear();
                }
            }
        }

        private void DHT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(DHT.Text);
                    WriteToSingleSmartAir("DHT " + DHT.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    DHT.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    DHT.Clear();
                }
            }
        }

        private void YRL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(YRL.Text);
                    WriteToSingleSmartAir("YRL " + YRL.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    YRL.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    YRL.Clear();
                }
            }
        }

        private void HGT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double value = 0;
                try
                {
                    value = Convert.ToDouble(HGT.Text);
                    WriteToSingleSmartAir("HGT " + HGT.Text, 0);
                    string[] temp = sender.ToString().Split(':');
                    int tempIndex = temp[1].IndexOf("\r");
                    string Temp = temp[1].Substring(1, tempIndex);
                    HGT.Text = Temp;
                }
                catch
                {
                    MessageBox.Show("Try Again", "The inserted value is incorrect");
                    HGT.Clear();
                }
            }
        }
    }
}

