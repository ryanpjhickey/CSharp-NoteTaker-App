using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

private static string noteData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Notes\";

  private static void readCommand()
      {

           Console.Write(Directory.GetDirectoryRoot(noteData));
           string readConsole = Console.ReadLine();

           switch (readConsole.ToLower())
           {

               case "new":
                   newNote();
                   Main(null);
                   break;
               case "edit":
                   editNote();
                   Main(null);
                   break;
               case "read":
                   readNote();
                   Main(null);
                   break;
               case "delete":
                   deleteNote();
                   Main(null);
                   break;
               case "shownotes":
                   showNotes();
                   Main(null);
                   break;
               case "dir":
                   notesData();
                   Main(null);
                   break;
               case "cls":
                   Console.Clear();
                   Main(null);
                   break;
               case "exit":
                   exit();
                   break;
               default:
                   commandsAvailable();
                   Main(null);
                   break;
           }
       }

readCommand();
Console.ReadLine();

Main() {
    newNote() {
        Console.WriteLine("Please Enter Note:\n");
        string input = Console.ReadLine();
        XmlWriterSettings noteSettings = new XmlWriterSettings();
        noteSettings.CheckCharacters = false;
        noteSettings.ConformanceLevel = ConformanceLevel.Auto;
        noteSettings.Indent = true;
        string fileName = DateTime.Now.ToString("dd-MM-yy") + ".xml";

        using (XmlWriter newNote = XmlWriter.Create(noteData + fileName, noteSettings))
            {
                newNote.WriteStartDocument();
                newNote.WriteStartElement("Note");
                newNote.WriteElementString("body", input);
                newNote.WriteEndElement();
                newNote.Flush();
                newNote.Close();
            }
    }

    editNote() {

        Console.WriteLine("Please enter file name.\n");
        string fileName = Console.ReadLine().ToLower();

            if (File.Exists(noteData + fileName))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(noteData + fileName);
                    Console.Write(doc.SelectSingleNode("//body").InnerText);
                    string readInput = Console.ReadLine();
                    if (readInput.ToLower() == "cancel")
                    {
                        Main(null);
                    }
                    else
                    {
                        string newText = doc.SelectSingleNode("//body").InnerText = readInput;
                        doc.Save(noteData + fileName);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not edit note following error occurred: " + ex.Message);
                }

            }
            else
            {
                Console.WriteLine("File not found\n");
            }
    }




    deleteNote() {

            Console.WriteLine("Please enter file name\n");
            string fileName = Console.ReadLine();

            if (File.Exists(noteData + fileName))

            {
                Console.WriteLine(Environment.NewLine + "Are you sure you wish to delete this file? Y/N\n");
                string confirmation = Console.ReadLine().ToLower();
                if (confirmation == "y")

                {
                    try
                    {
                        File.Delete(noteData + fileName);
                        Console.WriteLine("File has been deleted\n");
                    }

                    catch (Exception ex)

                    {
                        Console.WriteLine("File not deleted following error occured: " + ex.Message);
                    }
                }
                else if (confirmation == "n")
                {
                    Main(null);
                }
                else
                {
                    Console.WriteLine("Invalid command\n");
                    deleteNote();
                }
            }
            else
            {
                Console.WriteLine("File does not exist\n");
                deleteNote();
            }
    }




    commandsAvailable() {
        Console.WriteLine(" New - Create a new note\n Edit - Edit a note\n Read -  Read a note\n showNotes - List all notes\n exit - exit the application\n newDir - Opens note directory\n Help - Shows this help message\n");
    }




    exit() {
        Environment.exit(0);
    }



    showNotes() {
        string noteLocation = noteData;
        DirectoryInfo newDir = new DirectoryInfo(noteLocation);

            if (Directory.Exists(noteLocation))
            {

                FileInfo[] noteFiles = newDir.GetFiles("*.xml");

                if (noteFiles.Count() != 0)
                {

                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop + 2);
                    Console.WriteLine("+------------+");
                    foreach (var item in noteFiles)
                    {
                            Console.WriteLine("  " +item.Name);
                    }

                    Console.WriteLine(Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("No notes found.\n");
                }
            }
            else
            {
                Console.WriteLine(" Directory does not exist.....creating directory\n");
                Directory.CreateDirectory(noteLocation);
                Console.WriteLine(" Directory: " + noteLocation + " created successfully.\n");
            }
    }
    notesData() {
        Process.Start("explorer.exe", noteData);
    }
}