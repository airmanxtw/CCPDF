public class ZipResizer : BaseResizer
{
    public ZipResizer(FileInfo oriFile, FileInfo? outputFile) : base(oriFile, outputFile){}

    public override byte[] resize(int maxWidth = 800)
    {
        return pdf.ResizeZip(_oriFileBytes,maxWidth);
    }
}
