using Module_4;

Console.WriteLine("Enter path:");
var path = Console.ReadLine();

if (path == null)
{
    throw new ArgumentNullException(nameof(path));
}
if (!Directory.Exists(path))
{
    throw new DirectoryNotFoundException("Invalid directory path.");
}

Predicate<string> predicate = (filter) => filter.EndsWith("pdf");

FileSystemVisitor fileSystemVisitor;

fileSystemVisitor = new FileSystemVisitor(path, predicate);

fileSystemVisitor.VisitStarted += (sender, e) => { Console.WriteLine("VisitStarted"); };
fileSystemVisitor.VisitFinished += (sender, e) => { Console.WriteLine("VisitFinished"); };
fileSystemVisitor.DirectoryFound += (sender, e) => { Console.WriteLine($"DirectoryFound: {e.Path}"); };
fileSystemVisitor.FileFound += (sender, e) => { Console.WriteLine($"FileFound: {e.Path}"); };
fileSystemVisitor.FilteredDirectoryFound += (sender, e) => { Console.WriteLine($"FilteredFileFound: {e.Path}"); };
fileSystemVisitor.FilteredFileFound += (sender, e) => { Console.WriteLine($"FilteredFileFound: {e.Path}"); };

var resultList = new List<string>();
try
{
    foreach (var item in fileSystemVisitor.VisitFileSystem())
    {
        resultList.Add(item);
    }
}
catch (DirectoryNotFoundException e)
{
    throw new DirectoryNotFoundException("Invalid directory path.", e);
}
catch (Exception)
{
    throw;
}

foreach (var item in resultList)
{
    Console.WriteLine(item);
}

Console.WriteLine("Press any key to continue...");
Console.ReadKey();