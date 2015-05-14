using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Data;
using System.Data.OleDb;
using System.IO;


namespace XSLTCreator
{
    static class FileReader
    {

        
        public static void GetXsltColumns(ref List<XsltColumn> XsltColumns, string path)
        {

           XmlDocument doc = new XmlDocument();
            doc.Load(path);
            String testing = doc.InnerXml;

            var XmlDoc = testing.Split('<');
            var XSDSimpleElements = XmlDoc.Where(str => str.StartsWith("xs:element") && str.EndsWith("/>"));

            foreach (string value in XSDSimpleElements)
            {
                string value1 = value.Replace("name=\"", "'");
                int startPos = value1.IndexOf("'") + 1;
                int length = value1.IndexOf("\"") - startPos;

                XsltColumn xsltcolumn = new XsltColumn();
                xsltcolumn.Value = value1.Substring(startPos, length);

                if (!XsltColumns.Contains(xsltcolumn))
                    XsltColumns.Add(xsltcolumn);
            }

        }



        public static  DataView ReadExcelFile(String ExcelPath)
        {
            try
            {
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                if (ExcelPath.LastIndexOf("xlsx") > 0)
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelPath + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

                DataSet output = new DataSet();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
						{
							null,
							null,
							null,
							"TABLE"
						});

                    DataRow schemaRow = schemaTable.Rows[0];

                    OleDbCommand cmd = new OleDbCommand("SELECT  * FROM [" + schemaRow["TABLE_NAME"].ToString() + "]", conn);
                    cmd.CommandType = CommandType.Text;
                    DataTable xlsfile = new DataTable(schemaRow["TABLE_NAME"].ToString());
                    output.Tables.Add(xlsfile);
                    new OleDbDataAdapter(cmd).Fill(xlsfile);
                    if (xlsfile != null)
                    {

                        xlsfile.TableName = "PositionMaster";
                        //  Converting XLS to Input/Serialized XML
                        DataTable outputTable = xlsfile.Copy();
                        for (int i = 0; i < outputTable.Columns.Count; i++)
                            outputTable.Columns[i].ColumnName = "COL" + i;

                        output.Tables.Clear();
                        output.Tables.Add(outputTable);

                        StreamWriter file = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "\\serializedXML.xml");
                        file.WriteLine(output.GetXml());
                        file.Close();

                        Dictionary<String, String> filecols = new Dictionary<string, string>();

                        for (int i = 0; i < outputTable.Columns.Count; i++)
                            filecols.Add(xlsfile.Columns[i].ColumnName, outputTable.Columns[i].ColumnName);

                        CacheHandler.GetInstance().FileColumns = filecols;

                        return xlsfile.DefaultView;

                    }
                    return null;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
