﻿using JsonDocumentsManager;
using OpenQA.Selenium;
using StatesAndEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRobot;
using TheRobot.Requests;

namespace Liberty.PagesStates;

public class ProcessaAlertas : BaseState
{
    public ProcessaAlertas(Robot robot, InputJsonDocument inputdata, ResultJsonDocument resultJson) : base("ProcessaAlertas", robot, inputdata, resultJson)
    {
    }

    public override void Execute()
    {
        var alertas = _robot.Execute(new GetElementsListRequest
        {
            By = By.XPath("//div[@role='alert']")
        }).Result.WebElements;

        //TODO: Register alerts on result.
        foreach (var alert in alertas)
        {
            _results.AddResultMessage("Mensagem [CIA]", alert.Text);

            if (alert.Text.Contains("Fator de ajuste com restrição"))
            {
                _robot.Execute(new SetTextWithKeyDownAndBackSpaceRequest()
                {
                    BackSpaceNumber = 7,
                    By = By.Id("TipoCobertura_FatorAjuste_1"),
                    Text = "10000"
                }).Wait();
            }
            if (alert.Text.Contains("A cobertura de Peças"))
            {
                _robot.Execute(new SelectBy2ClicksRequest()
                {
                    By1 = By.XPath("//div[@id='PecasReposicao_1_chosen']//b"),
                    By2 = By.XPath("//div[@id='PecasReposicao_1_chosen']//li[contains(text(),'ORIGINAIS')]")
                }).Wait();
            }
        }

        Console.WriteLine(_results.GetDocument());

        _robot.Execute(new ClickRequest()
        {
            By = By.Id("btnCalcular"),
            Timeout = TimeSpan.FromSeconds(1),
            DelayAfter = TimeSpan.FromSeconds(20)
        }).Wait();
    }
}