using Newtonsoft.Json;
public class FileController
{
    /// Uses the NewtonSoft JSON package to perform Serialization / Deserialization.
    /// https://www.newtonsoft.com/json


    /// <summary>
    /// 
    /// </summary>
    /// <param name="game"></param>
    public void GridSerialization(Game game)
    {
        // Check if directory exists
        string saveDirectory = "Saves";
        if (!Path.Exists(saveDirectory))
        {
            try
            {
                Directory.CreateDirectory(saveDirectory);
            }
            catch (Exception e)
            {
                IOController.PrintError($"Error: unable to create Saves directory, {e.Message}");
                return;
            }
        }

        // Create filename
        string timestamp = DateTime.Now.ToString("yyyMMdd_HHmmss");
        string fileName = $"Game_{timestamp}.json";
        string filePath = Path.Combine(saveDirectory, fileName);

        // Content to write to file
        string json = JsonConvert.SerializeObject(game, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

        try
        {
            // Write to file
            File.WriteAllText(filePath, json);
            IOController.PrintSuccess($"Game saved to {filePath}");
        }
        catch (Exception e)
        {
            IOController.PrintError($"Error: Unable to write to save directory, {e.Message}");
        }
    }

    public Game GridDeserialization(string filePath) // Currently doesn't perform any validation of filePath
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<Game>(json, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
    }
}