using DanceStudio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;


namespace DanceStudio.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HallController : ControllerBase
	{
		
		private readonly ILogger<HallController> _logger;

		public HallController(ILogger<HallController> logger) 
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<Models.Hall> Get()
		{
			return null;
		}

		private IEnumerable<string> GetHalls(string id)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"SELECT * FROM Hall" +
				"WHERE [Id_Hall] = @paramId ";
			List<String> result = new List<String>();
			using(SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", id);
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						result.Add(reader.GetString(0));
					}
					reader.Close();
				}
				catch (Exception ex)
				{
					
				}
				return result;
			}
		}
	}
}
