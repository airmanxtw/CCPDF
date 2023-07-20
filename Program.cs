// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using Resizer;

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

var noPromptOption = new Option<Boolean?>(
            name: "--noPrompt",
            description: "Write file without prompt",
            getDefaultValue: () => false);

var rootCommand = new RootCommand("CMPDF-based commands,in order to compress the pdf size");

var compCommand = new Command("compress", "compress the pdf file")
    {
        fileOption,
        widthOption,
        outputOption,
        noPromptOption,
    };

var reimgCommand = new Command("resize", "resize the image file")
    {
        fileOption,
        widthOption,
        outputOption,
        noPromptOption,
    };

var rezipCommand = new Command("rezip", "resize image files or pdf files in zip file")
    {
        fileOption,
        widthOption,
        outputOption,
        noPromptOption,
    };
var rerarCommand = new Command("rerar", "resize image files or pdf files in rar file")
    {
        fileOption,
        widthOption,
        outputOption,
        noPromptOption,
    };


rootCommand.AddCommand(compCommand);
rootCommand.AddCommand(reimgCommand);
rootCommand.AddCommand(rezipCommand);
rootCommand.AddCommand(rerarCommand);

compCommand.SetHandler((file, maxWidth, outputFile, noPrompt) =>
    {
        process(file, maxWidth, outputFile, noPrompt ?? false, CCPDFTYPE.compress);
    },
    fileOption, widthOption, outputOption, noPromptOption);

reimgCommand.SetHandler((file, maxWidth, outputFile, noPrompt) =>
    {
        process(file, maxWidth, outputFile, noPrompt ?? false, CCPDFTYPE.resize);
    },
    fileOption, widthOption, outputOption, noPromptOption);

rezipCommand.SetHandler((file, maxWidth, outputFile, noPrompt) =>
    {
        process(file, maxWidth, outputFile, noPrompt ?? false, CCPDFTYPE.rezip);
    },
    fileOption, widthOption, outputOption, noPromptOption);

rerarCommand.SetHandler((file, maxWidth, outputFile, noPrompt) =>
    {
        process(file, maxWidth, outputFile, noPrompt ?? false, CCPDFTYPE.rerar);
    },
    fileOption, widthOption, outputOption, noPromptOption);



static void process(FileInfo file, int maxWidth, FileInfo? outputFile, bool noPrompt, CCPDFTYPE cmd)
{
    Console.WriteLine($"Processing {file.Name}, please wait....");
    BaseResizer resizer = cmd == CCPDFTYPE.compress ? new PdfResizer(file, outputFile) :
                          cmd == CCPDFTYPE.resize ? new ImgResizer(file, outputFile) :
                          cmd == CCPDFTYPE.rerar ? new RarResizer(file, outputFile) : new ZipResizer(file, outputFile);
    byte[] newBytes = resizer.Resize(maxWidth);
    resizer.WriteFile(newBytes, !noPrompt);
}

return await rootCommand.InvokeAsync(args);


