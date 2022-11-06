
using System;
using System.IO;

public class RankingDataManager
{
    private readonly string FILE_NAME = @"ranking.json";
    private RankingData dataToSave;

    public void temp() {
        writeScoreToFile();


    }

    public void writeScoreToFile() {
       
        if (File.Exists(FILE_NAME)) {
            
        } else {
            createNewFile();
        }
    }

    private void createNewFile() {
        try {
            using (StreamWriter sw = File.CreateText(FILE_NAME)) {

                sw.WriteLine("{");
                sw.WriteLine("\"foos\" : [{");
                sw.WriteLine("\"prop1\":\"value1\"");
                sw.WriteLine("}, {");
                sw.WriteLine("\"prop1\":\"value3\"");
                sw.WriteLine("}]");
                sw.WriteLine("}");
            
                
            }
        } catch (Exception ex) {
         
        }
    }

    //public RankingData getData() {
    //    var file = findRankingDataFile();

    //    return new RankingData();
    //}
}
