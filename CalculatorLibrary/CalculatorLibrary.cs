using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public Calculator()
        {
            //// Traceクラスの設定
            //// アプリの実行時に同名ファイルを新規作成＆上書き
            //StreamWriter logFile = File.CreateText("calculator.log");
            //Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            //Trace.AutoFlush = true;
            //Trace.WriteLine("Startiing Calculator Log");
            //Trace.WriteLine(String.Format("Started{0}", System.DateTime.Now.ToString()));

            // JSONファイルクラスの設定
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            // JSONファイルの書き込み構成設定
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    // Traceクラスを入れることでログファイルに書き込みをする
                    //Trace.WriteLine(String.Format("{0}+{1}={2}", num1, num2, result));

                    // JSON用設定
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    //Trace.WriteLine(String.Format("{0}-{1}={2}", num1, num2, result));
                    writer.WriteValue("Subject");
                    break;
                case "m":
                    result = num1 * num2;
                    //Trace.WriteLine(String.Format("{0}*{1}={2}", num1, num2, result));
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        //Trace.WriteLine(String.Format("{0}/{1}={2}", num1, num2, result));
                    }
                    writer.WriteValue("Devide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }

            // JSONファイルの該当箇所に書き込み終了
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void Finish() // JSONファイルの全体書き込み終了
        {
            writer.WriteEndArray(); // 配列を閉じる
            writer.WriteEndObject(); // "Operations"を閉じる
            writer.Close(); // 全体を閉じる
        }
    }
}
