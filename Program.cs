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
        CMPDF.PDF pdf = new CMPDF.PDF();
        Console.WriteLine($"Processing {file.Name}, please wait....");
        var fileBytes = File.ReadAllBytes(file.FullName);
        var oriSize = fileBytes.Length;

        var cbytes = pdf.compression(fileBytes, maxWidth);
        var comSize = cbytes.Length;
        if (outputFile != null)
        {
            File.WriteAllBytes(outputFile.FullName, cbytes);
            Console.WriteLine($"Write {outputFile.Name} completed {efficiency(oriSize, comSize)}");
        }
        else
        {
            File.WriteAllBytes(file.FullName, cbytes);
            Console.WriteLine($"Overwrite {file.Name} completed {efficiency(oriSize, comSize)}");
        }
    },
    fileOption, widthOption, outputOption);

reimgCommand.SetHandler((file, maxWidth, outputFile) =>
    {
        CMPDF.PDF pdf = new CMPDF.PDF();
        Console.WriteLine($"Processing {file.Name}, please wait....");
        var fileBytes = File.ReadAllBytes(file.FullName);
        var oriSize = fileBytes.Length;

        var cbytes = pdf.ResizeImage(fileBytes, maxWidth,false);
        var comSize = cbytes.Length;
        if (outputFile != null)
        {
            File.WriteAllBytes(outputFile.FullName, cbytes);
            Console.WriteLine($"Write {outputFile.Name} completed {efficiency(oriSize, comSize)}");
        }
        else
        {
            File.WriteAllBytes(file.FullName, cbytes);
            Console.WriteLine($"Overwrite {file.Name} completed {efficiency(oriSize, comSize)}");
        }
    },
    fileOption, widthOption, outputOption);



static string efficiency(int ori, int com)
{
    return $"[{(int)(((double)(ori - com) / ori) * 100)}%]";
}

return await rootCommand.InvokeAsync(args);


