using System;
using System.Collections.Generic;
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
    internal class Button1Truncatecs : Button
    {
        protected override async void OnClick()
        {

            MessageBox.Show("truncate 1");
            string output_polys = System.IO.Path.Combine(System.IO.Path.Combine(ArcGIS.Desktop.Core.Project.Current.HomeFolderPath, "bufferOutPuts.gdb"), "buffer_custom"); // @"C:\data\ca_ozone.gdb\ozone_buff";
            var param_values = Geoprocessing.MakeValueArray(output_polys);

            //Geoprocessing.OpenToolDialog("TruncateTable_management", param_values);
            var result = await Geoprocessing.ExecuteToolAsync("TruncateTable_management", param_values);


        }
    }
}
