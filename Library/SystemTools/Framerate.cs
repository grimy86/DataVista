using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DataVista.SystemTools
{
    public class Framerate
    {
        #region FIELDS
        private static int _buffer;
        private static Stopwatch _stopwatch;
        private static double _count;
        #endregion

        #region PROPERTIES
        public static double Count
        {
            get { return _count; }
        }
        #endregion

        #region CONSTRUCTOR
        static Framerate()
        {
            _buffer = 0;
            _stopwatch = Stopwatch.StartNew();

            // Subscribe to CompositionTarget.Rendering event to track frame rendering
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }
        #endregion

        #region METHODS
        private static void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            // Increment frame count for each frame rendering
            _buffer++;

            // Calculate elapsed time
            TimeSpan elapsed = _stopwatch.Elapsed;

            // Update FPS every second
            if (elapsed >= TimeSpan.FromSeconds(1))
            {
                // Calculate FPS (frames per second)
                _count = _buffer / elapsed.TotalSeconds;

                // Reset frame count and stopwatch for the next calculation
                _buffer = 0;
                _stopwatch.Restart();
            }
        }

        public static void ShowFrameRate(UIElement uIElement)
        {
            string FrameRate = $"[FPS: {_count}]";

            if (uIElement is TextBlock textBlock)
            {
                textBlock.Text = $"[FPS: {_count}]";
            }
            else if (uIElement is ContentControl contentControl)
            {
                contentControl.Content = $"[FPS: {_count}]";
            }
            else
            {
                throw new ArgumentException("Unsupported UIElement type, use Textblock or ContentControl types.");
            }
        }
        #endregion
    }
}
