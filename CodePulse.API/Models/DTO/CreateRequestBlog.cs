﻿namespace CodePulse.API.Models.DTO
{
    public class CreateRequestBlog
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? UrlHandle { get; set; }
        public string? FeatureImageUrl { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? Author { get; set; }
        public bool IsVisible { get; set; }
        public Guid[] Categories { get; set; }
    }
}
