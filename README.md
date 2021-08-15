# .NET GraphLoom

A .NET standard implementation of a RDB to Semantic Graph mapping API. 

[![CI](https://github.com/jiefenn8/dotnet-graphloom/workflows/CI/badge.svg)](https://github.com/jiefenn8/dotnet-graphloom/actions?query=workflow%3ACI)[![codecov](https://codecov.io/gh/jiefenn8/dotnet-graphloom/branch/master/graph/badge.svg?token=9FWejJ5K5K)](https://codecov.io/gh/jiefenn8/dotnet-graphloom)[![Apache 2.0 License](https://img.shields.io/badge/license-apache2-green.svg) ](https://github.com/jiefenn8/graphloom/blob/master/LICENSE.md)

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
using GraphLoom.Mapper;
using GraphLoom.Mapper.RDF;
using GraphLoom.Mapper.RDF.R2RML;
using System;
using VDS.RDF;
using VDS.RDF.Writing;

namespace ExampleApp1
{
    public class Program
    {
        //Example using GraphLoom to convert inputsource to RDF.
        public static void Main(string[] args)
        {
            //Parse the r2rml document into a r2rml map.
            R2RMLParser r2rmlParser = new R2RMLParser();
            R2RMLMap r2rmlMap = r2rmlParser.Parse("my_r2rml_doc.ttl", new Uri("http://www.mydomainname.com/ns#"));

            //Provide own inputsource implementation with r2rml map.
            IInputSource inputSource = new YourIInputSourceImplementation();
            IGraphMapper rdfMapper = RDFMapperFactory.CreateDefaultRDFMapper();
            IGenericGraph result = rdfMapper.MapToGraph(inputSource, r2rmlMap);
            
            //Manipulate result or write to DB store or File.
            
            //Writing to file using dotNetRDF API.
            IGraph vdsGraph = (IGraph)result;
            vdsGraph.SaveToFile("my_rdf_result.nt", new NTriplesWriter());
        }
    }
}
```

### ToDo

* ~~Add [R2RML](https://www.w3.org/TR/r2rml/) implementation.~~ Initial mvp implement done. 
* Add graph output to graph-db(like Neo4J) support.
* Add [RML](rml.io) implementation (JSON, CSV and XML data source).
* Add Label Property Graph implementation.

## Built With

* [dotNetRDF](https://www.dotnetrdf.org/ "dotNetRDF - An open source .NET library for RDF") 

## License

This project is licensed under the Apache License - see the [LICENSE](LICENSE) file for details
