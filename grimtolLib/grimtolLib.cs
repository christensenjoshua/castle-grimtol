using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grimtolLib
{
    public class Gameplay
    {
        internal int PosX { get; set; } = 2;
        internal int PosY { get; set; } = 0;
        internal List<string> Items { get; set; } = new List<string>();
        internal Room[,] Map { get; set; }
        internal bool Playing { get; set; } = false;


        public string StartGame()
        {
            CreateRooms();
            Playing = true;
            string addon = Look();
            return $"You have entered the castle.\n{addon}\nPress any key to continue.";
        }
        public bool GameState()
        {
            return Playing;
        }
        public string ProcessCommand(string cmd)
        {
            if (cmd == "")
            {
                return "Invalid action!";
            }
            cmd = cmd.ToUpper();
            cmd.Trim();
            string firstPart;
            string secondPart;
            if(cmd.Contains(' '))
            {
                firstPart = cmd.Split(' ')[0];
                secondPart = cmd.Split(' ')[1];
            }
            else
            {
                firstPart = cmd;
                secondPart = "";
            }
            // need to handle split when command is only one thing.
            Room currRoom = Map[PosX, PosY];
            switch (firstPart)
            {
                case "GO":
                    // NEED TO COMPENSATE FOR BAD INPUTS
                    switch (secondPart.Substring(0, 1))
                    {
                        case "N":
                            if (PosX <= 0)
                            {
                                return "Invalid Selection";
                            }
                            PosX--;
                            break;
                            ;
                        case "S":
                            if (PosX >= 2)
                            {
                                return "Invalid Selection";
                            }
                            PosX++;
                            break;
                            ;
                        case "W":
                            if (PosY <= 0)
                            {
                                return "Invalid Selection";
                            }
                            PosY--;
                            break;
                            ;
                        case "E":
                            if (PosY >= 2)
                            {
                                return "Invalid Selection";
                            }
                            PosY++;
                            break;
                            ;
                        default:
                            return "Invalid selection"
                            ;
                    }
                    return Look();
                case "H":
                case "HELP":
                    return "[GO] North, South, East, West\n" +
                        "[HELP]\n" +
                        "[INV]entory\n" +
                        "[LOOK] around\n" +
                        "[TAKE] something from the room.\n" +
                        "[USE] an [ITEMNAME]\n" +
                        "[EX]it."
                    ;
                case "INV":
                    string itemResp = "Inventory:";
                    foreach (string item in Items)
                    {
                        itemResp += $"\n{item}";
                    }
                    return itemResp;
                    ;
                case "EX":
                case "EXIT":
                    Playing = false;
                    return "Thanks for playing!";
                    ;
                case "LOOK":
                    return Look();
                    ;
                case "TAKE":
                    if(currRoom.item == "")
                    {
                        return "There is nothing here for you to take.";
                    }
                    Items.Add(currRoom.item);
                    currRoom.item = "";
                    return $"You took the -{currRoom.item}-.";
                    ;
                case "USE":
                    // no option, sho inventory
                    if (currRoom.name != "mysterious room")
                    {
                        return "Nothing for you to do here.";
                    }
                    if (secondPart == "")
                    {
                        string inventory = "Inventory:";
                        foreach (string item in Items)
                        {
                            inventory += $"\n{item}";
                        }
                        return inventory;
                    }
                    else
                    {
                        string itemName = secondPart.ToLower();
                        if(Items.Contains(itemName)){
                            if(itemName == "key" && Items.Contains("dog treat")){
                                // win condition
                                Playing = false;
                                return $"Trying to use {itemName}\n" +
                                "You open the locked door."+
                                "A big dog sits on the other side...\n"+
                                "He looks at you hungrily, and you remember you have a dog treat.\n" +
                                "You give the dog the dog treat.\n" +
                                "You and the dog leave the castle together; he's a good boy!!";
                            }
                            if(itemName == "key")
                            {
                                // lose condition
                                Playing = false;
                                return $"Trying to use {itemName}\n" +
                                "You open the locked door.\n" +
                                "A big dog sits on the other side...\n" +
                                "He gobbles you up and you die!\n" +
                                "Please play again!";
                            }
                            return "You cannot use that here.";
                        }
                        else
                        {
                            return "You do not have that item.";
                        }
                    }
                    ;
                default:
                    return "Invalid action! Type H for Help.";
            }
        }
        internal class Room
        {
            public string name;
            public string item;
            public string description;
            public Room(string aName, string anItem, string aDescription)
            {
                name = aName;
                item = anItem;
                description = aDescription;
            }
        }
        internal void CreateRooms()
        {
            Room kitchen = new Room("kitchen", "", "The kitchen is absolutely filthy. There is an empty dog bowl on the floor.");
            Room bedroom = new Room("bedroom", "key", "The bedroom is in disarray.  You see something glittering on the bed.");
            Room den = new Room("den", "dog treat", "The den is surprisingly clean. There are dog treats scattered across the floor.");
            Room empty = new Room("empty room", "", "The room appears empty.");
            Room final = new Room("mysterious room", "", "The room feels odd to you. There is a locked door in the corner");
            Map = new Room[3, 3] {
            { bedroom, empty, den},
            { empty, kitchen, empty},
            { empty, empty, final}
            };
        }
        internal string Look()
        {
            Room currRoom = Map[PosX, PosY];
            return $"You arrive in the {currRoom.name}. {currRoom.description}";
        }
    }
}
