﻿namespace DanceStudio.Models
{
    public class Client
    {
        public int IdClient { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set;}
        public int IdTrainer { get; set; }
    }
}
