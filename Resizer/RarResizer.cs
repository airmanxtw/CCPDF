namespace Resizer;
public class RarResizer : BaseResizer
{
    public RarResizer(FileInfo oriFile, FileInfo? outputFile) : base(oriFile, outputFile){}

    public override byte[] Resize(int maxWidth = 800)
    {                                                             
        return pdf.resizeRar(_oriFileBytes,maxWidth);
    }
}