using Newtonsoft.Json;
using RestSharp;
using ShiftLogger.Models;

namespace ShiftService;

public class ShiftService
{
    public List<ShiftItem> GetShifts()
    {
        var client = new RestClient("https://localhost:7036/api/");
        var request = new RestRequest("ShiftItems");
        var response = client.ExecuteAsync(request);

        List<ShiftItem> shiftItems = new();

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;

            shiftItems = JsonConvert.DeserializeObject<List<ShiftItem>>(rawResponse);

            return shiftItems;
        }
        return shiftItems;
    }

    public ShiftItem GetShiftAtIndex(int index)
    {
        var client = new RestClient("https://localhost:7036/api/");
        var request = new RestRequest($"ShiftItems/{index}");
        var response = client.ExecuteAsync(request);

        ShiftItem shiftItem = new();

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;

            shiftItem = JsonConvert.DeserializeObject<ShiftItem>(rawResponse);

            return shiftItem;
        }
        else return null;
    }

    public bool PostNewShift(ShiftItem shift)
    {
        var client = new RestClient("https://localhost:7036/api/");
        var request = new RestRequest("ShiftItems", Method.Post);
        request.AddBody(shift);
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
        {
            return true;
        }
        else return false;
    }
    
    public bool UpdateShift(ShiftItem shiftToUpdate)
    {
        var client = new RestClient("https://localhost:7036/api/");
        var request = new RestRequest($"ShiftItems/{shiftToUpdate.Id}", Method.Put);
        request.AddBody(shiftToUpdate);
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
        {
            return true;
        }
        else return false;
    }


    public bool CheckIfShiftExistsAtIndex(int index)
    {
        var client = new RestClient("https://localhost:7036/api/");
        var request = new RestRequest($"ShiftItems/{index}");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }
        else return false;
    }

    public bool DeleteShift(int index)
    {
        var client = new RestClient("https://localhost:7036/api/");
        var request = new RestRequest($"ShiftItems/{index}", Method.Delete);
        var response = client.ExecuteAsync(request);

        if (!CheckIfShiftExistsAtIndex(index))
        {
            return true;
        }
        else return false;
    }
}
