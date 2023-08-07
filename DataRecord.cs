using CsvHelper.Configuration.Attributes;

namespace _219003234_Naidoo_KN_AAI
{
    /// <summary>
    /// This class is used to store the data
    /// </summary>
    public class DataRecord
    {
        [Name("id")] //this is the parameter in the csv file
        public int Id { get; set; } //this is what i have chosen to call it

        [Name("Product ID")]
        public string ProductId { get; set; }

        [Name("Type")]
        public string Type { get; set; }

        [Name("Air temperature [K]")]
        public double AirTemperature { get; set; }

        [Name("Process temperature [K]")]
        public double ProcessTemperature { get; set; }

        [Name("Rotational speed [rpm]")]
        public int RotationalSpeed { get; set; }

        [Name("Torque [Nm]")]
        public double Torque { get; set; }

        [Name("Tool wear [min]")]
        public int ToolWear { get; set; }

        [Name("Machine failure")]
        public int MachineFailure { get; set; }

        [Name("TWF")] 
        public int TWF { get; set; }

        [Name("HDF")]
        public int HDF { get; set; }

        [Name("PWF")]
        public int PWF { get; set; }

        [Name("OSF")]
        public int OSF { get; set; }

        [Name("RNF")]
        public int RNF { get; set; }
    }
}
