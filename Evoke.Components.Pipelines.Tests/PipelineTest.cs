using System;
using System.Collections.Generic;
using Moq;
using Evoke.Components.Pipelines.Interfaces;
using Xunit;

namespace Evoke.Components.Pipelines.Tests
{
    public class PipelineTest
    {
        [Fact]
        public void TheStageShouldBeAddedAndCalledWhenProcessedbyDefaultProcessor()
        {
            // Given we have a pipeline stage
            Mock<IPipelineStage<int>> stageMock = new Mock<IPipelineStage<int>>();
            stageMock.Setup(stage => stage.Process(1)).Returns(2);

            // And we provide the stage to the pipeline
            // and we run the pipeline with payload int:1
            int result = (new Pipeline<int>())
                .Pipe(stageMock.Object)
                .Process(1);

            // Then the result should be the stage return value int:2
            Assert.True(2 == result);

            // And the stage should have been called with int:1
            stageMock.Verify(stage => stage.Process(1));
        }

        [Fact]
        public void TheCustomProcessorShouldBeUsedIfProvided()
        {
            // Given we have a custom processor
            Mock<IPipelineProcessor<int>> mockProcessor = new Mock<IPipelineProcessor<int>>();

            // And it has a Process method
            mockProcessor
                .Setup(processor => processor
                       .Process(It.IsAny<int>(), It.IsAny<ICollection<IPipelineStage<int>>>()))
                                .Returns(2);

            // And we have a stage to add to the pipeline
            Mock<IPipelineStage<int>> stageMock = new Mock<IPipelineStage<int>>();
            stageMock.Setup(stage => stage.Process(1)).Returns(2);

            // And we run the pipeline with our custom processor
            int result = (new Pipeline<int>(mockProcessor.Object))
               .Pipe(stageMock.Object)
               .Process(1);

            // Then our custom processor should have been called instead of the default one
            mockProcessor.Verify(processor => processor.Process(1, It.IsAny<ICollection<IPipelineStage<int>>>()));
        }
    }
}
