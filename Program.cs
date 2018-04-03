using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using DXANET;


namespace dxa_dict_src_creator
{
    class Program
    {
        static List<Domain> readDomains = new List<Domain>();
        static List<DictElement> outputData = new List<DictElement>();
        static int Main(string[] args)
        {
            string[] dataToRead = new string[]{"ce.json","edu.json","fi.json","fs.json","ss.json","trc.json"};
            foreach (string s in dataToRead)
            {
                readDomains.Add(ParseFileToDomain(s));
            }
            readDomains.ForEach(rd => {rd.ToDictElementList().ForEach(dictEle => {outputData.Add(dictEle);});});

            string output = "";

            foreach (DictElement de in outputData)
            {
                output = output + de.elementName + "," + de.domainAcronym + ",";
                if (de == outputData[outputData.Count-1])
                {
                    output = output.Substring(0, output.Length-1);
                }                
            }
            StreamWriter sw = new StreamWriter("DictData.csv",false);
            sw.Write(output);
            sw.Close();

            //StreamWriter sw = new StreamWriter("DictData.json",false);
            //sw.Write(JsonConvert.SerializeObject(JsonConvert.SerializeObject(outputData)));
            //sw.Close();
            return 0;
        }
        static Domain ParseFileToDomain(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string data = sr.ReadToEnd();
            sr.Close();
            return JsonConvert.DeserializeObject<Domain>(data);    
        }
    }
}
