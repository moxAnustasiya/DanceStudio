using DanceStudio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DanceStudio.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientController : ControllerBase
	{
		private readonly ILogger<ClientController> _logger;

		public ClientController(ILogger<ClientController> logger)
		{
			_logger = logger;
		}
		// GET: api/<ClientController>
		[HttpGet]
		public IEnumerable<Client> Get()
		{
			List<Client> clients = GetClients();
			return clients;
		}

		// GET api/<ClientController>/5
		[HttpGet("{id}")]
		public Client Get(int id)
		{
			List<Client> clients = GetClients();
			foreach (var client in clients)
			{
				if (client.IdClient == id)
					return client;
			}
			return null;
		}

		// POST api/<ClientController>
		[HttpPost]
		public void Post([FromBody] Client client)
		{
			List<Client> clients = GetClients();
			bool flag = true;
			if (client != null)
			{
				foreach (var c in clients)
				{
					if (c.IdClient == client.IdClient)
					{ flag = false; break; }
				}
				if (flag)
				{
					clients.Add(client);
					AddClient(client);
				}
			}
		}

		// PUT api/<ClientController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Client client)
		{
			List<Client> clients = GetClients();
			if (client.IdClient == id)
			{
				for (int i = 0; i < clients.Count; i++)
				{
					if (clients[i].IdClient == id)
					{
						clients[i] = client;
					}
				}
				UpdateClient(client, id);
			}
			else
			{
				bool flag = true;
				foreach (var c in clients)
				{
					if (client.IdClient == c.IdClient)
					{
						flag = false; break;
					}
				}
				if (flag)
				{
					clients[id] = client;
					UpdateClient(client, id);
				}
			}
		}

		// DELETE api/<ClientController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			List<Client> clients = GetClients();
			for (int i = 0; i < clients.Count; i++)
			{
				if (id == clients[i].IdClient)
				{
					clients.Remove(clients[i]);
					DeleteHall(id);
					break;
				}
			}
		}

		private List<Client> GetClients()
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"SELECT * FROM Client ";
			List<Client> clients = new List<Client>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				//cmd.Parameters.AddWithValue("@paramId", id);
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						Client client= new Client();
						client.IdClient = reader.GetInt32(0);
						client.Name = reader.GetString(1);
						client.LastName = reader.GetString(2);
						client.Phone = reader.GetString(3);
						client.IdTrainer = reader.GetInt32(4);
						clients.Add(client);
					}
					reader.Close();
				}
				catch (Exception ex)
				{

				}
			}
			return clients;
		}
		private void AddClient(Client client)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"INSERT INTO Client (Id_Client, FirstName, LastName, Phone, Id_Trainer) \n VALUES (@paramId, @paramName, @paramLastName, @paramPhone, @paramIdTrainer)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", client.IdClient);
				cmd.Parameters.AddWithValue("@paramName", client.Name);
				cmd.Parameters.AddWithValue("@paramLastName", client.LastName);
				cmd.Parameters.AddWithValue("@paramPhone", client.Phone);
				cmd.Parameters.AddWithValue("@paramIdTrainer", client.IdTrainer);
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
		private void UpdateClient(Client client, int id)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
			"UPDATE Client SET Id_Client = @paramId , FirstName = @paramName , LastName = @paramLastName , Phone = @paramPhone , Id_Trainer = @paramIdTrainer WHERE Id_Client = @ID";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", client.IdClient);
				cmd.Parameters.AddWithValue("@paramName", client.Name);
				cmd.Parameters.AddWithValue("@paramLastName", client.LastName);
				cmd.Parameters.AddWithValue("@paramPhone", client.Phone);
				cmd.Parameters.AddWithValue("@paramIdTrainer", client.IdTrainer);
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
			string queryString = "DELETE FROM Client WHERE Id_Client = @paramId ";
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
