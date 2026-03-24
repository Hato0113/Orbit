using ConsoleAppFramework;
using Orbit;

var app = ConsoleApp.Create();
app.Add<Initialize>();
app.Add<Clean>();
app.Add<Workspace>("ws");
app.Add<TaskAdd>();
app.Add<TaskShow>();
app.Add<TaskDone>();
app.Add<TaskRemove>();
app.Add<TaskRename>();
app.Add<TaskTag>();
app.Add<Description>();
app.Add<Progress>();
app.Add<Usage>();
app.Run(args);
