
# File Converter

![ci build](https://github.com/visualsanity/CloudCommerceGroup/workflows/ci%20build/badge.svg) ![ci build](https://github.com/visualsanity/CloudCommerceGroup/workflows/ci%20build/badge.svg)

## Description

A console application to convert Csv to Json or Xml. 

It takes three arguments:
* --i -the input csv file
* --o -the output file, either json and xml
* --d -the csv delimiter

## Description

Three ways to run the application:

1. Clone the repository, and run the solution (FileConverter.sln)
	* Set the properties application arguments:
		--i Sample.csv --o output.json --d ,
2. Clone and Build the application.
	* Copy the FileConverter.exe to a directory
	* There is a Sample.csv file to get you started
	* From the commandline run: FileConverter.exe --i Sample.csv --o output.json --d 	
3. Run Docker
	*