// See https://aka.ms/new-console-template for more information

using System.CommandLine;

var fileOption = new Option<FileInfo>(
            name: "--file",
            description: "The file to read");

var widthOption = new Option<int>(
            name: "--maxWidth",
            description: "Minimum image width to compress",
            getDefaultValue: () => 800);


var outputOption = new Option<FileInfo?>(
            name: "--outputFile",
            description: "The compressed file");

var rootCommand = new RootCommand("CMPDF-based commands,in order to compress the pdf size");

var compCommand = new Command("compress", "compress the pdf file")
    {
        fileOption,
        widthOption,
        outputOption,
    };

var reimgCommand = new Command("resize", "resize the image file")
    {
        fileOption,
        widthOption,
        outputOption,
    };

rootCommand.AddCommand(compCommand);
rootCommand.AddCommand(reimgCommand);

compCommand.SetHandler((file, maxWidth, outputFile) =>
    {
        process(file, maxWidth, outputFile, CCPDFTYPE.compress);
    },
    fileOption, widthOption, outputOption);

reimgCommand.SetHandler((file, maxWidth, outputFile) =>
    {
        process(file, maxWidth, outputFile, CCPDFTYPE.resize);
    },
    fileOption, widthOption, outputOption);

static void process(FileInfo file, int maxWidth, FileInfo? outputFile, CCPDFTYPE cmd)
{    
    Console.WriteLine($"Processing {file.Name}, please wait....");
    BaseResizer resizer = cmd == CCPDFTYPE.compress ? new PdfResizer(file,outputFile) : new ImgResizer(file,outputFile);
    byte[] newBytes = resizer.resize(maxWidth);
    resizer.writeFile(newBytes); 
}

return await rootCommand.InvokeAsync(args);


