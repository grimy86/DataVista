using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DataVista.Library.Classes
{
    public class FramerateManager
    {
        #region FIELDS
        private static int _frameCount;
        private static Stopwatch _stopwatch;
        private static double _frameRate;
        #endregion

        #region PROPERTIES
        public static double FrameRate
        {
            get { return _frameRate; }
        }
        #endregion

        #region CONSTRUCTOR
        static FramerateManager()
        {
            _frameCount = 0;
            _stopwatch = Stopwatch.StartNew();

            // Subscribe to CompositionTarget.Rendering event to track frame rendering
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }
        #endregion

        #region METHODS
        private static void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            // Increment frame count for each frame rendering
            _frameCount++;

            // Calculate elapsed time
            TimeSpan elapsed = _stopwatch.Elapsed;

            // Update FPS every second
            if (elapsed >= TimeSpan.FromSeconds(1))
            {
                // Calculate FPS (frames per second)
                _frameRate = _frameCount / elapsed.TotalSeconds;

                // Reset frame count and stopwatch for the next calculation
                _frameCount = 0;
                _stopwatch.Restart();
            }
        }

        public static void ShowFrameRate(UIElement uIElement)
        {
            string FrameRate = $"[FPS: {_frameRate}]";

            if (uIElement is TextBlock textBlock)
            {
                textBlock.Text = $"[FPS: {_frameRate}]";
            }
            else if (uIElement is ContentControl contentControl)
            {
                contentControl.Content = $"[FPS: {_frameRate}]";
            }
            else
            {
                throw new ArgumentException("Unsupported UIElement type, use Textblock or ContentControl types.");
            }
        }
        #endregion
    }
}
