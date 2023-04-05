using DanceStudio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DanceStudio.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TrainerController : ControllerBase
	{
		private readonly ILogger<TrainerController> _logger;

		public TrainerController(ILogger<TrainerController> logger)
		{
			_logger = logger;
		}

		// GET: api/<TrainerController>
		[HttpGet]
		public IEnumerable<Trainer> Get()
		{
			List<Trainer> trainers = GetTrainers();
			return trainers;
		}

		// GET api/<TrainerController>/5
		[HttpGet("{id}")]
		public Trainer Get(int id)
		{
			List<Trainer> trainers = GetTrainers();
			foreach (var trainer in trainers) 
			{
				if(trainer.IdTrainer == id) 
					return trainer;
			}
			return null;
		}

		// POST api/<TrainerController>
		[HttpPost]
		public void Post([FromBody] Trainer trainer)
		{
			List<Trainer> trainers = GetTrainers();
			bool flag = true;
			if (trainer != null)
			{
				foreach (var t in trainers)
				{
					if (t.IdTrainer == trainer.IdTrainer)
					{ flag = false; break; }
				}
				if (flag)
				{
					trainers.Add(trainer);
					AddTrainer(trainer);
				}
			}
		}

		// PUT api/<TrainerController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Trainer trainer)
		{
			List<Trainer> trainers = GetTrainers();
			if (trainer.IdTrainer == id)
			{
				for (int i = 0; i < trainers.Count; i++)
				{
					if (trainers[i].IdTrainer == id)
					{
						trainers[i] = trainer;
					}
				}
				UpdateTrainer(trainer, id);
			}
			else
			{
				bool flag = true;
				foreach (var t in trainers)
				{
					if (trainer.IdTrainer == t.IdTrainer)
					{
						flag = false; break;
					}
				}
				if (flag)
				{
					trainers[id] = trainer;
					UpdateTrainer(trainer, id);
				}
			}
		}

		// DELETE api/<TrainerController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			List<Trainer> trainers = GetTrainers();
			for (int i = 0; i < trainers.Count; i++)
			{
				if (id == trainers[i].IdTrainer)
				{
					trainers.Remove(trainers[i]);
					DeleteTrainer(id);
					break;
				}
			}
		}
		
		private List<Trainer> GetTrainers()
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"SELECT * FROM Trainer ";
			List<Trainer> trainers = new List<Trainer>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						Trainer trainer = new Trainer();
						trainer.IdTrainer = reader.GetInt32(0);
						trainer.Name = reader.GetString(1);
						trainer.DanceStyle = reader.GetString(2);
						trainer.Phone = reader.GetString(3);
						trainer.IdHall = reader.GetInt32(4);
						trainers.Add(trainer);
					}
					reader.Close();
				}
				catch (Exception ex)
				{

				}
			}
			return trainers;
		}
		private void AddTrainer(Trainer trainer)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"INSERT INTO Trainer (Id_Trainer, FirstName, Dance_Style, Phone, Id_Hall) \n VALUES (@paramId, @paramName, @paramDance, @paramPhone, @paramIdHall)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", trainer.IdTrainer);
				cmd.Parameters.AddWithValue("@paramName", trainer.Name);
				cmd.Parameters.AddWithValue("@paramPhone", trainer.Phone);
				cmd.Parameters.AddWithValue("@paramDance", trainer.DanceStyle);
				cmd.Parameters.AddWithValue("@paramIdHall", trainer.IdHall);
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
		private void UpdateTrainer(Trainer trainer, int id)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
			"UPDATE Hall SET Id_Trainer = @paramId , FirstName = @paramName, Dance_Style = @paramDance, Phone = @paramPhone, Id_Hall = @paramIdHall  WHERE Id_Trainer = @ID";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", trainer.IdTrainer);
				cmd.Parameters.AddWithValue("@paramName", trainer.Name);
				cmd.Parameters.AddWithValue("@paramPhone", trainer.Phone);
				cmd.Parameters.AddWithValue("@paramDance", trainer.DanceStyle);
				cmd.Parameters.AddWithValue("@paramIdHall", trainer.IdHall);
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
		private void DeleteTrainer(int id)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			string queryString = "DELETE FROM Trainer WHERE Id_Trainer = @paramId ";
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
