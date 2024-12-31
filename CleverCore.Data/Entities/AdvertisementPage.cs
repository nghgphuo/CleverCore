using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CleverCore.Infrastructure.SharedKernel;

namespace CleverCore.Data.Entities
{
    [Table("AdvertisementPages")]
    public class AdvertisementPage : DomainEntity<string>
    {
        public string Name { get; set; }

        public virtual ICollection<AdvertisementPosition> AdvertisementPositions { get; set; }
    }
}
