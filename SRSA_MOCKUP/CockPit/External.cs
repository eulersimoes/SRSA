using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace COCKPIT
{
    [ComVisibleAttribute(true)]
    public class External
    {
        # region Public events

        /// <summary>
        /// Event fired when the Google Earth plugin is ready.
        /// </summary>
        public event EventHandler PluginReady;

        /// <summary>
        /// Event fired when the Google Earth
        /// plugin detects a mouse-click event.
        /// </summary>
        public event EventHandler KmlMouseClickEvent;

        # endregion

        # region Public methods

        /// <summary>
        /// Called by the javascript when the
        /// Google Earth plugin successfully loads.
        /// </summary>
        /// <param name="pluginInstance">The plugin instance</param>
        public void JSInitSuccessCallback(object pluginInstance)
        {
            if (PluginReady != null)
                PluginReady(pluginInstance, EventArgs.Empty);
        }

        /// <summary>
        /// Called by the javascript when the
        /// Google Earth plugin fails to load.
        /// </summary>
        /// <param name="error">The error message</param>
        public void JSInitFailureCallback(string error)
        {
            throw new Exception("Error: " + error);
        }

        /// <summary>
        /// Called by the JavaScript when the Google Earth
        /// plugin detects a mouse-click event.
        /// </summary>
        /// <param name="mouseEvent">The KML mouse event</param>
        public void JSMouseClickEventCallback(object mouseEvent)
        {
            if (KmlMouseClickEvent != null)
                KmlMouseClickEvent(mouseEvent, EventArgs.Empty);
        }

        # endregion
    }
}
