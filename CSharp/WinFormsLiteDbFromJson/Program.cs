using WinFormsLiteDbFromJson.Controllers;
using WinFormsLiteDbFromJson.Entities;

namespace WinFormsLiteDbFromJson
{
    internal static class Program
    {
        private static IDatabaseService<Entity> _dbService;
        private static DataController _dataController;
        private static SetupData _setupData;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            _setupData = new SetupData(AppDomain.CurrentDomain.BaseDirectory);
            _dbService = new LiteDBService<Entity>(_setupData.FullPath);
            _dataController = new DataController(_setupData, _dbService);

            var mainForm = new Form1(_setupData, _dataController);


            _setupData.DbFullPathChanged += ChangedDbPath;

 

            mainForm.FormClosed += OnDespose;
            Application.Run(mainForm);
        }
        static void ChangedDbPath(string newPath)
        {
            //If will be required copy db from 1 path to other
            _dataController.ChangedDbPath(newPath, false);
        }


        private static void OnDespose(object? sender, FormClosedEventArgs e)
        {
            _dataController.Dispose();
            _setupData.DbFullPathChanged -= ChangedDbPath;
        }
    }
}