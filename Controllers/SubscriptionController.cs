using DanceStudio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DanceStudio.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubscriptionController : ControllerBase
	{
		private readonly ILogger<SubscriptionController> _logger;

		public SubscriptionController(ILogger<SubscriptionController> logger)
		{
			_logger = logger;
		}
		// GET: api/<SubscriptionController>
		[HttpGet]
		public IEnumerable<Subscription> Get()
		{
			List<Subscription>subscriptions = GetSubscriptions();
			return subscriptions;
		}

		// GET api/<SubscriptionController>/5
		[HttpGet("{id}")]
		public Subscription Get(int id)
		{
			List<Subscription> subscriptions = GetSubscriptions();
			foreach (var subscription in subscriptions)
			{
				if(subscription.IdSubscription == id) return subscription;
			}
			return null;
		}

		// POST api/<SubscriptionController>
		[HttpPost]
		public void Post([FromBody] Subscription subscription)
		{
			List<Subscription> subscriptions = GetSubscriptions();
			bool flag = true;
			if (subscription != null)
			{
				foreach (var s in subscriptions)
				{
					if (s.IdSubscription == subscription.IdSubscription)
					{ flag = false; break; }
				}
				if (flag)
				{
					subscriptions.Add(subscription);
					AddSubscription(subscription);
				}
			}
		}

		// PUT api/<SubscriptionController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Subscription subscription)
		{
			List<Subscription> subscriptions = GetSubscriptions();
			if (subscription.IdSubscription == id)
			{
				for (int i = 0; i < subscriptions.Count; i++)
				{
					if (subscriptions[i].IdSubscription == id)
					{
						subscriptions[i] = subscription;
					}
				}
				UpdateSubscription(subscription, id);
			}
			else
			{
				bool flag = true;
				foreach (var s in subscriptions)
				{
					if (subscription.IdSubscription == s.IdSubscription)
					{
						flag = false; break;
					}
				}
				if (flag)
				{
					subscriptions[id] = subscription;
					UpdateSubscription(subscription, id);
				}
			}
		}

		// DELETE api/<SubscriptionController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			List<Subscription> subscriptions = GetSubscriptions();
			for (int i = 0; i < subscriptions.Count; i++)
			{
				if (id == subscriptions[i].IdSubscription)
				{
					subscriptions.Remove(subscriptions[i]);
					DeleteSubscription(id);
					break;
				}
			}
		}

		private List<Subscription> GetSubscriptions()
		{
			List<Subscription> subscriptions = new List<Subscription>();
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"SELECT * FROM Subscription ";

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
						Subscription subscription = new Subscription();
						subscription.IdSubscription = reader.GetInt32(0);
						subscription.Price = reader.GetInt32(1);
						subscription.DanceStyle = reader.GetString(2);
						subscriptions.Add(subscription);
					}
					reader.Close();
				}
				catch (Exception ex)
				{

				}
			}
			return subscriptions;
		}
		private void AddSubscription(Subscription subscription)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
				"INSERT INTO Subscription (Id_Subscription, Price, Dance_Style) \n VALUES (@paramId, @paramPrice, @paramDance)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", subscription.IdSubscription);
				cmd.Parameters.AddWithValue("@paramPrice", subscription.Price);
				cmd.Parameters.AddWithValue("@paramDance", subscription.DanceStyle);
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
		private void UpdateSubscription(Subscription subscription, int id)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			String queryString =
			"UPDATE Subscription SET Id_Subscription = @paramId , Price = @paramPrice, Dance_Style = @paramDance WHERE Id_Subscription = @ID";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramId", subscription.IdSubscription);
				cmd.Parameters.AddWithValue("@paramPrice", subscription.Price);
				cmd.Parameters.AddWithValue("@paramDance", subscription.DanceStyle);
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
		private void DeleteSubscription(int id)
		{
			String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Lenovo\\source\\repos\\DanceStudio\\Data Base\\DanseStudio.mdf\";Integrated Security=True";
			string queryString = "DELETE FROM Subscription WHERE Id_Subscription = @paramId ";
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
