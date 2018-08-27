using System;
using System.Collections.Generic;
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
