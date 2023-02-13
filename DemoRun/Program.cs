using FileSystem;
using RunasLib;
using Plugin;
using System.Threading;
using FsImplementation;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var fs = new FileSystemFactory("/home/garry/runas_demo_dir");


var pluginRegistry = new PluginRegistry(fs);

var demoPlugin = new DemoPlugin();
var metadata = new DemoMetadata();

pluginRegistry.RegisterPlugin(demoPlugin, metadata);

Thread.Sleep(2000);



