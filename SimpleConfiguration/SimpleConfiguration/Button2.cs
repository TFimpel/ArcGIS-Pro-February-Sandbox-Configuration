using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;

namespace SimpleConfiguration
{
    internal class Button2 : Button
    {
        protected override async void OnClick()
        {
            MessageBox.Show("test2");

            // pass the full path of the service
            //string tool_path = @"C:\data\agsonline.ags\Network/ESRI_DriveTime_US\CreateDriveTimePolygons";

            //string in_point = @"C:\MyProject\MyProject.gdb\apoint";
            var inputlayer = (MapView.Active.Map.Layers.First(layer => true) as FeatureLayer);
            //string input_points = @"C:\data\ca_ozone.gdb\ozone_points";
            string output_polys = System.IO.Path.Combine(System.IO.Path.Combine(ArcGIS.Desktop.Core.Project.Current.HomeFolderPath, "bufferOutPuts.gdb"), "buffer_60miles"); // @"C:\data\ca_ozone.gdb\ozone_buff";
            string buffer_dist = "60 Miles";
            string line_side = "FULL";
            string line_end_type = "ROUND";
            string dissolve_option = "ALL";
            string dissolve_field = "";
            string method = "GEODESIC";
            var param_values = Geoprocessing.MakeValueArray(inputlayer, output_polys, buffer_dist, line_side, line_end_type, dissolve_option, dissolve_field, method);
            //Geoprocessing.OpenToolDialog("analysis.Buffer", param_values);
            //string tool_path = Geoprocessing.p

            var result = await Geoprocessing.ExecuteToolAsync("analysis.Buffer", param_values);
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

        }
    }
}
