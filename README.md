
# File Converter

![ci build](https://github.com/visualsanity/CloudCommerceGroup/workflows/ci%20build/badge.svg) ![ci docker](https://github.com/visualsanity/Utilities/workflows/ci%20docker/badge.svg)

## Description

A inline console application to convert data from one structure to another. The application uses the Composition Framework (MEF) via plugins. To create more conversion types, create your plugin by implementing the IComponent interface in Converter.Core. (see https://github.com/visualsanity/Utilities/tree/master/src/Plugins/FileConverter.Json)

To create a plugin

1. cd into your desired directory
2. run `dotnet new MyPlugin classlib`. Change MyPlugin name to reflect your conversion you trying to achieve.
3. Add the Coverter.Core nuget package to your project `Install-Package FileConverter.Core`
4. Implement the IConverter interface from the FileConverter.Core package
5. Implement the Process method to convert your files
	* See https://github.com/visualsanity/Utilities/tree/master/src/Plugins/FileConverter.Json


See roadmap for future updates.

It takes three arguments:
* --i -the input csv file
* --o -the output file, either json and xml
* --m -the metadata that needs to be passed into the Process method e.g Delimiter type ","

## Description

Three ways to run the application:

1. Clone the repository, and run the solution (FileConverter.sln)
	* Set the launchSettings.json arguments:
		--i Sample.csv --o output/output.json
2. Clone and Build the application.
	* Copy the FileConverter.exe to a directory
	* There is a Sample.csv file to get you started
	* From the commandline run: FileConverter.exe --i Sample.csv --o output.json --m ,	
3. Docker. 
	* Copy the sample csv file from support/sample.csv into a desired directory
	* Open the command line and run the following commands
		- docker pull visualsanity/utilities:latest
		- CD into the directory that you copied the sample.csv
		- docker run visualsanity/utilities:latest --i Sample.csv --o Output.json

## Roadmap
* Implement a Composition framework
* Update parse function
* Convert json to csv
* Convert xml to csv
