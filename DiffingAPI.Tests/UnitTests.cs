using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiffingAPI.Models;
using DiffingAPI.Controllers;

namespace DiffingAPI.Tests {
  [TestClass]
  public class UnitTests {

    public UnitTests() {
    }

    // Test IsValidDataPair with only left value
    [TestMethod]
    public void IsValidDataPair_NoLeftData_False() {
      DataPair dataPair = new DataPair() { Id = 1, LeftData = "AAAAAA==" };

      bool result = dataPair.IsValidDataPair();

      Assert.AreEqual(false, result);
    }

    // Test IsValidDataPair with invalid data
    [TestMethod]
    public void IsValidDataPair_InvalidData_False() {
      DataPair dataPair = new DataPair() { Id = 1, LeftData = "AAAAAA==", RightData = "T" };

      bool result = dataPair.IsValidDataPair();

      Assert.AreEqual(false, result);
    }

    // Test IsValidDataPair with valid data
    [TestMethod]
    public void IsValidDataPair_True() {
      DataPair dataPair = new DataPair() { Id = 1, LeftData = "AAAAAA==", RightData = "AAA=" };

      bool result = dataPair.IsValidDataPair();

      Assert.AreEqual(true, result);
    }

    // Test IsValidDataPair with valid data
    [TestMethod]
    public void GenerateDiffResult_SizeDoesNotMatch() {
      DataPair dataPair = new DataPair() { Id = 1, LeftData = "AAAAAA==", RightData = "AAA=" };

      DiffResult result = dataPair.GenerateDiffResult();

      Assert.AreEqual("SizeDoesNotMatch", result.DiffResultType);
      Assert.AreEqual(0, result.Diffs.Count);
    }

    // Test IsValidDataPair with valid data
    [TestMethod]
    public void GenerateDiffResult_ContentDoesNotMatch() {
      DataPair dataPair = new DataPair() { Id = 1, LeftData = "AAAAAA==", RightData = "AQABAQ==" };

      DiffResult result = dataPair.GenerateDiffResult();

      Assert.AreEqual("ContentDoesNotMatch", result.DiffResultType);
      Assert.AreEqual(2, result.Diffs.Count);
    }
  }
}