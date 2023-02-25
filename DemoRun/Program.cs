
using FsImplementation;
using DocumentDb;
using LiteDB;
using Commands;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var db = new LiteDatabase(@"/home/garry/runas_db");
var attrStorage = new AttrStorage(db);
var fs = new FileSystemFactory("/home/garry/runas_demo_dir", attrStorage);
var docDb = new DocumentDbFactory(db);
var commandsService = new CommandService("http://localhost:8089", "runas");
var pluginRegistry = new RunasLib.PluginRegistry(fs, docDb, commandsService.GetPluginRegistry());
var demoPlugin = new DemoPlugin();
var metadata = new DemoMetadata();




pluginRegistry.RegisterPlugin(demoPlugin, metadata);

pluginRegistry.RegisterPlugin(new PingPlugin(), new PingMetadata());



await commandsService.RunAsync();




