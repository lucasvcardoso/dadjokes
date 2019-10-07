using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHazDadJokes.API.Lib
{
    public class DadJokesServiceConfiguration : ConfigurationSection, IDadJokesServiceConfiguration
    {
        [ConfigurationProperty("Limit", IsRequired = true)]
        public int Limit {
            get { return (int)this["Limit"]; }
            set { this["Limit"] = value; }
        }
        [ConfigurationProperty("Url", IsRequired = true)]
        public string Url {
            get{ return (string)this["Url"]; }
            set { this["Url"] = value; }
        }
    }
}
