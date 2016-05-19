using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace pdfConfigurator
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        string configFilePath="./Sys/Conf.cf";
        pdfConfigParam PdfConfig; 
        private void mainForm_Load(object sender, EventArgs e)
        {
            PdfConfig = new pdfConfigParam(configFilePath);
        }

    }
    class pdfConfigParam 
    {
        char[] CompanyName=new char[20];
        public string companyName;
	    char[] Serialnumber=new char[7];
        public string serialNumber;
	    char[] ProductionDate=new char[32];
        public string productionDate;
	    char[] ProductLot=new char[32];
        public string productLot;
	    char[] FirmwareVersion=new char[32];
        public string firmwareVersion;
	    char[] OriginalTimeZone=new char[32];
        public string originalTimeZone;
        public UInt16 SamplingRate_s;
        public UInt16 StartDelay_s;
        public UInt16 AlarmDelay_s;
	    char AlarmType;
	    char ParameterCount;
	    char[] ParamA_Name=new char[32];
	    char[] ParamB_Name=new char[32];
	    char[] ParamC_Name=new char[32];
	    char[] ParamA_Unit=new char[4];
	    char[] ParamB_Unit=new char[4];
        char[] ParamC_Unit = new char[4];
	    public float ParamA_HighAlarm;
        public float ParamB_HighAlarm;
        public float ParamC_HighAlarm;
        public float ParamA_LowAlarm;
        public float ParamB_LowAlarm;
        public float ParamC_LowAlarm;
        public pdfConfigParam(string filePath) 
        {
            try
            {
                byte[] paramBytes = File.ReadAllBytes(filePath);
                int i = 0;
                Array.Copy(paramBytes, i, CompanyName, 0, 20); i += 20;
                Array.Copy(paramBytes, i, Serialnumber, 0, 7); i += 7;
                Array.Copy(paramBytes, i, ProductionDate, 0, 32); i += 32;
                Array.Copy(paramBytes, i, ProductLot, 0, 32); i += 32;
                Array.Copy(paramBytes, i, FirmwareVersion, 0, 32); i += 32;
                Array.Copy(paramBytes, i, OriginalTimeZone, 0, 32); i += 32;
                i++;

                UInt16 templow = (UInt16)paramBytes[i++];
                UInt16 tempHigh = (UInt16)paramBytes[i++];
                tempHigh <<= 8;
                SamplingRate_s = (UInt16)(tempHigh + templow);

                templow = (UInt16)paramBytes[i++];
                tempHigh = (UInt16)paramBytes[i++];
                tempHigh <<= 8;
                StartDelay_s = (UInt16)(tempHigh + templow);

                templow = (UInt16)paramBytes[i++];
                tempHigh = (UInt16)paramBytes[i++];
                tempHigh <<= 8;
                AlarmDelay_s = (UInt16)(tempHigh + templow);

                AlarmType = (char)paramBytes[i++];
                ParameterCount = (char)paramBytes[i++];
                Array.Copy(paramBytes, i, ParamA_Name, 0, 32); i += 32;
                Array.Copy(paramBytes, i, ParamB_Name, 0, 32); i += 32;
                Array.Copy(paramBytes, i, ParamC_Name, 0, 32); i += 32;
                Array.Copy(paramBytes, i, ParamA_Unit, 0, 4); i += 4;
                Array.Copy(paramBytes, i, ParamB_Unit, 0, 4); i += 4;
                Array.Copy(paramBytes, i, ParamC_Unit, 0, 4); i += 4;

                ParamA_HighAlarm=BitConverter.ToSingle(paramBytes, i); i += 4;
                ParamB_HighAlarm = BitConverter.ToSingle(paramBytes, i); i += 4;
                ParamC_HighAlarm = BitConverter.ToSingle(paramBytes, i); i += 4;
                ParamA_LowAlarm = BitConverter.ToSingle(paramBytes, i); i += 4;
                ParamB_LowAlarm = BitConverter.ToSingle(paramBytes, i); i += 4;
                ParamC_LowAlarm = BitConverter.ToSingle(paramBytes, i); i += 4;

                //companyName = BitConverter.ToString(CompanyName);

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }

    }
}
