using System;
using System.Linq;
using AForge.Neuro;
using AForge.Neuro.Learning;
using System.IO;

namespace MachineLearning
{
	public class BackPropagation
	{
		string outputDir = @"F:/FCE/Output/";
		public void UpdateNeuralNet(string csvString, string trainedNetName)
		{
			if (!File.Exists(outputDir + "dataset/" + trainedNetName + ".tmp"))
			{
				var lines = csvString.Split('\n');
				var csv = from line in lines
						  select (line.Split(',')).ToArray();

				double[][] inputs = new double[150][];
				double[][] outputs = new double[150][];
				string[] types = new string[150];
				int rowIndex = 0;
				foreach (var row in csv)
				{
					int colIndex = 0;
					inputs[rowIndex] = new double[4];
					foreach (var col in row)
					{
						if (colIndex < row.Length - 1)
						{
							inputs[rowIndex][colIndex] = double.Parse(col);
						}
						else
						{
							types[rowIndex] = col;
						}
						colIndex++;
					}
					rowIndex++;
				}
				string[] distinct = types.Select(o => o.Replace("\r", "")).Distinct().ToArray();
				PutDistinctLevel(trainedNetName, distinct);
				BackPropagation bp = new BackPropagation();
				rowIndex = 0;
				foreach (var row in types)
				{
					outputs[rowIndex] = new double[3];
					outputs[rowIndex] = bp.getIndex(row.Replace("\r", ""), distinct);
					rowIndex++;
				}

				// create neural network
				ActivationNetwork network = new ActivationNetwork(new SigmoidFunction(0.43), 4, 8, 3);
				BackPropagationLearning teacher = new BackPropagationLearning(network);
				teacher.LearningRate = 0.2;
				teacher.Momentum = 0;
				// loop
				double error = 1;
				while (error > 0.01)
				{
					// run epoch of learning procedure
					error = teacher.RunEpoch(inputs, outputs);
					Console.WriteLine("error is: " + error);
					// check error value to see if we need to stop
					// ...
				}

				network.Save(outputDir + @"dataset/" + trainedNetName + ".tmp");
			}
		}

		public string EvaluateData(string trainedNetName, string[] distinct, double[] data)
		{
			BackPropagation bp = new BackPropagation();
			Network loadedNet = Network.Load(outputDir + @"dataset/"+ trainedNetName + ".tmp");

			double[] test = loadedNet.Compute(data);
			return bp.getResult(test, distinct);
		}

		public string[] GetDistinctLevel(string trainedNetName)
		{
			string text = File.ReadAllText(outputDir + @"dataset/" + trainedNetName + "_dis.tmp");
			string[] lines = text.Split(',');

			return lines;
		}

		public void PutDistinctLevel(string trainedNetName, string[] lines)
		{
			string fileContent = string.Join(",", lines);
			File.WriteAllText(outputDir +@"dataset/" + trainedNetName + "_dis.tmp", fileContent);
		}

		public double[] getIndex(string item, string[] distinct)
		{
			double[] index = { -1, -1, -1 };
			for (int i = 0; i < distinct.Length; i++)
			{
				if (distinct[i] == item)
				{
					if ((i + 1) == 1)
					{
						index[0] = 0;
						index[1] = 0;
						index[2] = 1;
					}
					else if ((i + 1) == 2)
					{
						index[0] = 0;
						index[1] = 1;
						index[2] = 0;
					}
					else if ((i + 1) == 3)
					{
						index[0] = 1;
						index[1] = 0;
						index[2] = 0;
					}
				}
			}
			return index;
		}

		public string getResult(double[] output, string[] expected)
		{
			string temp = Math.Round(output[0]).ToString() + Math.Round(output[1]) + Math.Round(output[2]);
			if (temp.Equals("001"))
			{
				return expected[0];
			}
			else if (temp.Equals("010")) {
				return expected[1];
			}
			else if(temp.Equals("100"))
			{
				return expected[2];
			}
			return "not sure";
		}
	}
}
