# .NET GraphLoom

A .NET standard implementation of a RDB to Semantic Graph mapping API. 

[![Build status](https://ci.appveyor.com/api/projects/status/pd813dcsc96n675m?svg=true)](https://ci.appveyor.com/project/jiefenn8/dotnet-graphloom)[![codecov](https://codecov.io/gh/jiefenn8/dotnet-graphloom/branch/master/graph/badge.svg)](https://codecov.io/gh/jiefenn8/dotnet-graphloom)[![Apache 2.0 License](https://img.shields.io/badge/license-apache2-green.svg) ](https://github.com/jiefenn8/dotnet-graphloom/blob/master/LICENSE.md)

## Description

Part of CONVERT component for web and graph parent side project [ws-projects](https://github.com/jiefenn8/ws-projects).

A relational database to graph mapping API. Mapping relational database data from a provided interface and mapping rules to generate a graph output dataset. 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See the prerequisties and deployment notes below on how to use the project on a development environment.

### Prerequisites

 * GraphLoom.Core - .NET Standard 2.0
 * GraphLoom test - .NET Framework 4.7.2 
 * [Nuget](https://www.nuget.org/) for package handling.

### Installation

Clone the repository with cmd (or terminal) with Git installed.
```
cd <install directory of choice>
git clone https://github.com/jiefenn8/dotnet-graphloom.git
```

### Usage example

```
//Quick example to RDF graph
using GraphLoom.Mapper;
using GraphLoom.Mapper.RDF;
using GraphLoom.Mappoer.Configuration;

//Declare and initalise required implementations
IInputSource InputSource = new YourInputSourceImpl();
IMapperConfig MapperConfig = new YouMapperConfigImpl(); 

//Map data
IGraphMapper RDFMapper = RDFMapperFactory.createDefaultRDFMapper();
IGenericGraph output = RDFMapper.MapToGraph(InputSource, MapperConfig);

//Rest of your code handling output. e.g. To file or graph database
```

### Plans

* Add [R2RML](https://www.w3.org/TR/r2rml/) implementation
* Add graph RDF file output support
* Add graph to graph-db/triplestore support
* Add [RML](rml.io) implementation (JSON, CSV and XML data source)
* Add Label Property Graph implementation

## Built With

* [dotNetRDF](https://www.dotnetrdf.org/ "dotNetRDF - An open source .NET library for RDF") 

## License

This project is licensed under the Apache License - see the [LICENSE.md](LICENSE.md) file for details
