namespace DiffingAPI.Models {
  public class DiffResult {
    public DiffResult() {
      DiffResultType = string.Empty;
      Diffs = new List<Diff>();
    }
    public string DiffResultType { get; set; }
    public List<Diff> Diffs;
  }
  public class Diff {
    public int Offset { get; set; }
    public int Length { get; set; }
  }
}
