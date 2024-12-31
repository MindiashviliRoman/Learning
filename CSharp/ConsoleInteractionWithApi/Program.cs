
using ConsoleInteractionWithApi.ApiServices;
using ConsoleInteractionWithApi.ApiServices.OpenMeteo;
using ConsoleInteractionWithApi.DataModule;
using ConsoleInteractionWithApi.IOModule;
using ConsoleInteractionWithApi.IOModule.Command;
using ConsoleInteractionWithApi.StorageModule;
using System.Text.Json;

namespace ConsoleInteractionWithApi
{
    class Program
    {
        const string ProgramDescription = "Программа предоставляет доступ к апи на ресурсе https://open-meteo.com/\r\n" +
                                            "Чтобы уточнить доступные команды введите символ \"?\" и нажмите Enter.";

        const string HelpDescription = "     \"" + ConsoleCommand.GetWeatherStrCommand + " /latitude /longitude\" - получить текущие данные о погоде из апи." +
                                                                "Параметры latitude и longitude - обязательные в строгом порядке\r\n\n" +
                                                  "     \"" + ConsoleCommand.GetWeatherWithIntervalStrCommand + "\" - получить прогнозные данные о погоде из апи.\r\n\n" +
                                                  "     \"" + ConsoleCommand.SaveDataStrCommand + " /[filepath]\" - записать только что полученные данные в файл." +
                                                                "Если параметр filepath не указан - запись будет производиться в дефолтный файл " +
                                                                "{../ConsoleInteractionWithApi.csv}.\r\n\n" +
                                                  "     \"" + ConsoleCommand.ReadStrCommand + " /[filepath] /n[readLinesCount]\" - прочитать записанные данные о погоде " +
                                                                "(filepath - путь к файлу данных, readLinesCount - число " +
                                                                "последних записей, которое необходимо прочесть из файла и вывести на экран. " +
                                                                "если этого аргумента нет - выведется весь файл).\r\n\n" +
                                                  "     \"" + ConsoleCommand.QuiteStrCommand + "\" - выход из приложения.\r\n";

        private static CsvFile csvFileProvider;
        private static IInputProvider inputProvider;
        private static IOutputProvider outputProvider;
        private static IMeteoProvider meteoProvider;

        private static List<BaseWeatherData> currentData;

        static void Main(string[] args)
        {
            Console.WriteLine(ProgramDescription);
            InitModules();
            var curCommand = inputProvider.GetNextCommand();
            while (!inputProvider.IsQuiteCommand(curCommand))
            {
                switch (curCommand)
                {
                    case HelpCommand:
                        GetHelp();
                        break;
                    case GetWeatherWithIntervalCommand command1:
                        GetWeatherForecastData(command1);
                        break;
                    case GetWeatherCommand command2:
                        GetWeatherData(command2);
                        break;
                    case SaveCommand command3:
                        WriteData(command3);
                        break;
                    case ReadCommand command4:
                        ReadData(command4);
                        break;
                    case ProxyCommand command4:
                        outputProvider.OutputLine(command4.Message, ConsoleColor.Red);
                        break;
                    default:
                        break;
                }
                curCommand = inputProvider.GetNextCommand();
            }
            meteoProvider.Dispose();
        }
        private static void InitModules()
        {
            csvFileProvider = new CsvFile();
            inputProvider = new ConsoleInputProvider();
            outputProvider = new ConsoleOutputProvider();
            meteoProvider = new OpenMeteoApiController();
            currentData = new List<BaseWeatherData>();
        }

        private static void GetHelp()
        {
            outputProvider.OutputLine(HelpDescription);
        }

        private static async void GetWeatherData(GetWeatherCommand command)
        {
            try
            {
                var curWeather = await meteoProvider.GetCurrentWeatherAsync(command.Latitude, command.Longitude);
                outputProvider.OutputData(curWeather);
                currentData.Clear();
                currentData.Add(curWeather);
            }
            catch (JsonException e)
            {
                outputProvider.OutputLine(e.Message, ConsoleColor.DarkRed);
            }
            catch (Exception e)
            {
                outputProvider.OutputLine(e.Message, ConsoleColor.Red);
            }
        }

        private static async void GetWeatherForecastData(GetWeatherWithIntervalCommand command)
        {
            try
            {
                var forecastWeather = await meteoProvider.GetForecastWeatherAsync(command.Latitude, command.Longitude, command.DateFrom.ToString("yyyy-MM-dd"), command.DateTo.ToString("yyyy-MM-dd"));
                outputProvider.OutputData(forecastWeather);
                currentData = forecastWeather;
            }
            catch (JsonException e)
            {
                outputProvider.OutputLine(e.Message, ConsoleColor.DarkRed);
            }
            catch (Exception e)
            {
                outputProvider.OutputLine(e.Message, ConsoleColor.Red);
            }
        }

        private static void WriteData(SaveCommand command)
        {
            if(currentData.Count > 0)
            {
                csvFileProvider.Write(command.FilePath, currentData);
                outputProvider.OutputLine($"Данные были сохранены в файл {command.FilePath}", ConsoleColor.Yellow);
            }
            else
            {
                outputProvider.OutputLine("Нет данных для записи", ConsoleColor.Yellow);
            }
        }
        private static void ReadData(ReadCommand command)
        {
            if (File.Exists(command.FilePath))
            {
                var readCountLines = command.CountLines;
                var data = csvFileProvider.Read(command.FilePath, readCountLines);

                if(data != null)
                {
                    outputProvider.OutputLine($"Данные, полученые из файла {command.FilePath}: ", ConsoleColor.Yellow);
                    outputProvider.OutputData(data, ConsoleColor.DarkYellow);
                }
                else
                {
                    outputProvider.OutputLine($"Файл {command.FilePath} пустой", ConsoleColor.Yellow);
                }

            }
            else
            {
                outputProvider.OutputLine($"Не найден файл c записанными данными (по пути {command.FilePath})", ConsoleColor.Yellow);
            }
        }

    }
}



