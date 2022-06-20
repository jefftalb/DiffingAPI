using System.ComponentModel.DataAnnotations;

namespace DiffingAPI.Models {
  public class DataPair {
    public int Id { get; set; }
    public string? LeftData { get; set; }
    public string? RightData { get; set; }


    /// <summary>
    /// Checks if both data fields exist in data pair
    /// </summary>
    /// <param name="dataPair">Data pair to be validated</param>
    public bool IsValidDataPair() {
      // Check if data is Base64 encoded string and decode.
      try {
        byte[] decodedLeftData = Convert.FromBase64String(LeftData);
        byte[] decodedRightData = Convert.FromBase64String(RightData);
      }
      catch (Exception) {
        return false;
      }

      return LeftData != null && RightData != null;
    }

    /// <summary>
    /// Genarate a diff result for a data pair
    /// </summary>
    /// <param name="dataPair">Data pair to be diffed</param>
    /// <returns>Diff result for data pair</returns>
    public DiffResult GenerateDiffResult() {
      var result = new DiffResult() {
        DiffResultType = "Equals"
      };
      byte[] decodedLeftData = Convert.FromBase64String(LeftData);
      byte[] decodedRightData = Convert.FromBase64String(RightData);

      if (decodedLeftData.Length != decodedRightData.Length) {
        result.DiffResultType = "SizeDoesNotMatch";

        return result;
      } else {
        int offset = -1;
        int length = 0;
        for (int i = 0; i < decodedLeftData.Length; i++) {
          if (decodedLeftData[i] != decodedRightData[i]) {
            if (offset == -1) {
              offset = i;
            }
            length++;
          } else if (offset != -1) {
            result.Diffs.Add(new Diff() { Offset = offset, Length = length });
            offset = -1;
            length = 0;
          }
        }

        // If the diff did not end before the end of the data add the diff.
        if (offset != -1) {
          result.Diffs.Add(new Diff() { Offset = offset, Length = length });
        }

        if (result.Diffs.Count > 0) {
          result.DiffResultType = "ContentDoesNotMatch";
        }

        return result;
      }
    }
  }
}
