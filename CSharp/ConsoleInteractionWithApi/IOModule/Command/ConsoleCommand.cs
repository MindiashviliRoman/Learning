
using System.Globalization;

namespace ConsoleInteractionWithApi.IOModule.Command
{
    internal class ConsoleCommand
    {
        public const string GetWeatherStrCommand = "getweather";
        public const string GetWeatherWithIntervalStrCommand = "getweatheri";
        public const string SaveDataStrCommand = "save";
        public const string ReadStrCommand = "readfile";
        public const string QuiteStrCommand = "q";
        public const string QuiteStrCommandU = "Q";
        public const string HelpStrCommand = "?";

        public string Command { get; protected set; }

        protected ConsoleCommand()
        {

        }

        public static ConsoleCommand ParseConsoleCommand(string line)
        {
            var array = line.Split('/');
            if (array.Length == 0 && array.Length > 3)
            {
                throw new Exception("[Incorrect command] Не распознана команда. Обратитесь в справку и изучите доступные команды (?)");
            }
            switch (array[0].Trim())
            {
                case QuiteStrCommand:
                case QuiteStrCommandU:
                    {
                        if (array.Length == 1)
                        {
                            throw new Exception($"[Incorrect command {QuiteStrCommand}] Обратитесь в справку и изучите доступные команды (?)");
                        }
                        return new QuiteCommand(QuiteStrCommand);
                    }
                case HelpStrCommand:
                    {
                        if (array.Length != 1)
                        {
                            throw new Exception($"[Incorrect command {HelpStrCommand}] Обратитесь в справку и изучите доступные команды (?)");
                        }
                        return new HelpCommand(HelpStrCommand);
                    }
                case GetWeatherStrCommand:
                    {
                        float latitude = float.NaN;
                        float longitude = float.NaN;
                        if (array.Length == 3 && !String.IsNullOrEmpty(array[1]) && !String.IsNullOrEmpty(array[2]))
                        {
                            if (float.TryParse(array[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var value1))
                            {
                                latitude = value1;
                            }
                            if (float.TryParse(array[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var value2))
                            {
                                longitude = value2;
                            }
                        }
                        if (float.IsNaN(latitude))
                        {
                            throw new Exception($"[Incorrect command {GetWeatherStrCommand}] Не распознан аргумент latitude. Обратитесь в справку и изучите доступные команды (?)");
                        }
                        if (float.IsNaN(longitude))
                        {
                            throw new Exception($"[Incorrect command {GetWeatherStrCommand}] Не распознан аргумент longitude. Обратитесь в справку и изучите доступные команды (?)");
                        }
                        return new GetWeatherCommand(GetWeatherStrCommand, latitude, longitude);
                    }
                case GetWeatherWithIntervalStrCommand:
                    {
                        float latitude = float.NaN;
                        float longitude = float.NaN;
                        DateTime dateFrom = default;
                        DateTime dateTo = default;
                        if (array.Length == 5 && !String.IsNullOrEmpty(array[1]) && !String.IsNullOrEmpty(array[2])
                            && !String.IsNullOrEmpty(array[3]) && !String.IsNullOrEmpty(array[4]))
                        {
                            if (float.TryParse(array[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var value1))
                            {
                                latitude = value1;
                            }
                            if (float.TryParse(array[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var value2))
                            {
                                longitude = value2;
                            }
                            if (DateTime.TryParse(array[3], out var value3))
                            {
                                dateFrom = value3;
                            }
                            if (DateTime.TryParse(array[4], out var value4))
                            {
                                dateTo = value4;
                            }
                        }
                        if (float.IsNaN(latitude))
                        {
                            throw new Exception($"[Incorrect command {GetWeatherWithIntervalStrCommand}] Не распознан аргумент latitude. Обратитесь в справку и изучите доступные команды (?)");
                        }
                        if (float.IsNaN(longitude))
                        {
                            throw new Exception($"[Incorrect command {GetWeatherWithIntervalStrCommand}] Не распознан аргумент longitude. Обратитесь в справку и изучите доступные команды (?)");
                        }
                        if (dateFrom == default)
                        {
                            throw new Exception($"[Incorrect command {GetWeatherWithIntervalStrCommand}] Не распознан аргумент dateFrom. Обратитесь в справку и изучите доступные команды (?)");
                        }
                        if (dateTo == default)
                        {
                            throw new Exception($"[Incorrect command {GetWeatherWithIntervalStrCommand}] Не распознан аргумент dateTo. Обратитесь в справку и изучите доступные команды (?)");
                        }
                        return new GetWeatherWithIntervalCommand(GetWeatherWithIntervalStrCommand, latitude, longitude, dateFrom, dateTo);
                    }
                case SaveDataStrCommand:
                    {
                        string filePath = null;
                        if (array.Length > 0)
                        {
                            if (array.Length == 2 && !String.IsNullOrEmpty(array[1]))
                            {
                                array[1] = array[1].Trim();
                                if (Path.IsPathFullyQualified(array[1]))
                                {
                                    filePath = array[1];
                                }
                            }
                            else
                            {
                                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName + ".csv");
                            }
                        }
                        if (string.IsNullOrEmpty(filePath))
                        {
                            throw new Exception($"[Incorrect command {SaveDataStrCommand}] Не распознан аргумент filePath или указан неверный путь. Обратитесь в справку и изучите доступные команды (?)");
                        }
                        return new SaveCommand(GetWeatherStrCommand, filePath);
                    }
                case ReadStrCommand:
                    {
                        string filePath = null;
                        int countRows = -1;
                        if (array.Length > 0)
                        {
                            var gettedCount = false;
                            var gettedFilePath = false;
                            if (array.Length > 1 && !String.IsNullOrEmpty(array[1]) && array[1].Length > 1)
                            {
                                if(array[1][0] == 'n')
                                {
                                    countRows = GetCountReadingRows(array[1].Substring(1));
                                    gettedCount = true;
                                }
                                else
                                {
                                    filePath = GetFilePath(array[1].Trim());
                                    gettedFilePath = true;
                                }
                            }
                            if (array.Length == 3 && !String.IsNullOrEmpty(array[2]))
                            {
                                if (array[2][0] == 'n')
                                {
                                    if (gettedCount)
                                    {
                                        throw new Exception($"[Incorrect command {ReadStrCommand}] В первом аргументе уже прочитан параметр readLinesCount. Поблема с чтением второго параметра. Обратитесь в справку и изучите доступные команды (?)");
                                    }
                                    countRows = GetCountReadingRows(array[2].Substring(1));
                                    gettedCount = true;
                                }
                                else
                                {
                                    if (gettedFilePath)
                                    {
                                        throw new Exception($"[Incorrect command {ReadStrCommand}] В первом аргументе уже прочитан параметр filePath. Поблема с чтением второго параметра. Обратитесь в справку и изучите доступные команды (?)");
                                    }
                                    filePath = GetFilePath(array[2].Trim());
                                    gettedFilePath = true;
                                }
                            }
                            if (array.Length == 2 & !(gettedFilePath || gettedCount))
                            {
                                throw new Exception($"[Incorrect command {ReadStrCommand}] Некорректно заданы аргументы команды. Обратитесь в справку и изучите доступные команды (?)");
                            }
                            if (array.Length == 3 & !(gettedFilePath && gettedCount))
                            {
                                throw new Exception($"[Incorrect command {ReadStrCommand}] Некорректно заданы аргументы команды. Обратитесь в справку и изучите доступные команды (?)");
                            }
                            if (filePath == null)
                            {
                                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName + ".csv");
                            }

                        }
                        if (string.IsNullOrEmpty(filePath))
                        {
                            throw new Exception($"[Incorrect command {ReadStrCommand}] Не распознан аргумент filePath или указан неверный путь. Обратитесь в справку и изучите доступные команды (?)");
                        }
                        if (!File.Exists(filePath))
                        {
                            throw new Exception($"Не найден файл по пути {filePath}");
                        }
                        return new ReadCommand(ReadStrCommand, filePath, countRows);
                    }
                default:
                    throw new Exception("[Incorrect command] Не распознана команда. Обратитесь в справку и изучите доступные команды (?)");
            }
        }

        public static string GetFilePath(string path)
        {
            if (Path.IsPathFullyQualified(path))
            {
                return path;
            }
            return null;
        }

        public static int GetCountReadingRows(string data)
        {
            var countRows = -1;
            if (!String.IsNullOrEmpty(data))
            {
                var countStrRow = data.Trim();
                if (int.TryParse(countStrRow, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                {
                    countRows = value;
                }
                else
                {
                    throw new Exception($"[Incorrect command {ReadStrCommand}] Не распознан аргумент readLinesCount. Обратитесь в справку и изучите доступные команды (?)");
                }
            }
            return countRows;
        }
    }
}
