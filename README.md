## CMPDF-based commands,in order to compress the pdf size

## install
```shell
dotnet tool install --global CCPDF --version 0.4.3
```
## uninstall
```shell
dotnet tool uninstall --global CCPDF
```

## example

```shell
ccpdf --help
```

## An example of compressing a PDF
```shell
ccpdf compress --file test.pdf --outputFile test_500.pdf --maxWidth 500
```

## zip,docx,pptx or xlsx
```shell
ccpdf rezip --file test.zip --outputFile test_500.zip --maxWidth 500
```

## Or use the forfiles command
```shell
forfiles /m *.pdf /d -365 /c "cmd /c ccpdf compress --file @file" /s
```


