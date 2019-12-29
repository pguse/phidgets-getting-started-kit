using System;
using Phidget22;
using Phidget22.Events;

namespace SimonGame
{
    class Program
    {
        static bool turnRedLEDOn = false;
        static bool turnGreenLEDOn = false;
        static bool flash = true;
        static string buttonClicks = "";

        private static void redButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true){
                if (turnRedLEDOn){
                    turnRedLEDOn = false;
                } else {
                    turnRedLEDOn = true;
                    buttonClicks += "r";
                }
            }
        }

        private static void greenButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true){
                if (turnGreenLEDOn){
                    turnGreenLEDOn = false;
                } else {
                    turnGreenLEDOn = true;
                    buttonClicks += "g";
                }
            }
        }

        static string createSequence(int n){
            Random rnd = new Random();
            var s = "";
            string ch;
            int c;

            for(var i=0; i < n; i++){
                c = rnd.Next(0,2);
                if (c == 0){
                    ch = "r";
                } else {
                    ch = "g";
                }
                s = s + ch;
            }

            return s;
        }
        static void Main(string[] args)
        {
            bool programIsRunning = true;
            int numBlinks = 1;
            int score = 0;
            string sequence;
            
            //create objects
            DigitalInput redButton = new DigitalInput();
            DigitalInput greenButton = new DigitalInput();
            DigitalOutput redLED = new DigitalOutput();
            DigitalOutput greenLED = new DigitalOutput();

            //address objects
            redButton.HubPort = 0;
            redButton.IsHubPortDevice = true;
            redLED.HubPort = 1;
            redLED.IsHubPortDevice = true;

            greenButton.HubPort = 5;
            greenButton.IsHubPortDevice = true;
            greenLED.HubPort = 4;
            greenLED.IsHubPortDevice = true;

            //add event handlers
            redButton.StateChange += redButton_StateChange;
            greenButton.StateChange += greenButton_StateChange;

            //open objects
            redButton.Open(1000);
            redLED.Open(1000);
            greenButton.Open(1000);
            greenLED.Open(1000);

            sequence = createSequence(numBlinks);

            // randomly light LED's 10 times
            while (programIsRunning){
                
                if (flash){
                    foreach (char ch in sequence) {
                        if (ch == 'r'){
                            redLED.State = true;
                            System.Threading.Thread.Sleep(300);
                            redLED.State = false;
                        } else {
                            greenLED.State = true;
                            System.Threading.Thread.Sleep(300);
                            greenLED.State = false;
                        }
                        System.Threading.Thread.Sleep(300);
                    } 
                    flash = false;
                }
                
                if (turnRedLEDOn){
                    redLED.State = true;
                    System.Threading.Thread.Sleep(300);
                    redLED.State = false;
                    turnRedLEDOn = false;
                }

                if (turnGreenLEDOn){
                    greenLED.State = true;
                    System.Threading.Thread.Sleep(300);
                    greenLED.State = false;
                    turnGreenLEDOn = false;
                }

                if (sequence.Length == buttonClicks.Length){
                    if (sequence.Equals(buttonClicks)){
                        score += numBlinks;
                        numBlinks++;
                        sequence = createSequence(numBlinks);
                        buttonClicks = "";
                        flash = true;
                        Console.WriteLine("Correct!  Here comes Level {0} ...", numBlinks);
                        System.Threading.Thread.Sleep(1000);
                    } else {
                        Console.WriteLine("Score: {0}", score);
                        programIsRunning = false;
                    }
                }

                if (Console.KeyAvailable){
                    Console.WriteLine("Ending Program");
                    Console.WriteLine("Score: {0}", score);
                    programIsRunning = false;
                }
                //sleep for 150 milliseconds before checking keys again
                System.Threading.Thread.Sleep(150);
            }

            //close objects
            redButton.Close();
            redLED.Close();
            greenButton.Close();
            greenLED.Close();
        }
    }
}
