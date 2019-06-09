using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class HighScoreManager
{
    private List<HighScore> _highScores = new List<HighScore>();

    private FileStream _fileHandle;
    private DataContractJsonSerializer _jsonSerializer;
    
    public HighScoreManager(string file)
    {
        try
        {
            _fileHandle = File.Open(file, FileMode.OpenOrCreate);
            _jsonSerializer = new DataContractJsonSerializer(typeof(List<HighScore>));
            if (_fileHandle.Length > 0)
            {
                _highScores = (List<HighScore>) _jsonSerializer.ReadObject(_fileHandle);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateOrInsertHighScore(string initials, float time)
    {
        var existing = _highScores.FirstOrDefault(hs => hs.Initials == initials);
        if (existing != null)
        {
            if (existing.Time > time)
            {
                existing.Time = time;
            }
        }
        else
        {
            _highScores.Add(new HighScore(initials, time));
        }

        _fileHandle.Position = 0;
        _jsonSerializer.WriteObject(_fileHandle, _highScores);
        _fileHandle.Flush();
    }

    public List<HighScore> GetSortedHighScore()
    {
        _highScores.Sort((s1, s2) => s1.Time.CompareTo(s2.Time));
        return _highScores;
    }
}
