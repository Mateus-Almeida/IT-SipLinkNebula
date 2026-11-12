using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using CMESAPI;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.NetworkInformation;

namespace IT_SipLinkNebula
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string logFilePath = ConfigurationManager.AppSettings["logFilePath"];
        string EquipamentCode = ConfigurationManager.AppSettings["EquipamentCode"];
        int TimerInterval = int.Parse(ConfigurationManager.AppSettings["TimerInterval"]);
        int PCMQuantity = int.Parse(ConfigurationManager.AppSettings["PCMQuantity"]);

        string xmlporcsv = ConfigurationManager.AppSettings["xmlporcsv"];

        string filename = "";
        List<string> PanelCode = new List<string>();
        List<string> Channel = new List<string>();
        List<string> SIP1 = new List<string>();
        List<string> Result = new List<string>();


        List<string> footer = new List<string>();
        List<string> GetBlock = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(logFilePath + "\\CSV\\"))
            {
                Directory.CreateDirectory(logFilePath + "\\CSV\\");
            }
            if (!Directory.Exists(logFilePath + "\\XML\\"))
            {
                Directory.CreateDirectory(logFilePath + "\\XML\\");
            }

            if (!Directory.Exists(logFilePath + "\\ERROR\\"))
            {
                Directory.CreateDirectory(logFilePath + "\\ERROR\\");
            }

            if (Directory.Exists(logFilePath + "\\CSV\\") && Directory.Exists(logFilePath + "\\XML\\"))
            {
                TimerLog.Start();
            }
        }

        private void TimerLog_Tick(object sender, EventArgs e)
        {
            //Cria pasta
            TimerLog.Stop();
            TimerLog.Interval = TimerInterval;

            if (!Directory.Exists(logFilePath + "\\CSV\\"))
            {
                Directory.CreateDirectory(logFilePath + "\\CSV\\");
            }
            if (!Directory.Exists(logFilePath + "\\XML\\"))
            {
                Directory.CreateDirectory(logFilePath + "\\XML\\");
            }
            if (!Directory.Exists(logFilePath + "\\ERROR\\"))
            {
                Directory.CreateDirectory(logFilePath + "\\ERROR\\");
            }

            MoveAfterProcess();

            TimerLog.Start();
        }

        public void MoveAfterProcess()
        {
            var logPathInfo = new DirectoryInfo(logFilePath);
            int fileCount = Directory.EnumerateFiles(logFilePath, "*.csv", SearchOption.TopDirectoryOnly).Count();

            if (fileCount != 0)
            {
                foreach (var item in logPathInfo.EnumerateFiles())
                {
                    filename = item.FullName.ToString();
                    lbl_status.Text = "Processando: " + Path.GetFileNameWithoutExtension(filename);

                    Refresh();

                    if (ReadCSV(filename) == true)
                    {
                        var ack2Path = logFilePath + "\\CSV\\" + DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US")) + "\\";
                        var fileDestination = ack2Path + Path.GetFileName(item.Name);

                        if (!Directory.Exists(ack2Path))
                            Directory.CreateDirectory(ack2Path);

                        while (true)
                        {
                            try
                            {
                                File.Copy(item.FullName, fileDestination);
                                File.Delete(item.FullName);
                                break;
                            }

                            catch
                            {
                                try
                                {
                                    File.Delete(ack2Path + item.Name);
                                    File.Copy(item.FullName, fileDestination);
                                }
                                catch
                                {
                                }
                                File.Delete(item.FullName);
                                break;
                            }
                        }
                    }
                    else
                    {
                        var ack2Path = logFilePath + "\\ERROR\\" + DateTime.Now.ToString("dd-MM-yyyy", new CultureInfo("en-US")) + "\\";
                        var fileDestination = ack2Path + Path.GetFileName(item.Name);

                        if (!Directory.Exists(ack2Path))
                            Directory.CreateDirectory(ack2Path);

                        while (true)
                        {
                            try
                            {
                                File.Copy(item.FullName, fileDestination);
                                File.Delete(item.FullName);
                                break;
                            }

                            catch
                            {
                                try
                                {
                                    File.Delete(ack2Path + item.Name);
                                    File.Copy(item.FullName, fileDestination);
                                }
                                catch
                                {
                                }
                                File.Delete(item.FullName);
                                break;
                            }
                        }

                        lbl_status.Text = Path.GetFileNameWithoutExtension(filename) + " Não associado";
                    }
                }
            }
            else
            {
                lbl_status.Text = "Aguardando arquivo .CSV";
            }
        }
        public bool ReadCSV(string localfile)
        {
            clearlist();

            try
            {
                string[] linhas = File.ReadAllLines($@"{localfile}");
                int rowCount = linhas.Length;
                int csvposicao = 0;

                string xml0 = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                string xml1 = "<vCheckTester xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"Valor.vCheckTester.xsd\" Version=\"4.0\" WorkOrderNumber=\"\">";
                string xml2 = "</vCheckTester>";


                string[][] data = new string[rowCount][];
                for (int i = 1; i < linhas.Length; i++)
                {
                    string[] row = linhas[i].Split(',');
                    data[i] = row;

                    PanelCode.Add(row[0]);
                    Channel.Add(row[1]);
                    SIP1.Add(row[2]);
                    Result.Add(row[4]);

                }

                if (SIP1.Count == PCMQuantity)
                {
                    int sequence = 1;
                    string JigStatus = linkfixture(PanelCode[0].ToString());

                    if (JigStatus.Contains("OK"))
                    {
                        foreach (string s in PanelCode)
                        {
                            try
                            {
                                string datahora = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                                string resultado = Result[csvposicao].ToString().ToUpper();
                                string unitnumber = SIP1[csvposicao].ToString().ToUpper();
                                string fixture = PanelCode[csvposicao].ToString().ToUpper();
                                string channel = Channel[csvposicao].ToString().ToUpper();

                                if (resultado.Contains("PASS"))
                                {
                                    string x1 = $"<Unit Timestamp=\"{datahora}\" UnitNumber=\"\" SerialNumber=\"{unitnumber}\" StatusCode=\"PASS\">";
                                    string x2 = $"<Header TestFixtureNumber=\"{fixture}\" TestHeadNumber=\"{channel}\"/>,";
                                    string x3 = "</Unit>";

                                    footer.Add(x1 + x2 + x3);
                                }
                                sequence++;
                                csvposicao++;
                            }
                            catch
                            {
                                lbl_status.ForeColor = Color.Red;
                            }
                        }

                        //Gera arquivo
                        string virgulareplace = string.Join(",", footer).Replace(",", "" + System.Environment.NewLine);
                        string conteudo = xml0 + System.Environment.NewLine + xml1 + System.Environment.NewLine + virgulareplace + System.Environment.NewLine + xml2;

                        File.WriteAllText($@"{logFilePath + "\\XML\\"}\" + $@"{PanelCode[0]}" + ".xml", conteudo);

                        lbl_ultimoserial.Text = $"Status Fixture: {JigStatus}" + Environment.NewLine + $"Ultimo arquivo gerado: {PanelCode[0]}";
                        lbl_ultimoserial.ForeColor = Color.Green;
                        clearlist();

                        DGV_Info.Rows.Insert(0, JigStatus, Path.GetFileNameWithoutExtension(localfile) + " " + DateTime.Now.ToString("HH:mm"));
                        DGV_Info.Rows[0].Cells[0].Style.BackColor = Color.Green;
                        DGV_Info.Rows[0].Cells[0].Style.ForeColor = Color.White;

                        DGV_Info.Rows[0].Cells[1].Style.BackColor = Color.Green;
                        DGV_Info.Rows[0].Cells[1].Style.ForeColor = Color.White;


                        return true;
                    }
                    else
                    {
                        clearlist();

                        DGV_Info.Rows.Insert(0, JigStatus, Path.GetFileNameWithoutExtension(localfile) + " " + DateTime.Now.ToString("HH:mm"));
                        DGV_Info.Rows[0].Cells[0].Style.BackColor = Color.Red;
                        DGV_Info.Rows[0].Cells[0].Style.ForeColor = Color.White;

                        DGV_Info.Rows[0].Cells[1].Style.BackColor = Color.Red;
                        DGV_Info.Rows[0].Cells[1].Style.ForeColor = Color.White;
                        return false;

                    }

                }
                else
                {
                    clearlist();

                    DGV_Info.Rows.Insert(0, $"QUANTIDADE LIDO {SIP1.Count} DIFERENTE DO CONFIG {PCMQuantity}", Path.GetFileNameWithoutExtension(localfile) + "   " + DateTime.Now.ToString("HH:mm"));
                    DGV_Info.Rows[0].Cells[0].Style.BackColor = Color.Red;
                    DGV_Info.Rows[0].Cells[0].Style.ForeColor = Color.White;

                    DGV_Info.Rows[0].Cells[1].Style.BackColor = Color.Red;
                    DGV_Info.Rows[0].Cells[1].Style.ForeColor = Color.White;

                    return false;
                }
            }
            catch
            {
                lbl_status.ForeColor = Color.Red;
                clearlist();
                return false;
            }
        }

        public string linkfixture(string fixturenumber)
        {
            try
            {
                cSelcompAPI api = new cSelcompAPI();
                api.DeLinkFixtureFromSerialNumber(fixturenumber);

                bool status = true;
                string ReadSIP2 = "";
                int Position = 1;
                foreach (var spi2 in SIP1)
                {
                    if (spi2.ToString() != "NG")
                    {
                        Dictionary<string, string> Response = api.GetSerialNumber(spi2);
                        string SN2 = Response["SERIALNUMBER"].ToString().ToUpper();
                        if (SN2 != "")
                        {
                            ReadSIP2 += SN2.ToString() + ",";
                        }
                        else
                        {
                            DGV_Info.Rows.Insert(0, $"{spi2} POSIÇÃO {Position} SEM RESULTADO", $"{fixturenumber} " + DateTime.Now.ToString("HH:mm"));
                            DGV_Info.Rows[0].Cells[0].Style.BackColor = Color.Red;
                            DGV_Info.Rows[0].Cells[0].Style.ForeColor = Color.White;

                            DGV_Info.Rows[0].Cells[1].Style.BackColor = Color.Red;
                            DGV_Info.Rows[0].Cells[1].Style.ForeColor = Color.White;

                            status = false;
                        }
                    }
                    else
                    {
                        DGV_Info.Rows.Insert(0, $"{spi2} NA POSIÇÃO {Position}", $"{fixturenumber} " + DateTime.Now.ToString("HH:mm"));
                        DGV_Info.Rows[0].Cells[0].Style.BackColor = Color.Red;
                        DGV_Info.Rows[0].Cells[0].Style.ForeColor = Color.White;

                        DGV_Info.Rows[0].Cells[1].Style.BackColor = Color.Red;
                        DGV_Info.Rows[0].Cells[1].Style.ForeColor = Color.White;

                        status = false;
                    }
                    Position++;
                }

                if (status == true)
                {
                    api.DeLinkFixtureFromSerialNumber(fixturenumber);
                    string result = api.LinkFixtureToSerialNumber(fixturenumber, ReadSIP2.TrimEnd(','), EquipamentCode);

                    string[] resultsplit = result.Split('#');

                    if (result.Contains("true#SUCCESS"))
                    {
                        return "OK: " + resultsplit[1];
                    }
                    else
                    {
                        return "ERRO: " + resultsplit[1];
                    }
                }
                else
                {
                    return "FXTURE NÃO ASSOCIADO";
                }
            }
            catch 
            {
                return "ERRO";
            }
        }
        public void clearlist()
        {
            PanelCode.Clear();
            Channel.Clear();
            SIP1.Clear();
            Result.Clear();

            footer.Clear();
        }
    }
}
