namespace Resizer;
public class PdfResizer : BaseResizer
{
    public PdfResizer(FileInfo oriFile, FileInfo? outputFile) : base(oriFile, outputFile) { }

    public override byte[] Resize(int maxWidth)
    {
        return pdf.compression(_oriFileBytes, maxWidth);
    }

}
