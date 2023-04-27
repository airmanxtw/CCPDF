## CMPDF-based commands,in order to compress the pdf size

##install
```
dotnet tool install --global CCPDF --version 0.3.0
```

## example

```
ccpdf --help
```

## An example of compressing a PDF
```
ccpdf compress --file test.pdf --outputFile test_500.pdf --maxWidth 500
```
