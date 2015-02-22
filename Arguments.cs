﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CommandLineArgs
{
    public static class Arguments
    {
        public static T Parse<T>(string[] args)
        {
            T _options = Activator.CreateInstance<T>();
            
            Dictionary<string, string> _args = ToDictionary(args);

            PropertyInfo[] properties = typeof(T).GetProperties();
            int totalProperties = properties.Count();

            //parse all available properties
            for (int i = 0; i < totalProperties; i++ )
            {
                PropertyInfo prop = properties[i];

                //get custom attributes for each property
                object[] attributes = prop.GetCustomAttributes(true);
                int totalAttributes = attributes.Count();
                
                //parse through the custom attributes
                for (int j = 0; j < totalAttributes; j++)
                {
                    ArgumentAttribute attr = attributes[j] as ArgumentAttribute;
                    //if attribute is a valid ArgumentAttribute type, assign value
                    if(attr != null)
                    {
                        //if property found on command line arguments
                        if(_args.ContainsKey(attr.ShortName) || _args.ContainsKey(attr.LongName))
                        {
                            //if property is writeable
                            if(prop.CanWrite)
                            {
                                //assign default value if not present
                                object attrValue = _args[attr.ShortName] ?? _args[attr.LongName] ?? attr.DefaultValue;
                                //set value to property
                                prop.SetValue(_options, Convert.ChangeType(attrValue,prop.PropertyType), null);
                            }
                        }
                    }
                }
            }

            return _options;
        }

        public static Dictionary<string,string> ToDictionary(string[] args)
        {
            Dictionary<string, string> _args = new Dictionary<string, string>();

            if (args != null)
            {
                string argKey = String.Empty;

                foreach(string arg in args)
                {
                    //if argument is a key
                    if (arg.StartsWith("-"))
                    {
                        //push key
                        argKey = arg.Replace("-",String.Empty);
                        _args.Add(argKey, String.Empty);
                    }
                    //push value
                    else
                    {
                        _args[argKey] = arg;
                    }
                }
            }

            return _args;
        }
    }
}