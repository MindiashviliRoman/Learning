using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows;
using WpfSqlAny.Logic;
using WpfSqlAny.Windows;

namespace WpfSqlAny
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string NAME_TABLE_HEADER = "NAME";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        public static void ErrorMessage(string message)
        {
            MessageBox.Show(message, "error message", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static List<string> GetDataByColumnName(string name, DataTable dt)
        {
            List<string> result = new List<string>();
            int nNameColumn = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ToString().ToUpper() == name)
                {
                    nNameColumn = i;
                    break;
                }
            }
            foreach (DataRow row in dt.Rows)
            {

                result.Add(row[nNameColumn].ToString());
            }
            return result;
        }

        public static object CreateTypeWithRandomStringFields(object[] fields, string[] names)
        {
            // Let's start by creating a new assembly
            AssemblyName dynamicAssemblyName = new AssemblyName("SupportAssemply");
            AssemblyBuilder dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(dynamicAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicModule = dynamicAssembly.DefineDynamicModule("SupportAssemply");

            // Now let's build a new type
            TypeBuilder dynamicAnonymousType = dynamicModule.DefineType("MyAnon", TypeAttributes.Public);

            for (var i = 0; i < fields.Length; i++)
            {
                var field = dynamicAnonymousType.DefineField(names[i], typeof(string), FieldAttributes.Public);
            }
            // Return the type to the caller
            var type = dynamicAnonymousType.CreateType();

            dynamic myVar = Activator.CreateInstance(type);
            var myFields = myVar.GetType().GetFields();
            for (var i = 0; i < myFields.Length; i++)
            {
                myFields[i].SetValue(myVar, fields[i].ToString()); 
            }
            return myVar;
        }
    }
}
