using System;
using System.ComponentModel.DataAnnotations;
using LINQtoCSV;
using System.IO;

namespace Derivco02
{
    [Serializable]
    public class ReadFrom
    {
        [CsvColumn(Name= "Name", FieldIndex =1)]
        [Required(ErrorMessage = "Please make sore that your file has Name in required flield")]
        public string Boys { get; set; }

        [CsvColumn(Name = "Gender", FieldIndex = 2)]
        [Required(ErrorMessage = "Please spacify gender.")]
        public string Girls { get; set; }



    }
}
 