## Introducion
This package provides a simple plug and play implementation of the Pipeline Pattern. It allows you to re-use code. You can compare a pipeline to a production-line, where every stage in the production performs a certain action on a certain payload, and passes it on to the next stage. Stages can act on, manipulate, decorate, or even replace the payload.

This package is maintained by @ruudschuurmans.

## Goals
- Provide a very simple .NET Standard 2.0 implementation of the Pipeline pattern.
- Be immutable and easily customizable/extendable to ones own needs.

## Installation
This package is available on NuGet.org, so installation is as easy as running:
`dotnet add package Evoke.Components.Pipelines`

## Basic usage
If you find yourself passing results from one function to another to complete a series of tasks on a given subject, you might want to convert it into a pipeline.

```csharp
using Evoke.Components.Pipelines.Interfaces;

namespace MyApplication
{
    public class Playground
    {
        // Create some stages to use in our pipeline
        // The stage interface is generic so you can provide the payload
        // type you want to run down the pipeline.
        protected class TimesTwoStage : IPipelineStage<int>
        {
            public int Process(int payload)
            {
                return payload * 2;
            }
        }

        // Create yet another stage
        protected class AddOneStage : IPipelineStage<int>
        {
            public int Process(int payload)
            {
                return payload + 1;
            }
        }

        public Playground()
        {
            // Create a pipeline
            Pipeline<int> demoPipeline = new Pipeline<int>();

            // Add the stages
            demoPipeline.Pipe(new AddOneStage())
                        .Pipe(new TimesTwoStage());
            
            // Run the pipeline with a given payload
            int result = demoPipeline.Process(1);
        }
    }
}

```