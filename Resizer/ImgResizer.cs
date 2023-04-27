public class ImgResizer : BaseResizer
{
    
    public ImgResizer(FileInfo oriFile, FileInfo? outputFile) : base(oriFile, outputFile) { }


    public override byte[] resize(int maxWidth)
    {
        return pdf.ResizeImage(_oriFileBytes, maxWidth, false);
    }
}
