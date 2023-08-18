using System.ComponentModel.DataAnnotations;

namespace BankCrud.Domain.Entities;

public class BaseEntity
{

    public long Id { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }

    [Required]
    public int CreatorId { get; set; }
    public int? ModifierId { get; set; }
    public int? DeletorId { get; set; }
}