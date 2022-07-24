using System;
using System.Collections.Generic;
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

namespace parto_project
{
    public partial class Form1 : Form
    {
        #region variables
        Config config;
        UdpClient udp;
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
            string jsonString = File.ReadAllText("C:\\Users\\arash\\Desktop\\parto project\\Config.json");
            config = JsonConvert.DeserializeObject<Config>(jsonString);
        }
        private void udpInitializer()
        {
            udp = new UdpClient(config.UdpPort);
            udp.Connect(IPAddress.Broadcast, config.UdpPort);
        }
        #endregion
    }
}
