using DanceStudio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace DanceStudio.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HallController : ControllerBase
	{
		private readonly ILogger<HallController> _logger;

		public HallController(ILogger<HallController> logger) 
		{
			_logger = logger;
		}
		// GET: api/<HallController>
		[HttpGet]
		public IEnumerable<Hall> Get()
		{
			List<Hall> halls = GetHalls();
			return halls;
		}

		// GET api/<HallController>/5
		[HttpGet("{id}")]
		public Hall Get(int id)
		{
			List<Hall> halls = GetHalls();
			if (halls.Count != 0)
			{
				foreach (Hall hall in halls)
				{
					if (hall.IdHall == id)
					{
						return hall;
					}
				}
			}
			return null;
		}

		// POST api/<TrainerController>
		[HttpPost]
		public void Post([FromBody] Hall hall)
		{
			List<Hall> halls = GetHalls();
			bool flag = true;
			if(hall != null)
			{
				foreach(var h in halls)
				{
					if(h.IdHall == hall.IdHall || h.NameHall == hall.NameHall)
					{ flag = false; break; }
				}
				if(flag)
				{
					halls.Add(hall);
					AddHall(hall);
				}
			}
		}

		// PUT api/<TrainerController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Hall hall)
		{
			List<Hall> halls = GetHalls();
			if (hall.IdHall == id) 
			{
				for(int i = 0; i < halls.Count; i++)
				{
					if (halls[i].IdHall == id)
					{
						halls[i] = hall;
					}
				}
				UpdateHall(hall, id);
			}
			else
			{
				bool flag = true;
				foreach(var h in halls)
				{
					if (hall.IdHall == h.IdHall)
					{
						flag= false; break;
					}	
				}
				if(flag)
				{
					halls[id] = hall;
					UpdateHall(hall, id);
				}
			}
		}

		// DELETE api/<TrainerController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			List<Hall> halls = GetHalls();
			for(int i = 0; i < halls.Count; i++)
			{
				if(id == halls[i].IdHall)
				{
					halls.Remove(halls[i]);
					DeleteHall(id);
					break;
				}
			}
		}

		private List<Hall> GetHalls()
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"SELECT * FROM Hall "; 
			List<Hall> halls = new List<Hall>();
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
			return halls;
		}
		private void AddHall(Hall hall)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"INSERT INTO Hall (Id_Hall, Name_Hall) \n VALUES (@paramId, @paramName)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", hall.IdHall);
				cmd.Parameters.AddWithValue("@paramName", hall.NameHall);
				try
				{
					connection.Open();
					cmd.ExecuteNonQuery();
					connection.Close();
				}
				catch (Exception ex)
				{

				}
			}
		}
		private void UpdateHall(Hall hall, int id)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
			"UPDATE Hall SET Id_Hall = @paramId , Name_Hall = @paramName WHERE Id_Hall = @ID";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", hall.IdHall);
				cmd.Parameters.AddWithValue("@paramName", hall.NameHall);
				cmd.Parameters.AddWithValue("@ID", id);
				try
				{
					connection.Open();
					int n = cmd.ExecuteNonQuery();
					connection.Close();
				}
				catch (Exception ex)
				{

				}
			}
		}
		private void DeleteHall(int id)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			string queryString = "DELETE FROM Hall WHERE Id_Hall = @paramId ";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", id);
				try
				{
					connection.Open();
					cmd.ExecuteNonQuery();
					connection.Close();
				}
				catch (Exception ex)
				{

				}
			}
		}
	}
}
