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
		private List<Hall>halls = new List<Hall>();

		public HallController(ILogger<HallController> logger) 
		{
			_logger = logger;
		}

		[HttpGet]
		public List<Models.Hall> Get()
		{
			GetHalls();
			return halls;
		}

		[HttpGet]
		public Models.Hall GetId(int id)
		{
			GetHalls();
			if(halls.Count != 0)
			{
				foreach (Hall hall in halls)
				{
					if(hall.IdHall == id)
					{
						return hall;
					}
				}
			}
			return null;
		}

		private void GetHalls()
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"SELECT * FROM Hall "; 
				//"WHERE [Id_Hall] = @paramId ";
			
			using(SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				//cmd.Parameters.AddWithValue("@paramId", id);
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						Hall hall = new Hall();
						hall.IdHall = reader.GetInt32(0);
						hall.NameHall = reader.GetString(1);
						halls.Add(hall);
					}
					reader.Close();
				}
				catch (Exception ex)
				{
					
				}
			}
		}
	}
}
