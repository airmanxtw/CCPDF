using NeoSmart.PrettySize;
namespace Resizer;
public abstract class BaseResizer
{
    protected CMPDF.PDF pdf = new();
    protected byte[] _oriFileBytes;
    protected string _oriFileName;
    protected FileInfo? _outputFile;
    protected BaseResizer(FileInfo oriFile, FileInfo? outputFile)
    {                
        _oriFileBytes = File.ReadAllBytes(oriFile.FullName);
        _oriFileName = oriFile.FullName;
        _outputFile = outputFile;
    }
    abstract public byte[] Resize(int maxWidth = 800);
    public void WriteFile(byte[] newFile, bool Prompt)
    {
        if (newFile.Length < _oriFileBytes.Length)
        {
            var newSize = (new PrettySize(newFile.Length)).Format(UnitBase.Base10,UnitStyle.Smart);
            //var oriSize = (new PrettySize(_oriFileBytes.Length)).Format(UnitBase.Base10,UnitStyle.Smart);            
            
            string? toWrite;
            if (Prompt)
            {
                Console.Write(string.Format("Do you want to {0} to the file({1})? [Y/N]",
                _outputFile != null ? "Write" : "Overwrite", newSize));
                toWrite = Console.ReadLine();
            }
            else
                toWrite = "Y";

            if (!string.IsNullOrEmpty(toWrite) && toWrite.ToUpper() == "Y")
            {
                File.WriteAllBytes(_outputFile != null ? _outputFile.FullName : _oriFileName, newFile);
                Console.WriteLine(string.Format("{0} {1} completed {2}",
                _outputFile != null ? "Write" : "Overwrite",
                _outputFile != null ? _outputFile.Name : Path.GetFileName(_oriFileName),
                Efficiency(_oriFileBytes.Length, newFile.Length))
                );
            }
            else
            {
                Console.WriteLine("Cancel writing to the file");
            }

        }
        else
        {
            Console.WriteLine("The expected reduction in file space was not achieved, so writing was abandoned.");
        }
    }

    private static string Efficiency(int ori, int com)
    {
        var newSize = new PrettySize(com).Format(UnitBase.Base10,UnitStyle.Smart);
        var oriSize = new PrettySize(ori).Format(UnitBase.Base10,UnitStyle.Smart);
        return $"[{newSize}/{oriSize}] [{(int)((double)(ori - com) / ori * 100)}%]";
    }

}