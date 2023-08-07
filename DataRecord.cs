using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _219003234_Naidoo_KN_AAI
{
    /// <summary>
    /// This class is used to store the data
    /// </summary>
    public class DataRecord
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string Type { get; set; }
        public double AirTemperature { get; set; }
        public double ProcessTemperature { get; set; }
        public int RotationalSpeed { get; set; }
        public double Torque { get; set; }
        public int ToolWear { get; set; }
        public int MachineFailure { get; set; }
   /*
        public int TWF { get; set; }
        public int HDF { get; set; }
        public int PWF { get; set; }
        public int OSF { get; set; }
        public int RNF { get; set; }
   */
        public DataRecord(
            int id,
            string productId,
            string type,
            double airTemperature,
            double processTemperature,
            int rotationalSpeed,
            double torque,
            int toolWear,
            int machineFailure
           /* int twf,
            int hdf,
            int pwf,
            int osf,
            int rnf*/
        )
        {
            Id = id;
            ProductId = productId;
            Type = type;
            AirTemperature = airTemperature;
            ProcessTemperature = processTemperature;
            RotationalSpeed = rotationalSpeed;
            Torque = torque;
            ToolWear = toolWear;
            MachineFailure = machineFailure;
           /* TWF = twf;
            HDF = hdf;
            PWF = pwf;
            OSF = osf;
            RNF = rnf;*/
        }
    }
}
