using DiffingAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DiffingAPI.Tests {
  [TestClass]
  public class IntegrationTests {
    private HttpClient _httpClient;

    public IntegrationTests() {
      var webAppFactory = new WebApplicationFactory<Program>();
      _httpClient = webAppFactory.CreateDefaultClient();
    }

    // Test getting a diff that does not exist
    [TestMethod]
    public async Task GetDiff1_ReturnNotFound() {
      var response = await _httpClient.GetAsync("/v1/diff/1");

      Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    // Test getting a diff that only has a left value
    [TestMethod]
    public async Task PutDiffLeft2_GetDiff2_ReturnNotFound() {
      var dataModel = new DataModel() { data = "AAAAAA==" };
      var jsonString = JsonSerializer.Serialize(dataModel);
      var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
      var response = await _httpClient.PutAsync("/v1/diff/2/left", content);

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

      response = await _httpClient.GetAsync("/v1/diff/2");

      Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    // Test getting a diff that is equal
    [TestMethod]
    public async Task PutDiffLeft3_PutDiffLeft3_GetDiff3_ReturnEqual() {
      var dataModel = new DataModel() { data = "AAAAAA==" };
      var jsonString = JsonSerializer.Serialize(dataModel);
      var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
      var response = await _httpClient.PutAsync("/v1/diff/3/left", content);

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
      
      dataModel = new DataModel() { data = "AAAAAA==" };
      jsonString = JsonSerializer.Serialize(dataModel);
      content = new StringContent(jsonString, Encoding.UTF8, "application/json");
      response = await _httpClient.PutAsync("/v1/diff/3/right", content);

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

      response = await _httpClient.GetAsync("/v1/diff/3");
      var diffJson = await response.Content.ReadAsStringAsync();

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
      Assert.AreEqual("{\"Diffs\":[],\"DiffResultType\":\"Equals\"}", diffJson);
    }

    // Test getting a diff with unequal lengths
    [TestMethod]
    public async Task PutDiffLeft4_PutDiffLeft4_GetDiff4_ReturnSizeDoesNotMatch() {
      var dataModel = new DataModel() { data = "AAAAAA==" };
      var jsonString = JsonSerializer.Serialize(dataModel);
      var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
      var response = await _httpClient.PutAsync("/v1/diff/4/left", content);

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

      dataModel = new DataModel() { data = "AAA=" };
      jsonString = JsonSerializer.Serialize(dataModel);
      content = new StringContent(jsonString, Encoding.UTF8, "application/json");
      response = await _httpClient.PutAsync("/v1/diff/4/right", content);

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

      response = await _httpClient.GetAsync("/v1/diff/4");
      var diffJson = await response.Content.ReadAsStringAsync();

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
      Assert.AreEqual("{\"Diffs\":[],\"DiffResultType\":\"SizeDoesNotMatch\"}", diffJson);
    }

    // Test getting a diff with same length but different values
    [TestMethod]
    public async Task PutDiffLeft5_PutDiffLeft5_GetDiff5_ReturnContentDoesNotMatch() {
      var dataModel = new DataModel() { data = "AAAAAA==" };
      var jsonString = JsonSerializer.Serialize(dataModel);
      var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
      var response = await _httpClient.PutAsync("/v1/diff/4/left", content);

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

      dataModel = new DataModel() { data = "AQABAQ==" };
      jsonString = JsonSerializer.Serialize(dataModel);
      content = new StringContent(jsonString, Encoding.UTF8, "application/json");
      response = await _httpClient.PutAsync("/v1/diff/4/right", content);

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

      response = await _httpClient.GetAsync("/v1/diff/4");
      var diffJson = await response.Content.ReadAsStringAsync();

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
      Assert.AreEqual("{\"Diffs\":[{\"Offset\":0,\"Length\":1},{\"Offset\":2,\"Length\":2}],\"DiffResultType\":\"ContentDoesNotMatch\"}", diffJson);
    }
  }
}