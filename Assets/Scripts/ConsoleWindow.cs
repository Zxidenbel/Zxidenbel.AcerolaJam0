using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ConsoleWindow : MonoBehaviour
{
    private TextMeshProUGUI Text;
    public int topIndex = 0;
    private int lastTopIndex = 0;
    void Start()
    {
        Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if(topIndex > 32 + Console.ConsoleLines.Count)
        {
            topIndex = 32 + Console.ConsoleLines.Count;
        }
        if(topIndex < 0)
        {
            topIndex = 0;
        }
        if(Console.LogUpdated || lastTopIndex != topIndex)
        {
            StringBuilder _consoleText = new();
            int index = 0;
            foreach(string line in Console.ConsoleLines)
            {
                index++;
                if(Console.ConsoleLines.Count - index < topIndex)
                {
                    continue;
                }
                _consoleText.Insert(0, line + "\n");
            }
            Text.text = _consoleText.ToString();
            Console.LogUpdated = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)) // test cases
        {
            Console.Log("Space pressed.");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Console.Log("G pressed.");
        }
        lastTopIndex = topIndex;
    }

    public void Scroll(int speed)
    {
        topIndex += speed;
    }
}

public static class Console
{
    public static Queue<string> ConsoleLines;
    public static bool LogUpdated;

    static Console()
    {
        ConsoleLines = new Queue<string>();
        Console.LogUpdated = false;
    }

    public static void Log(string message)
    {
        ConsoleLines.Enqueue(message);
        LogUpdated = true;
        if (ConsoleLines.Count > 128)
        {
            ConsoleLines.Dequeue();
        }
    }
}
