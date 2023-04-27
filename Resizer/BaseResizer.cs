public abstract class BaseResizer
{
    protected CMPDF.PDF pdf = new CMPDF.PDF();
    protected byte[] _oriFileBytes;
    protected string _oriFileName;
    protected FileInfo? _outputFile;      
    protected BaseResizer(FileInfo oriFile, FileInfo? outputFile)
    {
        _oriFileBytes = File.ReadAllBytes(oriFile.FullName);
        _oriFileName = oriFile.FullName;
        _outputFile = outputFile;
    }
    abstract public byte[] resize(int maxWidth=800);
    public void writeFile(byte[] newFile)
    {
        if (newFile.Length < _oriFileBytes.Length)
        {
            File.WriteAllBytes(_outputFile != null ? _outputFile.FullName : _oriFileName, newFile);
            Console.WriteLine(string.Format("{0} {1} completed {2}",
            _outputFile != null ? "Write" : "Overwrite",
            _outputFile != null ? _outputFile.Name : Path.GetFileName(_oriFileName),
            efficiency(_oriFileBytes.Length, newFile.Length))
            );
        }
        else
        {
            Console.WriteLine("The expected reduction in file space was not achieved, so writing was abandoned.");
        }
    }

    private string efficiency(int ori, int com)
    {
        return $"[{(int)(((double)(ori - com) / ori) * 100)}%]";
    }

}