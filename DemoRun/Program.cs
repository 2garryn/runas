using FileSystem;
using RunasLib;
using Plugin;
using System.Threading;
using FsImplementation;
using DocumentDb;




// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var fs = new FileSystemFactory("/home/garry/runas_demo_dir");
var docDb = new DocumentDbFactory("/home/garry/runas_db");

var pluginRegistry = new PluginRegistry(fs, docDb);

var demoPlugin = new DemoPlugin();
var metadata = new DemoMetadata();

pluginRegistry.RegisterPlugin(demoPlugin, metadata);


Thread.Sleep(2000);



