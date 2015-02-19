using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADs.CommandLineArgs
{
    [AttributeUsage(AttributeTargets.Property, Inherited=false,AllowMultiple=false)]
    public class ArgumentAttribute : Attribute
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public object DefaultValue { get; set; }
        public string HelpText { get; set; }

        public ArgumentAttribute(string ShortName, string LongName)
        {
            this.ShortName = ShortName;
            this.LongName = LongName;
        }
    }
}
