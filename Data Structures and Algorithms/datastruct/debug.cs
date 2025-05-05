using System;
using System.Collections.Generic;
using System.Diagnostics;

public class TaskExecutor
{
    private Queue<string> taskQueue = new Queue<string>();
    private const int MaxRetries = 3;

    /// <summary>
    /// Adds a task to the execution queue.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the task is null or empty, it is not added to the queue and a log message is written.
    /// </para>
    /// </remarks>
    /// <param name="task">The task to execute.</param>
    public void AddTask(string task)
    {
        if (string.IsNullOrWhiteSpace(task))
        {
            Log("Invalid task: null or empty. Skipped adding to queue.");
            return;
        }
        taskQueue.Enqueue(task);
        Log($"Task added: {task}");
    }

    /// <summary>
    /// Processes the tasks in the execution queue.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If a task fails, it is retried up to <see cref="MaxRetries"/> times. If the task still fails, it is skipped.
    /// </para>
    /// </remarks>
    public void ProcessTasks()
    {
        while (taskQueue.Count > 0)
        {
            string task = taskQueue.Dequeue();
            bool success = false;
            int attempt = 0;

            while (!success && attempt < MaxRetries)
            {
                attempt++;
                try
                {
                    ExecuteTask(task);
                    Log($"Task '{task}' completed successfully on attempt {attempt}.");
                    success = true;
                }
                catch (Exception ex)
                {
                    Log($"Error processing task '{task}' on attempt {attempt}: {ex.Message}");
                }
            }

            if (!success)
            {
                Log($"Task '{task}' failed after {MaxRetries} attempts. Skipped.");
            }
        }
    }

    private void ExecuteTask(string task)
    {
        // Simulated logic, replace with real work
        if (task.Contains("fail"))
        {
            throw new InvalidOperationException("Simulated task failure.");
        }

        Console.WriteLine($"Processing task: {task}");
    }

    private void Log(string message)
    {
        // Replace this with real logging in production
        Console.WriteLine($"[{DateTime.Now}] {message}");
    }
}
