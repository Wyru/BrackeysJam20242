using System;
using UnityEngine;

public enum TaskType
{
    ClockIn,
    WorkAtComputer,
    Leave
}

public class Task
{
    public TaskType TaskType { get; private set; }
    public bool IsCompleted { get; private set; }
    public string Description { get; private set; }

    public Task(TaskType taskType, string description)
    {
        TaskType = taskType;
        Description = description;
        IsCompleted = false;
    }

    public void CompleteTask()
    {
        IsCompleted = true;
    }
}