using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTemplate.Classes {
    public class ServerInfo {

        public class Root {
            public bool online { get; set; }
            public string ip { get; set; }
            public int port { get; set; }
            public string hostname { get; set; }
            public Debug debug { get; set; }
            public string version { get; set; }
            public Protocol protocol { get; set; }
            public string icon { get; set; }
            public string software { get; set; }
            public Map map { get; set; }
            public string gamemode { get; set; }
            public string serverid { get; set; }
            public bool eula_blocked { get; set; }
            public Motd motd { get; set; }
            public Player players { get; set; }
            public List<Plugin> plugins { get; set; }
            public List<Mod> mods { get; set; }
            public Info info { get; set; }
        }

        public class Debug {
            public bool ping { get; set; }
            public bool query { get; set; }
            public bool srv { get; set; }
            public bool querymismatch { get; set; }
            public bool ipinsrv { get; set; }
            public bool cnameinsrv { get; set; }
            public bool animatedmotd { get; set; }
            public bool cachehit { get; set; }
            public int cachetime { get; set; }
            public int cacheexpire { get; set; }
            public int apiversion { get; set; }
        }

        public class Protocol {
            public int version { get; set; }
            public string name { get; set; }
        }

        public class Map {
            public string raw { get; set; }
            public string clean { get; set; }
            public string html { get; set; }
        }

        public class Motd {
            public List<string> raw { get; set; }
            public List<string> clean { get; set; }
            public List<string> html { get; set; }
        }

        public class Player {
            public string name { get; set; }
            public string uuid { get; set; }
        }

        public class Plugin {
            public string name { get; set; }
            public string version { get; set; }
        }

        public class Mod {
            public string name { get; set; }
            public string version { get; set; }
        }

        public class Info {
            public List<string> raw { get; set; }
            public List<string> clean { get; set; }
            public List<string> html { get; set; }
        }

    }
}
