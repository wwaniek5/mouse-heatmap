namespace MouseHeatmap.Collector.Tests
{
    internal class MockTimeProvider:ITimeProvider
    {
        public long CurrentTime { get; set; }

        public long Now()
        {
            return CurrentTime;
        }
    }
}