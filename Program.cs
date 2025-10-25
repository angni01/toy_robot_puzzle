using System;
using System.Drawing;
using System.Text.RegularExpressions;

public class SharedContext
{
    public string[] faces { get; set; } = {"NORTH", "EAST", "SOUTH", "WEST"};
    public int currX { get; set; } = 0;
    public int currY { get; set;} = 0;
    public string direction { get; set; } = "";

    public SharedContext(string[]? args = null)
    {
        // For Unit test
        if (args != null)
        {
            currX = int.Parse(args[0]);
            currY = int.Parse(args[1]);
            direction = args[2];
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        
        var context = new SharedContext();
        var action = new Action(context);
        //string[] faces = {"NORTH", "EAST", "SOUTH", "WEST"};
        //int currX=0;
        //int currY=0;
        //string direction="";

        // For Unit test
        if (args.Length > 0)
        {
            context.currX = int.Parse(args[0]);
            context.currY = int.Parse(args[1]);
            context.direction = args[2];
        }

        Console.WriteLine("Toy Robot Puzzle!");
        string command = Console.ReadLine() ?? "";

        while (context.direction.Length == 0 || command != "REPORT")
        {
            string[] cmdPart = command.Split(' ');
            string act = cmdPart[0];

            if (context.direction.Length > 0 || act == "PLACE")
            {
                switch (act)
                {
                    case "PLACE":
                        if (cmdPart.Length > 1) action.PlaceRobot(cmdPart[1]);
                        break;
                    case "MOVE":
                        action.MoveRobot();
                        break;
                    case "LEFT":
                        action.ChangeDirection("LEFT");
                        break;
                    case "RIGHT":
                        action.ChangeDirection("RIGHT");
                        break;
                    default: break;
                }
            }
            command = Console.ReadLine() ?? "";
        }
        Console.WriteLine(context.currX + ", " + context.currY + " " + context.direction); 
    }
}

public class Action
{
    private readonly SharedContext _context;

    public Action(SharedContext context)
    {
        _context = context;
    }

    public void PlaceRobot(string cmdValue){
            string[] value = cmdValue.Split(',');
            if(_context.direction.Length == 0)
            {
                bool isValid = Regex.IsMatch(cmdValue, @"^[0-4],[0-4],[A-Z]+$");
                if(isValid){
                    _context.currX = int.Parse(value[0]);
                    _context.currY = int.Parse(value[1]);
                    _context.direction = value[2];
                }
            }
            else{
                _context.currX = int.Parse(value[0]);
                _context.currY = int.Parse(value[1]);
                if(value.Length > 2){
                    _context.direction = value[2];
                }
            }         
        } 
    
    public void ChangeDirection(string dir){
            int dirIdx = Array.IndexOf(_context.faces, _context.direction);
            int newIdx = dir == "LEFT" ? dirIdx - 1 : dirIdx + 1;
            if(newIdx < 0)
            {
                newIdx = 3;
            }
            else if ( newIdx > 3)
            {
                newIdx = 0;
            }
            _context.direction = _context.faces[newIdx];
            
        }
    
    public void MoveRobot()
        {
            switch(_context.direction)            {
                case "NORTH": if(_context.currY + 1 < 5) _context.currY = _context.currY + 1; 
                            break;
                case "EAST": if(_context.currX + 1 < 5) _context.currX = _context.currX + 1;  
                            break;
                case "SOUTH": if(_context.currY - 1 >= 0) _context.currY = _context.currY - 1;  
                            break;
                case "WEST": if(_context.currX - 1 >= 0) _context.currX = _context.currX - 1;  
                            break;
                default: break;
            }
        }
}