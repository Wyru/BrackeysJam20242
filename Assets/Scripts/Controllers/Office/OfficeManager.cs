using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    public static OfficeManager instance;
    [SerializeField] private TMP_Text tasksText;
    private List<Task> tasks = new List<Task>();
    private int currentTaskIndex = 0;
    private bool isAtComputer = false;
    public bool fullWorkDone = false;
    public bool clockIn = false;
    private Task finalTask;
    private Task leaveTask;
    public List<string> ListaClockIn;
    public DefaultCanvasBehavior canvas;

    void Awake()
    {
        instance = this;
        GameManager.OnSatisfactionChange += GameManagerOnSatisfactionChange;
        GameManager.OnWorkDoneDayChange += GameManagerOnWorkDoneTodayChange;
        canvas = FindAnyObjectByType<DefaultCanvasBehavior>();
        tasksText = canvas._taskText;
        tasksText.gameObject.SetActive(true);

        InitializeTasks();
        UpdateTasksText();
    }

    void InitializeTasks()
    {
        Debug.Log("Test");
        tasks.Add(new Task(TaskType.ClockIn, "Clock-In on the work!"));
        tasks.Add(new Task(TaskType.WorkAtComputer, "Go to the computer and work at least once!"));
    }

    void GameManagerOnSatisfactionChange(int value, int satisfaction, int maxSatisfaction, int globalSatisfaction)
    {
        if (satisfaction >= 100 && finalTask != null && !finalTask.IsCompleted)
        {
            fullWorkDone = true;
            CompleteFinalTask();
        }
    }

    void GameManagerOnWorkDoneTodayChange(int value, int workDoneToday)
    {
        if (tasks[currentTaskIndex].TaskType == TaskType.WorkAtComputer && workDoneToday >= 1)
        {
            CompleteTask();
        }
    }

    // Handle task completion logic
    public void PlayerClockIn()
    {
        if (tasks[currentTaskIndex].TaskType == TaskType.ClockIn)
        {
            DialogSystemController.ShowDialogs(ListaClockIn);
            clockIn = true;
            CompleteTask();
        }
    }

    private void CompleteTask()
    {
        tasks[currentTaskIndex].CompleteTask();
        UpdateTasksText();
        currentTaskIndex++;

        if (currentTaskIndex < tasks.Count)
        {

        }
        else
        {
            AddFinalTask();
        }
    }

    private void AddFinalTask()
    {
        finalTask = new Task(TaskType.Leave, "You can leave or work more!");
        tasks.Add(finalTask);

        UpdateTasksText();
    }

    private void CompleteFinalTask()
    {
        finalTask.CompleteTask();
        currentTaskIndex = 0;
        AddLeaveTask();
        UpdateTasksText();
    }

    private void AddLeaveTask()
    {
        tasks.Clear();
        leaveTask = new Task(TaskType.Leave, "Work done! You can leave");
        tasks.Add(leaveTask);

        UpdateTasksText();
    }

    public void EnterComputer()
    {
        if (tasks[currentTaskIndex].TaskType == TaskType.WorkAtComputer)
        {
            isAtComputer = true;
        }
    }

    public void LeaveComputer()
    {
        if (isAtComputer && GameManager.instance.satistafactionToday < GameManager.instance.maxSatistafaction)
        {
            isAtComputer = false;
        }
    }

    private void UpdateTasksText()
    {
        tasksText.text = "Tasks:\n";
        for (int i = 0; i < tasks.Count; i++)
        {
            string descriptionColor = tasks[i].IsCompleted ? "#00FF00" : "#FFFFFF";
            string statusColor = tasks[i].IsCompleted ? "#00FF00" : "#FF0000";

            // Format the task line with rich text
            string status = tasks[i].IsCompleted
                ? $"<s><color={statusColor}>Done</color></s>"
                : $"<color={statusColor}>Pending</color>";

            tasksText.text += $"{i + 1}. <color={descriptionColor}>{tasks[i].Description}</color> - {status}\n";
        }
    }

    void OnDestroy()
    {
        GameManager.OnSatisfactionChange -= GameManagerOnSatisfactionChange;
        GameManager.OnWorkDoneDayChange -= GameManagerOnWorkDoneTodayChange;
        tasksText.text = "Go home!";
    }
}
