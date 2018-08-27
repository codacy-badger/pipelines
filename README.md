## Introducion
This package provides a plug and play implementation of the Pipeline Pattern. It allows you to re-use code. You can compare a pipeline to a production-line, where every stage in the production performs a certain action on a certain payload, and passes it on to the next stage.

## Goals
- Provide a very simple .NET Standard 2.0 implementation of the Pipeline pattern.
- Be immutable and easily customizable to ones own needs.

## Basic usage
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