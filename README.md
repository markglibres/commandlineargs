# commandlineargs
Command Line Arguments Parser for console applications using C#

Usage: youconsoleapp.exe -f param1Value -action "Parameter 2 value"

1. Create your class with the structure of your parameters. For example:


```
#!c#

using CommandLineArgs;

public class Options
{
	[ArgumentAttribute("f", "filename", HelpText = "Offline page to display.")]
	public string inputFile { get; set; }

	[ArgumentAttribute("i", "iis", HelpText = "IIS version installed on the server", DefaultValue = "7")]
	public string iisVersion { get; set; }

	[ArgumentAttribute("a", "action", HelpText = "Action to execute.")]
	public string action { get; set; }

}

```

2. On your main program, pass your class on the Parser together with the console arguments. For example:


```
#!c#

static void Main(string[] args)
{
	Options options = CommandLineArgs.Arguments.Parse<Options>(args);
}
```


3. You may now access your parameters with the following code:


```
#!c#

Console.WriteLine(options.inputFile);
Console.WriteLine(options.action);
```