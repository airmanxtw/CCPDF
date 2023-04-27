## CMPDF-based commands,in order to compress the pdf size

## install
```shell
dotnet tool install --global CCPDF --version 0.3.0
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
