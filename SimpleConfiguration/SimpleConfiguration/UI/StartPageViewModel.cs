using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.IO;
using System.Collections.ObjectModel;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Catalog;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
//using ArcGIS.Desktop.TaskAssistant;


namespace SimpleConfiguration.UI
{
    /// <summary>
    /// Business logic for the StartPage
    /// </summary>
    class StartPageViewModel : PropertyChangedBase
    {

        #region Properties
        /// <summary>
        /// Collection of all ArcGIS Pro project files.
        /// </summary>
        public ICollection<FileInfo> ProProjects
        {
            get
            {
                var fileInfos = Project.GetRecentProjects().Select(f => new FileInfo(f));
                var projectCollection = new Collection<FileInfo>(fileInfos.ToList());
                if (projectCollection.Count == 1) _selectedProjectFile = projectCollection[0];
                return projectCollection;
            }
        }

        private FileInfo _selectedProjectFile;
        /// <summary>
        /// ArcGIS Pro project file is selected and will be opened.
        /// </summary>
        public FileInfo SelectedProjectFile
        {
            get { return _selectedProjectFile; }
            set
            {
                SetProperty(ref _selectedProjectFile, value, () => SelectedProjectFile);


            }
        }

        #endregion

        #region Commands
        private ICommand _about;
        private ICommand _newP;
        public string s = DateTime.Now.ToString(@"New Project MM-dd-yyyy h-mmtt");

        /// <summary>
        /// Command opens the ArcGIS Pro OpenItemDialog API method to browse to a specific project file.
        /// </summary>
        public ICommand OpenProjectCommand
        {
            get
            {
                return StartPageViewModelHelper.OpenProjectCommand;
            }
        }
        /// <summary>
        /// Command opens the selected ArcGIS Pro project.
        /// </summary>
        public ICommand OpenSelectedProjectCommand
        {
            get
            {

                return new RelayCommand((args) => Project.OpenAsync(SelectedProjectFile.FullName), () => SelectedProjectFile != null);
            }
        }
        /// <summary>
        /// Command opens the ArcGIS Pro backstage
        /// </summary>
        public ICommand AboutArcGISProCommand
        {
            get
            {
                if (_about == null)
                    _about = new RelayCommand(() => FrameworkApplication.OpenBackstage("esri_core_aboutTab"));
                return _about;
            }
        }


        //private readonly string s;

        public ICommand NewProjectCommand
        {

            get
            {
                Debug.WriteLine("hello");
                CreateProjectSettings projectSettings = new CreateProjectSettings()
                {
                    //Sets the name of the project that will be created
                    //Name = @"MyProject123456789.aprx"
                    Name = s,
                    //Name = "MyProjectTemplate123.aprx",
                    TemplatePath = "C:\\Users\\fimpe\\Documents\\ArcGIS\\ProjectTemplates\\MyProjectTemplate4.aptx",
                    //TemplatePath = @"https://umn.maps.arcgis.com/home/item.html?id=fcf5616ccb954e6c9d351cd071e5d2ad", //...not sure if opening a .aptx from ArcGIS Online works somehow.
                    LocationPath = "C:\\Users\\fimpe\\OneDrive\\MGIS\\GEOG 8990 Spring 2018\\sondbox February 2"
                };

                if (_newP == null)
                    //_newP = new RelayCommand(async () => await Project.CreateAsync(projectSettings), TaskAssistantModule.OpenTaskAsync(@"\\sbe1\solutions\Tasks\Get Started.esriTasks"));
                    //_newP = new RelayCommand(() => Debug.WriteLine("hi
                    _newP = new RelayCommand(async () => await Project.CreateAsync(projectSettings));
                    return _newP;
            }
        }
        #endregion
    }
    #region StartPageViewModel helper class
    /// <summary>
    /// 
    /// </summary>
    internal class StartPageViewModelHelper
    {
        internal static void BrowseToProject()
        {
            var dlg = new OpenItemDialog();

            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var initFolder = Path.Combine(System.IO.Path.Combine(myDocs, "ArcGIS"), "Projects");
            if (!Directory.Exists(initFolder))
                initFolder = myDocs;
            dlg.Title = "Open Project";
            dlg.InitialLocation = initFolder;
            dlg.Filter = ArcGIS.Desktop.Catalog.ItemFilters.projects;

            if (dlg.ShowDialog() ?? false)
            {
                var item = dlg.Items.FirstOrDefault();
                if (item != null)
                {
                    Project.OpenAsync(item.Path);
                }
            }
        }
        internal static ICommand OpenProjectCommand
        {
            get
            {

                return new RelayCommand((args) => BrowseToProject(), () => true); ;
            }
        }
    }
    #endregion
}
