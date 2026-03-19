using ConsoleAppFramework;
using Orbit;

var app = ConsoleApp.Create();
app.Add<Initialize>();
app.Add<Workspace>("ws");
app.Add<TaskCommand>();
app.Add<Description>();
app.Add<Progress>();
app.Run(args);
