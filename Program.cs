﻿using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";
// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
logger.Info("Program started");
string file = "mario.csv";
// make sure movie file exists
if (!File.Exists(file))
{
    logger.Error("File does not exist: {File}", file);
}
else
{
        // create parallel lists of character details
    // lists are used since we do not know number of lines of data
    List<Character> characters = [];
        // to populate the lists with data, read from the data file
    try
    {
        StreamReader sr = new(file);
        // first line contains column headers
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
                        if (line is not null)
            {
                Character character = new();
                // character details are separated with comma(,)
                string[] characterDetails = line.Split(',');
                // 1st array element contains id
                character.Id = UInt64.Parse(characterDetails[0]);
                // 2nd array element contains character name
                character.Name = characterDetails[1] ?? string.Empty;
                // 3rd array element contains character description
                character.Description = characterDetails[2] ?? string.Empty;
                // 4th array is the species
                character.Species.Add(characterDetails[3]) ?? string.Empty;
                // 5th array is game first appeared in
                character.FirstAppearances.Add(characterDetails[4]) ?? string.Empty;
                // 6th array is year appered
                character.YearCreated.Add(int.Parse(characterDetails[5])) ?? string.Empty;
                characters.Add(character);
            }
        }
        sr.Close();
    }
    catch (Exception ex)
    {
        logger.Error(ex.Message);
    }
        string? choice;
    do
    {
        // display choices to user
        Console.WriteLine("1) Add Character");
        Console.WriteLine("2) Display All Characters");
        Console.WriteLine("Enter to quit");
        // input selection
        choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);
        if (choice == "1")
        {
            // Add Character
            Character character = new();
                        Console.WriteLine("Enter new character name: ");
            character.Name = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(character.Name)){
                                // check for duplicate name
                List<string> LowerCaseNames = character.ConvertAll(character => character.Name.ToLower());
                if (LowerCaseNames.Contains(character.Name.ToLower()))
                {
                    logger.Info($"Duplicate name {character.Name}");
                }
                else
                {
                    // generate id - use max value in Ids + 1
                    character.Id = characters.Max(character => character.Id) + 1;
                                        // input character description
                    Console.WriteLine("Enter description:");
                    character.Description = Console.ReadLine() ?? string.Empty;
                    // input species
                    Console.WriteLine("Enter species:");
                    character.SpeciesInput = Console.ReadLine() ?? string.Empty;
                    // input first appearance
                    Console.WriteLine("Enter first appearance:");
                    character.FirstAppearance = Console.ReadLine() ?? string.Empty;
                    // input year created
                    Console.WriteLine("Enter year created: ");
                    character.yearCreated = Console.ReadLine() ?? string.Empty;
                    // Console.WriteLine($"{Id}, {Name}, {Description}");
                    // create file from data
                    StreamWriter sw = new(file, true);
                    sw.WriteLine($"{character.Id},{character.Name},{character.Description},{charcter.SpeciesInput},{charcter.FirstAppearance},{character.yearCreated}");
                    sw.Close();
                    // add new character details to Lists
                    characters.Add(character);
                    // log transaction
                    logger.Info($"Character id {character.Id} added");
                }
            } else {
                logger.Error("You must enter a name");
            }
        }
        else if (choice == "2")
        {
            // Display All Characters
                        // loop thru List
            foreach(Character character in characters)
            {
                Console.WriteLine(character.Display());
            }
        }
    } while (choice == "1" || choice == "2");
}
logger.Info("Program ended");