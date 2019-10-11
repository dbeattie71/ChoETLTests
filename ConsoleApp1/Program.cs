using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChoETL;

namespace ConsoleApp1
{
    public partial class EmployeeRec
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
    }

    [ChoMetadataRefType(typeof(EmployeeRec))]
    [ChoCSVRecordObject(Encoding = "UTF-32", ErrorMode = ChoErrorMode.ReportAndContinue,IgnoreFieldValueMode = ChoIgnoreFieldValueMode.Any, ThrowAndStopOnMissingField = false,ObjectValidationMode = ChoObjectValidationMode.Off)]
    public class EmployeeRecMeta : IChoNotifyRecordRead
    {
        [ChoCSVRecordField(1, FieldName = "id", ErrorMode = ChoErrorMode.ReportAndContinue)]
        [Range(1, 1, ErrorMessage = "Id must be > 0.")]
        //[ChoFallbackValue(1)]
        public int Id { get; set; }
        [ChoCSVRecordField(2, FieldName = "Name")]
        //[StringLength(1)]
        [DefaultValue("ZZZ")]
        [ChoFallbackValue("XXX")]
        public string Name { get; set; }

        public bool AfterRecordFieldLoad(object target, long index, string propName, object value)
        {
            throw new NotImplementedException();
        }

        public bool AfterRecordLoad(object target, long index, object source, ref bool skip)
        {
            throw new NotImplementedException();
        }

        public bool BeforeRecordFieldLoad(object target, long index, string propName, ref object value)
        {
            throw new NotImplementedException();
        }

        public bool BeforeRecordLoad(object target, long index, ref object source)
        {
            throw new NotImplementedException();
        }

        public bool BeginLoad(object source)
        {
            throw new NotImplementedException();
        }

        public void EndLoad(object source)
        {
            throw new NotImplementedException();
        }

        public bool RecordFieldLoadError(object target, long index, string propName, object value, Exception ex)
        {
            return true;
        }

        public bool RecordLoadError(object target, long index, object source, Exception ex)
        {
            throw new NotImplementedException();
        }

        public bool SkipUntil(long index, object source)
        {
            return index <= 2 ? true : false;
        }

        public bool DoWhile(long index, object source)
        {
            throw new NotImplementedException();
        }
    }

    public class Program
    {
        public static void Main(string[] args) {
            //File.OpenRead()
            var fullPath = ChoPath.GetFullPath( "Emp.csv" );
            using (var choCsvReader = new ChoCSVReader<EmployeeRec>("D:\\sourcecontrol\\repos\\ChoETLTests\\ConsoleApp1\\Emp.csv").WithFirstLineHeader()) {
                foreach( var employeeRec in choCsvReader.AsEnumerable( ) ) {
                    Console.WriteLine( employeeRec.DumpAsJson() );
                }
            }

        }
    }
}
