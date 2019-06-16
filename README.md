# .NET GraphLoom

A .NET standard implementation of a RDB to Semantic Graph mapping API. 

[![Build Status](https://travis-ci.org/jiefenn8/dotnet-graphloom.svg?branch=master)](https://travis-ci.org/jiefenn8/dotnet-graphloom)[![codecov](https://codecov.io/gh/jiefenn8/dotnet-graphloom/branch/master/graph/badge.svg)](https://codecov.io/gh/jiefenn8/dotnet-graphloom)[![Apache 2.0 License](https://img.shields.io/badge/license-apache2-green.svg) ](https://github.com/jiefenn8/dotnet-graphloom/blob/master/LICENSE.md)

## Description

Part of CONVERT component for web and graph parent side project [ws-projects](https://github.com/jiefenn8/ws-projects).

A relational database to graph mapping API. Mapping relational database data from a provided interface and mapping rules to generate a graph output dataset. 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See the prerequisties and deployment notes below on how to use the project on a development environment.

### Prerequisites

To be announced

### Installation

Clone the repository with cmd (or terminal) with Git installed.
```
cd <install directory of choice>
git clone https://github.com/jiefenn8/dotnet-graphloom.git
```

### Usage example

To be done

### Plans

* Add [R2RML](https://www.w3.org/TR/r2rml/) implementation
* Add graph RDF file output support
* Add graph to graph-db/triplestore support
* Add [RML](rml.io) implementation (JSON, CSV and XML data source)
* Remove dependency from Jena to Common RDF

## Built With

* [dotNetRDF](https://www.dotnetrdf.org/ "dotNetRDF - An open source .NET library for RDF") 

## License

This project is licensed under the Apache License - see the [LICENSE.md](LICENSE.md) file for details
