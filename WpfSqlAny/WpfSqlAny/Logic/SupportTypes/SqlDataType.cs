using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSqlAny.Logic.SupportTypes
{
    #region Support type
    public enum DataType
    {
        Not_Valid,
        INTEGER,
        DESIMAL,
        TEXT,
        VARCHAR,
    }
    #endregion

    public struct SqlDataType
    {
        public DataType DType;
        public int[] Optional;

        private static readonly string[] dataTypeNames;
        private static readonly DataType[] dataTypeValues;

        private static readonly Dictionary<DataType, Type> _dataTypeMap =
            new Dictionary<DataType, Type>() { 
                { DataType.VARCHAR, typeof(string) },
                { DataType.TEXT, typeof(string) },
                { DataType.INTEGER, typeof(Int64) },
                { DataType.DESIMAL, typeof(decimal) },
                { DataType.Not_Valid, typeof(object) },
            };

        public static string[] GetDataTypes()
        {
            return dataTypeNames;
        }

        static SqlDataType()
        {
            var dtType = typeof(DataType);
            dataTypeNames = Enum.GetNames(dtType);
            var arr = Enum.GetValues(dtType);
            dataTypeValues = new DataType[arr.Length];
            Array.Copy(arr, dataTypeValues, arr.Length);
        }

        public SqlDataType(DataType type, int opt1 = -1, int opt2 = -1)
        {
            DType = type;
            Optional = null;
            if (opt1 > -1 && opt2 == -1)
            {
                Optional = new int[] { opt1 };
            }
            else if (opt1 == -1 && opt2 > -1)
            {
                Optional = new int[] { opt2 };
            }
            else if (opt1 > -1 && opt2 > -1)
            {
                Optional = new int[] { opt1, opt2 };
            }
        }

        public override string ToString()
        {
            switch (DType)
            {
                case DataType.DESIMAL:
                    return $"{DType}({Optional[0]},{Optional[1]})";

                case DataType.VARCHAR:
                    return $"{DType}({Optional[0]})";
            }
            return DType.ToString();
        }

        public Type GetMappedType()
        {
            return _dataTypeMap[DType];
        }

        public static SqlDataType GetTypeFromName(string name)
        {
            var result = new SqlDataType(DataType.Not_Valid);
            for (var i = 0; i < dataTypeNames.Length; i++)
            {
                if (name.Contains(dataTypeNames[i]))
                {
                    result.DType = dataTypeValues[i];
                    switch (result.DType)
                    {
                        case DataType.DESIMAL:
                            {
                                var substr = name.Substring(dataTypeNames[i].Length + 1,
                                    dataTypeNames[i].Length - name.Length - 1);
                                var args = substr.Split(',');
                                result.Optional = new int[args.Length];
                                for (var j = 0; j < args.Length; j++)
                                {
                                    if (int.TryParse(args[j], out var val))
                                    {
                                        result.Optional[j] = val;
                                    }
                                }
                                break;
                            }
                        case DataType.VARCHAR:
                            {
                                var substr = name.Substring(dataTypeNames[i].Length + 1,
                                    dataTypeNames[i].Length - name.Length - 1);
                                result.Optional = new int[1];
                                if (int.TryParse(substr, out var val))
                                {
                                    result.Optional[0] = val;
                                }
                                break;
                            }
                    }
                    return result;
                }
            }
            return result;
        }
    }
}
