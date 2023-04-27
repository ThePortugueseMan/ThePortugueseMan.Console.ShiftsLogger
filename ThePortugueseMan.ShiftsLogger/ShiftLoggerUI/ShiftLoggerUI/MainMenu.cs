using ConsoleTableExt;
using ShiftLogger.Models;
using ShiftService;

namespace Screens;

public class MainMenu
{
    AskInput.AskInput askInput = new();
    ShiftService.ShiftService shiftService = new ();
    public void View()
    {
        bool exitMenu = false;
        List<object> optionsString = new List<object> {
            "1 - View all shifts",
            "2 - Enter a new shift",
            "3 - Edit a shift",
            "4 - Delete a shift",
            "0 - Exit App"};

        while (!exitMenu)
        {
            Console.Clear();
            ConsoleTableBuilder.From(optionsString)
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .WithColumn("Shifts Logger")
                .ExportAndWriteLine();
            Console.Write("\n");
            switch (askInput.PositiveNumber("Please select a valid option"))
            {
                case 0: exitMenu = true; break;
                case 1: ViewShifts(); break;
                case 2: EnterShift(); break;
                case 3: EditShift(); break;
                case 4: DeleteShifts();  break;
                default: break;
            }
        }
        return;
    }

    private void DisplayAllShifts()
    {
        Console.Clear();
        TableVisualization.TableVisualization.ShowTable(shiftService.GetShifts(), "Shifts");
    }

    public void ViewShifts()
    {
        DisplayAllShifts();
        askInput.AnyKeyToContinue();
    }

    public void EnterShift()
    {
        Console.Clear();
        string workerId;
        DateTime startTime;
        DateTime endTime;

        Console.WriteLine("Write your worker ID");
        workerId = Console.ReadLine();

        DateTime[] interval = askInput.DateIntervalWithHours("Insert shift start time.", "Insert end time.");
        if (interval is null) return;

        ShiftItem newShift = new ShiftItem
        {
            WorkerId = workerId,
            StartTime = interval[0],
            EndTime = interval[1]
        };

        shiftService.PostNewShift(newShift);
    }

    public void EditShift()
    {
        int input = -1;

        while (input != 0)
        {
            DisplayAllShifts();
            input = askInput.PositiveNumber("Write the index of the shift you want to edit, or 0 to return");

            if (input == 0) continue;

            if (!shiftService.CheckIfShiftExistsAtIndex(input))
            {
                Console.WriteLine($"Shift {input} not found...");
                askInput.AnyKeyToContinue();
                continue;
            }
            else
            {
                ShiftItem oldShift = shiftService.GetShiftAtIndex(input);
                List<ShiftItem> visualizeShift = new List<ShiftItem>
                {
                    oldShift
                };
                TableVisualization.TableVisualization.ShowTable(visualizeShift, "Shift");

                Console.WriteLine("Write the new worker ID");
                oldShift.WorkerId = Console.ReadLine();

                DateTime[] interval = askInput.DateIntervalWithHours("Insert shift start time.", "Insert end time.");
                if (interval is null) return;
                
                oldShift.StartTime= interval[0];
                oldShift.EndTime= interval[1];

                shiftService.UpdateShift(oldShift);

            }
        }
        return;
    }


    public void DeleteShifts()
    {
        int input = -1;

        while(input !=0)
        {
            DisplayAllShifts();
            input = askInput.PositiveNumber("Write the index of the shift you want to delete, or 0 to return");

            if (input == 0) continue;

            shiftService.CheckIfShiftExistsAtIndex(input);

            if (shiftService.DeleteShift(input))
            {
                Console.WriteLine($"Shift {input} deleted sucessfully!");
            }
            else Console.WriteLine("Couldn't delete the shift...");
            
            askInput.AnyKeyToContinue();
            Console.Clear();
        }
        return;
    }
}
