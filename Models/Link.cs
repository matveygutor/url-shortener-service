﻿namespace test.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string? LongURL { get; set; }
        public string? ShortURL { get; set; }
        public DateTime Date { get; set; }
        public int Click { get; set; }

    }
}