using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace SimpleConfiguration
{
    internal class Button3Truncate : Button
    {
        protected override async void OnClick()
        {
            MessageBox.Show("truncate 3");
            string output_polys = System.IO.Path.Combine(System.IO.Path.Combine(ArcGIS.Desktop.Core.Project.Current.HomeFolderPath, "bufferOutPuts.gdb"), "buffer_100miles"); // @"C:\data\ca_ozone.gdb\ozone_buff";
            var param_values = Geoprocessing.MakeValueArray(output_polys);

            //Geoprocessing.OpenToolDialog("TruncateTable_management", param_values);
            var result = await Geoprocessing.ExecuteToolAsync("TruncateTable_management", param_values);
        }
    }
}
