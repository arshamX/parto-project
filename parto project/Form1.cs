using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace parto_project
{
    public partial class Form1 : Form
    {
        #region variables
        Config config;
        UdpClient udp;
        Thread receiveT;
        bool threadFlag;
        private ConcurrentQueue<Data[]> received = new ConcurrentQueue<Data[]>();
        #endregion
        #region initform
        public Form1()
        {
            getConfig();
            InitializeComponent();
            lblChanel.Text = config.ChanelLabel;
            Initialiver(config.NumberOfChanels,config.Colors.Split(','));
            udpInitializer();

        }
        #endregion
        #region methods
        private void getConfig()
        {
            string jsonString = File.ReadAllText("Config.json");
            config = JsonConvert.DeserializeObject<Config>(jsonString);
        }
        private void udpInitializer()
        {
            udp = new UdpClient(config.UdpPort);
            udp.Connect(IPAddress.Broadcast, config.UdpPort);
        }
        private void receive()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any,config.UdpPort);
            while(threadFlag)
            {
                string data = Encoding.UTF8.GetString(udp.Receive(ref iPEndPoint));
                toJson(ref data);
            }
        }
        private void toJson(ref string input)
        {
            received.Enqueue(JsonConvert.DeserializeObject<Data[]>(input));
        }
        private void threadStarter()
        {
            receiveT = new Thread(receive);
            receiveT.Start();
        }
        #endregion

        private void btnstart_Click(object sender, EventArgs e)
        {
            threadFlag = true;
            threadStarter();
            timer1.Enabled = true;
            timer1.Start();
            btnstart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            threadFlag = false;
            timer1.Enabled = false;
            timer1.Stop();
            btnstart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            receiveT.Abort();
            udp.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Data[] datas;
            bool flag = received.TryDequeue(out datas);
            if(flag)
            {
                for (int i = 0; i < config.NumberOfChanels; i++)
                {
                    this.textBoxes[i].Text = $"{datas[i].Name} \n \"RADIO_TYPE\":\"{datas[i].RADIO_TYPE}\"  \"RCC_SERVER_PORT\":\"{datas[i].RCC_SERVER_PORT}\"\n\"MUTE_DURATION_SECONDS\":\"{datas[i].MUTE_DURATION_SECONDS}\"    \"IS_VAD_ACTIVE\":\"{datas[i].IS_VAD_ACTIVE}\"";
                }
            }
        }
    }
}
