using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace OnlineShop.Domain.Models;

public class Dish
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(50)]
    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
    public string Name { get; set; } = default!;
    
    [Required]
    [StringLength(500)]
    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
    public string Description { get; set; } = default!;

    public Photo? Photo { get; set; }

    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
    public double Price { get; set; }

    public List<Category> Categories { get; set; } = new();

    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
    public bool IsAvailable { get; set; } = true;

    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
    public string Currency { get; set; } = default!;

    public List<Review> Reviews { get; set; } = new();
}