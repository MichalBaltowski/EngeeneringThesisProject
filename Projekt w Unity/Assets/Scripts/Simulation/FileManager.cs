using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager {
    public readonly string FILE_NAME;
    private DataForTxt dataToSave;
    private string currentDateTime;

    public FileManager() {
        FILE_NAME = @"score.txt";
        currentDateTime = DateTime.Now.ToString();
    }

    public List<RankingData> deserializeRecordsFromFile() {
        List<RankingData> result = new List<RankingData>();
        if (File.Exists(FILE_NAME)) {
            string text = getAllTextFromFile();
            if (text.Length > 0) {
                result = deserializeRecords(text);
            }
        }
        return result;
    }

    public void writeScoreToFile(DataForTxt dataToSaveInFile) {
        dataToSave = dataToSaveInFile;
        if (File.Exists(FILE_NAME)) {
            appendRecordToFile();
        } else {
            createNewFile();
        }
    }

    private string getAllTextFromFile() {
        try {
            return File.ReadAllText(FILE_NAME);
        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
            return null;
        }
    }

    private void createNewFile() {
        try {
            using (StreamWriter sw = File.CreateText(FILE_NAME)) {
                sw.WriteLine("1;New record added; {0}", currentDateTime);
                addDataToFile(sw);
            }
        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
        }
    }

    private void appendRecordToFile() {
        try {
            using (StreamWriter sw = File.AppendText(FILE_NAME)) {
                sw.WriteLine("\n>");
                sw.WriteLine("1;New record added; {0}", currentDateTime);
                addDataToFile(sw);
            }
        } catch (Exception ex) {
            Debug.LogError(ex.ToString());
        }
    }

    private void addDataToFile(StreamWriter sw) {
        sw.WriteLine("2;Duration;" + dataToSave.getDuration());
        sw.WriteLine("3;How many cars finished;" + dataToSave.getnumberOfFinishingCar());
        sw.WriteLine("4;Generation;" + dataToSave.getGenerationNumber());
        sw.WriteLine("5;Population Size;" + dataToSave.getPopulationSize());
        sw.WriteLine("6;Mutation chance;" + dataToSave.getMutationChance());
        sw.WriteLine("7;Mutation strength;" + dataToSave.getMutationStrength());
    }

    private List<RankingData> deserializeRecords(String text) {
        List<RankingData> result = new List<RankingData>();

        var recordsCollection = text.Split('>');
        foreach (string record in recordsCollection) {
            var recordAttributes = record.Trim().Split('\n');

            int generation = 0;
            string duration = "";
            int populationSize = 0;
            float mutationChance = 0f;
            float mutationStrength = 0f;

            foreach (string line in recordAttributes) {
                var attribute = line.Split(';');
                int attributeNumber = Int16.Parse(attribute[0]);

                switch (attributeNumber) {
                    case 2:
                        duration = attribute[2];
                        break;
                    case 4:
                        generation = Int16.Parse(attribute[2]);
                        break;
                    case 5:
                        populationSize = Int16.Parse(attribute[2]);
                        break;
                    case 6:
                        mutationChance = float.Parse(attribute[2]);
                        break;
                    case 7:
                        mutationStrength = float.Parse(attribute[2]);
                        break;
                    default:
                        break;
                }
            }

            var temp = RankingDataBuilder.get()
                 .withGenerationNumber(generation)
                 .withDuration(duration)
                 .withPopulationSize(populationSize)
                 .withMutationChance(mutationChance)
                 .withMutationStrength(mutationStrength)
                 .createRankingData();
            result.Add(temp);
        }

        return result;
    }
}
