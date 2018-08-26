using System;

using System.Collections.Generic;
using Evoke.Components.Pipelines.Interfaces;
using Evoke.Components.Pipelines.Processors;

namespace Evoke.Components.Pipelines
{
    /// <summary>
    /// A Pipeline with various stages and a processor that is
    /// used to process the stages in the pipeline.
    /// </summary>
    public class Pipeline<TPayload> : IPipeline<TPayload>
    {
        private readonly List<IPipelineStage<TPayload>> _stages;
        private readonly IPipelineProcessor<TPayload> _processor;

        /// <summary>
        /// Initializes a new instance of an immutable Pipeline that either uses given 
        /// processor or the default UninterruptibleProcessor as its processor.
        /// </summary>
        /// <param name="processor">Processor.</param>
        public Pipeline(IPipelineProcessor<TPayload> processor = null)
        {
            _processor = processor ?? new UninterruptibleProcessor<TPayload>();
            _stages = new List<IPipelineStage<TPayload>>();
        }

        /// <summary>
        /// Adds a given stage to the pipeline.
        /// </summary>
        public IPipeline<TPayload> Pipe(IPipelineStage<TPayload> stage)
        {
            _stages.Add(stage);
            return this;
        }

        /// <summary>
        /// Process a given payload through the stages in this pipeline using
        /// the processor in this pipeline.
        /// </summary>
        public TPayload Process(TPayload payload) 
        {
            return _processor.Process(payload, _stages);
        }
    }
}
