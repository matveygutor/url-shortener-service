using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    public class Link
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        [RegularExpression(@"^https?:\/\/[A-Za-z0-9:.]*([\/]{1}.*\/?)$", ErrorMessage = "Incorrect URL")]
        public string? LongURL { get; set; }
        
        public string? Token { get; set; }
        
        public string? ShortURL { get; set; }
        
        public DateTime Date { get; set; }
        
        public int Click { get; set; }

    }
}
