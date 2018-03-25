using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MouseHeatmap.Collector
{
    internal class NewScreenUnitsCalculator
    {
        public NewScreenUnitsCalculator()
        {
        }

        internal List<ScreenUnit> CalculateForNewEvent(
            MouseEventArgs lastEvent,
            MouseEventArgs newEvent,
            long timeOfLastEvent,
            long timeOfNewEvent)
        {
            var newScreenUnits = new List<ScreenUnit>();

            newScreenUnits.Add(
                CreateScreenUnitForFinishPosition(newEvent));
            newScreenUnits.AddRange(
                CreateScreenUnitsForAllIntermediatePositions(lastEvent,
                                                            newEvent,
                                                            timeOfLastEvent,
                                                            timeOfNewEvent));
            
            return newScreenUnits;
        }

        private IEnumerable<ScreenUnit> CreateScreenUnitsForAllIntermediatePositions(MouseEventArgs lastEvent, MouseEventArgs newEvent, long timeOfLastEvent, long timeOfNewEvent)
        {
            var intermediateScreenUnits = new List<ScreenUnit>();

            var speed = CalculateSpeed(lastEvent, newEvent, timeOfLastEvent, timeOfNewEvent);

            var coursorPath = CoursorPath.FromEndBlocks(lastEvent.ToScreenBlock(), newEvent.ToScreenBlock());
            foreach (Point screenBlock in coursorPath)
            {
                intermediateScreenUnits.Add(new ScreenUnit
                {
                    X = screenBlock.X,
                    Y = screenBlock.Y,
                    MousePassedCount = 1,
                    MouseFinishedCount = 0,
                    SpeedCount = speed,
                });
            }

            return intermediateScreenUnits;
        }

        private static ScreenUnit CreateScreenUnitForFinishPosition(MouseEventArgs newEvent)
        {
            return new ScreenUnit
            {
                X = newEvent.ToScreenBlock().X,
                Y = newEvent.ToScreenBlock().Y,
                MousePassedCount = 0,
                MouseFinishedCount = 1,
                SpeedCount = 0,
            };
        }

        private long CalculateSpeed(MouseEventArgs lastEvent, MouseEventArgs newEvent, long timeOfLastEvent, long timeOfNewEvent)
        {
            var distance = PointUtils.CalculateDistance(lastEvent.Location, newEvent.Location);

            var time = timeOfNewEvent - timeOfLastEvent;

            var speed = (long)(distance * Math.Pow(10, 7) / time);
            return speed;
        }
    }
}