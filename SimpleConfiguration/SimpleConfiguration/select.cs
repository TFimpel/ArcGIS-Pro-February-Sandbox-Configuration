using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;

namespace SimpleConfiguration
{
    internal class select : Button
    {
        protected override async void OnClick()
        {
            await ArcGIS.Desktop.Framework.Threading.Tasks.QueuedTask.Run(() =>
            {
                var allLayers = MapView.Active.Map.Layers;
                foreach (var l in allLayers)
                {
                    var fs = l as FeatureLayer;

                    if (fs == null) continue;

                    Debug.WriteLine(l.Name);

                    if (l.Name == "US Interstate Highways")
                    {
                        fs.SetSelectable(true);
                    }
                    else
                    {
                        fs.SetSelectable(false);
                        fs.SetTransparency(50);
                    }
                }
            });


                //inputlayer.SetSelectable;

                await FrameworkApplication.SetCurrentToolAsync("esri_mapping_selectByRectangleTool");

        }
    }
}
