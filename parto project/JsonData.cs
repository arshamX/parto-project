using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parto_project
{
    public struct Config
    {
        public  string Colors { get; set; }
        public  string ChanelLabel { get; set; }
        public  int NumberOfChanels { get; set; }
        public  int UdpPort { get; set; }
    }

    public struct Data
    {
        public string Name { get; set; }
        public string RADIO_TYPE { get; set; }
        public string GST_AUDIO_DEVICE { get; set; }
        public string RCC_SERVER_PORT { get; set; }
        public string OVERRIDE_PTT_ON_SAME_PRIORITY { get; set; }
        public string  MUTE_DURATION_SECONDS { get; set; }
        public string  MUTE_LEVEL { get; set; }
        public string  PTT_TIMEOUT_SECONDS { get; set; }
        public string  IS_VAD_ACTIVE { get; set; }
        public string DEBUG_SERIAL_PORT { get; set; }
    }

}
