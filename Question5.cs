using System;

public class Class1
{
	public Class1()
	{
        DataTable PCR;
        DataTable NGS;

        //Read in PCR files
        var filesPCR = Directory.EnumerateFiles("path", "PCR*.csv");
        foreach (string file in files)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                   DataAdapter.fill(ReadCSVFile(file), "PCR");

            }
        }

        var filesNGS = Directory.EnumerateFiles("path", "NGS*.csv");
        foreach (string file in files)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                DataAdapter.fill(ReadCSVFile(file), "NGS");

            }

            

            // merge tables 

        }

        ////////////////// now merge the two data tables
        ///


        // set primary keys of tables
        NGS.PrimaryKey = new DataColumn[] { idColumn };

        PCR.PrimaryKey = new DataColumn[] { idColumn };

        DataTable Merged;

        // merge the two tables.  using linQ
        var result = from dataRows1 in PCR.AsEnumerable()
                     join dataRows2 in NGS.AsEnumerable()
                     on dataRows1.Field<string>("ID") equals dataRows2.Field<string>("ID")

                     select dtResult.LoadDataRow(new object[]
                     {
                dataRows1.Field<string>("ID"),
                dataRows1.Field<string>("name"),
                dataRows2.Field<int>("etc"),
                      }, false);


    }


    public DataTable ReadCsvFile(string FileName)
    {

        DataTable dtCsv = new DataTable();
        string Fulltext;
        if (FileUpload1.HasFile)
        {
            
            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j]); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }
        }
        return dtCsv;
    }

}

}
