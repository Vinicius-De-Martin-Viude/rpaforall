﻿using Json.Path;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace StatesAndEvents;

public class BaseOrcamento : IDadosOrcamento
{
    public JsonDocument JsonDoc { get; set; }

    public BaseOrcamento(string jsonFilePath)
    {
        string text = File.ReadAllText(jsonFilePath);
        JsonDoc = JsonDocument.Parse(text);
    }

    public string GetData(JsonPath jsonPath)
    {
        var matches = jsonPath.Evaluate(JsonDoc.RootElement).Matches;
        if (matches.Count == 0)
        {
            return String.Empty;
        }
        return matches[0].Value.ToString();
    }

    public Task ReadJson(string jsonFilePath)
    {
        throw new NotImplementedException();
    }

    public string GetData(string jsonPath)
    {
        var theJsonPath = JsonPath.Parse(jsonPath);
        return GetData(theJsonPath);
    }
}