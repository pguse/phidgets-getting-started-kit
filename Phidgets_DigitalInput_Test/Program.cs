﻿using System;
using Phidget22;
using Phidget22.Events;

namespace Phidgets_DigitalInput_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            bool programIsRunning = true;

            //create object
            DigitalInput redButton = new DigitalInput();

            //address object
            redButton.HubPort = 0;
            redButton.IsHubPortDevice = true;
            redButton.StateChange += redButton_StateChange;

            //open object
            redButton.Open(1000);

            while (programIsRunning){
                
                //check for key press
                if (System.Console.KeyAvailable){
                    System.Console.WriteLine("Ending Program");
                    programIsRunning = false;
                }

                System.Threading.Thread.Sleep(150);
            }
            

            //close object
            redButton.Close();
        }

        private static void redButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            //read digital input state
                if (e.State == true){
                    System.Console.WriteLine("Red Button Pressed");
                } else {
                    System.Console.WriteLine("Red Button Not Pressed");
                }
        }
    }
}
