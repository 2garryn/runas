using FileSystem;
using RunasLib;
using Plugin;
using System.Threading;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var fs = new FileSystemFactory("/home/garry/runas/runas_demo_dir", null);


var pluginRegistry = new PluginRegistry(fs);

var demoPlugin = new DemoPlugin();
var metadata = new DemoMetadata();

pluginRegistry.RegisterPlugin(demoPlugin, metadata);

Thread.Sleep(2000);



