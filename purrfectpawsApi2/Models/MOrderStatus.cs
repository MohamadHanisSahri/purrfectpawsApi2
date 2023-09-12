using System;
using System.Collections.Generic;

namespace purrfectpawsApi2.Models;

public partial class MOrderStatus
{
    public int OrderStatusId { get; set; }

    public string Status { get; set; } = null!;
}
