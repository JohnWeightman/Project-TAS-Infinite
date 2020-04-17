using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{
    class Campaigns
    {
        public string Name = "";
        public bool Complete = false;
        public List<Module> Modules = new List<Module>();
    }

    class Module
    {
        public string Name = "";
        public Options Options = new Options();
        public List<string> Story = new List<string>();
    }

    class Options
    {
        public List<string> OptionsList = new List<string>();
    }
}
