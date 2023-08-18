using Mapster;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankCrud.Service.ServiceModels
{
    public class BaseServiceModel
    {
        public long Id { get; set; }
        //[AdaptIgnore]
        public DateTime AddedDate { get; set; }
        //[AdaptIgnore]
        public DateTime? ModifiedDate { get; set; }
       // [AdaptIgnore]
        public DateTime? DeletedDate { get; set; }
        //[AdaptIgnore]
        public bool IsDeleted { get; set; }
       // [AdaptIgnore]
        public int CreatorId { get; set; } = 1;
       // [AdaptIgnore]
        public int? ModifierId { get; set; } = 1;
      //  [AdaptIgnore]
        public int? DeletorId { get; set; } = 1;

    }
}
