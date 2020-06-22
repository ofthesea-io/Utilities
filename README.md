
# File Converter

![ci build](https://github.com/visualsanity/CloudCommerceGroup/workflows/ci%20build/badge.svg) ![ci docker](https://github.com/visualsanity/Utilities/workflows/ci%20docker/badge.svg)

## Description

A console application to convert CSV to JSON or XML. Currently, the console only implements a simple parse of the CSV. As long as the CSV is well-formed it can be of any length. As more data types get added the need to implement a full-blown factory like pattern with MEF will become more essential. Right now, this stripped down version will suffice.

See roadmap for future updates.

It takes three arguments:
* --i -the input csv file
* --o -the output file, either json and xml
* --d -the csv delimiter

## Description

Three ways to run the application:

1. Clone the repository, and run the solution (FileConverter.sln)
	* Set the launchSettings.json arguments:
		--i Sample.csv --o output/output.json --d ,
2. Clone and Build the application.
	* Copy the FileConverter.exe to a directory
	* There is a Sample.csv file to get you started
	* From the commandline run: FileConverter.exe --i Sample.csv --o output.json --d ,	
3. Docker. 
	* Copy the sample csv file from support/sample.csv into a desired directory
	* Open the command line and run the following commands
		- docker pull visualsanity/utilities:latest
		- CD into the directory that you copied the sample.csv
		- docker run visualsanity/utilities:latest --i Sample.csv --o Output.json

## Roadmap
* Update parse function
* Convert json to csv
* Convert xml to csv
