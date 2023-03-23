using DanceStudio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;


namespace DanceStudio.Controllers
{
	//[ApiController]
	public class HallController : Controller
	{
		
		private readonly ILogger<HallController> _logger;

		public HallController(ILogger<HallController> logger) 
		{
			_logger = logger;
		}

		[HttpGet]
		public Models.Hall Get(int id)
		{
			return GetHall(id);
		}

		private Models.Hall GetHall(int id)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"SELECT * FROM Hall " +
				"WHERE [Id_Hall] = @paramId ";
			Hall hall = new Hall();
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
						hall.IdHall = reader.GetInt32(0);
						hall.NameHall = reader.GetString(1);
					}
					reader.Close();
				}
				catch (Exception ex)
				{
					
				}
				return hall;
			}
		}
	}
}
