using System;
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

    public void writeScoreToFile(DataForTxt dataToSaveInFile) {
        dataToSave = dataToSaveInFile;
        if (File.Exists(FILE_NAME)) {
            appendRecordToFile();
        } else {
            createNewFile();
        }
    }

    private void createNewFile() {
        try {
            using (StreamWriter sw = File.CreateText(FILE_NAME)) {
                sw.WriteLine("New file created: {0}", currentDateTime);
                sw.WriteLine("Author: Micha³ Ba³towski");
                addDataToFile(sw);
            }
        } catch (Exception ex) {
            Debug.Log(ex.ToString());
        }
    }

    private void appendRecordToFile() {
        try {
            using (StreamWriter sw = File.AppendText(FILE_NAME)) {
                sw.WriteLine("New record added: {0}", currentDateTime);
                addDataToFile(sw);
            }
        } catch (Exception ex) {
            Debug.Log(ex.ToString());
        }
    }

    private void addDataToFile(StreamWriter sw) {
        sw.WriteLine("Czas trwania ewolucji " + dataToSave.getTimeInSecondsSinceStartup() + " sekund");
        sw.WriteLine("Do mety dotar³y " + dataToSave.getnumberOfFinishingCar() + " samochody");
        sw.WriteLine("Generacja " + dataToSave.getGenerationNumber());
        sw.WriteLine("Wielkoœæ populacji " + dataToSave.getPopulationSize());
        sw.WriteLine("Mutation chance " + dataToSave.getMutationChance());
        sw.WriteLine("Mutation strength " + dataToSave.getMutationStrength());
        sw.WriteLine("\n==============================\n");
    }
}
