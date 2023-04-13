using DanceStudio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DanceStudio.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Payment_AccountingController : ControllerBase
	{
		private readonly ILogger<Payment_AccountingController> _logger;
		private readonly IConfiguration Configuration;
		private String connectionString;

		public Payment_AccountingController(ILogger<Payment_AccountingController> logger, IConfiguration configuration)
		{
			_logger = logger;
			this.Configuration = configuration;
			connectionString = Configuration.GetConnectionString("defaultConnection");
		}

		// GET: api/<Payment_AccountingController>
		[HttpGet]
		public IEnumerable<PaymentAccount> Get()
		{
			List<PaymentAccount> payment_accounting = GetPayment_Accounting();
			return payment_accounting;
		}

		// GET api/<Payment_AccountingController>/5
		[HttpGet("{id}")]
		public PaymentAccount Get(int id)
		{
			List<PaymentAccount> payment_accounting = GetPayment_Accounting();
			if (payment_accounting.Count != 0)
			{
				foreach (PaymentAccount payment in payment_accounting)
				{
					if (payment.IdClient == id)
					{
						return payment;
					}
				}
			}
			return null;
		}

		// POST api/<Payment_AccountingController>
		[HttpPost]
		public void Post([FromBody] PaymentAccount p)
		{
			List<PaymentAccount> payment_accounting = GetPayment_Accounting();
			bool flag = true;
			if (p != null)
			{
				foreach (var payment in payment_accounting)
				{
					if (payment.IdClient == p.IdClient)
					{ flag = false; break; }
				}
				if (flag)
				{
					payment_accounting.Add(p);
					AddPA(p);
				}
			}
		}

		// PUT api/<Payment_AccountingController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] PaymentAccount payment)
		{
			List<PaymentAccount> payment_accounting = GetPayment_Accounting();
			for(int i = 0; i < payment_accounting.Count; i++)
			{
				if (payment_accounting[i].IdClient == id)
				{
					if(payment.Month == payment_accounting[i].Month && payment.IdClient == payment_accounting[i].IdClient && payment.IdSubscription == payment_accounting[i].IdSubscription)
					{
						payment_accounting[i] = payment;
						UpdatePA(payment, id);
					}
				}
			}
		}

		// DELETE api/<Payment_AccountingController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			List<PaymentAccount> payment_accounting = GetPayment_Accounting();
			for (int i = 0; i < payment_accounting.Count; i++)
			{
				if (id == payment_accounting[i].IdClient)
				{
					payment_accounting.Remove(payment_accounting[i]);
					DeletePA(id);
					break;
				}
			}
		}
		private List<PaymentAccount> GetPayment_Accounting()
		{
			String queryString =
				"SELECT * FROM Payment_Accounting ";
			List<PaymentAccount> payment_accounting = new List<PaymentAccount>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						PaymentAccount account = new PaymentAccount();
						account.IdSubscription = reader.GetInt32(0);
						account.IdClient = reader.GetInt32(1);
						account.Month = reader.GetString(2);
						account.isPayment = reader.GetBoolean(3);
						payment_accounting.Add(account);
					}
					reader.Close();
				}
				catch (Exception ex)
				{

				}
			}
			return payment_accounting;
		}
		private void AddPA(PaymentAccount p)
		{
			String queryString =
				"INSERT INTO Payment_Accounting (Id_Subscription, Id_Client, Month_now, Payment) \n VALUES (@paramIdS, @paramIdC, @paramMonth, @paramP)";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramIdS", p.IdSubscription);
				cmd.Parameters.AddWithValue("@paramIdC", p.IdClient);
				cmd.Parameters.AddWithValue("@paramMonth", p.Month);
				cmd.Parameters.AddWithValue("@paramP", p.isPayment);
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
		private void UpdatePA(PaymentAccount p, int id)
		{
			String queryString =
			"UPDATE Payment_Accounting SET Payment = @paramP WHERE ( Id_Client = @IdCl AND Id_Subscription = @IdS AND Month_now = @Month )";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(queryString, connection);
				cmd.Parameters.AddWithValue("@paramP", p.isPayment);
				cmd.Parameters.AddWithValue("@IdCl", p.IdClient);
				cmd.Parameters.AddWithValue("@IdS", p.IdSubscription);
				cmd.Parameters.AddWithValue("@Month", p.Month);
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
		private void DeletePA(int id)
		{
			string queryString = "DELETE FROM Payment_Accounting WHERE Id_Client = @paramId ";
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
