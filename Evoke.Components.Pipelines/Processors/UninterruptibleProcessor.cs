using System;

using System.Collections.Generic;
using Evoke.Components.Pipelines.Interfaces;

namespace Evoke.Components.Pipelines.Processors
{
    /// <summary>
    /// Default pipeline processor that processes the given payload through
    /// the given stages and returns the final payload as result.
    /// </summary>
    public class UninterruptibleProcessor<TPayload> : IPipelineProcessor<TPayload>
    {
        /// <summary>
        /// Process the payload through all the given stages in provided order.
        /// </summary>
        public TPayload Process(TPayload payload, ICollection<IPipelineStage<TPayload>> stages)
        {
            foreach(IPipelineStage<TPayload> stage in stages)
            {
                payload = stage.Process(payload);
            }

            return payload;
        }
    }
}
