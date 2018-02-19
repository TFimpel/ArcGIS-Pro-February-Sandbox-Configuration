using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;

namespace SimpleConfiguration
{
    internal class Button1 : Button
    {
        protected override void OnClick()
        {
            MessageBox.Show("test");
            var inputlayer = (MapView.Active.Map.Layers.First(layer => true) as FeatureLayer);
            //string input_points = @"C:\data\ca_ozone.gdb\ozone_points";
            string output_polys = System.IO.Path.Combine(System.IO.Path.Combine(ArcGIS.Desktop.Core.Project.Current.HomeFolderPath, "bufferOutPuts.gdb"), "buffer_custom"); // @"C:\data\ca_ozone.gdb\ozone_buff";
            string buffer_dist = "Miles";
            string line_side = "FULL";
            string line_end_type = "ROUND";
            string dissolve_option = "ALL";
            string dissolve_field = "";
            string method = "GEODESIC";
            var param_values = Geoprocessing.MakeValueArray(inputlayer, output_polys, buffer_dist, line_side, line_end_type, dissolve_option, dissolve_field, method);

            Geoprocessing.OpenToolDialog("analysis.Buffer", param_values);
        }
    }



}
