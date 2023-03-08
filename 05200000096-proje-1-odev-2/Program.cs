using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp
{
    public class Program
    {
        static Random r = new Random(); //Defines a random value.
        public static void Main(string[] args)
        {
            //This n number defines how many dendrite the neuron will have
            Console.WriteLine("Enter number of values you want the neuron to get: ");
            int n = Convert.ToInt32(Console.ReadLine());
            //This part gets the lambda value
            Console.WriteLine("Enter lambda: ");
            double λ = Convert.ToDouble(Console.ReadLine());
            neuron test = new neuron(λ, n);
            // The answer  will be  explained later gets defined and assigned as "yes"
            string answer;
            string answer2;
            answer = "yes";
            answer2 = "no";
            //This loop continues until answer 1 is equal to yes which asked to user to determine is they want to contiue operating with same weights as the previous loop.
            while (answer == "yes")
            {
                // The number of epochs and how many data sets the current loop will get is taken
                Console.WriteLine("Enter number of epcochs you want your neuron to loop trough: ");
                int epoch = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the number of value sets you will enter later:   ");
                int valueS = Convert.ToInt32(Console.ReadLine());
                double[,] allvalues = new double[valueS, n]; // A list that takes all the values in the data set
                double acc = 0;
                for (int a = 0; a < epoch; a++) // Turns for each epoch.
                {
                    int correctnumber = 0; //Number of correct outputs in each epoch
                    for (int b = 0; b < valueS; b++) // Turns for each data set.
                    {
                        for (int i = 0; i < n; i++) // Turns for each value.
                        {
                            double xi; // Value
                            if (a == 0) // Only turns for the first epoch, othervise it turns for itself
                            {
                                Console.WriteLine("Enter one of the values: ");
                                xi = (Convert.ToDouble(Console.ReadLine())) / 10;
                                test.values.Add(xi);
                                allvalues[b, i] = xi;
                            }
                            else  // Turns for already taken values
                            {
                                xi = allvalues[b, i];
                                test.values.Add(xi);
                            }
                        }
                        test.findProduct();
                        test.findTarget();
                        test.findOutput();
                        if (answer2 == "no") // If no continues looping without educating.
                        {
                            test.Educate();
                        }
                        Console.WriteLine(test.istrue);
                        if (test.istrue == true) // Finds how many true outputs the neuron has given
                        {
                            correctnumber++;
                        }
                        Console.WriteLine(string.Join("\t", test.weights));
                        Console.WriteLine(test.target);
                        Console.WriteLine(test.output);
                        // Clears everything for the next data set.
                        test.values.Clear();
                        test.products.Clear();
                    }
                    Console.WriteLine(correctnumber);
                    Console.WriteLine(valueS);
                    //Finds and writes acc of current epoch
                    acc = (Convert.ToDouble(correctnumber) / Convert.ToDouble(valueS)) * 100;
                    Console.WriteLine("Acc of current epoch is: %" + acc);
                }
                Console.WriteLine("Acc of current computation is: %" + acc); // Prints the final acc which is acc of all current computation
                // Asks user if they want  keep using current weight values, if not stops the code. 
                Console.WriteLine("Keep the weight values? ( say \"yes\" if you want to  keep testing; else say \"no\")"); 
                string input = (Console.ReadLine());
                answer = input.ToLower();
                if(answer == "no") { Environment.Exit(0); }
                // Asks user if they want to start testing which stops the education process for the later loop. 
                Console.WriteLine("Start Testing? ( say \"yes\" if you want to  keep testing; elsesay \"no\")");
                string input2 = Console.ReadLine();
                answer2 = input2.ToLower();

            }
        }
    }
    public class neuron
    {
        public List<double> weights = new List<double>(); 
        public List<double> values = new List<double>(); 
        public List<double> products = new List<double>();
        public double w; //A singular wieght
        public int n; //This n number defines how many tails the neuron will have
        public int output; // output which could either be -1 or 1
        public double acoutput; //Actual output which is the answer of the "toplama işlevi"
        public int target; // target which could either be -1 or 1
        public double actarget; // Actual target which is sum of values in a singular data set
        public double λ;
        public bool istrue; // A boolean which checks if the outcome is true or not
        public neuron(double tλ, int tn) // Constructor a random weight value generated in here
        {
            n = tn;
            λ = tλ;
            for (int i = 0; i < n; i++)
            {
                w = (new Random().NextDouble() * 2) - 1;
                weights.Add(w);
            }
            istrue = false;

        }
        public void findProduct() // Does the xi.wi pat of the ''toplama işlevi''
        {
            for (int i = 0; i < n; i++)
            {
                products.Add(values[i] * weights[i]);
            }
        }
        public void findTarget() // Finds expected outcome of the addition process
        {
            if (values.Sum() > 0)
            {
                target = 1;
                actarget = values.Sum(); 
            }
            else if (values.Sum() < 0)
            {
                target = -1;
                actarget = values.Sum();
            }
        }
        public void findOutput() // Outcome of the "toplama işlevi"
        {
            if (products.Sum() > 0)
            {
                output = 1;
                acoutput = products.Sum(); 
            }
            else if (products.Sum() < 0)
            {
                output = -1;
                acoutput = products.Sum();
            }
        }
        public void Educate()  // Does the educating process acording to given equation
        {
            if ((target > output) || (target < output))
            {
                for (int i = 0; i < n; i++)
                {
                    weights[i] += λ * (actarget - acoutput) * values[i];
                }
            }
            else { istrue = true; }
        }
    }
}